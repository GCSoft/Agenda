/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	girContacto
' Autor:	Ruben.Cobos
' Fecha:	30-Marzo-2015
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

namespace Agenda.Web.Application.WebApp.Private.Gira
{
    public partial class girContacto : System.Web.UI.Page
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

        void DeleteContacto(Int32 GiraContactoId){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPGira oBPGira = new BPGira();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTGira.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTGira.GiraContactoId = GiraContactoId;
                oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );

                // Transacción
                oENTResponse = oBPGira.DeleteGiraContacto(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar formulario
                SelectGira();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Contacto eliminado con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void InsertContacto(){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPGira oBPGira = new BPGira();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTGira.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                oENTGira.Contacto.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTGira.Contacto.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTGira.Contacto.Organizacion = this.txtPopUpOrganizacion.Text.Trim();
                oENTGira.Contacto.Telefono = this.txtPopUpTelefono.Text.Trim();
                oENTGira.Contacto.Email = this.txtPopUpEmail.Text.Trim();
                oENTGira.Contacto.Comentarios = this.ckePopUpComentarios.Text.Trim();

                // Transacción
                oENTResponse = oBPGira.InsertGiraContacto(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar formulario
                SelectGira();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Contacto asociado con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectContacto_ForEdit(Int32 GiraContactoId){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();

            BPGira oBPGira = new BPGira();

            try
            {

                
                // Formulario
                oENTGira.GiraContactoId = GiraContactoId;
                oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                oENTGira.Activo = 1;

                // Transacción
                oENTResponse = oBPGira.SelectGiraContacto(oENTGira);

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

        void SelectGira(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTGira oENTGira = new ENTGira();

            BPGira oBPGira = new BPGira();

            try
            {

                // Formulario
                oENTGira.GiraId = Int32.Parse(this.hddGiraId.Value);

                // Transacción
                oENTResponse = oBPGira.SelectGira_Detalle(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Carátula compacta
                this.lblGiraNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString();
                this.lblGiraFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHora"].ToString();

                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[3];
                this.gvContacto.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateContacto(Int32 GiraContactoId){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPGira oBPGira = new BPGira();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTGira.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTGira.GiraContactoId = GiraContactoId;
                oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                oENTGira.Contacto.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTGira.Contacto.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTGira.Contacto.Organizacion = this.txtPopUpOrganizacion.Text.Trim();
                oENTGira.Contacto.Telefono = this.txtPopUpTelefono.Text.Trim();
                oENTGira.Contacto.Email = this.txtPopUpEmail.Text.Trim();
                oENTGira.Contacto.Comentarios = this.ckePopUpComentarios.Text.Trim();

                // Transacción
                oENTResponse = oBPGira.UpdateGiraContacto(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar formulario
                SelectGira();

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
                this.hddGiraContactoId.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes MenuPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddGiraContactoId.Value = idItem.ToString();

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



        // Giras de la página

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

                // Obtener GiraId
                this.hddGiraId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

				// Carátula
                SelectGira();

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
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girDetalleGira.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void gvContacto_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 GiraContactoId = 0;

            String strCommand = "";
            Int32 intRow = 0;

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el Gira RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                GiraContactoId = Int32.Parse(this.gvContacto.DataKeys[intRow]["GiraContactoId"].ToString());

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, GiraContactoId);
                        break;

                    case "Eliminar":
                        DeleteContacto(GiraContactoId);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void gvContacto_RowDataBound(object sender, GridViewRowEventArgs e){
           ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String GiraContactoId = "";
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
                GiraContactoId = this.gvContacto.DataKeys[e.Row.RowIndex]["GiraContactoId"].ToString();
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



        // Giras del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddGiraContactoId.Value == "0"){

                    InsertContacto();
                }else{

                    UpdateContacto(Int32.Parse(this.hddGiraContactoId.Value));
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