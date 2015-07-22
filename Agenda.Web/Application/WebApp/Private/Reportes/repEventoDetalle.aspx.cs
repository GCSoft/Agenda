/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	repEventoDetalle
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
    public partial class repEventoDetalle : BPPage
    {
       

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

        String GetUTCDate(){
            DateTime tempDate = DateTime.Now;
            String UTCCurrentDate = "";

            try
            {

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

        void CrearReporte(DataSet dsContenidoPrensa){
            ReportDataSource repDataSource;
            ReportParameter[] repParameters;

            DateTime dtNow = DateTime.Now;
            
            try
            {

                // Reset
                this.rptEventoDetalle.Reset();

                // DataSources
                repDataSource = new ReportDataSource("dsComiteRecepcion", dsContenidoPrensa.Tables[1]);
                this.rptEventoDetalle.LocalReport.DataSources.Add(repDataSource);

                repDataSource = new ReportDataSource("dsOrdenDia", dsContenidoPrensa.Tables[2]);
                this.rptEventoDetalle.LocalReport.DataSources.Add(repDataSource);

                repDataSource = new ReportDataSource("dsAcomodo", dsContenidoPrensa.Tables[3]);
                this.rptEventoDetalle.LocalReport.DataSources.Add(repDataSource);

                repDataSource = new ReportDataSource("dsObservaciones", dsContenidoPrensa.Tables[4]);
                this.rptEventoDetalle.LocalReport.DataSources.Add(repDataSource);

                // Path
                this.rptEventoDetalle.LocalReport.ReportPath = Server.MapPath("~/Application/WebApp/Private/Reportes/RDLC/rptEventoDetalle.rdlc");

                // Parameters
                repParameters = new ReportParameter[] {
                    new ReportParameter("FechaReporte", dsContenidoPrensa.Tables[0].Rows[0]["FechaReporte"].ToString() ),
                    new ReportParameter("FechaReporteNatural", dsContenidoPrensa.Tables[0].Rows[0]["FechaReporteNatural"].ToString() ),
                    new ReportParameter("HoraInicio", dsContenidoPrensa.Tables[0].Rows[0]["HoraInicio"].ToString() ),
                    new ReportParameter("HoraFin", dsContenidoPrensa.Tables[0].Rows[0]["HoraFin"].ToString() ),
                    new ReportParameter("EventoNombre", dsContenidoPrensa.Tables[0].Rows[0]["EventoNombre"].ToString() ),
                    new ReportParameter("TipoAcomodo", dsContenidoPrensa.Tables[0].Rows[0]["TipoAcomodo"].ToString() )
                    //new ReportParameter("FechaConsulta", dtNow.Day.ToString() + "-" + dtNow.ToString("MMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) + "-" + dtNow.Year.ToString() ),
                    //new ReportParameter("HoraConsulta", "Hora: " +  dtNow.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX")) )
                };

                this.rptEventoDetalle.LocalReport.SetParameters(repParameters);

                // Refresh
                this.rptEventoDetalle.LocalReport.Refresh();

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
                    if (!this.wucDate.IsValidDate()) { throw new Exception("El campo [Fecha] es requerido"); }
                }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTEvento.EventoId = 0;
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.EstatusEventoId = 0;
                oENTEvento.PrioridadId = 0;
                oENTEvento.Nivel = 1;
                oENTEvento.Dependencia = 0;

                oENTEvento.FechaInicio = (CheckDate ? this.wucDate.DisplayUTCDate : GetUTCDate());
                oENTEvento.FechaFin = (CheckDate ? this.wucDate.DisplayUTCDate : GetUTCDate());
                oENTEvento.PalabraClave = "";

                // Transacción
                oENTResponse = oBPEvento.SelectEvento(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Listado de Eventos
                this.gvEvento.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvEvento.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEventoDetalle(Int32 EventoId, Int32 GiraId, Int32 GiraConfiguracionId){
            ENTResponse oENTResponse = new ENTResponse();
            ENTReporte oENTReporte = new ENTReporte();

            BPReporte oBPReporte = new BPReporte();

            try
            {

                // Formulario
                oENTReporte.EventoId = EventoId;
                oENTReporte.GiraId = GiraId;
                oENTReporte.GiraConfiguracionId = GiraConfiguracionId;

                // Transacción
                oENTResponse = oBPReporte.SelectReporte_ContenidoPrensa(oENTReporte);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Crear reporte
                CrearReporte(oENTResponse.DataSetResponse);

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

                // Estado inicial
                SelectEvento(false);

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnBuscar.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                SelectEvento(true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }
        }

        protected void gvEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 intRow = 0;
            String strCommand = "";

            Int32 EventoId = 0;
            Int32 GiraId = 0;
            Int32 GiraConfiguracionId = 0;

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el Evento RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                EventoId = Int32.Parse(this.gvEvento.DataKeys[intRow]["EventoId"].ToString());
                GiraId = Int32.Parse(this.gvEvento.DataKeys[intRow]["GiraId"].ToString());
                GiraConfiguracionId = Int32.Parse(this.gvEvento.DataKeys[intRow]["GiraConfiguracionId"].ToString());

                // Acción
                switch (strCommand){
                    case "Visualizar":

                        SelectEventoDetalle(EventoId, GiraId, GiraConfiguracionId);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }
        }

        protected void gvEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgReporte = null;

            String EventoNombre = "";
            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgReporte = (ImageButton)e.Row.FindControl("imgReporte");

                // Datakeys
                EventoNombre = this.gvEvento.DataKeys[e.Row.RowIndex]["EventoNombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Generar reporte [" + EventoNombre + "]";
                imgReporte.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgReporte.ClientID + "').src='../../../../Include/Image/Buttons/Reporte_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgReporte.ClientID + "').src='../../../../Include/Image/Buttons/Reporte.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvEvento, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.btnBuscar.ClientID + "'); }", true);
            }

        }


    }
}