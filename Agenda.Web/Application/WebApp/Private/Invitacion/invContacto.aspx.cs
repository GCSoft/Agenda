/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invContacto
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
    public partial class invContacto : System.Web.UI.Page
    {
        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }


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

        void DeleteContacto(Int32 InvitacionContactoId){
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
                oENTInvitacion.InvitacionContactoId = InvitacionContactoId;
                oENTInvitacion.InvitacionId = Int32.Parse( this.hddInvitacionId.Value );

                // Transacción
                oENTResponse = oBPInvitacion.DeleteInvitacionContacto(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar formulario
                SelectInvitacion();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Contacto eliminado con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void InsertContacto(){
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
                oENTInvitacion.InvitacionId = Int32.Parse( this.hddInvitacionId.Value );
                oENTInvitacion.Contacto.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTInvitacion.Contacto.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTInvitacion.Contacto.Organizacion = this.txtPopUpOrganizacion.Text.Trim();
                oENTInvitacion.Contacto.Telefono = this.txtPopUpTelefono.Text.Trim();
                oENTInvitacion.Contacto.Email = this.txtPopUpEmail.Text.Trim();
                oENTInvitacion.Contacto.Comentarios = this.ckePopUpComentarios.Text.Trim();

                // Transacción
                oENTResponse = oBPInvitacion.InsertInvitacionContacto(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar formulario
                SelectInvitacion();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Contacto asociado con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectContacto_ForEdit(Int32 InvitacionContactoId){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                
                // Formulario
                oENTInvitacion.InvitacionContactoId = InvitacionContactoId;
                oENTInvitacion.InvitacionId = Int32.Parse( this.hddInvitacionId.Value );
                oENTInvitacion.Activo = 1;

                // Transacción
                oENTResponse = oBPInvitacion.SelectInvitacionContacto(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpPuesto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Puesto"].ToString();
                this.txtPopUpOrganizacion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Organizacion"].ToString();
                this.txtPopUpTelefono.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Telefono"].ToString();
                this.txtPopUpEmail.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Email"].ToString();
                this.ckePopUpComentarios.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

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

                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[3];
                this.gvContacto.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateContacto(Int32 InvitacionContactoId){
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
                oENTInvitacion.InvitacionContactoId = InvitacionContactoId;
                oENTInvitacion.InvitacionId = Int32.Parse( this.hddInvitacionId.Value );
                oENTInvitacion.Contacto.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTInvitacion.Contacto.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTInvitacion.Contacto.Organizacion = this.txtPopUpOrganizacion.Text.Trim();
                oENTInvitacion.Contacto.Telefono = this.txtPopUpTelefono.Text.Trim();
                oENTInvitacion.Contacto.Email = this.txtPopUpEmail.Text.Trim();
                oENTInvitacion.Contacto.Comentarios = this.ckePopUpComentarios.Text.Trim();

                // Transacción
                oENTResponse = oBPInvitacion.UpdateInvitacionContacto(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar formulario
                SelectInvitacion();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp

        void ClearPopUpPanel(){
            try
            {

                // Limpiar formulario
                this.txtPopUpNombre.Text = "";
                this.txtPopUpPuesto.Text = "";
                this.txtPopUpOrganizacion.Text = "";
                this.txtPopUpTelefono.Text = "";
                this.txtPopUpEmail.Text = "";
                this.ckePopUpComentarios.Text = "";

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddInvitacionContactoId.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes MenuPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddInvitacionContactoId.Value = idItem.ToString();

                // Detalle de acción
                switch (MenuPopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Contacto";
                        this.btnPopUpCommand.Text = "Agregar Contacto";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Contacto";
                        this.btnPopUpCommand.Text = "Actualizar Contacto";
                        SelectContacto_ForEdit(idItem);
                        break;

                    default:
                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {
                
                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                if (this.txtPopUpTelefono.Text.Trim() == "") { throw new Exception("* El campo [Teléfono] es requerido"); }

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

                // Estado inicial del formulario
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
			try
            {

                // Nuevo registro
                SetPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void gvContacto_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 InvitacionContactoId = 0;

            String strCommand = "";
            Int32 intRow = 0;

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el evento RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                InvitacionContactoId = Int32.Parse(this.gvContacto.DataKeys[intRow]["InvitacionContactoId"].ToString());

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, InvitacionContactoId);
                        break;

                    case "Eliminar":
                        DeleteContacto(InvitacionContactoId);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void gvContacto_RowDataBound(object sender, GridViewRowEventArgs e){
           ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String InvitacionContactoId = "";
            String ContactoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                InvitacionContactoId = this.gvContacto.DataKeys[e.Row.RowIndex]["InvitacionContactoId"].ToString();
                ContactoNombre = this.gvContacto.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Contacto [" + ContactoNombre + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip Delete
                sTootlTip = "Eliminar Contacto [" + ContactoNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvContacto_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvContacto, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }



        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddInvitacionContactoId.Value == "0"){

                    InsertContacto();
                }else{

                    UpdateContacto(Int32.Parse(this.hddInvitacionContactoId.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }


    }
}