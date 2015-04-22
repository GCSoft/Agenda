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
        GCParse gcParse = new GCParse();

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

        DataTable NuevaAgrupacion( String Item){
            DataTable dtAgrupacion = null;
            DataRow rowAgrupacion;

            try
            {

                // Recuperar agrupación
                dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                // Validación
                if ( dtAgrupacion.Select("Agrupacion='" + Item + "'").Length == 0 ){

                    rowAgrupacion = dtAgrupacion.NewRow();
                    rowAgrupacion["Row"] = dtAgrupacion.Rows.Count + 1;
                    rowAgrupacion["Agrupacion"] = Item;
                    dtAgrupacion.Rows.Add(rowAgrupacion);
                }

                // Actualizar ViewState
                this.ViewState["dtAgrupacion"] = dtAgrupacion;

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
            }

            return dtAgrupacion;
		}
        

        // Rutinas el programador

        void DeleteGiraConfiguracion(Int32 GiraConfiguracionId){
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
                oENTGira.GiraConfiguracionId = GiraConfiguracionId;
                oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );

                // Transacción
                oENTResponse = oBPGira.DeleteGiraConfiguracion(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar formulario
                SelectGira();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Partida eliminada con éxito!');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }
        
        void SelectGira(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTGira oENTGira = new ENTGira();

            BPGira oBPGira = new BPGira();

            DataTable dtAgrupacion;
            DataRow rowNuevaAgrupacion;

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

                // Agregar los grupos de la gira que no estén precargados
                dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];
                foreach( DataRow rowAgrupacion in oENTResponse.DataSetResponse.Tables[5].Rows ){

                    if ( dtAgrupacion.Select("Agrupacion='" + rowAgrupacion["Agrupacion"].ToString().Trim() + "'").Length == 0 ){

                        rowNuevaAgrupacion = dtAgrupacion.NewRow();
                        rowNuevaAgrupacion["Row"] = dtAgrupacion.Rows.Count + 1;
                        rowNuevaAgrupacion["Agrupacion"] = rowAgrupacion["Agrupacion"].ToString().Trim();
                        dtAgrupacion.Rows.Add(rowNuevaAgrupacion);
                    }
                }
                this.ViewState["dtAgrupacion"] = dtAgrupacion;

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
                this.ddlPopUp_EventoMedioComunicacion.DataTextField = "Nombre";
                this.ddlPopUp_EventoMedioComunicacion.DataValueField = "MedioComunicacionId";
                this.ddlPopUp_EventoMedioComunicacion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUp_EventoMedioComunicacion.DataBind();

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
                this.chklPopUp_EventoMedioTraslado.DataTextField = "Nombre";
                this.chklPopUp_EventoMedioTraslado.DataValueField = "MedioTrasladoId";
                this.chklPopUp_EventoMedioTraslado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.chklPopUp_EventoMedioTraslado.DataBind();

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
                this.ddlPopUp_EventoTipoAcomodo.DataTextField = "Nombre";
                this.ddlPopUp_EventoTipoAcomodo.DataValueField = "TipoAcomodoId";
                this.ddlPopUp_EventoTipoAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUp_EventoTipoAcomodo.DataBind();

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
                this.ddlPopUp_EventoTipoVestimenta.DataTextField = "Nombre";
                this.ddlPopUp_EventoTipoVestimenta.DataValueField = "TipoVestimentaId";
                this.ddlPopUp_EventoTipoVestimenta.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUp_EventoTipoVestimenta.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Giras de la página

        protected void Page_Load(object sender, EventArgs e){
            DataTable dtAgrupacion;
            DataRow rowAgrupacion;

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

                // Defnición del DropDownList que almacena la agrupación
                dtAgrupacion = new DataTable("dtAgrupacion");
                dtAgrupacion.Columns.Add("Row", typeof(Int32));
                dtAgrupacion.Columns.Add("Agrupacion", typeof(String));
                
                rowAgrupacion = dtAgrupacion.NewRow();
                rowAgrupacion["Row"] = "-2";
                rowAgrupacion["Agrupacion"] = "[Sin agrupación]";
                dtAgrupacion.Rows.Add(rowAgrupacion);

                rowAgrupacion = dtAgrupacion.NewRow();
                rowAgrupacion["Row"] = "-1";
                rowAgrupacion["Agrupacion"] = "[Otro]";
                dtAgrupacion.Rows.Add(rowAgrupacion);

                this.ViewState["dtAgrupacion"] = dtAgrupacion;

                // Llenado de controles
                SelectEstado_PopUp_LugarEvento();
                SelectMunicipio_PopUp_LugarEvento();
                SelectMedioTraslado();
                SelectMedioComunicacion();
                SelectTipoVestimenta();
                SelectTipoAcomodo();

                // Ocultar paneles
                ClearPopUp_TrasladoVehiculoPanel();
                ClearPopUp_TrasladoHelicopteroPanel();
                ClearPopUp_EventoPanel();
                ClearPopUp_ActividadGeneralPanel();
                ClearPopUpPanel_LugarEvento();

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

        protected void btnEvento_Click(object sender, EventArgs e){
			try
            {

                SetPopUp_EventoPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void btnActividadGeneral_Click(object sender, EventArgs e){
			try
            {

                SetPopUp_ActividadGeneralPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void gvPrograma_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 GiraConfiguracionId = 0;
            Int32 TipoGiraConfiguracionId = 0;

            String strCommand = "";
            Int32 intRow = 0;

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el Gira RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                GiraConfiguracionId = Int32.Parse(this.gvPrograma.DataKeys[intRow]["GiraConfiguracionId"].ToString());
                TipoGiraConfiguracionId = Int32.Parse(this.gvPrograma.DataKeys[intRow]["TipoGiraConfiguracionId"].ToString());

                // Acción
                switch (strCommand)
                {
                    case "Editar":

                        switch (TipoGiraConfiguracionId)
                        {
                            case 1: // Traslado en vehículo
                                SetPopUp_TrasladoVehiculoPanel(PopUpTypes.Update, GiraConfiguracionId);
                                break;

                            case 2: // Traslado en helicóptero
                                SetPopUp_TrasladoHelicopteroPanel(PopUpTypes.Update, GiraConfiguracionId);
                                break;

                            case 3: // Evento
                                SetPopUp_EventoPanel(PopUpTypes.Update, GiraConfiguracionId);
                                break;

                            case 4: // Actividad General
                                SetPopUp_ActividadGeneralPanel(PopUpTypes.Update, GiraConfiguracionId);
                                break;
                        }

                        break;

                    case "Eliminar":
                        DeleteGiraConfiguracion(GiraConfiguracionId);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void gvPrograma_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String ProgramaNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                ProgramaNombre = this.gvPrograma.DataKeys[e.Row.RowIndex]["ConfiguracionDetalle"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Programa [" + ProgramaNombre + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip Delete
                sTootlTip = "Eliminar Programa [" + ProgramaNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

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
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_TrasladoVehiculo.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoVehiculoDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = "";
                    oENTGira.HelipuertoDomicilio = "";
                    oENTGira.HelipuertoCoordenadas = "";
                    oENTGira.ConfiguracionActivo = 1;

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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracion_TrasladoVehiculo_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                DataTable dtAgrupacion;

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

                    // Recuperar agrupación
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                    // Llenado de formulario
                    this.txtPopUp_TrasladoVehiculoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.ddlAgrupacion_TrasladoVehiculo.SelectedValue = dtAgrupacion.Select("Agrupacion='" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString() + "'")[0]["Row"].ToString();
                    this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_TrasladoVehiculoPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                DataTable dtAgrupacion = null;

                try
                {

                    // Acciones comunes
                    this.pnlPopUp_TrasladoVehiculo.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();

                    // Actualizar combo
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];
                    this.ddlAgrupacion_TrasladoVehiculo.Items.Clear();
                    this.ddlAgrupacion_TrasladoVehiculo.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_TrasladoVehiculo.DataValueField = "Row";
                    this.ddlAgrupacion_TrasladoVehiculo.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_TrasladoVehiculo.DataBind();
                    if ( this.AgrupacionKey.Value != "" ) { this.ddlAgrupacion_TrasladoVehiculo.SelectedValue = this.AgrupacionKey.Value; }

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
                            SelectGiraConfiguracion_TrasladoVehiculo_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

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
                    oENTGira.TipoGiraConfiguracionId = 1; // Traslado en vehículo
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_TrasladoVehiculo.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoVehiculoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoVehiculoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoVehiculoDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = "";
                    oENTGira.HelipuertoDomicilio = "";
                    oENTGira.HelipuertoCoordenadas = "";
                    oENTGira.ConfiguracionActivo = 1;

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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración actualizada con éxito!');", true);

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
                    if (this.txtOtraAgrupacion_TrasladoVehiculo.Enabled) { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.ddlAgrupacion_TrasladoVehiculo.SelectedItem.Value == "-1") { throw (new Exception("El campo [Agrupación] es requerido")); }

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
            
            
            // Eventos del control de agrupación
            
            protected void btnNuevaAgrupacion_TrasladoVehiculo_Click(object sender, EventArgs e){
                DataTable dtAgrupacion;

                try
                {

                    // Nueva agrupación
                    dtAgrupacion = NuevaAgrupacion(this.txtOtraAgrupacion_TrasladoVehiculo.Text.Trim());

                    // Actualizar combo
                    this.ddlAgrupacion_TrasladoVehiculo.Items.Clear();
                    this.ddlAgrupacion_TrasladoVehiculo.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_TrasladoVehiculo.DataValueField = "Row";
                    this.ddlAgrupacion_TrasladoVehiculo.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_TrasladoVehiculo.DataBind();

                    // Seleccionar el item deseado
                    this.ddlAgrupacion_TrasladoVehiculo.SelectedValue = dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion_TrasladoVehiculo.Text.Trim() + "'")[0]["Row"].ToString();
                    this.AgrupacionKey.Value = this.ddlAgrupacion_TrasladoVehiculo.SelectedValue;

                    // Estado inicial
                    this.txtOtraAgrupacion_TrasladoVehiculo.Text = "";
                    this.txtOtraAgrupacion_TrasladoVehiculo.Enabled = false;
                    this.btnNuevaAgrupacion_TrasladoVehiculo.Enabled = false;

                    this.txtOtraAgrupacion_TrasladoVehiculo.CssClass = "Textbox_Disabled";
                    this.btnNuevaAgrupacion_TrasladoVehiculo.CssClass = "Button_Special_Gray";

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_TrasladoVehiculo.ClientID + "');", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
		    }

            protected void ddlAgrupacion_TrasladoVehiculo_SelectedIndexChanged(object sender, EventArgs e){
                try
                {

                    if( this.ddlAgrupacion_TrasladoVehiculo.SelectedItem.Value == "-1" ){

                        this.txtOtraAgrupacion_TrasladoVehiculo.Text = "";
                        this.txtOtraAgrupacion_TrasladoVehiculo.Enabled = true;
                        this.btnNuevaAgrupacion_TrasladoVehiculo.Enabled = true;

                        this.txtOtraAgrupacion_TrasladoVehiculo.CssClass = "Textbox_General";
                        this.btnNuevaAgrupacion_TrasladoVehiculo.CssClass = "Button_Special_Blue";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtOtraAgrupacion_TrasladoVehiculo.ClientID + "');", true);
                    }else{

                        this.txtOtraAgrupacion_TrasladoVehiculo.Text = "";
                        this.txtOtraAgrupacion_TrasladoVehiculo.Enabled = false;
                        this.btnNuevaAgrupacion_TrasladoVehiculo.Enabled = false;

                        this.txtOtraAgrupacion_TrasladoVehiculo.CssClass = "Textbox_Disabled";
                        this.btnNuevaAgrupacion_TrasladoVehiculo.CssClass = "Button_Special_Gray";

                        this.AgrupacionKey.Value = this.ddlAgrupacion_TrasladoVehiculo.SelectedValue;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_TrasladoVehiculo.ClientID + "');", true);
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
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
                    this.txtPopUp_TrasladoHelicopteroLugar.Text = "";
                    this.txtPopUp_TrasladoHelicopteroDomicilio.Text = "";
                    this.txtPopUp_TrasladoHelicopteroCoordenadas.Text = "";

                    // Estado incial de controles
                    this.pnlPopUp_TrasladoHelicoptero.Visible = false;
                    this.lblPopUp_TrasladoHelicopteroTitle.Text = "";
                    this.btnPopUp_TrasladoHelicopteroCommand.Text = "";
                    this.lblPopUp_TrasladoHelicopteroMessage.Text = "";
                    this.hddGiraConfiguracionId.Value = "";

                    this.gvComiteHelipuerto.DataSource = null;
                    this.gvComiteHelipuerto.DataBind();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void InsertConfiguracion_TrasladoHelicoptero(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                DataTable tblTemporal;
                DataRow rowTemporal;

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.TipoGiraConfiguracionId = 2; // Traslado en helicóptero
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_TrasladoHelicoptero.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoHelicopteroDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = this.txtPopUp_TrasladoHelicopteroLugar.Text;
                    oENTGira.HelipuertoDomicilio = this.txtPopUp_TrasladoHelicopteroDomicilio.Text;
                    oENTGira.HelipuertoCoordenadas = this.txtPopUp_TrasladoHelicopteroCoordenadas.Text;
                    oENTGira.ConfiguracionActivo = 1;

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);

                    oENTGira.DataTableComiteHelipuerto = new DataTable("DataTableComiteHelipuerto");
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteHelipuerto in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableComiteHelipuerto.NewRow();
                        rowTemporal["Orden"] = rowComiteHelipuerto["Orden"];
                        rowTemporal["Nombre"] = rowComiteHelipuerto["Nombre"];
                        rowTemporal["Puesto"] = rowComiteHelipuerto["Puesto"];
                        oENTGira.DataTableComiteHelipuerto.Rows.Add(rowTemporal);
                    }

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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracion_TrasladoHelicoptero_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                DataTable dtAgrupacion;

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

                    // Recuperar agrupación
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                    // Llenado de formulario
                    this.txtPopUp_TrasladoHelicopteroDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.ddlAgrupacion_TrasladoHelicoptero.SelectedValue = dtAgrupacion.Select("Agrupacion='" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString() + "'")[0]["Row"].ToString();
                    this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();
                    this.txtPopUp_TrasladoHelicopteroLugar.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["HelipuertoLugar"].ToString();
                    this.txtPopUp_TrasladoHelicopteroDomicilio.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["HelipuertoDomicilio"].ToString();
                    this.txtPopUp_TrasladoHelicopteroCoordenadas.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["HelipuertoCoordenadas"].ToString();

                    // Comité de recepción del helipuerto
                    this.gvComiteHelipuerto.DataSource = oENTResponse.DataSetResponse.Tables[2];
                    this.gvComiteHelipuerto.DataBind();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_TrasladoHelicopteroPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                DataTable dtAgrupacion = null;

                try
                {

                    // Acciones comunes
                    this.pnlPopUp_TrasladoHelicoptero.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();
                    this.tabFormulario_TrasladoHelicoptero.ActiveTabIndex = 0;

                    // Actualizar combo
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];
                    this.ddlAgrupacion_TrasladoHelicoptero.Items.Clear();
                    this.ddlAgrupacion_TrasladoHelicoptero.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_TrasladoHelicoptero.DataValueField = "Row";
                    this.ddlAgrupacion_TrasladoHelicoptero.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_TrasladoHelicoptero.DataBind();
                    if ( this.AgrupacionKey.Value != "" ) { this.ddlAgrupacion_TrasladoHelicoptero.SelectedValue = this.AgrupacionKey.Value; }

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
                            SelectGiraConfiguracion_TrasladoHelicoptero_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_TrasladoHelicoptero(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                DataTable tblTemporal;
                DataRow rowTemporal;

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraConfiguracionId = Int32.Parse(this.hddGiraConfiguracionId.Value);
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.TipoGiraConfiguracionId = 2; // Traslado en helicóptero
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_TrasladoHelicoptero.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_TrasladoHelicopteroTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_TrasladoHelicopteroTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_TrasladoHelicopteroDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = this.txtPopUp_TrasladoHelicopteroLugar.Text;
                    oENTGira.HelipuertoDomicilio = this.txtPopUp_TrasladoHelicopteroDomicilio.Text;
                    oENTGira.HelipuertoCoordenadas = this.txtPopUp_TrasladoHelicopteroCoordenadas.Text;
                    oENTGira.ConfiguracionActivo = 1;

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);

                    oENTGira.DataTableComiteHelipuerto = new DataTable("DataTableComiteHelipuerto");
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableComiteHelipuerto.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteHelipuerto in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableComiteHelipuerto.NewRow();
                        rowTemporal["Orden"] = rowComiteHelipuerto["Orden"];
                        rowTemporal["Nombre"] = rowComiteHelipuerto["Nombre"];
                        rowTemporal["Puesto"] = rowComiteHelipuerto["Puesto"];
                        oENTGira.DataTableComiteHelipuerto.Rows.Add(rowTemporal);
                    }

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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración actualizada con éxito!');", true);

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
                    if (this.txtOtraAgrupacion_TrasladoHelicoptero.Enabled) { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.ddlAgrupacion_TrasladoHelicoptero.SelectedItem.Value == "-1") { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.txtPopUp_TrasladoHelicopteroLugar.Text.Trim() == "") { throw new Exception("* El campo [Lugar del Helipuerto] es requerido"); }
                    if (this.txtPopUp_TrasladoHelicopteroDomicilio.Text.Trim() == "") { throw new Exception("* El campo [Domicilio del Helipuerto] es requerido"); }
                    if (this.txtPopUp_TrasladoHelicopteroCoordenadas.Text.Trim() == "") { throw new Exception("* El campo [Coordenadas del Helipuerto] es requerido"); }

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

            protected void btnAgregarComiteHelipuerto_Click(object sender, EventArgs e){
                DataTable tblComiteHelipuerto;
                DataRow rowComiteHelipuerto;

                try
                {

                    // Obtener DataTable del grid
                    tblComiteHelipuerto = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, false);

                    // Validaciones
                    if ( this.txtPopUp_TrasladoHelicopteroComiteNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre del comité de recepción")); }
                    if (this.txtPopUp_TrasladoHelicopteroComitePuesto.Text.Trim() == "") { throw (new Exception("Es necesario ingresar un puesto de recepción")); }

                    // Agregar un nuevo elemento
                    rowComiteHelipuerto = tblComiteHelipuerto.NewRow();
                    rowComiteHelipuerto["Orden"] = (tblComiteHelipuerto.Rows.Count + 1).ToString();
                    rowComiteHelipuerto["Nombre"] = this.txtPopUp_TrasladoHelicopteroComiteNombre.Text.Trim();
                    rowComiteHelipuerto["Puesto"] = this.txtPopUp_TrasladoHelicopteroComitePuesto.Text.Trim();
                    tblComiteHelipuerto.Rows.Add(rowComiteHelipuerto);

                    // Actualizar Grid
                    this.gvComiteHelipuerto.DataSource = tblComiteHelipuerto;
                    this.gvComiteHelipuerto.DataBind();

                    // Nueva captura
                    this.txtPopUp_TrasladoHelicopteroComiteNombre.Text = "";
                    this.txtPopUp_TrasladoHelicopteroComitePuesto.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_TrasladoHelicopteroComiteNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_TrasladoHelicopteroComiteNombre.ClientID + "'); }", true);
                }
            }

            protected void gvComiteHelipuerto_RowCommand(object sender, GridViewCommandEventArgs e){
                DataTable tblComiteHelipuerto;

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
                    Orden = this.gvComiteHelipuerto.DataKeys[intRow]["Orden"].ToString();

                    // Acción
                    switch (strCommand){

                        case "Eliminar":

                            // Obtener DataTable del grid
                            tblComiteHelipuerto = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);

                            // Remover el elemento
                            tblComiteHelipuerto.Rows.Remove( tblComiteHelipuerto.Select("Orden=" + Orden )[0] );

                            // Reordenar los Items restantes
                            intRow = 0;
                            foreach( DataRow rowComiteHelipuerto in tblComiteHelipuerto.Rows ){

                                tblComiteHelipuerto.Rows[intRow]["Orden"] = (intRow + 1);
                                intRow = intRow + 1;
                            }

                            // Actualizar Grid
                            this.gvComiteHelipuerto.DataSource = tblComiteHelipuerto;
                            this.gvComiteHelipuerto.DataBind();

                            // Nueva captura
                            this.txtPopUp_TrasladoHelicopteroComiteNombre.Text = "";
                            this.txtPopUp_TrasladoHelicopteroComitePuesto.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_TrasladoHelicopteroComiteNombre.ClientID + "'); }", true);

                            break;
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_TrasladoHelicopteroComiteNombre.ClientID + "'); }", true);
                }
            }

            protected void gvComiteHelipuerto_RowDataBound(object sender, GridViewRowEventArgs e){
                ImageButton imgDelete = null;

                String Orden = "";
                String ComiteHelipuertoNombre = "";

                String sImagesAttributes = "";
                String sTootlTip = "";

                try
                {

                    // Validación de que sea fila
                    if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                    // Obtener imagenes
                    imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                    // Datakeys
                    Orden = this.gvComiteHelipuerto.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                    ComiteHelipuertoNombre = this.gvComiteHelipuerto.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                    // Tooltip Edición
                    sTootlTip = "Eliminar a [" + ComiteHelipuertoNombre + "]";
                    imgDelete.Attributes.Add("title", sTootlTip);

                    // Atributos Over
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                    e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_Scroll'; " + sImagesAttributes);

                    // Atributos Out
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                    e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_Scroll'; " + sImagesAttributes);

                }catch (Exception ex){
                    throw (ex);
                }

            }

            protected void gvComiteHelipuerto_Sorting(object sender, GridViewSortEventArgs e){
                try
                {

                    gcCommon.SortGridView(ref this.gvComiteHelipuerto, ref this.hddSort, e.SortExpression, true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_TrasladoHelicopteroComiteNombre.ClientID + "'); }", true);
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
            
            
            // Eventos del control de agrupación
            
            protected void btnNuevaAgrupacion_TrasladoHelicoptero_Click(object sender, EventArgs e){
                DataTable dtAgrupacion;

                try
                {

                    // Nueva agrupación
                    dtAgrupacion = NuevaAgrupacion(this.txtOtraAgrupacion_TrasladoHelicoptero.Text.Trim());

                    // Actualizar combo
                    this.ddlAgrupacion_TrasladoHelicoptero.Items.Clear();
                    this.ddlAgrupacion_TrasladoHelicoptero.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_TrasladoHelicoptero.DataValueField = "Row";
                    this.ddlAgrupacion_TrasladoHelicoptero.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_TrasladoHelicoptero.DataBind();

                    // Seleccionar el item deseado
                    this.ddlAgrupacion_TrasladoHelicoptero.SelectedValue = dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion_TrasladoHelicoptero.Text.Trim() + "'")[0]["Row"].ToString();
                    this.AgrupacionKey.Value = this.ddlAgrupacion_TrasladoHelicoptero.SelectedValue;

                    // Estado inicial
                    this.txtOtraAgrupacion_TrasladoHelicoptero.Text = "";
                    this.txtOtraAgrupacion_TrasladoHelicoptero.Enabled = false;
                    this.btnNuevaAgrupacion_TrasladoHelicoptero.Enabled = false;

                    this.txtOtraAgrupacion_TrasladoHelicoptero.CssClass = "Textbox_Disabled";
                    this.btnNuevaAgrupacion_TrasladoHelicoptero.CssClass = "Button_Special_Gray";

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_TrasladoHelicoptero.ClientID + "');", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
		    }

            protected void ddlAgrupacion_TrasladoHelicoptero_SelectedIndexChanged(object sender, EventArgs e){
                try
                {

                    if( this.ddlAgrupacion_TrasladoHelicoptero.SelectedItem.Value == "-1" ){

                        this.txtOtraAgrupacion_TrasladoHelicoptero.Text = "";
                        this.txtOtraAgrupacion_TrasladoHelicoptero.Enabled = true;
                        this.btnNuevaAgrupacion_TrasladoHelicoptero.Enabled = true;

                        this.txtOtraAgrupacion_TrasladoHelicoptero.CssClass = "Textbox_General";
                        this.btnNuevaAgrupacion_TrasladoHelicoptero.CssClass = "Button_Special_Blue";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtOtraAgrupacion_TrasladoHelicoptero.ClientID + "');", true);
                    }else{

                        this.txtOtraAgrupacion_TrasladoHelicoptero.Text = "";
                        this.txtOtraAgrupacion_TrasladoHelicoptero.Enabled = false;
                        this.btnNuevaAgrupacion_TrasladoHelicoptero.Enabled = false;

                        this.txtOtraAgrupacion_TrasladoHelicoptero.CssClass = "Textbox_Disabled";
                        this.btnNuevaAgrupacion_TrasladoHelicoptero.CssClass = "Button_Special_Gray";

                        this.AgrupacionKey.Value = this.ddlAgrupacion_TrasladoHelicoptero.SelectedValue;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_TrasladoHelicoptero.ClientID + "');", true);
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
            }
            
            
        #endregion

        #region PopUp - Evento
            
            
            // Rutinas

            void ClearPopUp_EventoPanel(){
                try
                {

                    // Limpiar formulario
                    this.txtPopUp_EventoDetalle.Text = "";
                    this.wucPopUp_EventoTimerDesde.DisplayTime = "10:00 AM";
                    this.wucPopUp_EventoTimerHasta.DisplayTime = "10:00 AM";
                    this.txtPopUp_EventoLugar.Text = "";
                    this.hddLugarEventoId.Value = "";
                    this.txtPopUp_Domicilio.Text = "";
                    this.txtPopUp_EventoLugarArribo.Text = "";

                    // Estado incial de controles
                    this.pnlPopUp_Evento.Visible = false;
                    this.lblPopUp_EventoTitle.Text = "";
                    this.btnPopUp_EventoCommand.Text = "";
                    this.lblPopUp_EventoMessage.Text = "";
                    this.hddGiraConfiguracionId.Value = "";
                    this.hddLugarEventoId.Value = "0";
                    this.ddlPopUp_EventoMedioComunicacion.SelectedIndex = 0;
                    this.ddlPopUp_EventoTipoVestimenta.SelectedIndex = 0;
                    this.txtPopUp_EventoLugarArribo.Text = "";
                    this.txtPopUp_EventoTipoMontaje.Text = "";
                    this.txtPopUp_EventoAforo.Text = "";
                    this.txtPopUp_EventoCaracteristicasInvitados.Text = "";
                    this.rblPopUp_EventoConfirmacionEsposa.Enabled = false;
                    this.rblPopUp_EventoConfirmacionEsposa.ClearSelection();
                    this.txtPopUp_EventoTipoVestimentaOtro.Text = "";
                    this.txtPopUp_EventoMenu.Text = "";
                    this.txtPopUp_EventoPronostico.Text = "";
                    this.txtPopUp_EventoAccionRealizar.Text = "";
                    this.chklPopUp_EventoMedioTraslado.ClearSelection();
                    this.ddlPopUp_EventoTipoAcomodo.SelectedIndex = 0;

                    this.gvComite.DataSource = null;
                    this.gvComite.DataBind();

                    this.gvOrdenDia.DataSource = null;
                    this.gvOrdenDia.DataBind();

                    this.gvAcomodo.DataSource = null;
                    this.gvAcomodo.DataBind();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void InsertConfiguracion_Evento(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                DataTable tblTemporal;
                DataRow rowTemporal;

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.TipoGiraConfiguracionId = 3; // Evento
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_Evento.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_EventoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_EventoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_EventoDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = 1;

                    oENTGira.Evento.LugarEventoId = Int32.Parse(this.hddLugarEventoId.Value);
                    oENTGira.Evento.MedioComunicacionId = Int32.Parse(this.ddlPopUp_EventoMedioComunicacion.SelectedItem.Value);
                    oENTGira.Evento.TipoVestimentaId = Int32.Parse(this.ddlPopUp_EventoTipoVestimenta.SelectedItem.Value);
                    oENTGira.Evento.LugarArribo = this.txtPopUp_EventoLugarArribo.Text.Trim();
                    oENTGira.Evento.TipoMontaje = this.txtPopUp_EventoTipoMontaje.Text.Trim();
                    oENTGira.Evento.Aforo = Int32.Parse(this.txtPopUp_EventoAforo.Text.Trim());
                    oENTGira.Evento.CaracteristicasInvitados = this.txtPopUp_EventoCaracteristicasInvitados.Text.Trim();
                    oENTGira.Evento.Esposa = Int16.Parse((this.chkPopUp_EventoEsposaInvitada.Checked ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaSi = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "1" ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaNo = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "2" ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaConfirma = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "3" ? 1 : 0).ToString());
                    oENTGira.Evento.TipoVestimentaOtro = this.txtPopUp_EventoTipoVestimentaOtro.Text.Trim();
                    oENTGira.Evento.Menu = this.txtPopUp_EventoMenu.Text.Trim();
                    oENTGira.Evento.PronosticoClima = this.txtPopUp_EventoPronostico.Text.Trim();
                    oENTGira.Evento.AccionRealizar = this.txtPopUp_EventoAccionRealizar.Text.Trim();
                    
                    oENTGira.DataTableMedioTraslado = new DataTable("DataTableMedioTraslado");
                    oENTGira.DataTableMedioTraslado.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklPopUp_EventoMedioTraslado.Items.Count; k++) {
					    if(this.chklPopUp_EventoMedioTraslado.Items[k].Selected){
                            rowTemporal = oENTGira.DataTableMedioTraslado.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklPopUp_EventoMedioTraslado.Items[k].Value;
                            oENTGira.DataTableMedioTraslado.Rows.Add(rowTemporal);
					    }
				    }

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComite, true);

                    oENTGira.DataTableComiteRecepcion = new DataTable("DataTableComiteRecepcion");
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComite in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableComiteRecepcion.NewRow();
                        rowTemporal["Orden"] = rowComite["Orden"];
                        rowTemporal["Nombre"] = rowComite["Nombre"];
                        rowTemporal["Puesto"] = rowComite["Puesto"];
                        oENTGira.DataTableComiteRecepcion.Rows.Add(rowTemporal);
                    }

                    // Orden del día
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                    oENTGira.DataTableOrdenDia = new DataTable("DataTableOrdenDia");
                    oENTGira.DataTableOrdenDia.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableOrdenDia.Columns.Add("Detalle", typeof(String));
                    oENTGira.DataTableOrdenDia.Columns.Add("Columna3", typeof(String)); // El DataType es de 3 columnas
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableOrdenDia.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Detalle"] = rowComiteRecepcion["Detalle"];
                        rowTemporal["Columna3"] = "";
                        oENTGira.DataTableOrdenDia.Rows.Add(rowTemporal);
                    }

                    // Acomodo
                    oENTGira.Evento.TipoAcomodoId = Int32.Parse(this.ddlPopUp_EventoTipoAcomodo.SelectedItem.Value);

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    oENTGira.DataTableAcomodo = new DataTable("DataTableAcomodo");
                    oENTGira.DataTableAcomodo.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableAcomodo.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableAcomodo.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableAcomodo.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        oENTGira.DataTableAcomodo.Rows.Add(rowTemporal);
                    }

                    // Transacción
                    oENTResponse = oBPGira.InsertGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_EventoPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracion_Evento_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                DataTable dtAgrupacion;

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
                    this.lblPopUp_EventoMessage.Text = oENTResponse.MessageDB;

                    // Recuperar agrupación
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                    // Llenado de formulario
                    this.ddlAgrupacion_Evento.SelectedValue = dtAgrupacion.Select("Agrupacion='" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString() + "'")[0]["row"].ToString();
                    this.txtPopUp_EventoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.wucPopUp_EventoTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_EventoTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();

                    this.txtPopUp_EventoLugar.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString();
                    this.hddLugarEventoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoLugarEventoId"].ToString();
                    this.txtPopUp_Domicilio.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoLugarEvento"].ToString();

                    this.txtPopUp_EventoLugarArribo.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoLugarArribo"].ToString();
                    
                    for (int k = 0; k < this.chklPopUp_EventoMedioTraslado.Items.Count; k++) {
					    if( oENTResponse.DataSetResponse.Tables[3].Select("MedioTrasladoId=" + this.chklPopUp_EventoMedioTraslado.Items[k].Value).Length > 0 ){
                            this.chklPopUp_EventoMedioTraslado.Items[k].Selected = true;
					    }
				    }
                    
                    this.txtPopUp_EventoTipoMontaje.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoTipoMontaje"].ToString();
                    this.txtPopUp_EventoAforo.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoAforo"].ToString();
                    this.txtPopUp_EventoCaracteristicasInvitados.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoCaracteristicasInvitados"].ToString();
                    
                    if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoEsposa"].ToString() == "1" ){

                        this.chkPopUp_EventoEsposaInvitada.Checked = true;
                        this.rblPopUp_EventoConfirmacionEsposa.Enabled = true;

                        if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoEsposaSi"].ToString() == "1") { this.rblPopUp_EventoConfirmacionEsposa.SelectedValue = "1"; }
                        if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoEsposaNo"].ToString() == "1") { this.rblPopUp_EventoConfirmacionEsposa.SelectedValue = "2"; }
                        if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoEsposaConfirma"].ToString() == "1") { this.rblPopUp_EventoConfirmacionEsposa.SelectedValue = "3"; }
                    
                    }else{

                        this.chkPopUp_EventoEsposaInvitada.Checked = false;
                        this.rblPopUp_EventoConfirmacionEsposa.Enabled = false;
                        this.rblPopUp_EventoConfirmacionEsposa.ClearSelection();
                    }

                    this.ddlPopUp_EventoMedioComunicacion.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoMedioComunicacionId"].ToString();
                    this.ddlPopUp_EventoTipoVestimenta.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoTipoVestimentaId"].ToString();
                    this.txtPopUp_EventoTipoVestimentaOtro.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoTipoVestimentaOtro"].ToString();
                    this.txtPopUp_EventoMenu.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoMenu"].ToString();
                    this.txtPopUp_EventoPronostico.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoPronosticoClima"].ToString();
                    this.txtPopUp_EventoAccionRealizar.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoAccionRealizar"].ToString();

                    this.ddlPopUp_EventoTipoAcomodo.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoTipoAcomodoId"].ToString();

                    // Comité de recepción
                    this.gvComite.DataSource = oENTResponse.DataSetResponse.Tables[4];
                    this.gvComite.DataBind();

                    // Orden del día
                    this.gvOrdenDia.DataSource = oENTResponse.DataSetResponse.Tables[5];
                    this.gvOrdenDia.DataBind();

                    // Acomodo
                    this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[6];
                    this.gvAcomodo.DataBind();

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
                    this.txtPopUp_EventoLugar.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                    this.txtPopUp_Domicilio.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Direccion"].ToString();

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_EventoLugarArribo.ClientID + "');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_EventoPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                DataTable dtAgrupacion = null;

                try
                {

                    // Acciones comunes
                    this.pnlPopUp_Evento.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();
                    this.tabFormulario_Evento.ActiveTabIndex = 0;

                    // Actualizar combo
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];
                    this.ddlAgrupacion_Evento.Items.Clear();
                    this.ddlAgrupacion_Evento.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_Evento.DataValueField = "Row";
                    this.ddlAgrupacion_Evento.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_Evento.DataBind();
                    if ( this.AgrupacionKey.Value != "" ) { this.ddlAgrupacion_Evento.SelectedValue = this.AgrupacionKey.Value; }

                    // Detalle de acción
                    switch (PopUpType)
                    {
                        case PopUpTypes.Insert:

                            this.lblPopUp_EventoTitle.Text = "Nuevo Evento";
                            this.btnPopUp_EventoCommand.Text = "Agregar Evento";
                            break;

                        case PopUpTypes.Update:

                            this.lblPopUp_EventoTitle.Text = "Edición de Evento";
                            this.btnPopUp_EventoCommand.Text = "Actualizar Evento";
                            SelectGiraConfiguracion_Evento_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_Evento(){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPGira oBPGira = new BPGira();

                DataTable tblTemporal;
                DataRow rowTemporal;

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTGira.UsuarioId = oENTSession.UsuarioId;

                    // Formulario
                    oENTGira.GiraConfiguracionId = Int32.Parse(this.hddGiraConfiguracionId.Value);
                    oENTGira.GiraId = Int32.Parse( this.hddGiraId.Value );
                    oENTGira.TipoGiraConfiguracionId = 3; // Evento
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_Evento.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_EventoTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_EventoTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_EventoDetalle.Text.Trim();
                    oENTGira.ConfiguracionActivo = 1;

                    oENTGira.Evento.LugarEventoId = Int32.Parse(this.hddLugarEventoId.Value);
                    oENTGira.Evento.MedioComunicacionId = Int32.Parse(this.ddlPopUp_EventoMedioComunicacion.SelectedItem.Value);
                    oENTGira.Evento.TipoVestimentaId = Int32.Parse(this.ddlPopUp_EventoTipoVestimenta.SelectedItem.Value);
                    oENTGira.Evento.LugarArribo = this.txtPopUp_EventoLugarArribo.Text.Trim();
                    oENTGira.Evento.TipoMontaje = this.txtPopUp_EventoTipoMontaje.Text.Trim();
                    oENTGira.Evento.Aforo = Int32.Parse(this.txtPopUp_EventoAforo.Text.Trim());
                    oENTGira.Evento.CaracteristicasInvitados = this.txtPopUp_EventoCaracteristicasInvitados.Text.Trim();
                    oENTGira.Evento.Esposa = Int16.Parse((this.chkPopUp_EventoEsposaInvitada.Checked ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaSi = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "1" ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaNo = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "2" ? 1 : 0).ToString());
                    oENTGira.Evento.EsposaConfirma = Int16.Parse((this.rblPopUp_EventoConfirmacionEsposa.SelectedValue == "3" ? 1 : 0).ToString());
                    oENTGira.Evento.TipoVestimentaOtro = this.txtPopUp_EventoTipoVestimentaOtro.Text.Trim();
                    oENTGira.Evento.Menu = this.txtPopUp_EventoMenu.Text.Trim();
                    oENTGira.Evento.PronosticoClima = this.txtPopUp_EventoPronostico.Text.Trim();
                    oENTGira.Evento.AccionRealizar = this.txtPopUp_EventoAccionRealizar.Text.Trim();
                    
                    oENTGira.DataTableMedioTraslado = new DataTable("DataTableMedioTraslado");
                    oENTGira.DataTableMedioTraslado.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklPopUp_EventoMedioTraslado.Items.Count; k++) {
					    if(this.chklPopUp_EventoMedioTraslado.Items[k].Selected){
                            rowTemporal = oENTGira.DataTableMedioTraslado.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklPopUp_EventoMedioTraslado.Items[k].Value;
                            oENTGira.DataTableMedioTraslado.Rows.Add(rowTemporal);
					    }
				    }

                    // Comité de recepción
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComite, true);

                    oENTGira.DataTableComiteRecepcion = new DataTable("DataTableComite");
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableComiteRecepcion.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComite in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableComiteRecepcion.NewRow();
                        rowTemporal["Orden"] = rowComite["Orden"];
                        rowTemporal["Nombre"] = rowComite["Nombre"];
                        rowTemporal["Puesto"] = rowComite["Puesto"];
                        oENTGira.DataTableComiteRecepcion.Rows.Add(rowTemporal);
                    }

                    // Orden del día
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                    oENTGira.DataTableOrdenDia = new DataTable("DataTableOrdenDia");
                    oENTGira.DataTableOrdenDia.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableOrdenDia.Columns.Add("Detalle", typeof(String));
                    oENTGira.DataTableOrdenDia.Columns.Add("Columna3", typeof(String)); // El DataType es de 3 columnas
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableOrdenDia.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Detalle"] = rowComiteRecepcion["Detalle"];
                        rowTemporal["Columna3"] = "";
                        oENTGira.DataTableOrdenDia.Rows.Add(rowTemporal);
                    }

                    // Acomodo
                    oENTGira.Evento.TipoAcomodoId = Int32.Parse(this.ddlPopUp_EventoTipoAcomodo.SelectedItem.Value);

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    oENTGira.DataTableAcomodo = new DataTable("DataTableAcomodo");
                    oENTGira.DataTableAcomodo.Columns.Add("Orden", typeof(Int32));
                    oENTGira.DataTableAcomodo.Columns.Add("Nombre", typeof(String));
                    oENTGira.DataTableAcomodo.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTGira.DataTableAcomodo.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        oENTGira.DataTableAcomodo.Rows.Add(rowTemporal);
                    }

                    // Transacción
                    oENTResponse = oBPGira.UpdateGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_EventoPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración actualizada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void ValidatePopUp_EventoForm(){
                DataTable tblTemporal;
                DataRow rowTemporal;

                String ErrorDetailHour = "";
                Int32 ValidateNumber;

                try
                {

                    // Medios de traslado seleccionados
                    tblTemporal = new DataTable("DataTableMedioTraslado");
                    tblTemporal.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklPopUp_EventoMedioTraslado.Items.Count; k++) {
					    if(this.chklPopUp_EventoMedioTraslado.Items[k].Selected){
                            rowTemporal = tblTemporal.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklPopUp_EventoMedioTraslado.Items[k].Value;
                            tblTemporal.Rows.Add(rowTemporal);
					    }
				    }

                    // Validaciones
                    if (this.txtOtraAgrupacion_Evento.Enabled) { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.ddlAgrupacion_Evento.SelectedItem.Value == "-1") { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.txtPopUp_EventoDetalle.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                    if (!this.wucPopUp_EventoTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucPopUp_EventoTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora final del evento] es requerido: " + ErrorDetailHour); }
                    if( this.hddLugarEventoId.Value.Trim() == "" || this.hddLugarEventoId.Value.Trim() == "0" ){ throw (new Exception("Es necesario seleccionar un Lugar del Evento")); }
                    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario seleccionar un medio de traslado")); }
                    if (Int32.TryParse(this.txtPopUp_EventoAforo.Text, out ValidateNumber) == false) { throw (new Exception("La cantidad en Aforo debe de ser numérica")); }
                    if (this.txtPopUp_EventoAccionRealizar.Text.Trim() == "") { throw (new Exception("Es necesario determinar la acción a realizar")); }

                }catch (Exception ex){
                    this.tabFormulario_Evento.ActiveTabIndex = 0;
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_EventoCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    ValidatePopUp_EventoForm();

                    // Determinar acción
                    if (this.hddGiraConfiguracionId.Value == "0"){

                        InsertConfiguracion_Evento();
                    }else{

                        UpdateConfiguracion_Evento();
                    }

                }catch (Exception ex){
                    this.lblPopUp_EventoMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_EventoDetalle.ClientID + "');", true);
                }
            }
            
            protected void chkPopUp_EventoEsposaInvitada_CheckedChanged(object sender, EventArgs e){
                try
                {

                    if( this.chkPopUp_EventoEsposaInvitada.Checked ){

                        this.rblPopUp_EventoConfirmacionEsposa.Enabled = true;
                        this.rblPopUp_EventoConfirmacionEsposa.Items[0].Selected = true;
                    }else{

                        this.rblPopUp_EventoConfirmacionEsposa.Enabled = false;
                        this.rblPopUp_EventoConfirmacionEsposa.ClearSelection();
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }
            
            protected void ddlPopUp_EventoTipoVestimenta_SelectedIndexChanged(object sender, EventArgs e){
                try
                {

                    switch( this.ddlPopUp_EventoTipoVestimenta.SelectedItem.Value ){
                        case "6":   // Otro
                        
                            this.txtPopUp_EventoTipoVestimentaOtro.Enabled = true;
                            this.txtPopUp_EventoTipoVestimentaOtro.CssClass = "Textbox_General";
                            break;

                        default:

                            this.txtPopUp_EventoTipoVestimentaOtro.Text = "";
                            this.txtPopUp_EventoTipoVestimentaOtro.Enabled = false;
                            this.txtPopUp_EventoTipoVestimentaOtro.CssClass = "Textbox_Disabled";
                            break;
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }
            
            protected void hddLugarEventoId_ValueChanged(object sender, EventArgs e){
                try
                {

                    SelectLugarEvento();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoLugar.ClientID + "'); }", true);
                }
            }

            protected void imgCloseWindow_Evento_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_EventoPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
            // Eventos Comité de recepción
            
            protected void btnAgregarComite_Click(object sender, EventArgs e){
                DataTable tblComite;
                DataRow rowComite;

                try
                {

                    // Obtener DataTable del grid
                    tblComite = gcParse.GridViewToDataTable(this.gvComite, false);

                    // Validaciones
                    if ( this.txtPopUp_EventoComiteNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre del comité de recepción")); }
                    if (this.txtPopUp_EventoComitePuesto.Text.Trim() == "") { throw (new Exception("Es necesario ingresar un puesto de recepción")); }

                    // Agregar un nuevo elemento
                    rowComite = tblComite.NewRow();
                    rowComite["Orden"] = (tblComite.Rows.Count + 1).ToString();
                    rowComite["Nombre"] = this.txtPopUp_EventoComiteNombre.Text.Trim();
                    rowComite["Puesto"] = this.txtPopUp_EventoComitePuesto.Text.Trim();
                    tblComite.Rows.Add(rowComite);

                    // Actualizar Grid
                    this.gvComite.DataSource = tblComite;
                    this.gvComite.DataBind();

                    // Nueva captura
                    this.txtPopUp_EventoComiteNombre.Text = "";
                    this.txtPopUp_EventoComitePuesto.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_EventoComiteNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoComiteNombre.ClientID + "'); }", true);
                }
            }
            
            protected void gvComite_RowCommand(object sender, GridViewCommandEventArgs e){
                DataTable tblComite;

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
                    Orden = this.gvComite.DataKeys[intRow]["Orden"].ToString();

                    // Acción
                    switch (strCommand){

                        case "Eliminar":

                            // Obtener DataTable del grid
                            tblComite = gcParse.GridViewToDataTable(this.gvComite, true);

                            // Remover el elemento
                            tblComite.Rows.Remove( tblComite.Select("Orden=" + Orden )[0] );

                            // Reordenar los Items restantes
                            intRow = 0;
                            foreach( DataRow rowComite in tblComite.Rows ){

                                tblComite.Rows[intRow]["Orden"] = (intRow + 1);
                                intRow = intRow + 1;
                            }

                            // Actualizar Grid
                            this.gvComite.DataSource = tblComite;
                            this.gvComite.DataBind();

                            // Nueva captura
                            this.txtPopUp_EventoComiteNombre.Text = "";
                            this.txtPopUp_EventoComitePuesto.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_EventoComiteNombre.ClientID + "'); }", true);

                            break;
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoComiteNombre.ClientID + "'); }", true);
                }
            }

            protected void gvComite_RowDataBound(object sender, GridViewRowEventArgs e){
                ImageButton imgDelete = null;

                String Orden = "";
                String ComiteNombre = "";

                String sImagesAttributes = "";
                String sTootlTip = "";

                try
                {

                    // Validación de que sea fila
                    if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                    // Obtener imagenes
                    imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                    // Datakeys
                    Orden = this.gvComite.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                    ComiteNombre = this.gvComite.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                    // Tooltip Edición
                    sTootlTip = "Eliminar a [" + ComiteNombre + "]";
                    imgDelete.Attributes.Add("title", sTootlTip);

                    // Atributos Over
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                    e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_Scroll'; " + sImagesAttributes);

                    // Atributos Out
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                    e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_Scroll'; " + sImagesAttributes);

                }catch (Exception ex){
                    throw (ex);
                }

            }

            
            // Eventos del control de agrupación
            
            protected void btnNuevaAgrupacion_Evento_Click(object sender, EventArgs e){
                DataTable dtAgrupacion;

                try
                {

                    // Nueva agrupación
                    dtAgrupacion = NuevaAgrupacion(this.txtOtraAgrupacion_Evento.Text.Trim());

                    // Actualizar combo
                    this.ddlAgrupacion_Evento.Items.Clear();
                    this.ddlAgrupacion_Evento.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_Evento.DataValueField = "Row";
                    this.ddlAgrupacion_Evento.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_Evento.DataBind();

                    // Seleccionar el item deseado
                    this.ddlAgrupacion_Evento.SelectedValue = dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion_Evento.Text.Trim() + "'")[0]["Row"].ToString();
                    this.AgrupacionKey.Value = this.ddlAgrupacion_Evento.SelectedValue;

                    // Estado inicial
                    this.txtOtraAgrupacion_Evento.Text = "";
                    this.txtOtraAgrupacion_Evento.Enabled = false;
                    this.btnNuevaAgrupacion_Evento.Enabled = false;

                    this.txtOtraAgrupacion_Evento.CssClass = "Textbox_Disabled";
                    this.btnNuevaAgrupacion_Evento.CssClass = "Button_Special_Gray";

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_Evento.ClientID + "');", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
		    }

            protected void ddlAgrupacion_Evento_SelectedIndexChanged(object sender, EventArgs e){
                try
                {

                    if( this.ddlAgrupacion_Evento.SelectedItem.Value == "-1" ){

                        this.txtOtraAgrupacion_Evento.Text = "";
                        this.txtOtraAgrupacion_Evento.Enabled = true;
                        this.btnNuevaAgrupacion_Evento.Enabled = true;

                        this.txtOtraAgrupacion_Evento.CssClass = "Textbox_General";
                        this.btnNuevaAgrupacion_Evento.CssClass = "Button_Special_Blue";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtOtraAgrupacion_Evento.ClientID + "');", true);
                    }else{

                        this.txtOtraAgrupacion_Evento.Text = "";
                        this.txtOtraAgrupacion_Evento.Enabled = false;
                        this.btnNuevaAgrupacion_Evento.Enabled = false;

                        this.txtOtraAgrupacion_Evento.CssClass = "Textbox_Disabled";
                        this.btnNuevaAgrupacion_Evento.CssClass = "Button_Special_Gray";

                        this.AgrupacionKey.Value = this.ddlAgrupacion_Evento.SelectedValue;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_Evento.ClientID + "');", true);
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
            }
            
            
            // Eventos Orden del día
            
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
            
            
            // Eventos Acomodo
            
            protected void btnAgregarAcomodo_Click(object sender, EventArgs e){
                DataTable tblAcomodo;
                DataRow rowAcomodo;

                try
                {

                    // Obtener DataTable del grid
                    tblAcomodo = gcParse.GridViewToDataTable(this.gvAcomodo, false);

                    // Validaciones
                    if ( this.txtPopUp_EventoAcomodoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre del comité de recepción")); }
                    if (this.txtPopUp_EventoAcomodoPuesto.Text.Trim() == "") { throw (new Exception("Es necesario ingresar un puesto de recepción")); }

                    // Agregar un nuevo elemento
                    rowAcomodo = tblAcomodo.NewRow();
                    rowAcomodo["Orden"] = (tblAcomodo.Rows.Count + 1).ToString();
                    rowAcomodo["Nombre"] = this.txtPopUp_EventoAcomodoNombre.Text.Trim();
                    rowAcomodo["Puesto"] = this.txtPopUp_EventoAcomodoPuesto.Text.Trim();
                    tblAcomodo.Rows.Add(rowAcomodo);

                    // Actualizar Grid
                    this.gvAcomodo.DataSource = tblAcomodo;
                    this.gvAcomodo.DataBind();

                    // Nueva captura
                    this.txtPopUp_EventoAcomodoNombre.Text = "";
                    this.txtPopUp_EventoAcomodoPuesto.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_EventoAcomodoNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoAcomodoNombre.ClientID + "'); }", true);
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
                            this.txtPopUp_EventoAcomodoNombre.Text = "";
                            this.txtPopUp_EventoAcomodoPuesto.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUp_EventoAcomodoNombre.ClientID + "'); }", true);

                            break;
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoAcomodoNombre.ClientID + "'); }", true);
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
                    e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_Scroll'; " + sImagesAttributes);

                    // Atributos Out
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                    e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_Scroll'; " + sImagesAttributes);

                }catch (Exception ex){
                    throw (ex);
                }

            }
            
            
        #endregion

        #region PopUp - Actividad General
            
            
            // Rutinas

            void ClearPopUp_ActividadGeneralPanel(){
                try
                {

                    // Limpiar formulario
                    this.txtPopUp_ActividadGeneralDetalle.Text = "";
                    this.wucPopUp_ActividadGeneralTimerDesde.DisplayTime = "10:00 AM";
                    this.wucPopUp_ActividadGeneralTimerHasta.DisplayTime = "10:00 AM";

                    // Estado incial de controles
                    this.pnlPopUp_ActividadGeneral.Visible = false;
                    this.lblPopUp_ActividadGeneralTitle.Text = "";
                    this.btnPopUp_ActividadGeneralCommand.Text = "";
                    this.lblPopUp_ActividadGeneralMessage.Text = "";
                    this.hddGiraConfiguracionId.Value = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void InsertConfiguracion_ActividadGeneral(){
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
                    oENTGira.TipoGiraConfiguracionId = 4; // Actividad General
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_ActividadGeneral.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_ActividadGeneralTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_ActividadGeneralTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_ActividadGeneralDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = "";
                    oENTGira.HelipuertoDomicilio = "";
                    oENTGira.HelipuertoCoordenadas = "";
                    oENTGira.ConfiguracionActivo = 1;

                    // Transacción
                    oENTResponse = oBPGira.InsertGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ActividadGeneralPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración creada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SelectGiraConfiguracion_ActividadGeneral_ForEdit(Int32 GiraConfiguracionId){
                ENTGira oENTGira = new ENTGira();
                ENTResponse oENTResponse = new ENTResponse();

                BPGira oBPGira = new BPGira();

                DataTable dtAgrupacion;

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
                    this.lblPopUp_ActividadGeneralMessage.Text = oENTResponse.MessageDB;

                    // Recuperar agrupación
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                    // Llenado de formulario
                    this.txtPopUp_ActividadGeneralDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionDetalle"].ToString();
                    this.ddlAgrupacion_ActividadGeneral.SelectedValue = dtAgrupacion.Select("Agrupacion='" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionGrupo"].ToString() + "'")[0]["Row"].ToString();
                    this.wucPopUp_ActividadGeneralTimerDesde.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                    this.wucPopUp_ActividadGeneralTimerHasta.DisplayTime = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ActividadGeneralPanel(PopUpTypes PopUpType, Int32 idItem = 0){
                DataTable dtAgrupacion = null;

                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ActividadGeneral.Visible = true;
                    this.hddGiraConfiguracionId.Value = idItem.ToString();

                    // Actualizar combo
                    dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];
                    this.ddlAgrupacion_ActividadGeneral.Items.Clear();
                    this.ddlAgrupacion_ActividadGeneral.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_ActividadGeneral.DataValueField = "Row";
                    this.ddlAgrupacion_ActividadGeneral.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_ActividadGeneral.DataBind();
                    if ( this.AgrupacionKey.Value != "" ) { this.ddlAgrupacion_ActividadGeneral.SelectedValue = this.AgrupacionKey.Value; }

                    // Detalle de acción
                    switch (PopUpType)
                    {
                        case PopUpTypes.Insert:

                            this.lblPopUp_ActividadGeneralTitle.Text = "Nueva Actividad General";
                            this.btnPopUp_ActividadGeneralCommand.Text = "Agregar Actividad General";
                            break;

                        case PopUpTypes.Update:

                            this.lblPopUp_ActividadGeneralTitle.Text = "Edición de Actividad General";
                            this.btnPopUp_ActividadGeneralCommand.Text = "Actualizar Actividad General";
                            SelectGiraConfiguracion_ActividadGeneral_ForEdit(idItem);
                            break;

                        default:
                            throw (new Exception("Opción inválida"));
                    }

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ActividadGeneral(){
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
                    oENTGira.TipoGiraConfiguracionId = 4; // Actividad General
                    oENTGira.ConfiguracionGrupo = this.ddlAgrupacion_ActividadGeneral.SelectedItem.Text;
                    oENTGira.ConfiguracionHoraInicio = this.wucPopUp_ActividadGeneralTimerDesde.DisplayUTCTime;
                    oENTGira.ConfiguracionHoraFin = this.wucPopUp_ActividadGeneralTimerHasta.DisplayUTCTime;
                    oENTGira.ConfiguracionDetalle = this.txtPopUp_ActividadGeneralDetalle.Text.Trim();
                    oENTGira.HelipuertoLugar = "";
                    oENTGira.HelipuertoDomicilio = "";
                    oENTGira.HelipuertoCoordenadas = "";
                    oENTGira.ConfiguracionActivo = 1;

                    // Transacción
                    oENTResponse = oBPGira.UpdateGiraConfiguracion(oENTGira);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ActividadGeneralPanel();

                    // Actualizar formulario
                    SelectGira();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Configuración actualizada con éxito!');", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void ValidatePopUp_ActividadGeneralForm(){
                String ErrorDetailHour = "";

                try
                {
                
                    if (this.txtPopUp_ActividadGeneralDetalle.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                    if (!this.wucPopUp_ActividadGeneralTimerDesde.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora de inicio del evento] es requerido: " + ErrorDetailHour); }
                    if (!this.wucPopUp_ActividadGeneralTimerHasta.IsValidTime(ref ErrorDetailHour)) { throw new Exception("El campo [Hora final del evento] es requerido: " + ErrorDetailHour); }
                    if (this.txtOtraAgrupacion_ActividadGeneral.Enabled) { throw (new Exception("El campo [Agrupación] es requerido")); }
                    if (this.ddlAgrupacion_ActividadGeneral.SelectedItem.Value == "-1") { throw (new Exception("El campo [Agrupación] es requerido")); }

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ActividadGeneralCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    ValidatePopUp_ActividadGeneralForm();

                    // Determinar acción
                    if (this.hddGiraConfiguracionId.Value == "0"){

                        InsertConfiguracion_ActividadGeneral();
                    }else{

                        UpdateConfiguracion_ActividadGeneral();
                    }

                }catch (Exception ex){
                    this.lblPopUp_ActividadGeneralMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUp_ActividadGeneralDetalle.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ActividadGeneral_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ActividadGeneralPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }
            
            
            // Eventos del control de agrupación
            
            protected void btnNuevaAgrupacion_ActividadGeneral_Click(object sender, EventArgs e){
                DataTable dtAgrupacion;

                try
                {

                    // Nueva agrupación
                    dtAgrupacion = NuevaAgrupacion(this.txtOtraAgrupacion_ActividadGeneral.Text.Trim());

                    // Actualizar combo
                    this.ddlAgrupacion_ActividadGeneral.Items.Clear();
                    this.ddlAgrupacion_ActividadGeneral.DataTextField = "Agrupacion";
                    this.ddlAgrupacion_ActividadGeneral.DataValueField = "Row";
                    this.ddlAgrupacion_ActividadGeneral.DataSource = dtAgrupacion;
                    this.ddlAgrupacion_ActividadGeneral.DataBind();

                    // Seleccionar el item deseado
                    this.ddlAgrupacion_ActividadGeneral.SelectedValue = dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion_ActividadGeneral.Text.Trim() + "'")[0]["Row"].ToString();
                    this.AgrupacionKey.Value = this.ddlAgrupacion_ActividadGeneral.SelectedValue;

                    // Estado inicial
                    this.txtOtraAgrupacion_ActividadGeneral.Text = "";
                    this.txtOtraAgrupacion_ActividadGeneral.Enabled = false;
                    this.btnNuevaAgrupacion_ActividadGeneral.Enabled = false;

                    this.txtOtraAgrupacion_ActividadGeneral.CssClass = "Textbox_Disabled";
                    this.btnNuevaAgrupacion_ActividadGeneral.CssClass = "Button_Special_Gray";

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_ActividadGeneral.ClientID + "');", true);

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
		    }

            protected void ddlAgrupacion_ActividadGeneral_SelectedIndexChanged(object sender, EventArgs e){
                try
                {

                    if( this.ddlAgrupacion_ActividadGeneral.SelectedItem.Value == "-1" ){

                        this.txtOtraAgrupacion_ActividadGeneral.Text = "";
                        this.txtOtraAgrupacion_ActividadGeneral.Enabled = true;
                        this.btnNuevaAgrupacion_ActividadGeneral.Enabled = true;

                        this.txtOtraAgrupacion_ActividadGeneral.CssClass = "Textbox_General";
                        this.btnNuevaAgrupacion_ActividadGeneral.CssClass = "Button_Special_Blue";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtOtraAgrupacion_ActividadGeneral.ClientID + "');", true);
                    }else{

                        this.txtOtraAgrupacion_ActividadGeneral.Text = "";
                        this.txtOtraAgrupacion_ActividadGeneral.Enabled = false;
                        this.btnNuevaAgrupacion_ActividadGeneral.Enabled = false;

                        this.txtOtraAgrupacion_ActividadGeneral.CssClass = "Textbox_Disabled";
                        this.btnNuevaAgrupacion_ActividadGeneral.CssClass = "Button_Special_Gray";

                        this.AgrupacionKey.Value = this.ddlAgrupacion_ActividadGeneral.SelectedValue;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion_ActividadGeneral.ClientID + "');", true);
                    }

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
                }
            }

            
        #endregion
        
        #region PopUp - Lugar de Evento
            
            
            // Rutinas

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
            
            
            // Eventos

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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoLugar.ClientID + "');", true);
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUp_EventoLugar.ClientID + "');", true);
                }
            }

        #endregion


    }
}