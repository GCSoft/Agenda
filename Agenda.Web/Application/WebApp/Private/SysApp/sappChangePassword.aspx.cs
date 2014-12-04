/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:   sappChangePassword
' Autor:	Ruben.Cobos
' Fecha:	21-Octubre-2013
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
using Agenda.BusinessProcess.Page;
using Agenda.BusinessProcess.Object;
using Agenda.Entity.Object;

namespace Agenda.Web.Application.WebApp.Private.SysApp
{
    public partial class sappChangePassword : BPPage
    {

        // Utilerías
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

        void UpdateUserPassword(){
            BPUsuario oBPUsuario = new BPUsuario();

            ENTSession oENTSession = new ENTSession();
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Obtener sesion
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Datos del formulario
                oENTUsuario.UsuarioId = oENTSession.UsuarioId;
                oENTUsuario.Password = this.txtNuevoPassword.Text;
                oENTUsuario.PasswordAnterior = this.txtPasswordAnterior.Text;

                // Transacción
                oENTResponse = oBPUsuario.UpdateUsuario_ActualizaContrasena(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Su contraseña ha sido actualizada con éxito'); focusControl('" + this.txtPasswordAnterior.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }


        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Atributos de los controles
                this.btnActualizarPassword.Attributes.Add("onclick", "return validateNewPassword();");

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPasswordAnterior.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPasswordAnterior.ClientID + "');", true);
            }
        }

        protected void btnActualizarPassword_Click(object sender, EventArgs e){
            try
            {

                // Actualizar contraseña
                UpdateUserPassword();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPasswordAnterior.ClientID + "');", true);
            }
        }

    }
}