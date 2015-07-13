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

namespace Agenda.Web.Application.WebApp.Private.Gira
{
    public partial class girDatosGenerales : System.Web.UI.Page
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

        void SelectGira(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTGira oENTGira = new ENTGira();

            BPGira oBPGira = new BPGira();

            try
            {

                // Formulario
                oENTGira.GiraId = Int32.Parse(this.hddGiraId.Value);

                // Transacción
                oENTResponse = oBPGira.SelectGira_Detalle(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Carátula compacta
                this.lblGiraNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString();
                this.lblGiraFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHoraInicioTexto"].ToString() + " al " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHoraFinTexto"].ToString();
                
                // Precarga del formulario
                this.txtNombreGira.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString();
                this.wucCalendarInicio.SetDate(DateTime.Parse(oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaInicio"].ToString()));
                this.wucCalendarFin.SetDate(DateTime.Parse(oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaFin"].ToString()));
                this.wucTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraHoraInicioTexto"].ToString();
                this.wucTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraHoraFinTexto"].ToString();
                this.ckeGiraDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraDetalle"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateGira_DatosGenerales(){
            ENTGira oENTGira = new ENTGira();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPGira oBPGira = new BPGira();

            String ErrorDetailHour = "";

            try
            {

                // Validaciones
                if (this.txtNombreGira.Text.Trim() == "") { throw new Exception("El campo [Nombre del Gira] es requerido"); }
                if (!this.wucCalendarInicio.IsValidDate()) { throw new Exception("El campo [Fecha inicial de la Gira] es requerido"); }
                if (!this.wucCalendarFin.IsValidDate()) { throw new Exception("El campo [Fecha final de la Gira] es requerido"); }
                if (!this.wucTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del Gira] es requerido: " + ErrorDetailHour); }
                if (!this.wucTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de finalización del Gira] es requerido: " + ErrorDetailHour); }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTGira.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTGira.GiraId = Int32.Parse(this.hddGiraId.Value);
                oENTGira.GiraNombre = this.txtNombreGira.Text.Trim();
                oENTGira.FechaGiraInicio = this.wucCalendarInicio.DisplayUTCDate;
                oENTGira.FechaGiraFin = this.wucCalendarFin.DisplayUTCDate;
                oENTGira.HoraGiraInicio = this.wucTimerDesde.DisplayUTCTime;
                oENTGira.HoraGiraFin = this.wucTimerHasta.DisplayUTCTime;
                oENTGira.GiraDetalle = this.ckeGiraDetalle.Text.Trim();

                // Transacción
                oENTResponse = oBPGira.UpdateGira_DatosGira(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Giras de la página

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

                // Obtener GiraId
                this.hddGiraId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

				// Carátula y formulario
                SelectGira();

                // Estado inicial
                this.wucCalendarInicio.Width = 176;
                this.wucCalendarFin.Width = 176;

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Actualizar los datos generales
                UpdateGira_DatosGenerales();

				// Regresar
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girDetalleGira.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

				// Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girDetalleGira.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtNombreGira.ClientID + "'); }", true);
            }
		}

    }
}