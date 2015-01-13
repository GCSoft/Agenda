/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invDetalleInvitacion
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
    public partial class invDetalleInvitacion : System.Web.UI.Page
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


		
        // Rutinas del programador

        void CheckDeleteLinkComentario(){
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Obtener sesión
                oENTSession = (ENTSession)Session["oENTSession"];

                // Si es LugarEvento y esta asociado en la invitación podrá eliminar comentarios únicamente sus comentarios siempre y cuando no esté la invitación en estatus finales:
                // 1 - Cancelada
                // 2 - Declinada
                // 6 - Aprobada
                if ( oENTSession.RolId == 3 )
                {

                    if ( Int32.Parse(this.hddEstatusInvitacionId.Value) != 1 && Int32.Parse(this.hddEstatusInvitacionId.Value) != 2 && Int32.Parse(this.hddEstatusInvitacionId.Value) != 6 )
                    {

                        foreach (RepeaterItem repItem in repComentarios.Items)
                        {

                            HiddenField oHiddenField = (HiddenField)repItem.FindControl("hddUsuarioId_Comentario");
                            if ( oHiddenField.Value == oENTSession.UsuarioId.ToString() )
                            {

                                LinkButton oLinkButton = (LinkButton)repItem.FindControl("lnkEliminarComentario");
                                oLinkButton.Visible = true;

                                oLinkButton = (LinkButton)repItem.FindControl("lnkEditarComentario");
                                oLinkButton.Visible = true;
                            }

                        }

                    }
                }

                // Si es Administrador puede  editar y eliminar comentarios siempre y cuando no esté la invitación en estatus finales:
                // 1 - Cancelada
                // 2 - Declinada
                // 6 - Aprobada
                if ( oENTSession.RolId == 1 || oENTSession.RolId == 2 )
                {
                    if ( Int32.Parse(this.hddEstatusInvitacionId.Value) != 1 && Int32.Parse(this.hddEstatusInvitacionId.Value) != 2 && Int32.Parse(this.hddEstatusInvitacionId.Value) != 6 )
                    {

                        foreach (RepeaterItem repItem in repComentarios.Items)
                        {

                            LinkButton oLinkButton = (LinkButton)repItem.FindControl("lnkEliminarComentario");
                            oLinkButton.Visible = true;

                            oLinkButton = (LinkButton)repItem.FindControl("lnkEditarComentario");
                            oLinkButton.Visible = true;
                        }

                    }
                }

            }catch(Exception ex){
                throw(ex);
            }
		}

        void DeleteInvitacionComentario(Int32 InvitacionComentarioId) {
			BPInvitacion oBPInvitacion = new BPInvitacion();

			ENTInvitacion oENTInvitacion = new ENTInvitacion();
			ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

			try
			{

				// Datos de sesión
                oENTSession = (ENTSession)Session["oENTSession"];
				
				// Formulario
                oENTInvitacion.InvitacionComentarioId = InvitacionComentarioId;
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

				// Transacción
                oENTResponse = oBPInvitacion.DeleteInvitacionComentario(oENTInvitacion);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

			}catch (Exception ex){
				throw (ex);
			}
		}

        void InsertInvitacionComentario() {
			BPInvitacion oBPInvitacion = new BPInvitacion();

			ENTInvitacion oENTInvitacion = new ENTInvitacion();
			ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

			try
			{

				// Validaciones
				if (this.ckePopUpComentario.Text.Trim() == "") { throw (new Exception("Es necesario ingresar un comentario")); }

				// Datos de sesión
                oENTSession = (ENTSession)Session["oENTSession"];
				
				// Formulario
				oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);
                oENTInvitacion.ModuloId = 1; // Invitación
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;
				oENTInvitacion.Comentario = this.ckePopUpComentario.Text.Trim();

				// Transacción
				oENTResponse = oBPInvitacion.InsertInvitacionComentario(oENTInvitacion);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

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

                // Campos ocultos
                this.hddEstatusInvitacionId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusInvitacionId"].ToString();

                // Formulario
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();
                this.lblEstatusInvitacionNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusInvitacionNombre"].ToString();

                this.lblCategoriaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["CategoriaNombre"].ToString();
                this.lblConductoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConductoNombre"].ToString();
                this.lblPrioridadNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["PrioridadNombre"].ToString();

                this.lblSecretarioRamoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRamoNombre"].ToString();
                this.lblSecretarioResponsable.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioResponsable"].ToString();
                this.lblSecretarioRepresentante.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRepresentante"].ToString();

                this.lblLugarEventoCompleto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoCompleto"].ToString();
                this.lblEventoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoDetalle"].ToString();

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["InvitacionObservaciones"].ToString().Trim() == "" ){

                    this.lblObservacionesGenerales.Visible = false;
                    this.lblObservacionesGeneralesDetalle.Visible = false;
                }else{

                    this.lblObservacionesGenerales.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["InvitacionObservaciones"].ToString();
                }

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusInvitacionId"].ToString().Trim() != "2" ){ // Declinada

                    this.lblMotivoRechazo.Visible = false;
                    this.lblMotivoRechazoDetalle.Visible = false;
                }else{

                    this.lblMotivoRechazo.Visible = true;
                    this.lblMotivoRechazoDetalle.Visible = true;
                    this.lblMotivoRechazoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString();
                }

                // Funcionarios
                this.gvFuncionario.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvFuncionario.DataBind();

                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[3];
                this.gvContacto.DataBind();

                // Documentos
                if (oENTResponse.DataSetResponse.Tables[4].Rows.Count == 0){

					this.SinDocumentoLabel.Text = "<br /><br />No hay documentos anexados";
				}else{

					this.SinDocumentoLabel.Text = "";
                    this.dlstDocumentoList.DataSource = oENTResponse.DataSetResponse.Tables[4];
					this.dlstDocumentoList.DataBind();
				}

                // Comentarios
                if (oENTResponse.DataSetResponse.Tables[5].Rows.Count == 0){

					this.SinComentariosLabel.Text = "<br /><br />No hay comentarios para esta invitación";
					this.repComentarios.DataSource = null;
					this.repComentarios.DataBind();
					this.ComentarioTituloLabel.Text = "";
				}else{

					this.SinComentariosLabel.Text = "";
                    this.repComentarios.DataSource = oENTResponse.DataSetResponse.Tables[5];
					this.repComentarios.DataBind();
                    this.ComentarioTituloLabel.Text = oENTResponse.DataSetResponse.Tables[5].Rows.Count.ToString() + " comentarios";

					CheckDeleteLinkComentario();

				}

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectInvitacionComentario_Edit() {
			BPInvitacion oBPInvitacion = new BPInvitacion();

			ENTInvitacion oENTInvitacion = new ENTInvitacion();
			ENTResponse oENTResponse = new ENTResponse();

			try
			{
				
				// Formulario
                oENTInvitacion.InvitacionComentarioId = Int32.Parse(this.hddInvitacionComentarioId.Value);
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);
                oENTInvitacion.UsuarioId = 0;
                oENTInvitacion.Activo = 2;

				// Transacción
                oENTResponse = oBPInvitacion.SelectInvitacionComentario(oENTInvitacion);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Vaciado de datos
                this.ckePopUpComentario.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentario"].ToString();

			}catch (Exception ex){
				throw (ex);
			}
		}

        void SetErrorPage(){
			try
            {

                this.DatosGeneralesPanel.Visible = false;
                this.DatosEventoPanel.Visible = false;
                this.ContactoPanel.Visible = false;
                this.FuncionarioPanel.Visible = false;
                this.AdjuntarPanel.Visible = false;
                this.Historial.Visible = false;
                this.RechazarPanel.Visible = false;
                this.AprobarPanel.Visible = false;

                this.pnlPopUp.Visible = false;

            }catch (Exception){
				// Do Nothing
            }
		}

        void SetPermisosGenerales(Int32 RolId) {
			try
            {

                // Permisos por rol
				switch (RolId){

					case 1:	// System Administrator
                    case 2:	// Administrador
                        this.DatosGeneralesPanel.Visible = true;
                        this.DatosEventoPanel.Visible = true;
                        this.ContactoPanel.Visible = true;
                        this.FuncionarioPanel.Visible = true;
                        this.AdjuntarPanel.Visible = true;
                        this.Historial.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.AprobarPanel.Visible = true;
						break;

					case 3:	// Funcionario
						this.DatosGeneralesPanel.Visible = false;
                        this.DatosEventoPanel.Visible = false;
                        this.ContactoPanel.Visible = false;
                        this.FuncionarioPanel.Visible = false;
                        this.AdjuntarPanel.Visible = true;
                        this.Historial.Visible = false;
                        this.RechazarPanel.Visible = false;
                        this.AprobarPanel.Visible = false;
						break;

					default:
                        this.DatosGeneralesPanel.Visible = false;
                        this.DatosEventoPanel.Visible = false;
                        this.ContactoPanel.Visible = false;
                        this.FuncionarioPanel.Visible = false;
                        this.AdjuntarPanel.Visible = false;
                        this.Historial.Visible = false;
                        this.RechazarPanel.Visible = false;
                        this.AprobarPanel.Visible = false;
						break;

				}
	

            }catch (Exception ex){
				throw(ex);
            }
		}

		void SetPermisosParticulares(Int32 RolId, Int32 UsuarioId) {
			try
            {

                // Si es Administrador o LugarEvento puede agregar comentarios
				if ( RolId == 1 || RolId == 2 || RolId == 3 ) {

                    this.lnkAgregarComentario.Visible = true;

				}

				// La invitación no se podrá operar en los siguientes Estatus:
                // 1 - Cancelada
                // 2 - Declinada
                // 6 - Aprobada
				if ( Int32.Parse(this.hddEstatusInvitacionId.Value) == 1 || Int32.Parse(this.hddEstatusInvitacionId.Value) == 2 || Int32.Parse(this.hddEstatusInvitacionId.Value) == 6 ){

                    this.DatosGeneralesPanel.Visible = false;
                    this.DatosEventoPanel.Visible = false;
                    this.ContactoPanel.Visible = false;
                    this.FuncionarioPanel.Visible = false;
                    this.AdjuntarPanel.Visible = false;
                    this.RechazarPanel.Visible = false;
                    this.AprobarPanel.Visible = false;
                    this.lnkAgregarComentario.Visible = false;

				}

            }catch (Exception ex){
				throw(ex);
            }
		}

        void UpdateInvitacionComentario() {
			BPInvitacion oBPInvitacion = new BPInvitacion();

			ENTInvitacion oENTInvitacion = new ENTInvitacion();
			ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

			try
			{

				// Validaciones
				if (this.ckePopUpComentario.Text.Trim() == "") { throw (new Exception("Es necesario ingresar un comentario")); }

				// Datos de sesión
                oENTSession = (ENTSession)Session["oENTSession"];
				
				// Formulario
                oENTInvitacion.InvitacionComentarioId = Int32.Parse(this.hddInvitacionComentarioId.Value);
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;
				oENTInvitacion.Comentario = this.ckePopUpComentario.Text.Trim();

				// Transacción
                oENTResponse = oBPInvitacion.UpdateInvitacionComentario(oENTInvitacion);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

			}catch (Exception ex){
				throw (ex);
			}
		}



        // Eventos de la página

		protected void Page_Load(object sender, EventArgs e){
			ENTSession oENTSession = new ENTSession();
			String Key;

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

                switch (this.SenderId.Value){
					case "1":
                        this.Sender.Value = "invInvitacion.aspx";
                        break;

					case "2":
                        this.Sender.Value = "../AppIndex.aspx";
						break;

					case "3":
                        this.Sender.Value = "invListado.aspx";
						break;

                    default:
                        this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false);
                        return;
                }

                // Consultar detalle de la invitación
                SelectInvitacion();

                // Obtener sesión
                oENTSession = (ENTSession)Session["oENTSession"];

                // Seguridad
                SetPermisosGenerales(oENTSession.RolId);
                SetPermisosParticulares(oENTSession.RolId, oENTSession.UsuarioId);

                // Estado inicial del formulario
                this.pnlPopUp.Visible = false;

            }catch (Exception ex){
                SetErrorPage();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
            Response.Redirect(this.Sender.Value);
        }

        protected void dlstDocumentoList_ItemDataBound(Object sender, DataListItemEventArgs e){
            Label DocumentoLabel;
            Image DocumentoImage;
            DataRowView DataRow;

			String DocumentoId = "";
			String Key = "";

            try
            {

                // Validación de que sea Item 
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) { return; }

                // Obtener controles
                DocumentoImage = (Image)e.Item.FindControl("DocumentoImage");
                DocumentoLabel = (Label)e.Item.FindControl("DocumentoLabel");
                DataRow = (DataRowView)e.Item.DataItem;

				// Id del documento
				DocumentoId = DataRow["DocumentoId"].ToString();
				Key = gcEncryption.EncryptString(DocumentoId, true);

                // Configurar imagen
				DocumentoLabel.Text = DataRow["NombreDocumentoCorto"].ToString();

                DocumentoImage.ImageUrl = "~/Include/Image/File/" + DataRow["Icono"].ToString();
				DocumentoImage.ToolTip = DataRow["NombreDocumento"].ToString();
                DocumentoImage.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
                DocumentoImage.Attributes.Add("onmouseout", "this.style.cursor='auto'");
                DocumentoImage.Attributes.Add("onclick", "window.open('" + ResolveUrl("~/Include/Handler/Documento.ashx") + "?key=" + Key + "');");

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvContacto_RowDataBound(object sender, GridViewRowEventArgs e){
            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }                

                // Atributos Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; ");

                // Atributos Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; ");

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

        protected void gvFuncionario_RowDataBound(object sender, GridViewRowEventArgs e){
            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }                

                // Atributos Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; ");

                // Atributos Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; ");

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvFuncionario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvFuncionario, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }



        // Opciones de Menu (en orden de aparación)

		protected void InformacionGeneralButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDatosGenerales.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void DatosEventoButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDatosEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ContactoButton_Click(object sender, ImageClickEventArgs e){
			 String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invContacto.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void FuncionarioButton_Click(object sender, ImageClickEventArgs e){
			 String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invFuncionarios.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void AdjuntarButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDocumentos.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void RechazarButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invRechazar.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void AprobarButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invAprobar.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void HistorialButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invHistorial.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}



        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Determinar transacción
                switch( this.hddInvitacionComentarioId.Value ){
                    case "0":

                        InsertInvitacionComentario();
                        break;

                    default:

                        UpdateInvitacionComentario();
                        break;
                }

                // Actualizar el expediente
                SelectInvitacion();

                // Ocultar el panel
                this.pnlPopUp.Visible = false;

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                this.ckePopUpComentario.Focus();
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Ocultar el panel
                this.pnlPopUp.Visible = false;

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void lnkAgregarComentario_Click(object sender, EventArgs e){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.lblPopUpMessage.Text = "";
                this.ckePopUpComentario.Text = "";
                this.hddInvitacionComentarioId.Value = "0";

                // Personalizar PopUp
                this.lblPopUpTitle.Text = "Nuevo Comentario";
                this.btnPopUpCommand.Text = "Emitir Comentario";

                // Foco
                this.ckePopUpComentario.Focus();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void lnkEditarComentario_Click(object sender, CommandEventArgs  e){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.lblPopUpMessage.Text = "";
                this.hddInvitacionComentarioId.Value = e.CommandArgument.ToString();

                // Personalizar PopUp
                this.lblPopUpTitle.Text = "Editar Comentario";
                this.btnPopUpCommand.Text = "Ajustar Comentario";

                // Consulta de detalle de comentario
                SelectInvitacionComentario_Edit();

                // Foco
                this.ckePopUpComentario.Focus();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void lnkEliminarComentario_Click(object sender, CommandEventArgs  e){
            try
            {

                // Eliminar el comentario
                DeleteInvitacionComentario(Int32.Parse(e.CommandArgument.ToString()));

                // Actualizar detalle
                SelectInvitacion();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

    }
}