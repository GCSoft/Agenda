﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	repEvento
' Autor:	Ruben.Cobos
' Fecha:	06-Marzo-2015
'
' https://www.youtube.com/watch?v=859BmmVOm8M
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using GCUtility.Function;
using GCUtility.Security;
using Agenda.BusinessProcess.Object;
using Agenda.BusinessProcess.Page;
using Agenda.Entity.Object;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace Agenda.Web.Application.WebApp.Private.Reportes
{
    public partial class repEvento : BPPage
    {
       
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

        String GetUTCBeginDate(){
            DateTime tempDate = DateTime.Now;
            String UTCCurrentDate = "";

            try
            {

                // Inicio de mes
                tempDate = tempDate.AddDays( (DateTime.Now.Day - 1) * -1);

                // Año
                UTCCurrentDate = tempDate.Year.ToString() + "-";

                // Mes
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Month > 9 ? tempDate.Month.ToString() : "0" + tempDate.Month.ToString() ) + "-";

                // Día
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Day > 9 ? tempDate.Day.ToString() : "0" + tempDate.Day.ToString() );

            }catch(Exception ex){
                throw(ex);
            }

            return UTCCurrentDate;
        }

        String GetUTCEndDate(){
            DateTime tempDate = DateTime.Now;
            String UTCCurrentDate = "";

            try
            {

                // Inicio de mes
                tempDate = tempDate.AddDays( (DateTime.Now.Day - 1) * -1);
                tempDate = tempDate.AddMonths(1);

                // Año
                UTCCurrentDate = tempDate.Year.ToString() + "-";

                // Mes
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Month > 9 ? tempDate.Month.ToString() : "0" + tempDate.Month.ToString() ) + "-";

                // Día
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Day > 9 ? tempDate.Day.ToString() : "0" + tempDate.Day.ToString() );

            }catch(Exception ex){
                throw(ex);
            }

            return UTCCurrentDate;
        }



        // Rutinas del programador

        void CrearReporte(DataTable tblEventoResumen, DataTable tblEventoDetalle){
            ReportDataSource repDataSource;
            ReportParameter[] repParameters;
            
            try
            {

                // Reset
                this.rptViewerEvento.Reset();

                // DataSources
                repDataSource = new ReportDataSource("dsEventoResumen", tblEventoResumen);
                this.rptViewerEvento.LocalReport.DataSources.Add(repDataSource);

                repDataSource = new ReportDataSource("dsEventoDetalle", tblEventoDetalle);
                this.rptViewerEvento.LocalReport.DataSources.Add(repDataSource);

                // Path
                this.rptViewerEvento.LocalReport.ReportPath = Server.MapPath("~/Application/WebApp/Private/Reportes/RDLC/rptEvento.rdlc");

                // Parameters
                repParameters = new ReportParameter[] {
                    new ReportParameter("FechaInicial", this.wucBeginDate.DisplayDate ),
                    new ReportParameter("FechaFinal", this.wucEndDate.DisplayDate )
                };

                this.rptViewerEvento.LocalReport.SetParameters(repParameters);

                // Refresh
                this.rptViewerEvento.LocalReport.Refresh();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectDependencia(){
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Opciones por Rol
                switch( oENTSession.RolId ){
                    case 4: // Logística

                        this.ddlDependencia.Items.Insert(0, new ListItem("Logística", "1"));
                        break;

                    case 5: // Dirección de Protocolo

                        this.ddlDependencia.Items.Insert(0, new ListItem("Dirección de Protocolo", "2"));
                        break;

                    default:

                        this.ddlDependencia.Items.Insert(0, new ListItem("Dirección de Protocolo", "2"));
                        this.ddlDependencia.Items.Insert(0, new ListItem("Logística", "1"));
                        this.ddlDependencia.Items.Insert(0, new ListItem("[Todas]", "0"));
                        break;
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstatusEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEstatusEvento oENTEstatusEvento = new ENTEstatusEvento();
            ENTSession oENTSession = new ENTSession();

            BPEstatusEvento oBPEstatusEvento = new BPEstatusEvento();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEstatusEvento.RolId = oENTSession.RolId;

                // Formulario
                oENTEstatusEvento.EstatusEventoId = 0;
                oENTEstatusEvento.Nombre = "";

                // Transacción
                oENTResponse = oBPEstatusEvento.SelectEstatusEvento(oENTEstatusEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlEstatusEvento.DataTextField = "Nombre";
                this.ddlEstatusEvento.DataValueField = "EstatusEventoId";
                this.ddlEstatusEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlEstatusEvento.DataBind();

                // Agregar Item de selección
                this.ddlEstatusEvento.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEvento(Boolean CheckDate){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            try
            {

                // Validaciones
                if (CheckDate){
                    if (!this.wucBeginDate.IsValidDate()) { throw new Exception("El campo [Fecha Inicial] es requerido"); }
                    if (!this.wucEndDate.IsValidDate()) { throw new Exception("El campo [Fecha Inicial] es requerido"); }
                }
               
                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTEvento.EventoId = 0;
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.EstatusEventoId = Int32.Parse(this.ddlEstatusEvento.SelectedItem.Value);
                oENTEvento.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                oENTEvento.FechaInicio = (CheckDate ? this.wucBeginDate.DisplayUTCDate : GetUTCBeginDate());
                oENTEvento.FechaFin = (CheckDate ? this.wucEndDate.DisplayUTCDate : GetUTCEndDate());
                oENTEvento.PalabraClave = "";
                oENTEvento.Nivel = 1;
                oENTEvento.Dependencia = Int16.Parse(this.ddlDependencia.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPEvento.SelectEvento(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Crear reporte
                CrearReporte(oENTResponse.DataSetResponse.Tables[1], oENTResponse.DataSetResponse.Tables[0]);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectPrioridad(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTPrioridad oENTPrioridad = new ENTPrioridad();

            BPPrioridad oBPPrioridad = new BPPrioridad();

            try
            {

                // Formulario
                oENTPrioridad.PrioridadId = 0;
                oENTPrioridad.Nombre = "";
                oENTPrioridad.Activo = 1;

                // Transacción
                oENTResponse = oBPPrioridad.SelectPrioridad(oENTPrioridad);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPrioridad.DataTextField = "Nombre";
                this.ddlPrioridad.DataValueField = "PrioridadId";
                this.ddlPrioridad.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPrioridad.DataBind();

                // Agregar Item de selección
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Todas]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }


        
        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            DateTime tempDate = DateTime.Now;

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                SelectEstatusEvento();
                SelectPrioridad();
                SelectDependencia();

                // Por default preseleccionado todo el mes
                tempDate = tempDate.AddDays((DateTime.Now.Day - 1) * -1);
                this.wucBeginDate.SetDate(tempDate);

                tempDate = tempDate.AddMonths(1);
                tempDate = tempDate.AddDays(-1);
                this.wucEndDate.SetDate(tempDate);

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                SelectEvento(true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

    }
}