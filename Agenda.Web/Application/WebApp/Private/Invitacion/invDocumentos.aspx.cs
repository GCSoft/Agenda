﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invDocumentos
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
using System.IO;

namespace Agenda.Web.Application.WebApp.Private.Invitacion
{
    public partial class invDocumentos : System.Web.UI.Page
    {
        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Variables publicas
        ENTSession oCurrentSession;


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

        void DeleteDocumento(Int32 DocumentoId){
			ENTDocumento oENTDocumento = new ENTDocumento();
			ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

			BPDocumento oBPDocumento = new BPDocumento();

			try
			{

                // Obtener Sesion
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTDocumento.UsuarioId = oENTSession.UsuarioId;
                oENTDocumento.RolId = oENTSession.RolId;

				// Formulario
				oENTDocumento.DocumentoId = DocumentoId;
                oENTDocumento.ModuloId = 1; // Invitación

				// Consultar información del archivo
				oENTResponse = oBPDocumento.SelectDocumento_Path(oENTDocumento);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

				// Eliminar físicamente el archivo
                if (File.Exists(oENTResponse.DataSetResponse.Tables[1].Rows[0]["Ruta"].ToString())) { File.Delete(oENTResponse.DataSetResponse.Tables[1].Rows[0]["Ruta"].ToString()); }

				// Eliminar la referencia del archivo en la base de datos
				oENTResponse = oBPDocumento.DeleteDocumento(oENTDocumento);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

				// Estado inicial del formulario
				this.ckeDescripcion.Text = "";

				// Refrescar el formulario
                SelectInvitacion();

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);

			}catch ( IOException ioEx){

				throw (ioEx);
			}catch (Exception ex){

				throw (ex);
			}
		}

