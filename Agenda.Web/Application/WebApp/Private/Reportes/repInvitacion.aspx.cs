/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	repInvitacion
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
    public partial class repInvitacion : BPPage
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

        void CrearReporte(DataTable tblInvitacionResumen, DataTable tblInvitacionDetalle){
            ReportDataSource repDataSource;
            ReportParameter[] repParameters;
            
            try
            {

                // Reset
                this.rptViewerInvitacion.Reset();

                // DataSources
                repDataSource = new ReportDataSource("dsInvitacionResumen", tblInvitacionResumen);
                this.rptViewerInvitacion.LocalReport.DataSources.Add(repDataSource);

                repDataSource = new ReportDataSource("dsInvitacionDetalle", tblInvitacionDetalle);
                this.rptViewerInvitacion.LocalReport.DataSources.Add(repDataSource);

                // Path
                this.rptViewerInvitacion.LocalReport.ReportPath = Server.MapPath("~/Application/WebApp/Private/Reportes/RDLC/rptInvitacion.rdlc");

                // Parameters
                repParameters = new ReportParameter[] {
                    new ReportParameter("FechaInicial", this.wucBeginDate.DisplayDate ),
                    new ReportParameter("FechaFinal", this.wucEndDate.DisplayDate )
                };

                this.rptViewerInvitacion.LocalReport.SetParameters(repParameters);

                // Refresh
                this.rptViewerInvitacion.LocalReport.Refresh();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstatusInvitacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEstatusInvitacion oENTEstatusInvitacion = new ENTEstatusInvitacion();

            BPEstatusInvitacion oBPEstatusInvitacion = new BPEstatusInvitacion();

            try
            {

                // Formulario
                oENTEstatusInvitacion.EstatusInvitacionId = 0;
                oENTEstatusInvitacion.Nombre = "";
                oENTEstatusInvitacion.Operativo = 1;

                // Transacción
                oENTResponse = oBPEstatusInvitacion.SelectEstatusInvitacion(oENTEstatusInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlEstatusInvitacion.DataTextField = "Nombre";
                this.ddlEstatusInvitacion.DataValueField = "EstatusInvitacionId";
                this.ddlEstatusInvitacion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlEstatusInvitacion.DataBind();

                // Agregar Item de selección
                this.ddlEstatusInvitacion.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectInvitacion(Boolean CheckDate){
            ENTResponse oENTResponse = new ENTResponse();
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

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
                oENTInvitacion.InvitacionId = 0;
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;
                oENTInvitacion.EstatusInvitacionId = Int32.Parse( this.ddlEstatusInvitacion.SelectedItem.Value );
                oENTInvitacion.PrioridadId = Int32.Parse( this.ddlPrioridad.SelectedItem.Value );
                oENTInvitacion.FechaInicio = ( CheckDate ? this.wucBeginDate.DisplayUTCDate : GetUTCBeginDate() );
                oENTInvitacion.FechaFin = ( CheckDate ? this.wucEndDate.DisplayUTCDate : GetUTCEndDate() );
                oENTInvitacion.Nivel = 1;

                // Transacción
                oENTResponse = oBPInvitacion.SelectInvitacion(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

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


        
        // Invitaciones de la página

        protected void Page_Load(object sender, EventArgs e){
            DateTime tempDate = DateTime.Now;

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                SelectEstatusInvitacion();
                SelectPrioridad();

                // Por default preseleccionado todo el mes
                tempDate = tempDate.AddDays((DateTime.Now.Day - 1) * -1);
                this.wucBeginDate.SetDate(tempDate);

                tempDate = tempDate.AddMonths(1);
                tempDate = tempDate.AddDays(-1);
                this.wucEndDate.SetDate(tempDate);

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlEstatusInvitacion.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "'); }", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                SelectInvitacion(true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "'); }", true);
            }
        }

    }
}