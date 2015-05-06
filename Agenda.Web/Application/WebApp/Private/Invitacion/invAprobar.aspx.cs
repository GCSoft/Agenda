/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invAprobar
' Autor:	Ruben.Cobos
' Fecha:	22-Diciembre-2014
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
using Agenda.Entity.Object;
using Agenda.BusinessProcess.Object;
using System.Data;

namespace Agenda.Web.Application.WebApp.Private.Invitacion
{
    public partial class invAprobar : System.Web.UI.Page
    {
        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

		String GetKey(String sKey) {
			String Response = "";

			try{

				Response = gcEncryption.DecryptString(sKey, true);

			}catch(Exception){
				Response = "";
			}

			return Response;
		}


         // Rutinas el programador

        void SelectInvitacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTInvitacion oENTInvitacion = new ENTInvitacion();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                // Formulario
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);

                // Transacción
                oENTResponse = oBPInvitacion.SelectInvitacion_Detalle(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Carátula compacta
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();

                // Validación de envío de correos
                if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Representante"].ToString() != "0"){
                    this.rblNotificacion.SelectedValue = "3"; // Ninguno
                    this.rblNotificacion.Enabled = false;
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateInvitacion_Aprobar(){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);
                oENTInvitacion.Comentario = this.ckeComentarios.Text.Trim();
                oENTInvitacion.Notificacion = Int16.Parse(this.rblNotificacion.SelectedValue);

                // Transacción
                oENTResponse = oBPInvitacion.UpdateInvitacion_Aprobar(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

            }catch (Exception ex){
                throw (ex);
            }
        }


        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            String Key = "";

			try
            {

				// Validaciones de llamada
				if (Page.IsPostBack) { return; }
                if (this.Request.QueryString["key"] == null) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

				// Validaciones de parámetros
				Key = GetKey(this.Request.QueryString["key"].ToString());
				if (Key == "") { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }
				if (Key.ToString().Split(new Char[] { '|' }).Length != 2) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

                // Obtener InvitacionId
                this.hddInvitacionId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

				// Carátula
                SelectInvitacion();

                // Foco
                this.ckeComentarios.Focus();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); ", true);
                this.ckeComentarios.Focus();
            }
        }

        protected void btnAprobar_Click(object sender, EventArgs e){
            String sKey = "";

            try
            {

                // Aprobar
                UpdateInvitacion_Aprobar();

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDetalleInvitacion.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); ", true);
                this.ckeComentarios.Focus();
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

				// Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDetalleInvitacion.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); ", true);
                this.ckeComentarios.Focus();
            }
		}

    }
}