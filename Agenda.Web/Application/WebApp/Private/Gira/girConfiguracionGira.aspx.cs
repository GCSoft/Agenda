/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	girHistorial
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

namespace Agenda.Web.Application.WebApp.Private.Gira
{
    public partial class girConfiguracionGira : System.Web.UI.Page
    {
        

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }


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
                this.lblGiraFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHora"].ToString();

                // Programa
                this.gvPrograma.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvPrograma.DataBind();

                // Cargar Grupos de la gira
                this.wucGiraAgrupacion.CargarAgrupaciones(oENTResponse.DataSetResponse.Tables[5]);

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

                // Ocultar paneles
                ClearPopUp_TrasladoVehiculoPanel();
                ClearPopUp_TrasladoHelicopteroPanel();

				// Carátula
                SelectGira();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void btnTrasladoVehiculo_Click(object sender, EventArgs e){
			try
            {

                SetPopUp_TrasladoVehiculoPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void btnTrasladoHelicoptero_Click(object sender, EventArgs e){
			try
            {

                SetPopUp_TrasladoHelicopteroPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void gvPrograma_RowDataBound(object sender, GridViewRowEventArgs e){
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

        protected void gvPrograma_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvPrograma, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }


        #region PopUp - Traslado en Vehículo
            
            
            // Rutinas

            void ClearPopUp_TrasladoVehiculoPanel(){
                try
                {

                    // Limpiar formulario
                    this.txtPopUp_TrasladoVehiculoDetalle.Text = "";
                    this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayTime = "10:00 AM";
                    this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayTime = "10:00 AM";
                    this.ddlPopUp_TrasladoVehiculoStatus.SelectedValue = "1";

                    // Estado incial de controles
                    this.pnlPopUp_TrasladoVehiculo.Visible = false;
                    this.lblPopUp_TrasladoVehiculoTitle.Text = "";
                    this.btnPopUp_TrasladoVehiculoCommand.Text = "";
                    this.lblPopUp_TrasladoVehiculoMessage.Text = "";
                    this.hddGiraConfiguracionId.Value = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void InsertConfiguracion_TrasladoVehiculo(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.LugarEventoId = 0;
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.wucGiraAgrupacion.ItemDisplayed;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoVehiculoDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = Int16.Parse(this.ddlPopUp_TrasladoVehiculoStatus.SelectedItem.Value);

                    // Transacción
                    oENTResponse = oBPGira.InsertGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_TrasladoVehiculoPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuracion creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracionTrasladoVehiculo_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                try
                {
                    
                    // Formulario
                    oENTGira.GiraConfiguracionId = GiraConfiguracionId;
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.Activo = 1;

                    // Transacción
                    oENTResponse = oBPGira.SelectGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                    // Mensaje de la BD
                    this.lblPopUp_TrasladoVehiculoMessage.Text = oENTResponse.MessageDB;

                    // Llenado de formulario
                    this.txtPopUp_TrasladoVehiculoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.wucGiraAgrupacion.SelectItem(oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString());
                    this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();
                    this.ddlPopUp_TrasladoVehiculoStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_TrasladoVehiculoPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_TrasladoVehiculo.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();

                    // Detalle de acción
                    switch (PopUpType)
                    {
                        case PopUpTypes.Insert:

                            this.lblPopUp_TrasladoVehiculoTitle.Text = "Nuevo Traslado en Vehículo";
                            this.btnPopUp_TrasladoVehiculoCommand.Text = "Agregar Traslado en Vehículo";
                            break;

                        case PopUpTypes.Update:

                            this.lblPopUp_TrasladoVehiculoTitle.Text = "Edición de Traslado en Vehículo";
                            this.btnPopUp_TrasladoVehiculoCommand.Text = "Actualizar Traslado en Vehículo";
                            SelectGiraConfiguracionTrasladoVehiculo_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_TrasladoVehiculoDetalle.ClientID + "');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_TrasladoVehiculo(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraConfiguracionId = Int32.Parse(this.hddGiraConfiguracionId.Value);
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.LugarEventoId = 0;
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.wucGiraAgrupacion.ItemDisplayed;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoVehiculoDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = Int16.Parse(this.ddlPopUp_TrasladoVehiculoStatus.SelectedItem.Value);

                    // Transacción
                    oENTResponse = oBPGira.UpdateGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_TrasladoVehiculoPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuracion creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void ValidatePopUp_TrasladoVehiculoForm(){
                String ErrorDetailHour = "";

                try
                {
                
                    if (this.txtPopUp_TrasladoVehiculoDetalle.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                    if (!this.wucPopUp_TrasladoVehiculoTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucPopUp_TrasladoVehiculoTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora final del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucGiraAgrupacion.IsValidItemSelected()) { throw new Exception("El campo [Agrupación] es requerido"); }

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_TrasladoVehiculoCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    ValidatePopUp_TrasladoVehiculoForm();

                    // Determinar acción
                    if (this.hddGiraConfiguracionId.Value == "0"){

                        InsertConfiguracion_TrasladoVehiculo();
                    }else{

                        UpdateConfiguracion_TrasladoVehiculo();
                    }

                }catch (Exception ex){
                    this.lblPopUp_TrasladoVehiculoMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_TrasladoVehiculoDetalle.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_TrasladoVehiculo_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_TrasladoVehiculoPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }
            
            
        #endregion

        #region PopUp - Traslado en Helicóptero
            
            
            // Rutinas

            void ClearPopUp_TrasladoHelicopteroPanel(){
                try
                {

                    // Limpiar formulario
                    this.txtPopUp_TrasladoHelicopteroDetalle.Text = "";
                    this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayTime = "10:00 AM";
                    this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayTime = "10:00 AM";
                    this.ddlPopUp_TrasladoHelicopteroStatus.SelectedValue = "1";

                    // Estado incial de controles
                    this.pnlPopUp_TrasladoHelicoptero.Visible = false;
                    this.lblPopUp_TrasladoHelicopteroTitle.Text = "";
                    this.btnPopUp_TrasladoHelicopteroCommand.Text = "";
                    this.lblPopUp_TrasladoHelicopteroMessage.Text = "";
                    this.hddGiraConfiguracionId.Value = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void InsertConfiguracion_TrasladoHelicoptero(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.LugarEventoId = 0;
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.wucGiraAgrupacion.ItemDisplayed;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoHelicopteroDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = Int16.Parse(this.ddlPopUp_TrasladoHelicopteroStatus.SelectedItem.Value);

                    // Transacción
                    oENTResponse = oBPGira.InsertGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_TrasladoHelicopteroPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuracion creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracionTrasladoHelicoptero_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                try
                {
                    
                    // Formulario
                    oENTGira.GiraConfiguracionId = GiraConfiguracionId;
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.Activo = 1;

                    // Transacción
                    oENTResponse = oBPGira.SelectGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                    // Mensaje de la BD
                    this.lblPopUp_TrasladoHelicopteroMessage.Text = oENTResponse.MessageDB;

                    // Llenado de formulario
                    this.txtPopUp_TrasladoHelicopteroDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.wucGiraAgrupacion.SelectItem(oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString());
                    this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();
                    this.ddlPopUp_TrasladoHelicopteroStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_TrasladoHelicopteroPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_TrasladoHelicoptero.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();

                    // Detalle de acción
                    switch (PopUpType)
                    {
                        case PopUpTypes.Insert:

                            this.lblPopUp_TrasladoHelicopteroTitle.Text = "Nuevo Traslado en Helicóptero";
                            this.btnPopUp_TrasladoHelicopteroCommand.Text = "Agregar Traslado en Helicóptero";
                            break;

                        case PopUpTypes.Update:

                            this.lblPopUp_TrasladoHelicopteroTitle.Text = "Edición de Traslado en Helicóptero";
                            this.btnPopUp_TrasladoHelicopteroCommand.Text = "Actualizar Traslado en Helicóptero";
                            SelectGiraConfiguracionTrasladoHelicoptero_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_TrasladoHelicopteroDetalle.ClientID + "');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_TrasladoHelicoptero(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraConfiguracionId = Int32.Parse(this.hddGiraConfiguracionId.Value);
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.LugarEventoId = 0;
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.wucGiraAgrupacion.ItemDisplayed;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoHelicopteroDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = Int16.Parse(this.ddlPopUp_TrasladoHelicopteroStatus.SelectedItem.Value);

                    // Transacción
                    oENTResponse = oBPGira.UpdateGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_TrasladoHelicopteroPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuracion creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void ValidatePopUp_TrasladoHelicopteroForm(){
                String ErrorDetailHour = "";

                try
                {
                
                    if (this.txtPopUp_TrasladoHelicopteroDetalle.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                    if (!this.wucPopUp_TrasladoHelicopteroTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucPopUp_TrasladoHelicopteroTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora final del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucGiraAgrupacion.IsValidItemSelected()) { throw new Exception("El campo [Agrupación] es requerido"); }

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_TrasladoHelicopteroCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    ValidatePopUp_TrasladoHelicopteroForm();

                    // Determinar acción
                    if (this.hddGiraConfiguracionId.Value == "0"){

                        InsertConfiguracion_TrasladoHelicoptero();
                    }else{

                        UpdateConfiguracion_TrasladoHelicoptero();
                    }

                }catch (Exception ex){
                    this.lblPopUp_TrasladoHelicopteroMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_TrasladoHelicopteroDetalle.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_TrasladoHelicoptero_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_TrasladoHelicopteroPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }
            
            
        #endregion

    }
}