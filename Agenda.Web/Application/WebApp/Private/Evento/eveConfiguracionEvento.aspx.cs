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
    public partial class eveConfiguracionEvento : System.Web.UI.Page
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
            DataRow rowTemporal;

            Int32 AccordionSelectedIndex;
            Int32 ValidateNumber;

            Boolean Response = false;
            String CurrentClientID = this.ddlTipoVestimenta.ClientID;

            try
            {

                #region "Sección: Nombre del evento"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion1.SelectedIndex;
                    this.Accordion1.SelectedIndex = 0;

                    // Medios de traslado seleccionados
                    tblTemporal = new DataTable("DataTableMedioTraslado");
                    tblTemporal.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
					    if(this.chklMedioTraslado.Items[k].Selected){
                            rowTemporal = tblTemporal.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklMedioTraslado.Items[k].Value;
                            tblTemporal.Rows.Add(rowTemporal);
					    }
				    }
                
                    // Validaciones
                    CurrentClientID = this.ddlTipoVestimenta.ClientID;
                    if (this.ddlTipoVestimenta.SelectedItem.Value == "6" && this.txtTipoVestimentaOtro.Text.Trim() == "") { throw (new Exception("Es necesario ïngresar el tipo de vestimenta")); }

                    CurrentClientID = this.chklMedioTraslado.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario seleccionar un medio de traslado")); }

                    CurrentClientID = this.txtAforo.ClientID;
                    if ( Int32.TryParse(this.txtAforo.Text, out ValidateNumber) == false) { throw (new Exception("La cantidad en Aforo debe de ser numérica")); }

                    CurrentClientID = this.txtTipoMontaje.ClientID;
                    if (this.txtTipoMontaje.Text.Trim() == "") { throw (new Exception("Es necesario determinar el tipo de montaje")); }

                    CurrentClientID = this.txtAccionRealizar.ClientID;
                    if (this.txtAccionRealizar.Text.Trim() == "") { throw (new Exception("Es necesario determinar la acción a realizar")); }
                    
                    // Estado original del acordeón
                    this.Accordion1.SelectedIndex = AccordionSelectedIndex;
                    
                #endregion

                #region "Comité de recepción"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion2.SelectedIndex;
                    this.Accordion2.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtComiteRecepcionNombre.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el Comité de Recepción")); }

                    // Estado original del acordeón
                    this.Accordion2.SelectedIndex = AccordionSelectedIndex;

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

                #region "Acomodo"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion4.SelectedIndex;
                    this.Accordion4.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtAcomodoNombre.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el acomodo al evento")); }

                    // Estado original del acordeón
                    this.Accordion4.SelectedIndex = AccordionSelectedIndex;

                #endregion

                #region "Responsable del evento"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion5.SelectedIndex;
                    this.Accordion5.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtResponsableEventoNombre.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario ingresar por lo menos un responsable del evento")); }

                    // Estado original del acordeón
                    this.Accordion5.SelectedIndex = AccordionSelectedIndex;

                #endregion

                #region "Responsable de logística"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.Accordion6.SelectedIndex;
                    this.Accordion6.SelectedIndex = 0;

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);
                    
                    // Validaciones
                    CurrentClientID = this.txtResponsableLogisticaNombre.ClientID;
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario ingresar por lo menos un responsable de logística")); }

                    // Estado original del acordeón
                    this.Accordion6.SelectedIndex = AccordionSelectedIndex;

                #endregion

                // Validacion exitosa
                Response = true;

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + CurrentClientID + "'); }", true);
            }

            return Response;

        }


        // Rutinas el programador

        void SelectPropuestaAcomodo(){
            try
            {

                // Agregar Item de selección
                this.ddlPropuestaAcomodo.Items.Insert(0, new ListItem("No", "0"));
                this.ddlPropuestaAcomodo.Items.Insert(0, new ListItem("Si", "1"));

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

        void SelectMedioTraslado(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMedioTraslado oENTMedioTraslado = new ENTMedioTraslado();

            BPMedioTraslado oBPMedioTraslado = new BPMedioTraslado();

            try
            {

                // Formulario
                oENTMedioTraslado.MedioTrasladoId = 0;
                oENTMedioTraslado.Nombre = "";
                oENTMedioTraslado.Activo = 1;

                // Transacción
                oENTResponse = oBPMedioTraslado.SelectMedioTraslado(oENTMedioTraslado);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.chklMedioTraslado.DataTextField = "Nombre";
                this.chklMedioTraslado.DataValueField = "MedioTrasladoId";
                this.chklMedioTraslado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.chklMedioTraslado.DataBind();

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

                // Sección: Nombre del evento
                if ( oENTResponse.DataSetResponse.Tables[6].Rows.Count > 0 ){

                    this.ddlTipoVestimenta.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString();
                    this.ddlMedioComunicacion.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionId"].ToString();

                    for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
					    if( oENTResponse.DataSetResponse.Tables[7].Select("MedioTrasladoId=" + this.chklMedioTraslado.Items[k].Value).Length > 0 ){
                            this.chklMedioTraslado.Items[k].Selected = true;
					    }
				    }

                    this.txtPronostico.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["PronosticoClima"].ToString();
                    this.txtTemperaturaMinima.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMinima"].ToString();
                    this.txtTemperaturaMaxima.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMaxima"].ToString();
                    this.txtLugarArribo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["LugarArribo"].ToString();
                    this.txtAforo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString();
                    this.txtTipoMontaje.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString();

                    if ( oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString() == "1" ){

                        this.chkEsposaInvitada.Checked = true;
                        this.rblConfirmacionEsposa.Enabled = true;

                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaSi"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "1"; }
                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaNo"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "2"; }
                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaConfirma"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "3"; }
                    
                    }else{

                        this.chkEsposaInvitada.Checked = false;
                        this.rblConfirmacionEsposa.Enabled = false;
                        this.rblConfirmacionEsposa.ClearSelection();
                    }

                    this.txtAccionRealizar.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["AccionRealizar"].ToString();
                    this.txtCaracteristicasInvitados.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["CaracteristicasInvitados"].ToString();
                    this.txtMenu.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Menu"].ToString();


                    this.ddlTipoAcomodo.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoId"].ToString();
                    this.ddlPropuestaAcomodo.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["PropuestaAcomodo"].ToString();
                    this.txtAcomodoObservaciones.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["AcomodoObservaciones"].ToString();

                }

                // Sección: Comité de recepción
                this.gvComiteRecepcion.DataSource = oENTResponse.DataSetResponse.Tables[8];
                this.gvComiteRecepcion.DataBind();

                // Sección: Orden del día
                this.gvOrdenDia.DataSource = oENTResponse.DataSetResponse.Tables[9];
                this.gvOrdenDia.DataBind();

                // Sección: Acomodo
                this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[10];
                this.gvAcomodo.DataBind();

                // Sección: Responsable del evento
                this.gvResponsableEvento.DataSource = oENTResponse.DataSetResponse.Tables[11];
                this.gvResponsableEvento.DataBind();

                // Sección: Responsable de logística
                this.gvResponsableLogistica.DataSource = oENTResponse.DataSetResponse.Tables[12];
                this.gvResponsableLogistica.DataBind();

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

                #region "Sección: Nombre del evento"

                    // Formulario
                    oENTEvento.TipoVestimentaId = Int32.Parse(this.ddlTipoVestimenta.SelectedItem.Value);
                    oENTEvento.MedioComunicacionId = Int32.Parse(this.ddlMedioComunicacion.SelectedItem.Value);
                    oENTEvento.TipoVestimentaOtro = this.txtTipoVestimentaOtro.Text.Trim();
                    oENTEvento.PronosticoClima = this.txtPronostico.Text.Trim();
                    oENTEvento.TemperaturaMinima = this.txtTemperaturaMinima.Text.Trim();
                    oENTEvento.TemperaturaMaxima = this.txtTemperaturaMaxima.Text.Trim();
                    oENTEvento.Aforo = Int32.Parse(this.txtAforo.Text.Trim());
                    oENTEvento.TipoMontaje = this.txtTipoMontaje.Text.Trim();
                    oENTEvento.LugarArribo = this.txtLugarArribo.Text.Trim();
                    oENTEvento.Esposa = Int16.Parse( ( this.chkEsposaInvitada.Checked ? 1 : 0 ).ToString() );
                    oENTEvento.EsposaSi = Int16.Parse( ( this.rblConfirmacionEsposa.SelectedValue == "1" ? 1 : 0 ).ToString() );
                    oENTEvento.EsposaNo = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "2" ? 1 : 0).ToString());
                    oENTEvento.EsposaConfirma = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "3" ? 1 : 0).ToString());
                    oENTEvento.AccionRealizar = this.txtAccionRealizar.Text.Trim();
                    oENTEvento.CaracteristicasInvitados = this.txtCaracteristicasInvitados.Text.Trim();
                    oENTEvento.Menu = this.txtMenu.Text.Trim();

                    // Medios de traslado seleccionados
                    oENTEvento.DataTableMedioTraslado = new DataTable("DataTableMedioTraslado");
                    oENTEvento.DataTableMedioTraslado.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
					    if(this.chklMedioTraslado.Items[k].Selected){
                            rowTemporal = oENTEvento.DataTableMedioTraslado.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklMedioTraslado.Items[k].Value;
                            oENTEvento.DataTableMedioTraslado.Rows.Add(rowTemporal);
					    }
				    }

                #endregion

                #region "Sección: Comité de recepción"
                    
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

                #region "Sección: Acomodo"
                    
                    oENTEvento.TipoAcomodoId = Int32.Parse(this.ddlTipoAcomodo.SelectedItem.Value);
                    oENTEvento.PropuestaAcomodo = Int16.Parse(this.ddlPropuestaAcomodo.SelectedItem.Value);
                    oENTEvento.AcomodoObservaciones = this.txtAcomodoObservaciones.Text.Trim();

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

                #region "Sección: Responsable del evento"

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);
                    
                    oENTEvento.DataTableResponsable = new DataTable("DataTableResponsable");
                    oENTEvento.DataTableResponsable.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableResponsable.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableResponsable.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowResponsable in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableResponsable.NewRow();
                        rowTemporal["Orden"] = rowResponsable["Orden"];
                        rowTemporal["Nombre"] = rowResponsable["Nombre"];
                        rowTemporal["Puesto"] = rowResponsable["Puesto"];
                        oENTEvento.DataTableResponsable.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Responsable de logística"

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);
                    
                    oENTEvento.DataTableResponsableLogistica = new DataTable("DataTableResponsableLogistica");
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Contacto", typeof(String));
                    
                    foreach( DataRow rowResponsableLogistica in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableResponsableLogistica.NewRow();
                        rowTemporal["Orden"] = rowResponsableLogistica["Orden"];
                        rowTemporal["Nombre"] = rowResponsableLogistica["Nombre"];
                        rowTemporal["Contacto"] = rowResponsableLogistica["Contacto"];
                        oENTEvento.DataTableResponsableLogistica.Rows.Add(rowTemporal);
                    }

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
                SelectMedioTraslado();
                SelectMedioComunicacion();
                SelectTipoAcomodo();
                SelectPropuestaAcomodo();
                
				// Carátula y formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Validar formulario
                if ( !ValidaFormulario() ) { return; }

                // Actualizar los datos generales
                UpdateEvento_Configuracion();

				// Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
            }
		}

        protected void chkEsposaInvitada_CheckedChanged(object sender, EventArgs e){
            try
            {

                if( this.chkEsposaInvitada.Checked ){

                    this.rblConfirmacionEsposa.Enabled = true;
                    this.rblConfirmacionEsposa.Items[0].Selected = true;
                }else{

                    this.rblConfirmacionEsposa.Enabled = false;
                    this.rblConfirmacionEsposa.ClearSelection();
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
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


        // Eventos de Sección: Comité de recepción

        protected void btnAgregarResponsableEvento_Click(object sender, EventArgs e){
            DataTable tblResponsableEvento;
            DataRow rowResponsableEvento;

            try
            {

                // Obtener DataTable del grid
                tblResponsableEvento = gcParse.GridViewToDataTable(this.gvResponsableEvento, false);

                // Validaciones
                if ( this.txtResponsableEventoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                if ( this.txtResponsableEventoPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowResponsableEvento = tblResponsableEvento.NewRow();
                rowResponsableEvento["Orden"] = (tblResponsableEvento.Rows.Count + 1).ToString();
                rowResponsableEvento["Nombre"] = this.txtResponsableEventoNombre.Text.Trim();
                rowResponsableEvento["Puesto"] = this.txtResponsableEventoPuesto.Text.Trim();
                tblResponsableEvento.Rows.Add(rowResponsableEvento);

                // Actualizar Grid
                this.gvResponsableEvento.DataSource = tblResponsableEvento;
                this.gvResponsableEvento.DataBind();

                // Nueva captura
                this.txtResponsableEventoNombre.Text = "";
                this.txtResponsableEventoPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblResponsableEvento;

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
                Orden = this.gvResponsableEvento.DataKeys[intRow]["Orden"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblResponsableEvento = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);

                        // Remover el elemento
                        tblResponsableEvento.Rows.Remove( tblResponsableEvento.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowResponsableEvento in tblResponsableEvento.Rows ){

                            tblResponsableEvento.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvResponsableEvento.DataSource = tblResponsableEvento;
                        this.gvResponsableEvento.DataBind();

                        // Nueva captura
                        this.txtResponsableEventoNombre.Text = "";
                        this.txtResponsableEventoPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String Orden = "";
            String ResponsableEventoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvResponsableEvento.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ResponsableEventoNombre = this.gvResponsableEvento.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ResponsableEventoNombre + "]";
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

        protected void gvResponsableEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvResponsableEvento, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }

        
        // Eventos de Sección: Comité de recepción

        protected void btnAgregarResponsableLogistica_Click(object sender, EventArgs e){
            DataTable tblResponsableLogistica;
            DataRow rowResponsableLogistica;

            try
            {

                // Obtener DataTable del grid
                tblResponsableLogistica = gcParse.GridViewToDataTable(this.gvResponsableLogistica, false);

                // Validaciones
                if ( this.txtResponsableLogisticaNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                if ( this.txtResponsableLogisticaContacto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowResponsableLogistica = tblResponsableLogistica.NewRow();
                rowResponsableLogistica["Orden"] = (tblResponsableLogistica.Rows.Count + 1).ToString();
                rowResponsableLogistica["Nombre"] = this.txtResponsableLogisticaNombre.Text.Trim();
                rowResponsableLogistica["Contacto"] = this.txtResponsableLogisticaContacto.Text.Trim();
                tblResponsableLogistica.Rows.Add(rowResponsableLogistica);

                // Actualizar Grid
                this.gvResponsableLogistica.DataSource = tblResponsableLogistica;
                this.gvResponsableLogistica.DataBind();

                // Nueva captura
                this.txtResponsableLogisticaNombre.Text = "";
                this.txtResponsableLogisticaContacto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableLogistica_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblResponsableLogistica;

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
                Orden = this.gvResponsableLogistica.DataKeys[intRow]["Orden"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblResponsableLogistica = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);

                        // Remover el elemento
                        tblResponsableLogistica.Rows.Remove( tblResponsableLogistica.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowResponsableLogistica in tblResponsableLogistica.Rows ){

                            tblResponsableLogistica.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvResponsableLogistica.DataSource = tblResponsableLogistica;
                        this.gvResponsableLogistica.DataBind();

                        // Nueva captura
                        this.txtResponsableLogisticaNombre.Text = "";
                        this.txtResponsableLogisticaContacto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableLogistica_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String Orden = "";
            String ResponsableLogisticaNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvResponsableLogistica.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ResponsableLogisticaNombre = this.gvResponsableLogistica.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ResponsableLogisticaNombre + "]";
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

        protected void gvResponsableLogistica_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvResponsableLogistica, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }


    }
}