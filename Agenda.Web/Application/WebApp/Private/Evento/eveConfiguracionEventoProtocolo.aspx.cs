/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveConfiguracionEvento
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
    public partial class eveConfiguracionEventoProtocolo : System.Web.UI.Page
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();
        GCParse gcParse = new GCParse();


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

        Boolean ValidaFormulario(){
            DataTable tblTemporal;

            Int32 AccordionSelectedIndex;
            Int32 ValidateNumber;

            Boolean Response = false;
            String CurrentClientID = this.ddlTipoVestimenta.ClientID;

            try
            {

                #region "Sección: Datos Generales"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion1.SelectedIndex;
                    this.Accordion1.SelectedIndex = 0;
                
                    // Validaciones
                    CurrentClientID = this.ddlTipoVestimenta.ClientID;
                    if (this.ddlTipoVestimenta.SelectedItem.Value == "6" && this.txtTipoVestimentaOtro.Text.Trim() == "") { throw (new Exception("Es necesario ïngresar el tipo de vestimenta")); }

                    CurrentClientID = this.txtAforo.ClientID;
                    if ( Int32.TryParse(this.txtAforo.Text, out ValidateNumber) == false) { throw (new Exception("La cantidad en Aforo debe de ser numérica")); }
                    
                    // Estado original del acordeón
                    this.Accordion1.SelectedIndex = AccordionSelectedIndex;
                    
                #endregion

                #region "Acomodo"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion2.SelectedIndex;
                    this.Accordion2.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtAcomodoNombre.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el acomodo al evento")); }

                    // Estado original del acordeón
                    this.Accordion4.SelectedIndex = AccordionSelectedIndex;

                #endregion

                #region "Orden del día"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion3.SelectedIndex;
                    this.Accordion3.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtOrdenDiaDetalle.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar la Orden del Día")); }

                    // Estado original del acordeón
                    this.Accordion3.SelectedIndex = AccordionSelectedIndex;

                #endregion

                #region "Sección: Requerimentos Técnicos y Montaje"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion4.SelectedIndex;
                    this.Accordion4.SelectedIndex = 0;
                
                    // Validaciones
                    CurrentClientID = this.txtProtocoloBandera.ClientID;
                    if (this.txtProtocoloBandera.Text.Trim() == "") { throw (new Exception("El campo [Banderas] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloLeyenda.ClientID;
                    if (this.txtProtocoloLeyenda.Text.Trim() == "") { throw (new Exception("El campo [Leyenda] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloSonido.ClientID;
                    if (this.txtProtocoloSonido.Text.Trim() == "") { throw (new Exception("El campo [Sonido] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloDesayuno.ClientID;
                    if (this.txtProtocoloDesayuno.Text.Trim() == "") { throw (new Exception("El campo [Desayuno] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloSillas.ClientID;
                    if (this.txtProtocoloSillas.Text.Trim() == "") { throw (new Exception("El campo [Sillas] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloMesas.ClientID;
                    if (this.txtProtocoloMesas.Text.Trim() == "") { throw (new Exception("El campo [Mesas] es obligatorio")); }

                    CurrentClientID = this.txtProtocoloPresentacion.ClientID;
                    if (this.txtProtocoloPresentacion.Text.Trim() == "") { throw (new Exception("El campo [Presentación] es obligatorio")); }
                    
                    // Estado original del acordeón
                    this.Accordion1.SelectedIndex = AccordionSelectedIndex;
                    
                #endregion

                // Validacion exitosa
                Response = true;

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + CurrentClientID + "'); }", true);
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
                this.lblEventoFecha.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaLarga"].ToString();
                this.lblEventoHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHorario"].ToString();

                // Comentario en cuadernillo
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "0" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "0" ){ this.rblNotaDocumento.SelectedIndex = 0; }
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "1" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "0" ){ this.rblNotaDocumento.SelectedIndex = 1; }
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "0" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "1" ){ this.rblNotaDocumento.SelectedIndex = 2; }
                this.txtNotaDocumento.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString();

                // Sección: Nombre del evento
                if ( oENTResponse.DataSetResponse.Tables[6].Rows.Count > 0 ){

                    this.ddlTipoVestimenta.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString();
                    this.ddlMedioComunicacion.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionId"].ToString();
                    this.txtTipoVestimentaOtro.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaOtro"].ToString();
                    this.txtAforo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString();
                    this.txtProtocoloInvitacionA.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloInvitacionA"].ToString();
                    this.txtProtocoloResponsableEvento.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsableEvento"].ToString();
                    this.txtProtocoloBandera.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloBandera"].ToString();
                    this.txtProtocoloLeyenda.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloLeyenda"].ToString();
                    this.txtProtocoloResponsable.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsable"].ToString();
                    this.txtProtocoloSonido.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloSonido"].ToString();
                    this.txtProtocoloResponsableSonido.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsableSonido"].ToString();
                    this.txtProtocoloDesayuno.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloDesayuno"].ToString();
                    this.txtProtocoloSillas.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloSillas"].ToString();
                    this.txtProtocoloMesas.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloMesas"].ToString();
                    this.txtProtocoloPresentacion.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloPresentacion"].ToString();
                }

                // Sección: Acomodo
                this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[10];
                this.gvAcomodo.DataBind();

                // Sección: Orden del día
                this.gvOrdenDia.DataSource = oENTResponse.DataSetResponse.Tables[9];
                this.gvOrdenDia.DataBind();

                // Sección: Asistentes (Se almacena en la tabla de Comité de Recepción)
                this.gvComiteRecepcion.DataSource = oENTResponse.DataSetResponse.Tables[8];
                this.gvComiteRecepcion.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMedioComunicacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMedioComunicacion oENTMedioComunicacion = new ENTMedioComunicacion();

            BPMedioComunicacion oBPMedioComunicacion = new BPMedioComunicacion();

            try
            {

                // Formulario
                oENTMedioComunicacion.MedioComunicacionId = 0;
                oENTMedioComunicacion.Nombre = "";
                oENTMedioComunicacion.Activo = 1;

                // Transacción
                oENTResponse = oBPMedioComunicacion.SelectMedioComunicacion(oENTMedioComunicacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlMedioComunicacion.DataTextField = "Nombre";
                this.ddlMedioComunicacion.DataValueField = "MedioComunicacionId";
                this.ddlMedioComunicacion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMedioComunicacion.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoAcomodo(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.TipoAcomodoId = 0;
                oENTTipoAcomodo.Nombre = "";
                oENTTipoAcomodo.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoAcomodo.SelectTipoAcomodo(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoAcomodo.DataTextField = "Nombre";
                this.ddlTipoAcomodo.DataValueField = "TipoAcomodoId";
                this.ddlTipoAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoAcomodo.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoVestimenta(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoVestimenta oENTTipoVestimenta = new ENTTipoVestimenta();

            BPTipoVestimenta oBPTipoVestimenta = new BPTipoVestimenta();

            try
            {

                // Formulario
                oENTTipoVestimenta.TipoVestimentaId = 0;
                oENTTipoVestimenta.Nombre = "";
                oENTTipoVestimenta.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoVestimenta.SelectTipoVestimenta(oENTTipoVestimenta);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoVestimenta.DataTextField = "Nombre";
                this.ddlTipoVestimenta.DataValueField = "TipoVestimentaId";
                this.ddlTipoVestimenta.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoVestimenta.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateEvento_Configuracion(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            DataTable tblTemporal;
            DataRow rowTemporal;

            try
            {

                // Evento
                oENTEvento.EventoId = Int32.Parse( this.hddEventoId.Value );

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;

                #region "Sección: Datos Generales"

                    // Formulario
                    oENTEvento.TipoVestimentaId = Int32.Parse(this.ddlTipoVestimenta.SelectedItem.Value);
                    oENTEvento.MedioComunicacionId = Int32.Parse(this.ddlMedioComunicacion.SelectedItem.Value);
                    oENTEvento.TipoVestimentaOtro = this.txtTipoVestimentaOtro.Text.Trim();
                    oENTEvento.PronosticoClima = "";
                    oENTEvento.TemperaturaMinima = "";
                    oENTEvento.TemperaturaMaxima = "";
                    oENTEvento.Aforo = Int32.Parse(this.txtAforo.Text.Trim());
                    oENTEvento.TipoMontaje = "";
                    oENTEvento.LugarArribo = "";
                    oENTEvento.Esposa = 0;
                    oENTEvento.EsposaSi = 0;
                    oENTEvento.EsposaNo = 0;
                    oENTEvento.EsposaConfirma = 0;
                    oENTEvento.AccionRealizar = "";
                    oENTEvento.CaracteristicasInvitados = "";
                    oENTEvento.Menu = "";
                    oENTEvento.ProtocoloInvitacionA = this.txtProtocoloInvitacionA.Text.Trim();
                    oENTEvento.ProtocoloResponsableEvento = this.txtProtocoloResponsableEvento.Text.Trim();
                    
                #endregion

                #region "Sección: Acomodo"
                    
                    oENTEvento.TipoAcomodoId = Int32.Parse(this.ddlTipoAcomodo.SelectedItem.Value);

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    oENTEvento.DataTableAcomodo = new DataTable("DataTableAcomodo");
                    oENTEvento.DataTableAcomodo.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableAcomodo.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableAcomodo.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableAcomodo.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        oENTEvento.DataTableAcomodo.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Orden del día"
                    
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                    oENTEvento.DataTableOrdenDia = new DataTable("DataTableOrdenDia");
                    oENTEvento.DataTableOrdenDia.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableOrdenDia.Columns.Add("Detalle", typeof(String));
                    oENTEvento.DataTableOrdenDia.Columns.Add("Columna3", typeof(String)); // El DataType es de 3 columnas
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableOrdenDia.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Detalle"] = rowComiteRecepcion["Detalle"];
                        rowTemporal["Columna3"] = "";
                        oENTEvento.DataTableOrdenDia.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Requerimentos Técnicos y Montaje"

                    // Formulario
                    oENTEvento.ProtocoloBandera = this.txtProtocoloBandera.Text.Trim();
                    oENTEvento.ProtocoloLeyenda = this.txtProtocoloLeyenda.Text.Trim();
                    oENTEvento.ProtocoloResponsable = this.txtProtocoloResponsable.Text.Trim();
                    oENTEvento.ProtocoloSonido = this.txtProtocoloSonido.Text.Trim();
                    oENTEvento.ProtocoloResponsableSonido = this.txtProtocoloResponsableSonido.Text.Trim();
                    oENTEvento.ProtocoloDesayuno = this.txtProtocoloDesayuno.Text.Trim();
                    oENTEvento.ProtocoloSillas = this.txtProtocoloSillas.Text.Trim();
                    oENTEvento.ProtocoloMesas = this.txtProtocoloMesas.Text.Trim();
                    oENTEvento.ProtocoloPresentacion = this.txtProtocoloPresentacion.Text.Trim();
                    
                #endregion

                #region "Sección: Asistentes ( Comité de recepción ) "
                    
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);
                    
                    oENTEvento.DataTableComiteRecepcion = new DataTable("DataTableComiteRecepcion");
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableComiteRecepcion.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        oENTEvento.DataTableComiteRecepcion.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Nota en Documento"

                    oENTEvento.NotaInicioDocumento = Int16.Parse((rblNotaDocumento.SelectedIndex == 0 ? 0 : (rblNotaDocumento.SelectedIndex == 1 ? 1 : 0)).ToString());
                    oENTEvento.NotaFinDocumento = Int16.Parse((rblNotaDocumento.SelectedIndex == 0 ? 0 : (rblNotaDocumento.SelectedIndex == 1 ? 0 : 1)).ToString());
                    oENTEvento.NotaDocumento = this.txtNotaDocumento.Text.Trim();

                #endregion


                // Transacción
                oENTResponse = oBPEvento.UpdateEvento_Configuracion(oENTEvento);

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
                SelectTipoVestimenta();
                SelectMedioComunicacion();
                SelectTipoAcomodo();
                
				// Carátula y formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtProtocoloInvitacionA.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtProtocoloInvitacionA.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Validar formulario
                if (!ValidaFormulario()) { return; }

                // Actualizar los datos generales
                UpdateEvento_Configuracion();

				// Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtProtocoloInvitacionA.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtProtocoloInvitacionA.ClientID + "'); }", true);
            }
		}

        protected void ddlTipoVestimenta_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                switch( this.ddlTipoVestimenta.SelectedItem.Value ){
                    case "6":   // Otro
                        
                        this.txtTipoVestimentaOtro.Enabled = true;
                        this.txtTipoVestimentaOtro.CssClass = "Textbox_General";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtTipoVestimentaOtro.ClientID + "'); }", true);
                        break;

                    default:

                        this.txtTipoVestimentaOtro.Text = "";
                        this.txtTipoVestimentaOtro.Enabled = false;
                        this.txtTipoVestimentaOtro.CssClass = "Textbox_Disabled";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlMedioComunicacion.ClientID + "'); }", true);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtProtocoloInvitacionA.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Acomodo

        protected void btnAgregarAcomodo_Click(object sender, EventArgs e){
            DataTable tblAcomodo;
            DataRow rowAcomodo;

            try
            {

                // Obtener DataTable del grid
                tblAcomodo = gcParse.GridViewToDataTable(this.gvAcomodo, false);

                // Validaciones
                if ( this.txtAcomodoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                if ( this.txtAcomodoPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowAcomodo = tblAcomodo.NewRow();
                rowAcomodo["Orden"] = (tblAcomodo.Rows.Count + 1).ToString();
                rowAcomodo["Nombre"] = this.txtAcomodoNombre.Text.Trim();
                rowAcomodo["Puesto"] = this.txtAcomodoPuesto.Text.Trim();
                tblAcomodo.Rows.Add(rowAcomodo);

                // Actualizar Grid
                this.gvAcomodo.DataSource = tblAcomodo;
                this.gvAcomodo.DataBind();

                // Nueva captura
                this.txtAcomodoNombre.Text = "";
                this.txtAcomodoPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvAcomodo_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblAcomodo;

            String strCommand = "";
            String Orden = "";
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
                Orden = this.gvAcomodo.DataKeys[intRow]["Orden"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblAcomodo = gcParse.GridViewToDataTable(this.gvAcomodo, true);

                        // Remover el elemento
                        tblAcomodo.Rows.Remove( tblAcomodo.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowAcomodo in tblAcomodo.Rows ){

                            tblAcomodo.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvAcomodo.DataSource = tblAcomodo;
                        this.gvAcomodo.DataBind();

                        // Nueva captura
                        this.txtAcomodoNombre.Text = "";
                        this.txtAcomodoPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvAcomodo_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String Orden = "";
            String AcomodoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                AcomodoNombre = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + AcomodoNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvAcomodo_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvAcomodo, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Orden del día

        protected void btnAgregarOrdenDia_Click(object sender, EventArgs e){
            DataTable tblOrdenDia;
            DataRow rowOrdenDia;

            try
            {

                // Obtener DataTable del grid
                tblOrdenDia = gcParse.GridViewToDataTable(this.gvOrdenDia, false);

                // Validaciones
                if (this.txtOrdenDiaDetalle.Text.Trim() == "") { throw (new Exception("Es necesario ingresar el detalle de la orden del día")); }

                // Agregar un nuevo elemento
                rowOrdenDia = tblOrdenDia.NewRow();
                rowOrdenDia["Orden"] = (tblOrdenDia.Rows.Count + 1).ToString();
                rowOrdenDia["Detalle"] = this.txtOrdenDiaDetalle.Text.Trim();
                tblOrdenDia.Rows.Add(rowOrdenDia);

                // Actualizar Grid
                this.gvOrdenDia.DataSource = tblOrdenDia;
                this.gvOrdenDia.DataBind();

                // Nueva captura
                this.txtOrdenDiaDetalle.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }

        protected void gvOrdenDia_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblOrdenDia;

            String strCommand = "";
            String Orden = "";
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
                Orden = this.gvOrdenDia.DataKeys[intRow]["Orden"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblOrdenDia = gcParse.GridViewToDataTable(this.gvOrdenDia, true);

                        // Remover el elemento
                        tblOrdenDia.Rows.Remove( tblOrdenDia.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowOrdenDia in tblOrdenDia.Rows ){

                            tblOrdenDia.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvOrdenDia.DataSource = tblOrdenDia;
                        this.gvOrdenDia.DataBind();

                        // Nueva captura
                        this.txtOrdenDiaDetalle.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }

        protected void gvOrdenDia_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String Orden = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvOrdenDia.DataKeys[e.Row.RowIndex]["Orden"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar la posición [" + Orden + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvOrdenDia_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvOrdenDia, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }


        // Eventos de Sección: Comité de recepción

        protected void btnAgregarComiteRecepcion_Click(object sender, EventArgs e){
            DataTable tblComiteRecepcion;
            DataRow rowComiteRecepcion;

            try
            {

                // Obtener DataTable del grid
                tblComiteRecepcion = gcParse.GridViewToDataTable(this.gvComiteRecepcion, false);

                // Validaciones
                if ( this.txtComiteRecepcionNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                if ( this.txtComiteRecepcionPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowComiteRecepcion = tblComiteRecepcion.NewRow();
                rowComiteRecepcion["Orden"] = (tblComiteRecepcion.Rows.Count + 1).ToString();
                rowComiteRecepcion["Nombre"] = this.txtComiteRecepcionNombre.Text.Trim();
                rowComiteRecepcion["Puesto"] = this.txtComiteRecepcionPuesto.Text.Trim();
                tblComiteRecepcion.Rows.Add(rowComiteRecepcion);

                // Actualizar Grid
                this.gvComiteRecepcion.DataSource = tblComiteRecepcion;
                this.gvComiteRecepcion.DataBind();

                // Nueva captura
                this.txtComiteRecepcionNombre.Text = "";
                this.txtComiteRecepcionPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteRecepcion_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblComiteRecepcion;

            String strCommand = "";
            String Orden = "";
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
                Orden = this.gvComiteRecepcion.DataKeys[intRow]["Orden"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblComiteRecepcion = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);

                        // Remover el elemento
                        tblComiteRecepcion.Rows.Remove( tblComiteRecepcion.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowComiteRecepcion in tblComiteRecepcion.Rows ){

                            tblComiteRecepcion.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvComiteRecepcion.DataSource = tblComiteRecepcion;
                        this.gvComiteRecepcion.DataBind();

                        // Nueva captura
                        this.txtComiteRecepcionNombre.Text = "";
                        this.txtComiteRecepcionPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteRecepcion_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String Orden = "";
            String ComiteRecepcionNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvComiteRecepcion.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ComiteRecepcionNombre = this.gvComiteRecepcion.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ComiteRecepcionNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvComiteRecepcion_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvComiteRecepcion, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }


    }
}