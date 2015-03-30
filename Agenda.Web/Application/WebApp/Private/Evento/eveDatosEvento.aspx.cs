/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveDatosEvento
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
    public partial class eveDatosEvento : System.Web.UI.Page
    {
       
        // Servicios

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSColonia(string prefixText, int count, string contextKey){
			BPColonia oBPColonia = new BPColonia();
			ENTColonia oENTColonia = new ENTColonia();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Colonia Where ColoniaId = @ColoniaId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @ColoniaId = 0
			//				End

			try
			{

				// Formulario
                oENTColonia.ColoniaId = 0;
                oENTColonia.EstadoId = 0;
                oENTColonia.MunicipioId = Int32.Parse(contextKey);
				oENTColonia.Nombre = prefixText;
                oENTColonia.Activo = 1;

				// Transacción
				oENTResponse = oBPColonia.SelectColonia(oENTColonia);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowColonia in oENTResponse.DataSetResponse.Tables[1].Rows){
					Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowColonia["Nombre"].ToString(), rowColonia["ColoniaId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Colonias
			return ServiceResponse;
		}

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSLugarEvento(string prefixText, int count){
			BPLugarEvento oBPLugarEvento = new BPLugarEvento();
			ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From LugarEvento Where LugarEventoId = @LugarEventoId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @LugarEventoId = 0
			//				End

			try
			{

				// Formulario
                oENTLugarEvento.LugarEventoId = 0;
				oENTLugarEvento.Nombre = prefixText;
                oENTLugarEvento.Activo = 1;

				// Transacción
				oENTResponse = oBPLugarEvento.SelectLugarEvento(oENTLugarEvento);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowLugarEvento in oENTResponse.DataSetResponse.Tables[1].Rows){
                    Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowLugarEvento["NombreDisplay"].ToString(), rowLugarEvento["LugarEventoId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de LugarEventos
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
                this.lblEventoFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();

                // Precarga del formulario
                this.txtNombreEvento.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.wucCalendar.SetDate( DateTime.Parse( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFecha"].ToString() ) );
                this.wucTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHoraInicioEstandar"].ToString();
                this.wucTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHoraFinEstandar"].ToString();

                this.txtLugarEvento.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString();
                this.hddLugarEventoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoId"].ToString();

                this.txtMunicipio.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MunicipioNombre"].ToString();
                this.txtColonia.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ColoniaNombre"].ToString();
                this.txtCalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Calle"].ToString();
                this.txtNumeroExterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroExterior"].ToString();
                this.txtNumeroInterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroInterior"].ToString();
                this.ckeObservaciones.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoDetalle"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectLugarEvento(){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Formulario
                oENTLugarEvento.LugarEventoId = Int32.Parse(this.hddLugarEventoId.Value);
                oENTLugarEvento.Nombre = "";
                oENTLugarEvento.Activo = 1;

                // Transacción
                oENTResponse = oBPLugarEvento.SelectLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de controles
                this.txtLugarEvento.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtMunicipio.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MunicipioNombre"].ToString();
                this.txtColonia.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ColoniaNombre"].ToString();
                this.txtCalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Calle"].ToString();
                this.txtNumeroExterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroExterior"].ToString();
                this.txtNumeroInterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroInterior"].ToString();

                // Foco
                this.ckeObservaciones.Focus();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateEvento_DatosEvento(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            String ErrorDetailHour = "";

            try
            {

                // Validaciones
                if (this.txtNombreEvento.Text.Trim() == "") { throw new Exception("El campo [Nombre del evento] es requerido"); }
                if (!this.wucCalendar.IsValidDate()) { throw new Exception("El campo [Fecha del evento] es requerido"); }
                if (!this.wucTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour); }
                if (!this.wucTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de finalización del evento] es requerido: " + ErrorDetailHour); }
                if (this.hddLugarEventoId.Value.Trim() == "" || this.hddLugarEventoId.Value.Trim() == "0") { throw (new Exception("Es necesario seleccionar un Lugar del Evento")); }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                oENTEvento.EventoNombre = this.txtNombreEvento.Text.Trim();
                oENTEvento.FechaEvento = this.wucCalendar.DisplayUTCDate;
                oENTEvento.HoraEventoInicio = this.wucTimerDesde.DisplayUTCTime;
                oENTEvento.HoraEventoFin = this.wucTimerHasta.DisplayUTCTime;
                oENTEvento.LugarEventoId = Int32.Parse(this.hddLugarEventoId.Value);
                oENTEvento.EventoDetalle = this.ckeObservaciones.Text.Trim();

                // Transacción
                oENTResponse = oBPEvento.UpdateEvento_DatosEvento(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp de Lugar de Evento

        void ClearPopUpPanel_LugarEvento(){
            try
            {

                // Limpiar formulario
                this.txtPopUpNombre_LugarEvento.Text = "";
                this.ddlPopUpEstado_LugarEvento.SelectedIndex = 0;
                this.txtPopUpColonia_LugarEvento.Text = "";
                this.txtPopUpCalle_LugarEvento.Text = "";
                this.txtPopUpNumeroExterior_LugarEvento.Text = "";
                this.txtPopUpNumeroInterior_LugarEvento.Text = "";
                this.ckePopUpDescripcion_LugarEvento.Text = "";

                this.ddlPopUpMunicipio_LugarEvento.Items.Clear();
                this.ddlPopUpMunicipio_LugarEvento.Items.Insert(0, new ListItem("[Seleccione]", "0"));

                // Estado incial de controles
                this.pnlPopUp_LugarEvento.Visible = false;
                this.lblPopUpTitle_LugarEvento.Text = "";
                this.btnPopUpCommand_LugarEvento.Text = "";
                this.lblPopUpMessage_LugarEvento.Text = "";
                this.hddPopUpColoniaId_LugarEvento.Value = "";
                this.txtPopUpColonia_LugarEvento.Enabled = false;
                this.txtPopUpColonia_LugarEvento.CssClass = "Textbox_General_Disabled";

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia_LugarEvento.ContextKey = this.ddlPopUpMunicipio_LugarEvento.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void InsertLugarEvento(){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Validaciones
                if (this.txtPopUpNombre_LugarEvento.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                if (this.hddPopUpColoniaId_LugarEvento.Value.Trim() == "" || this.hddPopUpColoniaId_LugarEvento.Value.Trim() == "0") { throw new Exception("* Es necesario seleccionar una colonia"); }
                if (this.txtPopUpCalle_LugarEvento.Text.Trim() == "") { throw new Exception("* El campo [Calle] es requerido"); }
                if (this.txtPopUpNumeroExterior_LugarEvento.Text.Trim() == "") { throw new Exception("* El campo [Número Exterior] es requerido"); }

                // Formulario
                oENTLugarEvento.Nombre = this.txtPopUpNombre_LugarEvento.Text.Trim();
                oENTLugarEvento.ColoniaId = Int32.Parse(this.hddPopUpColoniaId_LugarEvento.Value);
                oENTLugarEvento.Calle = this.txtPopUpCalle_LugarEvento.Text.Trim();
                oENTLugarEvento.NumeroExterior = this.txtPopUpNumeroExterior_LugarEvento.Text.Trim();
                oENTLugarEvento.NumeroInterior = this.txtPopUpNumeroInterior_LugarEvento.Text.Trim();
                oENTLugarEvento.Activo = 1;
                oENTLugarEvento.Descripcion = this.ckePopUpDescripcion_LugarEvento.Text.Trim();
                oENTLugarEvento.Rank = 1;

                // Transacción
                oENTResponse = oBPLugarEvento.InsertLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel_LugarEvento();

                // Lugar de evento generado
                this.hddLugarEventoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoId"].ToString();
                SelectLugarEvento();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstado_PopUp_LugarEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEstado oENTEstado = new ENTEstado();

            BPEstado oBPEstado = new BPEstado();

            try
            {

                // Formulario
                oENTEstado.PaisId = 0;
                oENTEstado.EstadoId = 0;
                oENTEstado.Nombre = "";
                oENTEstado.Activo = 1;

                // Transacción
                oENTResponse = oBPEstado.SelectEstado(oENTEstado);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo de Estado
                this.ddlPopUpEstado_LugarEvento.DataTextField = "Nombre";
                this.ddlPopUpEstado_LugarEvento.DataValueField = "EstadoId";
                this.ddlPopUpEstado_LugarEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUpEstado_LugarEvento.DataBind();

                // Elemento extra
                this.ddlPopUpEstado_LugarEvento.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMunicipio_PopUp_LugarEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMunicipio oENTMunicipio = new ENTMunicipio();

            BPMunicipio oBPMunicipio = new BPMunicipio();

            try
            {

                // Formulario
                oENTMunicipio.EstadoId = Int32.Parse(this.ddlPopUpEstado_LugarEvento.SelectedValue);
                oENTMunicipio.MunicipioId = 0;
                oENTMunicipio.Nombre = "";
                oENTMunicipio.Activo = 1;

                 // Debido al número de municipio sólo se carga el combo cuando se selecciona un estado
                if( oENTMunicipio.EstadoId == 0 ){

                    this.ddlPopUpMunicipio_LugarEvento.Items.Clear();
                }else{

                    // Transacción
                    oENTResponse = oBPMunicipio.SelectMunicipio(oENTMunicipio);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Llenado de combo de municipio
                    this.ddlPopUpMunicipio_LugarEvento.DataTextField = "Nombre";
                    this.ddlPopUpMunicipio_LugarEvento.DataValueField = "MunicipioId";
                    this.ddlPopUpMunicipio_LugarEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.ddlPopUpMunicipio_LugarEvento.DataBind();

                }

                // Elemento extra
                this.ddlPopUpMunicipio_LugarEvento.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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

                // Estado inicial
                this.wucCalendar.Width = 176;

                SelectEstado_PopUp_LugarEvento();
                SelectMunicipio_PopUp_LugarEvento();
                ClearPopUpPanel_LugarEvento();

				// Carátula
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtNombreEvento.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreEvento.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Actualizar los datos del evento
                UpdateEvento_DatosEvento();

                // Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreEvento.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreEvento.ClientID + "'); }", true);
            }
		}


        // Eventos del autosuggest

        protected void hddLugarEventoId_ValueChanged(object sender, EventArgs e){
            try
            {

                SelectLugarEvento();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtLugarEvento.ClientID + "'); }", true);
            }
        }


        // Eventos del PopUp de Lugar de Evento

        protected void btnNuevoLugarEvento_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                this.pnlPopUp_LugarEvento.Visible = true;

                // Detalle de acción
                this.lblPopUpTitle_LugarEvento.Text = "Nuevo Lugar de Evento";
                this.btnPopUpCommand_LugarEvento.Text = "Crear Lugar de Evento";

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpNombre_LugarEvento.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtLugarEvento.ClientID + "');", true);
            }
        }

        protected void btnPopUpCommand_LugarEvento_Click(object sender, EventArgs e){
            try
            {

                InsertLugarEvento();

            }catch (Exception ex){
                this.lblPopUpMessage_LugarEvento.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpNombre_LugarEvento.ClientID + "'); }", true);
            }
        }

        protected void ddlPopUpEstado_LugarEvento_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Actualizar municipios
                SelectMunicipio_PopUp_LugarEvento();

                // Limpiado de controles
                this.txtPopUpColonia_LugarEvento.Text = "";
                this.hddPopUpColoniaId_LugarEvento.Value = "";

                // Inhabilitar filtro de colonia
                this.txtPopUpColonia_LugarEvento.Enabled = false;
                this.txtPopUpColonia_LugarEvento.CssClass = "Textbox_General_Disabled";

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlPopUpMunicipio_LugarEvento.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPopUpEstado_LugarEvento.ClientID + "'); }", true);
            }
        }

        protected void ddlPopUpMunicipio_LugarEvento_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Limpiado de controles
                this.txtPopUpColonia_LugarEvento.Text = "";
                this.hddPopUpColoniaId_LugarEvento.Value = "";

                if( this.ddlPopUpMunicipio_LugarEvento.SelectedItem.Value == "0" ){

                    // Inhabilitar filtro de colonia
                    this.txtPopUpColonia_LugarEvento.Enabled = false;
                    this.txtPopUpColonia_LugarEvento.CssClass = "Textbox_General_Disabled";

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlPopUpMunicipio_LugarEvento.ClientID + "'); }", true);

                }else{

                    // Habilitar filtro de colonia
                    this.txtPopUpColonia_LugarEvento.Enabled = true;
                    this.txtPopUpColonia_LugarEvento.CssClass = "Textbox_General";

                    // Configurar el context key del autosuggest de colonia
                    autosuggestColonia_LugarEvento.ContextKey = this.ddlPopUpMunicipio_LugarEvento.SelectedItem.Value;

				    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpColonia_LugarEvento.ClientID + "'); }", true);
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUpColonia_LugarEvento.ClientID + "'); }", true);
            }
        }

        protected void imgCloseWindow_LugarEvento_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel_LugarEvento();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtLugarEvento.ClientID + "');", true);
            }
        }


    }
}