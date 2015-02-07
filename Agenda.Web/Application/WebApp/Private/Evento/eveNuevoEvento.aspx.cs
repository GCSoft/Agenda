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
					Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowLugarEvento["Nombre"].ToString(), rowLugarEvento["LugarEventoId"].ToString());
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
                    oENTEvento.CategoriaId = Int32.Parse( this.ddlCategoria.SelectedItem.Value );
                    oENTEvento.ConductoId = Int32.Parse(this.ddlConducto.SelectedItem.Value);
                    oENTEvento.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                    oENTEvento.SecretarioId_Ramo = Int32.Parse(this.hddSecretarioRamoId.Value);
                    oENTEvento.SecretarioId_Responsable = Int32.Parse(this.hddResponsableId.Value);
                    oENTEvento.EventoObservaciones = this.ckeObservaciones.Text.Trim();
                #endregion

                #region TAB - Datos del evento
                    oENTEvento.EventoNombre = this.txtNombreEvento.Text.Trim();
                    oENTEvento.FechaEvento = this.wucCalendar.DisplayUTCDate;
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
                if( oENTSession.RolId == 4 ) { oENTEvento.Logistica = 1; }
                if( oENTSession.RolId == 5 ) { oENTEvento.Protocolo = 1; }

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
                JSScript = "function pageLoad(){ if( confirm('Se registró el evento exitosamente. ¿Desea ir al detalle para continuar con la captura?') ) { window.location.href('eveDetalleEvento.aspx?key=" + Key + "'); } else { window.location.href('eveNuevoEvento.aspx'); } }";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), JSScript, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void LimpiaFormulario(){
            try
            {

                // TAB - Datos generales
                this.ddlCategoria.SelectedIndex = 0;
                this.ddlConducto.SelectedIndex = 0;
                this.ddlPrioridad.SelectedIndex = 0;

                this.txtSecretarioRamo.Text = "";
                this.hddSecretarioRamoId.Value = "";

                this.txtResponsable.Text = "";
                this.hddResponsableId.Value = "";

                this.ckeObservaciones.Text = "";

                // TAB - Datos del evento
                this.txtNombreEvento.Text = "";
                this.wucCalendar.SetDate(DateTime.Now);
                this.wucTimerDesde.DisplayTime = "10:00 a.m.";

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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

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

        void ValidarFormulario(){
            String ErrorDetailHour = "";

            try
            {

                // TAB - Datos generales
                if ( this.ddlCategoria.SelectedIndex == 0 ) {
                    this.tabEvento.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Tipo de cita"));
                }

                if ( this.ddlConducto.SelectedIndex == 0 ) {
                    this.tabEvento.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Conducto"));
                }
                
                if ( this.ddlPrioridad.SelectedIndex == 0 ) {
                    this.tabEvento.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar una Prioridad"));
                }

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

                if ( !this.wucCalendar.IsValidDate() ) {
                    this.tabEvento.ActiveTabIndex = 1;
                    throw new Exception("El campo [Fecha del evento] es requerido");
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

                // TAB - Contacto
                if( this.txtContactoNombre.Text.Trim() == "" ){
                    this.tabEvento.ActiveTabIndex = 2;
                    throw (new Exception("Es necesario ingresar un Nombre de contacto"));
                }

                if( this.txtContactoTelefono.Text.Trim() == "" ){
                    this.tabEvento.ActiveTabIndex = 2;
                    throw (new Exception("Es necesario ingresar un Teléfono del contacto"));
                }

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

                // Llenado de controles
                SelectCategoria();
                SelectConducto();
                SelectPrioridad();

                // Estado inicial
                this.wucCalendar.Width = 176;

                // Foco
                this.tabEvento.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

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
        

    }
}