        void InsertDocumento(){
            ENTDocumento oENTDocumento = new ENTDocumento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

            BPDocumento oBPDocumento = new BPDocumento();

            try
            {

                // Validaciones
                if (this.ddlTipoDocumento.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar un tipo de documento")); }
                if (this.fupDocumento.PostedFile == null) { throw (new Exception("Es necesario seleccionar un documento")); }
				if (!this.fupDocumento.HasFile) { throw (new Exception("Es necesario seleccionar un documento")); }
				if (this.fupDocumento.PostedFile.ContentLength == 0) { throw (new Exception("Es necesario seleccionar un documento")); }
				
				 // Obtener Sesion
				oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTDocumento.UsuarioId = oENTSession.UsuarioId;
                oENTDocumento.RolId = oENTSession.RolId;

				// Formulario
				oENTDocumento.InvitacionId = Int32.Parse( this.hddInvitacionId.Value );
				oENTDocumento.EventoId = 0;
				oENTDocumento.ModuloId = 1; // Invitación
				oENTDocumento.TipoDocumentoId = Int32.Parse(this.ddlTipoDocumento.SelectedItem.Value);
				oENTDocumento.Extension = Path.GetExtension(this.fupDocumento.PostedFile.FileName);
				oENTDocumento.Nombre = this.fupDocumento.FileName;
                oENTDocumento.Descripcion = this.ckeDescripcion.Text.Trim();
                oENTDocumento.Ruta = oBPDocumento.UploadFile(this.fupDocumento.PostedFile, this.hddInvitacionId.Value, BPDocumento.RepositoryTypes.Invitacion);

				// Transacción
				oENTResponse = oBPDocumento.InsertDocumento(oENTDocumento);

				// Validaciones
				if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
				if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Estado inicial del formulario
                this.ckeDescripcion.Text = "";

                // Refrescar el formulario
                SelectInvitacion();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);

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
                this.lblEventoFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();

                // Documentos
                this.gvDocumento.DataSource = oENTResponse.DataSetResponse.Tables[4];
                this.gvDocumento.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoDocumento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoDocumento oENTTipoDocumento = new ENTTipoDocumento();

            BPTipoDocumento oBPTipoDocumento = new BPTipoDocumento();

            try
            {

                // Formulario
                oENTTipoDocumento.TipoDocumentoId = 0;
                oENTTipoDocumento.ModuloId = 1; // Invitación
                oENTTipoDocumento.Nombre = "";
                oENTTipoDocumento.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoDocumento.SelectTipoDocumento(oENTTipoDocumento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoDocumento.DataTextField = "Nombre";
                this.ddlTipoDocumento.DataValueField = "TipoDocumentoId";
                this.ddlTipoDocumento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoDocumento.DataBind();

                // Agregar Item de selección
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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

                // Llenado de controles
                SelectTipoDocumento();

				// Carátula
                SelectInvitacion();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e){
            try
            {
                
                InsertDocumento();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoDocumento.ClientID + "'); }", true);
            }
		}

        protected void gvDocumento_RowCommand(object sender, GridViewCommandEventArgs e){
			String CommandName = "";
			String DocumentoId = "";
			String sKey = "";

			Int32 iRow = 0;

            try
            {
                // Opción seleccionada
                CommandName = e.CommandName.ToString();

                // Se dispara el evento RowCommand en el ordenamiento
                if (CommandName == "Sort") { return; }

                // Fila
                iRow = Convert.ToInt32(e.CommandArgument.ToString());

                // DataKeys
                DocumentoId = gvDocumento.DataKeys[iRow]["DocumentoId"].ToString();

                // Acción
                switch (CommandName){
                    case "Visualizar":

						sKey = gcEncryption.EncryptString(DocumentoId, true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "window.open('" + ResolveUrl("~/Include/Handler/Documento.ashx") + "?key=" + sKey + "');", true);
                        break;

                    case "Borrar":
                        DeleteDocumento(Int32.Parse(DocumentoId));
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

		protected void gvDocumento_RowDataBound(object sender, GridViewRowEventArgs e){
			ImageButton imgView = null;
			ImageButton imgDelete = null;

			String DocumentoId = "";
            String ModuloId = "";
            String UsuarioId = "";
			String NombreDocumento = "";
			String Icono = "";

			String sImagesAttributes = "";
			String sToolTip = "";

			try
			{
				
				// Validación de que sea fila 
				if (e.Row.RowType != DataControlRowType.DataRow) { 
                    
                    // Usuario que consulta
                    oCurrentSession = (ENTSession)this.Session["oENTSession"];
                    return;
                }

				// Obtener objetos
				imgView = (ImageButton)e.Row.FindControl("imgView");
				imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

				// Datakeys
				DocumentoId = this.gvDocumento.DataKeys[e.Row.RowIndex]["DocumentoId"].ToString();
                ModuloId = this.gvDocumento.DataKeys[e.Row.RowIndex]["ModuloId"].ToString();
                UsuarioId = this.gvDocumento.DataKeys[e.Row.RowIndex]["UsuarioId"].ToString();
				Icono = this.gvDocumento.DataKeys[e.Row.RowIndex]["Icono"].ToString();
				NombreDocumento = this.gvDocumento.DataKeys[e.Row.RowIndex]["NombreDocumento"].ToString();

                // ToolTip Visualizar
				sToolTip = "Visualizar [" + NombreDocumento + "]";
                imgView.Attributes.Add("title", sToolTip);
				imgView.Attributes.Add("style", "cursor:hand;");
				imgView.ImageUrl = "~/Include/Image/File/" + Icono;

				// Seguridad
				if( UsuarioId != oCurrentSession.UsuarioId.ToString() && oCurrentSession.RolId > 2 ){

					imgDelete.Visible = false;

					// Atributos Over y Out
					e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; ");
					e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; ");

				}else{

                    // Tooltip Eliminar
					sToolTip = "Eliminar [" + NombreDocumento + "]";
                    imgDelete.Attributes.Add("title", sToolTip);
					imgDelete.Attributes.Add("style", "cursor:hand;");

					// Atributos Over
					sImagesAttributes = "document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
					e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

					// Atributos Out
					sImagesAttributes = "document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
					e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

				}

			}catch (Exception ex){
				throw (ex);
			}
		}

		protected void gvDocumento_Sorting(object sender, GridViewSortEventArgs e){
			try
			{

				gcCommon.SortGridView(ref this.gvDocumento, ref this.hddSort, e.SortExpression);

			}catch (Exception ex){
				ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
			}
		}

    }
}