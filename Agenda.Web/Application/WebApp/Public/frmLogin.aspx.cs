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



        // Funciones del programador

        String GetRedirectPage(){
            String Response = "../Private/AppIndex.aspx";
            String Key = "";
            String Sender = "";

            try
            {

                if (this.hddToken.Value != ""){

                    // Desencriptar
                    Key = this.hddToken.Value;
                    Key = gcEncryption.DecryptString(Key, false);

                    // Separar Sender de la llave
                    Sender = Key.ToString().Split(new Char[] { '|' })[0];
                    Key = Key.ToString().Split(new Char[] { '|' })[1];

                    // Validación
                    Key = Int32.Parse(Key).ToString();

                    // Empaquetar
                    Key = Key + "|2";
                    Key = gcEncryption.EncryptString(Key, true);

                    // Redireccionamiento
                    switch (Sender){
                        case "1":
                            Response = "../Private/Invitacion/invDetalleInvitacion.aspx?key=" + Key;
                            break;

                        case "2":
                            Response = "../Private/Evento/eveDetalleEvento.aspx?key=" + Key;
                            break;
                    }
                }


            }catch (Exception){
                //Do Nothing
            }

            return Response;
        }


        // Rutinas del programador

        void CookiesGetConfiguration(){
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

        void CookiesSetConfiguration(){
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

        void LoginUser(){
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

                // Redireccionar
                this.Response.Redirect(GetRedirectPage(), false);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void RecoveryPassword(){
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

                // Obtener Token de invitación
                if (this.Request.QueryString["key"] != null) { this.hddToken.Value = this.Request.QueryString["key"].ToString(); }

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