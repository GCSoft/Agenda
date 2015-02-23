/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveDetalleEvento
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


namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveDetalleEvento : System.Web.UI.Page
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

        void SelectEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();

            BPEvento oBPEvento = new BPEvento();

            try
            {

                // Formulario
                oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);

                // Transacción
                oENTResponse = oBPEvento.SelectEvento_Detalle(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Campos ocultos
                this.hddEstatusEventoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoId"].ToString();
                this.Expired.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Expired"].ToString();
                this.Logistica.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Logistica"].ToString();

                // Formulario
                this.lblDependenciaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Dependencia"].ToString();
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();
                this.lblEstatusEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoNombre"].ToString();

                this.lblCategoriaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["CategoriaNombre"].ToString();
                this.lblConductoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConductoNombre"].ToString();
                this.lblPrioridadNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["PrioridadNombre"].ToString();

                this.lblSecretarioRamoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRamoNombre"].ToString();
                this.lblSecretarioResponsable.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioResponsable"].ToString();
                this.lblSecretarioRepresentante.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRepresentante"].ToString();

                this.lblLugarEventoCompleto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoCompleto"].ToString();
                this.lblEventoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoDetalle"].ToString();

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoObservaciones"].ToString().Trim() == "" ){

                    this.lblObservacionesGenerales.Visible = false;
                    this.lblObservacionesGeneralesDetalle.Visible = false;
                }else{

                    this.lblObservacionesGenerales.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoObservaciones"].ToString();
                }

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoId"].ToString().Trim() != "2" ){ // Declinada

                    this.lblMotivoRechazo.Visible = false;
                    this.lblMotivoRechazoDetalle.Visible = false;
                }else{

                    this.lblMotivoRechazo.Visible = true;
                    this.lblMotivoRechazoDetalle.Visible = true;
                    this.lblMotivoRechazoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString();
                }

                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvContacto.DataBind();

                // Documentos
                if (oENTResponse.DataSetResponse.Tables[3].Rows.Count == 0){

					this.SinDocumentoLabel.Text = "<br /><br />No hay documentos anexados";
				}else{

					this.SinDocumentoLabel.Text = "";
                    this.dlstDocumentoList.DataSource = oENTResponse.DataSetResponse.Tables[3];
					this.dlstDocumentoList.DataBind();
				}

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetErrorPage(){
			try
            {

                this.EliminarRepresentantePanel.Visible = false;
                this.DatosGeneralesPanel.Visible = false;
                this.DatosEventoPanel.Visible = false;
                this.ProgramaLogisticaPanel.Visible = false;
                this.ProgramaProtocoloPanel.Visible = false;
                this.ContactoPanel.Visible = false;
                this.AdjuntarPanel.Visible = false;
                this.Historial.Visible = false;
                this.RechazarPanel.Visible = false;
                this.CuadernilloLogisticaPanel.Visible = false;
                this.CuadernilloProtocoloPanel.Visible = false;

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
                        this.EliminarRepresentantePanel.Visible = true;
                        this.DatosGeneralesPanel.Visible = true;
                        this.DatosEventoPanel.Visible = true;
                        this.ProgramaLogisticaPanel.Visible = ( this.Logistica.Value == "1" ? true : false );
                        this.ProgramaProtocoloPanel.Visible = ( this.Logistica.Value == "1" ? false : true );
                        this.ContactoPanel.Visible = true;
                        this.AdjuntarPanel.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloLogisticaPanel.Visible = (this.Logistica.Value == "1" ? true : false);
                        this.CuadernilloProtocoloPanel.Visible = (this.Logistica.Value == "1" ? false : true);
                        this.Historial.Visible = true;
						break;

                    case 4:	// Logística
                    case 5:	// Dirección de Protocolo
                        this.EliminarRepresentantePanel.Visible = true;
						this.DatosGeneralesPanel.Visible = true;
                        this.DatosEventoPanel.Visible = true;
                        this.ProgramaLogisticaPanel.Visible = ( this.Logistica.Value == "1" ? true : false );
                        this.ProgramaProtocoloPanel.Visible = ( this.Logistica.Value == "1" ? false : true );
                        this.ContactoPanel.Visible = true;
                        this.AdjuntarPanel.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloLogisticaPanel.Visible = false;
                        this.CuadernilloProtocoloPanel.Visible = false;
                        this.Historial.Visible = true;
						break;

					default:
                        this.EliminarRepresentantePanel.Visible = false;
                        this.DatosGeneralesPanel.Visible = false;
                        this.DatosEventoPanel.Visible = false;
                        this.ProgramaLogisticaPanel.Visible = false;
                        this.ProgramaProtocoloPanel.Visible = false;
                        this.ContactoPanel.Visible = false;
                        this.AdjuntarPanel.Visible = false;
                        this.RechazarPanel.Visible = false;
                        this.CuadernilloLogisticaPanel.Visible = false;
                        this.CuadernilloProtocoloPanel.Visible = false;
                        this.Historial.Visible = false;
						break;

				}
	

            }catch (Exception ex){
				throw(ex);
            }
		}

		void SetPermisosParticulares(Int32 RolId, Int32 UsuarioId) {
			try
            {

				// El Evento no se podrá operar en los siguientes Estatus:
                // 3 - Expirado
                // 4 - Cancelado
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) == 3 || Int32.Parse(this.hddEstatusEventoId.Value) == 4 || Int32.Parse(this.hddEstatusEventoId.Value) == 5 ){

                    this.DatosGeneralesPanel.Visible = false;
                    this.DatosEventoPanel.Visible = false;
                    this.ProgramaLogisticaPanel.Visible = false;
                    this.ProgramaProtocoloPanel.Visible = false;
                    this.ContactoPanel.Visible = false;
                    this.AdjuntarPanel.Visible = false;
                    this.RechazarPanel.Visible = false;

				}

                // Si el evento está cancelado o representado no se podrá generar el cuadernillo
                // 4 - Cancelado
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) == 4 || Int32.Parse(this.hddEstatusEventoId.Value) == 5 ){

                    this.CuadernilloLogisticaPanel.Visible = false;
                    this.CuadernilloProtocoloPanel.Visible = false;
				}

                // Si el evento no está representado no se podrá eliminar al respresentante
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) != 5 ){

                    this.EliminarRepresentantePanel.Visible = false;

				}

                // Independientemente del estatus, si ya expiró ocultar opciones no contempladas
				if ( this.Expired.Value == "1" ){

                    this.EliminarRepresentantePanel.Visible = false;

				}

            }catch (Exception ex){
				throw(ex);
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

                // Obtener EventoId
				this.hddEventoId.Value = Key.ToString().Split(new Char[] { '|' })[0];

                // Obtener Sender
				this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

                switch (this.SenderId.Value){
					case "1":
                        this.Sender.Value = "eveNuevoEvento.aspx";
                        break;

					case "2":
                        this.Sender.Value = "../AppIndex.aspx";
						break;

					case "3":
                        this.Sender.Value = "eveListadoEventos.aspx";
						break;

                    case "4":
                        this.Sender.Value = "eveCalendario.aspx";
                        break;

                    default:
                        this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false);
                        return;
                }

                // Atributos de controles
                this.CuadernilloLogisticaButton.Attributes.Add("onclick", "window.open('Cuadernillos/Logistica.aspx?key=" + gcEncryption.EncryptString(this.hddEventoId.Value, true) + "'); return false;");
                this.CuadernilloProtocoloButton.Attributes.Add("onclick", "window.open('Cuadernillos/Protocolo.aspx?key=" + gcEncryption.EncryptString(this.hddEventoId.Value, true) + "'); return false;");

                // Consultar detalle de El Evento
                SelectEvento();

                // Obtener sesión
                oENTSession = (ENTSession)Session["oENTSession"];

                // Seguridad
                SetPermisosGenerales(oENTSession.RolId);
                SetPermisosParticulares(oENTSession.RolId, oENTSession.UsuarioId);

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
            System.Web.UI.WebControls.Image DocumentoImage;
            DataRowView DataRow;

			String DocumentoId = "";
			String Key = "";

            try
            {

                // Validación de que sea Item 
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) { return; }

                // Obtener controles
                DocumentoImage = (System.Web.UI.WebControls.Image)e.Item.FindControl("DocumentoImage");
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



        // Opciones de Menu (en orden de aparación)

        protected void EliminarRepresentanteButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveEliminarRepresentante.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

		protected void InformacionGeneralButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDatosGenerales.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void DatosEventoButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDatosEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ProgramaLogisticaButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveConfiguracionEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ProgramaProtocoloButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveConfiguracionEventoProtocolo.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ContactoButton_Click(object sender, ImageClickEventArgs e){
			 String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveContacto.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void AdjuntarButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDocumentos.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void RechazarButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveCancelar.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void HistorialButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveHistorial.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

    }
}