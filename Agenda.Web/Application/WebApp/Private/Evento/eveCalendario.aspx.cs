/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveCalendario
' Autor:    Ruben.Cobos
' Fecha:    27-Octubre-2013
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveCalendario : BPPage
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Rutinas de recuperación de formulario

        void CreateCalendar(){
            try
            {

                // Parámetros del calendario
                this.wucFullCalendar.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                this.wucFullCalendar.Dependencia = Int16.Parse(this.ddlDependencia.SelectedItem.Value);
                this.wucFullCalendar.EventoNuevos = Int16.Parse((this.chkNuevo.Checked ? "1" : "0"));
                this.wucFullCalendar.EventoProceso = Int16.Parse((this.chkProceso.Checked ? "1" : "0"));
                this.wucFullCalendar.EventoExpirado = Int16.Parse((this.chkExpirado.Checked ? "1" : "0"));
                this.wucFullCalendar.EventoCancelado = Int16.Parse((this.chkCancelar.Checked ? "1" : "0"));
                this.wucFullCalendar.EventoRepresentado = Int16.Parse((this.chkRepresentado.Checked ? "1" : "0"));
                this.wucFullCalendar.MesActual = Int32.Parse(this.hddCurrentMonth.Value);
                this.wucFullCalendar.AnioActual = Int32.Parse(this.hddCurrentYear.Value);

                // Construir calendario
                this.wucFullCalendar.ConstruirCalendario();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void DefaultForm(){
            ENTSession oENTSession = new ENTSession();
            ENTFiltroCalendario oENTFiltroCalendario = new ENTFiltroCalendario();

            try
            {
                
                // Obtener la sesion
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Seguridad
                if  ( oENTSession.RolId == 4 || oENTSession.RolId == 5 ){ this.RepresentadoPanel.Visible = false; }

                // Valores default
                this.ddlPrioridad.SelectedValue = "0";
                this.ddlDependencia.SelectedValue = "0";
                this.chkNuevo.Checked = true;
                this.chkProceso.Checked = true;
                this.chkExpirado.Checked = true;
                this.chkCancelar.Checked = true;
                this.chkRepresentado.Checked = ( oENTSession.RolId == 4 || oENTSession.RolId == 5 ? false : true );
                this.hddCurrentMonth.Value = DateTime.Now.Month.ToString();
                this.hddCurrentYear.Value = DateTime.Now.Year.ToString();

                // Configurar sesión
                oENTFiltroCalendario.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                oENTFiltroCalendario.Dependencia = Int16.Parse(this.ddlDependencia.SelectedItem.Value);
                oENTFiltroCalendario.EventoNuevos = Int16.Parse((this.chkNuevo.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoProceso = Int16.Parse((this.chkProceso.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoExpirado = Int16.Parse((this.chkExpirado.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoCancelado = Int16.Parse((this.chkCancelar.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoRepresentado = Int16.Parse((this.chkRepresentado.Checked ? "1" : "0"));

                oENTFiltroCalendario.MesActual = Int32.Parse(this.hddCurrentMonth.Value);
                oENTFiltroCalendario.AnioActual = Int32.Parse(this.hddCurrentYear.Value);

                // Guardar el formulario en la sesión
                oENTSession.Entity = oENTFiltroCalendario;
                this.Session["oENTSession"] = oENTSession;

                // Crear el calendario
                CreateCalendar();

            }catch (Exception ex){
                throw (ex);
            }
        }

		void RecoveryForm(){
			ENTSession oENTSession = new ENTSession();
            ENTFiltroCalendario oENTFiltroCalendario;

            try
            {

				// Obtener la sesion
				oENTSession = (ENTSession)this.Session["oENTSession"];

				// Validaciones
                if (oENTSession.Entity == null) { DefaultForm();  return; }
                if (oENTSession.Entity.GetType().Name != "ENTFiltroCalendario") { DefaultForm(); return; }

                // Obtener Formulario
                oENTFiltroCalendario = (ENTFiltroCalendario)oENTSession.Entity;

				// Vaciar formulario
                this.ddlPrioridad.SelectedValue = oENTFiltroCalendario.PrioridadId.ToString();
                this.ddlDependencia.SelectedValue = oENTFiltroCalendario.Dependencia.ToString();
                this.chkNuevo.Checked = (oENTFiltroCalendario.EventoNuevos == 1 ? true: false);
                this.chkProceso.Checked = (oENTFiltroCalendario.EventoProceso == 1 ? true : false);
                this.chkExpirado.Checked = (oENTFiltroCalendario.EventoExpirado == 1 ? true : false);
                this.chkCancelar.Checked = (oENTFiltroCalendario.EventoCancelado == 1 ? true : false);
                this.chkRepresentado.Checked = (oENTFiltroCalendario.EventoRepresentado == 1 ? true : false);
                this.hddCurrentMonth.Value = oENTFiltroCalendario.MesActual.ToString();
                this.hddCurrentYear.Value = oENTFiltroCalendario.AnioActual.ToString();

                // Crear el calendario
                CreateCalendar();

            }catch (Exception ex){
				ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('No fue posible recuperar el formulario: " + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

		void SaveForm(){
			ENTSession oENTSession = new ENTSession();
            ENTFiltroCalendario oENTFiltroCalendario = new ENTFiltroCalendario();

            try
            {

                // Formulario
                oENTFiltroCalendario.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                oENTFiltroCalendario.Dependencia = Int16.Parse(this.ddlDependencia.SelectedItem.Value);
                oENTFiltroCalendario.EventoNuevos = Int16.Parse( ( this.chkNuevo.Checked ? "1" : "0" ) );
                oENTFiltroCalendario.EventoProceso = Int16.Parse((this.chkProceso.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoExpirado = Int16.Parse((this.chkExpirado.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoCancelado = Int16.Parse((this.chkCancelar.Checked ? "1" : "0"));
                oENTFiltroCalendario.EventoRepresentado = Int16.Parse((this.chkRepresentado.Checked ? "1" : "0"));

                oENTFiltroCalendario.MesActual = Int32.Parse(this.hddCurrentMonth.Value);
                oENTFiltroCalendario.AnioActual = Int32.Parse(this.hddCurrentYear.Value);
                

				// Obtener la sesion
				oENTSession = (ENTSession)this.Session["oENTSession"];

                // Guardar el formulario en la sesión
                oENTSession.Entity = oENTFiltroCalendario;
				this.Session["oENTSession"] = oENTSession;

                // Refrescar la página
                Response.Redirect("eveCalendario.aspx", false);

            }catch (Exception ex){
                throw (ex);
            }
		}



        // Rutinas del programador

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



        
        // Eventos de la página ( por orden de aparición)

        protected void Page_Load(object sender, EventArgs e){
            String Key = "";

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Inicialización
                SelectDependencia();
                SelectPrioridad();
                RecoveryForm();

                // Atributos de controles
                Key = gcEncryption.EncryptString("1", true);
                this.imgImprimir.Attributes.Add("onclick", "window.open('eveCalendarioCompleto.aspx?key=" + Key + "', 'FullCalendarWindow', 'menubar=1,resizable=1,width=1024,height=800'); return false;");

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlPrioridad.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void ddlPrioridad_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void ddlDependencia_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void chkNuevo_CheckedChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void chkProceso_CheckedChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void chkExpirado_CheckedChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void chkCancelar_CheckedChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }

        protected void chkRepresentado_CheckedChanged(object sender, EventArgs e){
            try
            {

                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }


        // Eventos del WUC

        protected void wucFullCalendar_ChangeMonth(){
            try
            {

                // Actualización  Mes y Año
                this.hddCurrentMonth.Value = this.wucFullCalendar.MesActual.ToString();
                this.hddCurrentYear.Value = this.wucFullCalendar.AnioActual.ToString();

                // Cargar eventos
                SaveForm();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPrioridad.ClientID + "');", true);
            }
        }
        
    }
}