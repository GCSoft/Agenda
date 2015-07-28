/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	girNuevaGira
' Autor:	Ruben.Cobos
' Fecha:	20-Marzo-2015
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

namespace Agenda.Web.Application.WebApp.Private.Gira
{
    public partial class girNuevaGira : BPPage
    {

        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();
        GCParse gcParse = new GCParse();



        // Rutinas del programador

        void InsertGira(){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPGira oBPGira = new BPGira();
            
            String JSScript = "";
            String Key = "";

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTGira.UsuarioId = oENTSession.UsuarioId;
                oENTGira.RolId = oENTSession.RolId;

                // Formulario
                oENTGira.GiraNombre = this.txtNombreGira.Text.Trim();
                oENTGira.FechaGiraInicio = this.wucCalendarInicio.DisplayUTCDate;
                oENTGira.FechaGiraFin = this.wucCalendarFin.DisplayUTCDate;
                oENTGira.HoraGiraInicio = this.wucTimerDesde.DisplayUTCTime;
                oENTGira.HoraGiraFin = this.wucTimerHasta.DisplayUTCTime;
                oENTGira.GiraDetalle = this.ckeGiraDetalle.Text.Trim();

                oENTGira.Contacto.Nombre = this.txtContactoNombre.Text.Trim();
                oENTGira.Contacto.Puesto = this.txtContactoPuesto.Text.Trim();
                oENTGira.Contacto.Organizacion = this.txtContactoOrganizacion.Text.Trim();
                oENTGira.Contacto.Telefono = this.txtContactoTelefono.Text.Trim();
                oENTGira.Contacto.Email = this.txtContactoEmail.Text.Trim();
                oENTGira.Contacto.Comentarios = this.ckeContactoComentarios.Text.Trim();

                oENTGira.EstatusGiraId = 1; // Nueva
               
                // Transacción
                oENTResponse = oBPGira.InsertGira(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                LimpiaFormulario();

                // Llave encriptada
                Key = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraId"].ToString() + "|1";
                Key = gcEncryption.EncryptString(Key, true);

                // Mensaje a desplegar y script
                JSScript = "function pageLoad(){ if( confirm('Se registró la Gira exitosamente. ¿Desea ir al detalle para continuar con la captura?') ) { window.location.href = 'girDetalleGira.aspx?key=" + Key + "'; } else { window.location.href = 'girNuevaGira.aspx'; } }";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), JSScript, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void LimpiaFormulario(){
            try
            {

                // TAB - Datos de la Gira
                this.txtNombreGira.Text = "";
                this.wucCalendarInicio.SetDate(DateTime.Now);
                this.wucCalendarFin.SetDate(DateTime.Now);
                this.wucTimerDesde.DisplayTime = "10:00";
                this.wucTimerHasta.DisplayTime = "10:00";
                this.ckeGiraDetalle.Text = "";

                // TAB - Contacto
                this.txtContactoNombre.Text = "";
                this.txtContactoPuesto.Text = "";
                this.txtContactoOrganizacion.Text = "";
                this.txtContactoTelefono.Text = "";
                this.txtContactoEmail.Text = "";
                this.ckeContactoComentarios.Text = "";

                // Foco y pestaña
                this.tabGira.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidarFormulario(){
            String ErrorDetailHour = "";

            try
            {

                // TAB - Datos del evento
                if( this.txtNombreGira.Text.Trim() == "" ){
                    this.tabGira.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario ingresar un Nombre de Gira"));
                }

                if ( !this.wucCalendarInicio.IsValidDate() ) {
                    this.tabGira.ActiveTabIndex = 0;
                    throw new Exception("El campo [Fecha de Inicio de la Gira] es requerido");
                }

                if ( !this.wucCalendarFin.IsValidDate() ) {
                    this.tabGira.ActiveTabIndex = 0;
                    throw new Exception("El campo [Fecha final de la Gira] es requerido");
                }

                if ( !this.wucTimerDesde.IsValidTime(ref ErrorDetailHour) ) {
                    this.tabGira.ActiveTabIndex = 0;
                    throw new Exception("El campo [Hora de inicio del Gira] es requerido: " + ErrorDetailHour);
                }

                if ( !this.wucTimerHasta.IsValidTime(ref ErrorDetailHour) ) {
                    this.tabGira.ActiveTabIndex = 0;
                    throw new Exception("El campo [Hora de finalización del Gira] es requerido: " + ErrorDetailHour);
                }


                //// TAB - Contacto
                //if( this.txtContactoNombre.Text.Trim() == "" ){
                //    this.tabGira.ActiveTabIndex = 1;
                //    throw (new Exception("Es necesario ingresar un Nombre de contacto"));
                //}

                //if( this.txtContactoTelefono.Text.Trim() == "" ){
                //    this.tabGira.ActiveTabIndex = 1;
                //    throw (new Exception("Es necesario ingresar un Teléfono del contacto"));
                //}

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Giras de la página

        protected void Page_Load(object sender, EventArgs e){
            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Estado inicial
                this.wucCalendarInicio.Width = 176;
                this.wucCalendarFin.Width = 176;

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }       

        protected void btnCancelar_Click(object sender, EventArgs e){
            try
            {

                LimpiaFormulario();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e){
            try
            {

                // Validar el formulario
                ValidarFormulario();

                // Transacción
                InsertGira();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }


    }
}