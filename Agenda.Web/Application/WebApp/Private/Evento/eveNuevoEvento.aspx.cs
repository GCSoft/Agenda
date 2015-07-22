/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invEvento
' Autor:	Ruben.Cobos
' Fecha:	09-Diciembre-2014
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
using Agenda.BusinessProcess.Page;
using Agenda.Entity.Object;
using System.Data;

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveNuevoEvento : BPPage
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
        GCParse gcParse = new GCParse();



        // Rutinas del programador

        void InsertEvento(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();
            
            String JSScript = "";
            String Key = "";

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;

                // Formulario

                #region TAB - Datos generales
                    oENTEvento.CategoriaId = 2; //Int32.Parse( this.ddlCategoria.SelectedItem.Value );
                    oENTEvento.ConductoId = 1; // Int32.Parse(this.ddlConducto.SelectedItem.Value);
                    oENTEvento.PrioridadId = 1; // Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                    oENTEvento.SecretarioId_Ramo = Int32.Parse(this.hddSecretarioRamoId.Value);
                    oENTEvento.SecretarioId_Responsable = Int32.Parse(this.hddResponsableId.Value);
                    oENTEvento.EventoObservaciones = this.ckeObservaciones.Text.Trim();
                #endregion

                #region TAB - Datos del evento
                    oENTEvento.EventoNombre = this.txtNombreEvento.Text.Trim();
                    oENTEvento.FechaInicio = this.wucCalendarInicio.DisplayUTCDate;
                    oENTEvento.FechaFin = this.wucCalendarFin.DisplayUTCDate;
                    oENTEvento.HoraEventoInicio = this.wucTimerDesde.DisplayUTCTime;
                    oENTEvento.HoraEventoFin = this.wucTimerHasta.DisplayUTCTime;
                    oENTEvento.LugarEventoId = Int32.Parse( this.hddLugarEventoId.Value );
                    oENTEvento.EventoDetalle = this.ckeDetalleEvento.Text.Trim();
                #endregion

                #region TAB - Contacto
                    oENTEvento.Contacto.Nombre = this.txtContactoNombre.Text.Trim();
                    oENTEvento.Contacto.Puesto = this.txtContactoPuesto.Text.Trim();
                    oENTEvento.Contacto.Organizacion = this.txtContactoOrganizacion.Text.Trim();
                    oENTEvento.Contacto.Telefono = this.txtContactoTelefono.Text.Trim();
                    oENTEvento.Contacto.Email = this.txtContactoEmail.Text.Trim();
                    oENTEvento.Contacto.Comentarios = this.ckeContactoComentarios.Text.Trim();
                #endregion

                oENTEvento.EstatusEventoId = 1;
                if( this.rblDependencia.SelectedValue == "1" ) { oENTEvento.Logistica = 1; }
                if( this.rblDependencia.SelectedValue == "2" ) { oENTEvento.Protocolo = 1; }

                // Transacción
                oENTResponse = oBPEvento.InsertEvento(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                LimpiaFormulario();

                // Llave encriptada
                Key = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoId"].ToString() + "|1";
                Key = gcEncryption.EncryptString(Key, true);

                // Mensaje a desplegar y script
                JSScript = "function pageLoad(){ if( confirm('Se registró el evento exitosamente. ¿Desea ir al detalle para continuar con la captura?') ) { window.location.href = 'eveDetalleEvento.aspx?key=" + Key + "'; } else { window.location.href = 'eveNuevoEvento.aspx'; } }";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), JSScript, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void LimpiaFormulario(){
            try
            {

                // TAB - Datos generales
                //this.ddlCategoria.SelectedIndex = 0;
                //this.ddlConducto.SelectedIndex = 0;
                //this.ddlPrioridad.SelectedIndex = 0;

                this.txtSecretarioRamo.Text = "";
                this.hddSecretarioRamoId.Value = "";

                this.txtResponsable.Text = "";
                this.hddResponsableId.Value = "";

                this.ckeObservaciones.Text = "";

                // TAB - Datos del evento
                this.txtNombreEvento.Text = "";
                this.wucCalendarInicio.SetDate(DateTime.Now);
                this.wucCalendarFin.SetDate(DateTime.Now);
                this.wucTimerDesde.DisplayTime = "10:00";
                this.wucTimerHasta.DisplayTime = "10:00";

                this.txtLugarEvento.Text = "";
                this.hddLugarEventoId.Value = "";

                this.txtMunicipio.Text = "";
                this.txtColonia.Text = "";
                this.txtCalle.Text = "";
                this.txtNumeroExterior.Text = "";
                this.txtNumeroInterior.Text = "";
                this.ckeDetalleEvento.Text = "";

                // TAB - Contacto
                this.txtContactoNombre.Text = "";
                this.txtContactoPuesto.Text = "";
                this.txtContactoOrganizacion.Text = "";
                this.txtContactoTelefono.Text = "";
                this.txtContactoEmail.Text = "";
                this.ckeContactoComentarios.Text = "";

                // Foco y pestaña
                this.tabEvento.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtSecretarioRamo.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        //void SelectCategoria(){
        //    ENTResponse oENTResponse = new ENTResponse();
        //    ENTCategoria oENTCategoria = new ENTCategoria();

        //    BPCategoria oBPCategoria = new BPCategoria();

        //    try
        //    {

        //        // Formulario
        //        oENTCategoria.CategoriaId = 0;
        //        oENTCategoria.Nombre = "";
        //        oENTCategoria.Activo = 1;

        //        // Transacción
        //        oENTResponse = oBPCategoria.SelectCategoria(oENTCategoria);

        //        // Validaciones
        //        if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
        //        if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

        //        // Llenado de combo
        //        this.ddlCategoria.DataTextField = "Nombre";
        //        this.ddlCategoria.DataValueField = "CategoriaId";
        //        this.ddlCategoria.DataSource = oENTResponse.DataSetResponse.Tables[1];
        //        this.ddlCategoria.DataBind();

        //        // Agregar Item de selección
        //        this.ddlCategoria.Items.Insert(0, new ListItem("[Seleccione]", "0"));

        //    }catch (Exception ex){
        //        throw (ex);
        //    }
        //}

        //void SelectConducto(){
        //    ENTResponse oENTResponse = new ENTResponse();
        //    ENTConducto oENTConducto = new ENTConducto();

        //    BPConducto oBPConducto = new BPConducto();

        //    try
        //    {

        //        // Formulario
        //        oENTConducto.ConductoId = 0;
        //        oENTConducto.Nombre = "";
        //        oENTConducto.Activo = 1;

        //        // Transacción
        //        oENTResponse = oBPConducto.SelectConducto(oENTConducto);

        //        // Validaciones
        //        if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
        //        if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

        //        // Llenado de combo
        //        this.ddlConducto.DataTextField = "Nombre";
        //        this.ddlConducto.DataValueField = "ConductoId";
        //        this.ddlConducto.DataSource = oENTResponse.DataSetResponse.Tables[1];
        //        this.ddlConducto.DataBind();

        //        // Agregar Item de selección
        //        this.ddlConducto.Items.Insert(0, new ListItem("[Seleccione]", "0"));

        //    }catch (Exception ex){
        //        throw (ex);
        //    }
        //}

        //void SelectPrioridad(){
        //    ENTResponse oENTResponse = new ENTResponse();
        //    ENTPrioridad oENTPrioridad = new ENTPrioridad();

        //    BPPrioridad oBPPrioridad = new BPPrioridad();

        //    try
        //    {

        //        // Formulario
        //        oENTPrioridad.PrioridadId = 0;
        //        oENTPrioridad.Nombre = "";
        //        oENTPrioridad.Activo = 1;

        //        // Transacción
        //        oENTResponse = oBPPrioridad.SelectPrioridad(oENTPrioridad);

        //        // Validaciones
        //        if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
        //        if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

        //        // Llenado de combo
        //        this.ddlPrioridad.DataTextField = "Nombre";
        //        this.ddlPrioridad.DataValueField = "PrioridadId";
        //        this.ddlPrioridad.DataSource = oENTResponse.DataSetResponse.Tables[1];
        //        this.ddlPrioridad.DataBind();

        //        // Agregar Item de selección
        //        this.ddlPrioridad.Items.Insert(0, new ListItem("[Seleccione]", "0"));

        //    }catch (Exception ex){
        //        throw (ex);
        //    }
        //}

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
                this.ckeDetalleEvento.Focus();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidarFormulario(){
            String ErrorDetailHour = "";

            try
            {

                // TAB - Datos generales
                //if ( this.ddlCategoria.SelectedIndex == 0 ) {
                //    this.tabEvento.ActiveTabIndex = 0;
                //    throw (new Exception("Es necesario seleccionar un Tipo de cita"));
                //}

                //if ( this.ddlConducto.SelectedIndex == 0 ) {
                //    this.tabEvento.ActiveTabIndex = 0;
                //    throw (new Exception("Es necesario seleccionar un Conducto"));
                //}
                
                //if ( this.ddlPrioridad.SelectedIndex == 0 ) {
                //    this.tabEvento.ActiveTabIndex = 0;
                //    throw (new Exception("Es necesario seleccionar una Prioridad"));
                //}

                if( this.hddSecretarioRamoId.Value.Trim() == "" || this.hddSecretarioRamoId.Value.Trim() == "0" ){
                    this.tabEvento.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Secretario Ramo"));
                }

                if( this.hddResponsableId.Value.Trim() == "" || this.hddResponsableId.Value.Trim() == "0" ){
                    this.tabEvento.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Responsable"));
                }

                // TAB - Datos del evento
                if( this.txtNombreEvento.Text.Trim() == "" ){
                    this.tabEvento.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario ingresar un Nombre de evento"));
                }

                if ( !this.wucCalendarInicio.IsValidDate() ) {
                    this.tabEvento.ActiveTabIndex = 1;
                    throw new Exception("El campo [Fecha de Inicio del evento] es requerido");
                }

                if ( !this.wucCalendarFin.IsValidDate() ) {
                    this.tabEvento.ActiveTabIndex = 1;
                    throw new Exception("El campo [Fecha Final del evento] es requerido");
                }

                if ( !this.wucTimerDesde.IsValidTime(ref ErrorDetailHour) ) {
                    this.tabEvento.ActiveTabIndex = 1;
                    throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour);
                }

                if ( !this.wucTimerHasta.IsValidTime(ref ErrorDetailHour) ) {
                    this.tabEvento.ActiveTabIndex = 1;
                    throw new Exception("El campo [Hora de finalización del evento] es requerido: " + ErrorDetailHour);
                }

                if( this.hddLugarEventoId.Value.Trim() == "" || this.hddLugarEventoId.Value.Trim() == "0" ){
                    this.tabEvento.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario seleccionar un Lugar del Evento"));
                }

                //// TAB - Contacto
                //if( this.txtContactoNombre.Text.Trim() == "" ){
                //    this.tabEvento.ActiveTabIndex = 2;
                //    throw (new Exception("Es necesario ingresar un Nombre de contacto"));
                //}

                //if( this.txtContactoTelefono.Text.Trim() == "" ){
                //    this.tabEvento.ActiveTabIndex = 2;
                //    throw (new Exception("Es necesario ingresar un Teléfono del contacto"));
                //}

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
                //if (this.txtPopUpNumeroExterior_LugarEvento.Text.Trim() == "") { throw new Exception("* El campo [Número Exterior] es requerido"); }

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
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                //SelectCategoria();
                //SelectConducto();
                //SelectPrioridad();

                // Estado inicial
                this.wucCalendarInicio.Width = 176;
                this.wucCalendarFin.Width = 176;

                SelectEstado_PopUp_LugarEvento();
                SelectMunicipio_PopUp_LugarEvento();
                ClearPopUpPanel_LugarEvento();
                
                // Dependencia
                oENTSession = (ENTSession)this.Session["oENTSession"];
                if (oENTSession.RolId == 4) { this.rblDependencia.SelectedValue = "1"; this.rblDependencia.Enabled = false; } // Logística
                if (oENTSession.RolId == 5) { this.rblDependencia.SelectedValue = "2"; this.rblDependencia.Enabled = false; } // Dirección de Protocolo

                // Foco
                this.tabEvento.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtSecretarioRamo.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }       

        protected void btnCancelar_Click(object sender, EventArgs e){
            try
            {

                LimpiaFormulario();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e){
            try
            {

                // Validar el formulario
                ValidarFormulario();

                // Transacción
                InsertEvento();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }



        // Eventos de panel - Datos del evento

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