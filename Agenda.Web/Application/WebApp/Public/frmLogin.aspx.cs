/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	frmLogin
' Autor:	Ruben.Cobos
' Fecha:	21-Octubre-2013
'
' Descripción:
'           Pantalla de autenticación de la aplicación
'
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
using Agenda.Entity.Object;

namespace Agenda.Web.Application.WebApp.Public
{
    public partial class frmLogin : System.Web.UI.Page
    {
       
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Rutinas del programador

        private void CookiesGetConfiguration(){
            try
            {

                // Usuario
                if (this.Request.Cookies[gcCommon.StampCookie("Email")] != null){
                    this.txtEmail.Text = gcEncryption.DecryptString(this.Server.HtmlEncode(this.Request.Cookies[gcCommon.StampCookie("Email")].Value), false);
                }

                // Password
                if (this.Request.Cookies[gcCommon.StampCookie("Password")] != null){
                    this.txtPassword.Attributes.Add("value", this.Server.HtmlEncode(this.Request.Cookies[gcCommon.StampCookie("Password")].Value));
                    this.chkRememberPassword.Checked = true;
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        private void CookiesSetConfiguration(){
            try
            {

                // Usuario
                this.Response.Cookies[gcCommon.StampCookie("Email")].Value = gcEncryption.EncryptString(this.txtEmail.Text, false);
                this.Response.Cookies[gcCommon.StampCookie("Email")].Expires = DateTime.Now.AddDays(100);

                // Password
                if (this.chkRememberPassword.Checked){
                    this.Response.Cookies[gcCommon.StampCookie("Password")].Value = gcEncryption.EncryptString((this.hddEncryption.Value == "1" ? gcEncryption.DecryptString(this.txtPassword.Text, false) : this.txtPassword.Text), false);
                    this.Response.Cookies[gcCommon.StampCookie("Password")].Expires = DateTime.Now.AddDays(100);
                }else{
                    this.Response.Cookies[gcCommon.StampCookie("Password")].Expires = DateTime.Now.AddDays(-1);
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        private void LoginUser(){
            BPUsuario oBPUsuario = new BPUsuario();

            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Datos del formulario
                oENTUsuario.Email = this.txtEmail.Text;
                oENTUsuario.Password = (this.hddEncryption.Value == "1" ? gcEncryption.DecryptString(this.txtPassword.Text, false) : this.txtPassword.Text);

                // Transacción
                oENTResponse = oBPUsuario.SelectUsuario_Autenticacion(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Usuario válido
                CookiesSetConfiguration();
                this.Response.Redirect("../Private/AppIndex.aspx", false);

            }catch (Exception ex){
                throw (ex);
            }
        }

        private void RecoveryPassword(){
            BPUsuario oBPUsuario = new BPUsuario();

            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Datos del formulario
                oENTUsuario.Email = this.txtEmail.Text;

                // Transacción
                oENTResponse = oBPUsuario.SelectUsuario_RecuperaContrasena(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Recuperación exitosa
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Los datos de recuperación de contraseña han sido enviados por correo electrónico'); function pageLoad(){ focusControl('" + this.txtEmail.ClientID + "'); }", true);

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

                // Variable de sesión incial. Previene Sys.Webforms.PageRequestManagerServerErrorException
                this.Session.Add("oENTSession", new ENTSession());

                // Configuraciones personalizadas guardadas en las Cookies
                CookiesGetConfiguration();

                // Atributos de los controles
                this.btnLogin.Attributes.Add("onclick", "return validateLogin();");
                this.btnRecoveryPassword.Attributes.Add("onclick", "return validateRecoveryPassword();");
                this.txtPassword.Attributes.Add("onchange", "document.getElementById('" + this.hddEncryption.ClientID + "').value = '0'");

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtEmail.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtEmail.ClientID + "'); }", true);
            }
		}

		protected void btnLogin_Click(object sender, EventArgs e){
            try
            {

                // Autenticar al usuario
                LoginUser();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtEmail.ClientID + "'); }", true);
            }
		}

		protected void btnRecoveryPassword_Click(object sender, EventArgs e){
            try
            {

                // Recuperar contraseña
                RecoveryPassword();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtEmail.ClientID + "'); }", true);
            }
		}

    }
}