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

                // Agregar Item de selección
                this.ddlMedioComunicacion.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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
                this.ddlMedioTraslado.DataTextField = "Nombre";
                this.ddlMedioTraslado.DataValueField = "MedioTrasladoId";
                this.ddlMedioTraslado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMedioTraslado.DataBind();

                // Agregar Item de selección
                this.ddlMedioTraslado.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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

                // Agregar Item de selección
                this.ddlTipoVestimenta.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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
                
                // Precarga del formulario
                this.ddlTipoVestimenta.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString();
                this.ddlMedioTraslado.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioTrasladoId"].ToString();
                this.ddlMedioComunicacion.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionId"].ToString();

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
                this.ckeMenu.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Menu"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateEvento_Configuracion(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            Int32 ValidateNumber;

            try
            {

                // Validaciones
                if (this.ddlTipoVestimenta.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar un Tipo de cita")); }
                if (this.ddlMedioTraslado.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar un MedioTraslado")); }
                if (this.ddlMedioComunicacion.SelectedIndex == 0) { throw (new Exception("Es necesario seleccionar una MedioComunicacion")); }
                
                if ( Int32.TryParse(this.txtAforo.Text, out ValidateNumber) == false) { throw (new Exception("Es necesario seleccionar un Secretario Ramo")); }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTEvento.EventoId = Int32.Parse( this.hddEventoId.Value );
                oENTEvento.TipoVestimentaId = Int32.Parse(this.ddlTipoVestimenta.SelectedItem.Value);
                oENTEvento.MedioTrasladoId = Int32.Parse(this.ddlMedioTraslado.SelectedItem.Value);
                oENTEvento.MedioComunicacionId = Int32.Parse(this.ddlMedioComunicacion.SelectedItem.Value);
                oENTEvento.PronosticoClima = this.txtPronostico.Text.Trim();
                oENTEvento.TemperaturaMinima = this.txtTemperaturaMinima.Text.Trim();
                oENTEvento.TemperaturaMaxima = this.txtTemperaturaMaxima.Text.Trim();
                oENTEvento.LugarArribo = this.txtLugarArribo.Text.Trim();
                oENTEvento.Aforo = Int32.Parse( this.txtAforo.Text.Trim() );
                oENTEvento.TipoMontaje = this.txtTipoMontaje.Text.Trim();
                oENTEvento.Esposa = Int16.Parse( ( this.chkEsposaInvitada.Checked ? 1 : 0 ).ToString() );
                oENTEvento.EsposaSi = Int16.Parse( ( this.rblConfirmacionEsposa.SelectedValue == "1" ? 1 : 0 ).ToString() );
                oENTEvento.EsposaNo = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "2" ? 1 : 0).ToString());
                oENTEvento.EsposaConfirma = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "3" ? 1 : 0).ToString());
                oENTEvento.AccionRealizar = this.txtAccionRealizar.Text.Trim();
                oENTEvento.CaracteristicasInvitados = this.txtCaracteristicasInvitados.Text.Trim();
                oENTEvento.Menu = this.ckeMenu.Text.Trim();

                // Transacción
                //oENTResponse = oBPEvento.UpdateEvento_DatosGenerales(oENTEvento);

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

				// Carátula y formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Actualizar los datos generales
                UpdateEvento_Configuracion();

				// Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
            }
		}

        protected void chkEsposaInvitada_CheckedChanged(object sender, EventArgs e){
            try
            {

                if( this.chkEsposaInvitada.Checked ){

                    this.rblConfirmacionEsposa.Enabled = true;
                }else{

                    this.rblConfirmacionEsposa.Enabled = false;
                    this.rblConfirmacionEsposa.ClearSelection();
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlTipoVestimenta.ClientID + "'); }", true);
            }
        }

    }
}