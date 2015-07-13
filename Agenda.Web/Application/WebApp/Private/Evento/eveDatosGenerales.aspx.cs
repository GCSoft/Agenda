/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveDatosGenerales
' Autor:	Ruben.Cobos
' Fecha:	27-Enero-2015
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
    public partial class eveDatosGenerales : System.Web.UI.Page
    {
       
        // Servicios

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSSecretario(string prefixText, int count){
			BPSecretario oBPSecretario = new BPSecretario();
			ENTSecretario oENTSecretario = new ENTSecretario();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Secretario Where SecretarioId = @SecretarioId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @SecretarioId = 0
			//				End

			try
			{

				// Formulario
                oENTSecretario.SecretarioId = 0;
				oENTSecretario.Nombre = prefixText;
                oENTSecretario.Activo = 1;

				// Transacción
				oENTResponse = oBPSecretario.SelectSecretario(oENTSecretario);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowSecretario in oENTResponse.DataSetResponse.Tables[1].Rows){
                    Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowSecretario["NombreTituloPuesto"].ToString(), rowSecretario["SecretarioId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Secretarios
			return ServiceResponse;
		}


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

        void SelectCategoria(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTCategoria oENTCategoria = new ENTCategoria();

            BPCategoria oBPCategoria = new BPCategoria();

            try
            {

                // Formulario
                oENTCategoria.CategoriaId = 0;
                oENTCategoria.Nombre = "";
                oENTCategoria.Activo = 1;

                // Transacción
                oENTResponse = oBPCategoria.SelectCategoria(oENTCategoria);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlCategoria.DataTextField = "Nombre";
                this.ddlCategoria.DataValueField = "CategoriaId";
                this.ddlCategoria.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlCategoria.DataBind();

                // Agregar Item de selección
                this.ddlCategoria.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectConducto(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTConducto oENTConducto = new ENTConducto();

            BPConducto oBPConducto = new BPConducto();

            try
            {

                // Formulario
                oENTConducto.ConductoId = 0;
                oENTConducto.Nombre = "";
                oENTConducto.Activo = 1;

                // Transacción
                oENTResponse = oBPConducto.SelectConducto(oENTConducto);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlConducto.DataTextField = "Nombre";
                this.ddlConducto.DataValueField = "ConductoId";
                this.ddlConducto.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlConducto.DataBind();

                // Agregar Item de selección
                this.ddlConducto.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
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

                // Carátula compacta
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraInicioTexto"].ToString() + " al " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraFinTexto"].ToString();
                
                // Precarga del formulario
                this.ddlCategoria.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["CategoriaId"].ToString();
                this.ddlConducto.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConductoId"].ToString();
                this.ddlPrioridad.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["PrioridadId"].ToString();

                this.txtSecretarioRamo.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRamoNombre"].ToString();
                this.hddSecretarioRamoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Ramo"].ToString();

                this.txtResponsable.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioResponsable"].ToString();
                this.hddResponsableId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Responsable"].ToString();

                if( oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Representante"].ToString() != "0" ){

                    this.txtRepresentante.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRepresentante"].ToString();
                    this.hddRepresentanteId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Representante"].ToString();
                }

                this.ckeObservaciones.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoObservaciones"].ToString();

                // Validación de secretario representante
                if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioId_Representante"].ToString() != "0"){
                    this.btnDescartarRepresentante.Visible = true;
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectPrioridad(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTPrioridad oENTPrioridad = new ENTPrioridad();

            BPPrioridad oBPPrioridad = new BPPrioridad();

            try
            {

                // Formulario
                oENTPrioridad.PrioridadId = 0;
                oENTPrioridad.Nombre = "";
                oENTPrioridad.Activo = 1;

                // Transacción
                oENTResponse = oBPPrioridad.SelectPrioridad(oENTPrioridad);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPrioridad.DataTextField = "Nombre";
                this.ddlPrioridad.DataValueField = "PrioridadId";
                this.ddlPrioridad.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPrioridad.DataBind();

                // Agregar Item de selección
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateEvento_DatosGenerales(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            try
            {

                // Validaciones
                if (this.ddlCategoria.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar un Tipo de cita")); }
                if (this.ddlConducto.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar un Conducto")); }
                if (this.ddlPrioridad.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar una Prioridad")); }
                if (this.hddSecretarioRamoId.Value.Trim() == "" || this.hddSecretarioRamoId.Value.Trim() == "0") { throw (new Exception("Es necesario seleccionar un Secretario Ramo")); }
                if (this.hddResponsableId.Value.Trim() == "" || this.hddResponsableId.Value.Trim() == "0") { throw (new Exception("Es necesario seleccionar un Responsable")); }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTEvento.EventoId = Int32.Parse( this.hddEventoId.Value );
                oENTEvento.CategoriaId = Int32.Parse(this.ddlCategoria.SelectedItem.Value);
                oENTEvento.ConductoId = Int32.Parse(this.ddlConducto.SelectedItem.Value);
                oENTEvento.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                oENTEvento.SecretarioId_Ramo = Int32.Parse(this.hddSecretarioRamoId.Value);
                oENTEvento.SecretarioId_Responsable = Int32.Parse(this.hddResponsableId.Value);
                oENTEvento.SecretarioId_Representante = (this.hddRepresentanteId.Value.Trim() == "" || this.hddRepresentanteId.Value.Trim() == "0" ? 0 : Int32.Parse(this.hddRepresentanteId.Value));
                oENTEvento.EventoObservaciones = this.ckeObservaciones.Text.Trim();

                // Transacción
                oENTResponse = oBPEvento.UpdateEvento_DatosGenerales(oENTEvento);

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

                // Obtener EventoId
                this.hddEventoId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

                // Llenado de controles
                SelectCategoria();
                SelectConducto();
                SelectPrioridad();

				// Carátula y formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Actualizar los datos generales
                UpdateEvento_DatosGenerales();

				// Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);
            }
		}

        protected void btnDescartarRepresentante_Click(object sender, EventArgs e){
            try
            {

                // Limpiar Autosuggest
                this.txtRepresentante.Text = "";
                this.hddRepresentanteId.Value = "";

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

				// Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);
            }
		}

    }
}