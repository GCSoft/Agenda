/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	repInvitacionPrensa
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
    public partial class repInvitacionPrensa : BPPage
    {
       
        // Utilerías
        GCJavascript gcJavascript = new GCJavascript();


        // Rutinas del programador

        void CrearReporte(DataTable tblInvitacionPrensa){
            ReportDataSource repDataSource;
            ReportParameter[] repParameters;

            DateTime dtNow = DateTime.Now;
            
            try
            {

                // Reset
                this.rptInvitacionPrensa.Reset();

                // DataSources
                repDataSource = new ReportDataSource("dsReporteInvitacionPrensa", tblInvitacionPrensa);
                this.rptInvitacionPrensa.LocalReport.DataSources.Add(repDataSource);

                // Path
                this.rptInvitacionPrensa.LocalReport.ReportPath = Server.MapPath("~/Application/WebApp/Private/Reportes/RDLC/rptInvitacionPrensa.rdlc");

                // Parameters
                repParameters = new ReportParameter[] {
                    new ReportParameter("FechaReporte", this.wucDate.DisplayLongDate ),
                    new ReportParameter("FechaConsulta", dtNow.Day.ToString() + "-" + dtNow.ToString("MMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "-" + dtNow.Year.ToString() ),
                    new ReportParameter("HoraConsulta", "Hora: " +  dtNow.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) )
                };

                this.rptInvitacionPrensa.LocalReport.SetParameters(repParameters);

                // Refresh
                this.rptInvitacionPrensa.LocalReport.Refresh();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectInvitacionPrensa(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTReporte oENTReporte = new ENTReporte();
            ENTSession oENTSession = new ENTSession();

            BPReporte oBPReporte = new BPReporte();

            try
            {

                // Validaciones
                if (!this.wucDate.IsValidDate()) { throw new Exception("El campo [Fecha] es requerido"); }
               
                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTReporte.Fecha = this.wucDate.DisplayUTCDate;

                // Transacción
                oENTResponse = oBPReporte.SelectReporte_InvitacionPrensa(oENTReporte);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Crear reporte
                CrearReporte(oENTResponse.DataSetResponse.Tables[0]);

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

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnBuscar.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                SelectInvitacionPrensa();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }
        }

    }
}