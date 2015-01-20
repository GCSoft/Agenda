/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatLugarEvento
' Autor:	Ruben.Cobos
' Fecha:	08-Enero-2015
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

namespace Agenda.Web.Application.WebApp.Private.Catalogo
{
    public partial class catLugarEvento : BPPage
    {
        

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }

        
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


        // Rutinas del programador

        void InsertLugarEvento(){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Formulario
                oENTLugarEvento.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTLugarEvento.ColoniaId = Int32.Parse(this.hddPopUpColoniaId.Value);
                oENTLugarEvento.Calle = this.txtPopUpCalle.Text.Trim();
                oENTLugarEvento.NumeroExterior = this.txtPopUpNumeroExterior.Text.Trim();
                oENTLugarEvento.NumeroInterior = this.txtPopUpNumeroInterior.Text.Trim();
                oENTLugarEvento.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTLugarEvento.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTLugarEvento.Rank = 1;

                // Transacción
                oENTResponse = oBPLugarEvento.InsertLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectLugarEvento();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Lugar de Evento creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstatus(){
            try
            {

                // Opciones de DropDownList
                this.ddlStatus.Items.Insert(0, new ListItem("[Todos]", "2"));
                this.ddlStatus.Items.Insert(1, new ListItem("Activos", "1"));
                this.ddlStatus.Items.Insert(2, new ListItem("Inactivos", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstatus_PopUp(){
            try
            {

                // Opciones de DropDownList
                this.ddlPopUpStatus.Items.Insert(0, new ListItem("[Seleccione]", "2"));
                this.ddlPopUpStatus.Items.Insert(1, new ListItem("Activo", "1"));
                this.ddlPopUpStatus.Items.Insert(2, new ListItem("Inactivo", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectLugarEvento(){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();
            String MessageDB = "";

            try
            {

                // Formulario
                oENTLugarEvento.LugarEventoId = 0;
                oENTLugarEvento.Nombre = this.txtNombre.Text;
                oENTLugarEvento.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPLugarEvento.SelectLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                this.gvLugarEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvLugarEvento.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectLugarEvento_ForEdit(Int32 LugarEventoId){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Formulario
                oENTLugarEvento.LugarEventoId = LugarEventoId;
                oENTLugarEvento.Nombre = "";
                oENTLugarEvento.Activo = 2;

                // Transacción
                oENTResponse = oBPLugarEvento.SelectLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.ddlPopUpMunicipio.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MunicipioId"].ToString();
                this.txtPopUpColonia.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ColoniaNombre"].ToString();
                this.txtPopUpCalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Calle"].ToString();
                this.txtPopUpNumeroExterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroExterior"].ToString();
                this.txtPopUpNumeroInterior.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NumeroInterior"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.ckePopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();

                // Campos ocultos
                this.hddPopUpColoniaId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ColoniaId"].ToString();

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlPopUpMunicipio.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMunicipio(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMunicipio oENTMunicipio = new ENTMunicipio();

            BPMunicipio oBPMunicipio = new BPMunicipio();

            try
            {

                // Formulario
                oENTMunicipio.EstadoId = 19; //Nuevo León
                oENTMunicipio.MunicipioId = 0;
                oENTMunicipio.Nombre = "";
                oENTMunicipio.Activo = 1;

                // Transacción
                oENTResponse = oBPMunicipio.SelectMunicipio(oENTMunicipio);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo de municipio
                this.ddlPopUpMunicipio.DataTextField = "Nombre";
                this.ddlPopUpMunicipio.DataValueField = "MunicipioId";
                this.ddlPopUpMunicipio.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUpMunicipio.DataBind();

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlPopUpMunicipio.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateLugarEvento(Int32 LugarEventoId){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Formulario
                oENTLugarEvento.LugarEventoId = LugarEventoId;
                oENTLugarEvento.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTLugarEvento.ColoniaId = Int32.Parse(this.hddPopUpColoniaId.Value);
                oENTLugarEvento.Calle = this.txtPopUpCalle.Text.Trim();
                oENTLugarEvento.NumeroExterior = this.txtPopUpNumeroExterior.Text.Trim();
                oENTLugarEvento.NumeroInterior = this.txtPopUpNumeroInterior.Text.Trim();
                oENTLugarEvento.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTLugarEvento.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTLugarEvento.Rank = 1;

                // Transacción
                oENTResponse = oBPLugarEvento.UpdateLugarEvento(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectLugarEvento();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Lugar de Evento actualizado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateLugarEvento_Estatus(Int32 LugarEventoId, PopUpTypes PopUpType){
            ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPLugarEvento oBPLugarEvento = new BPLugarEvento();

            try
            {

                // Formulario
                oENTLugarEvento.LugarEventoId = LugarEventoId;
                switch (PopUpType)
                {
                    case PopUpTypes.Delete:
                        oENTLugarEvento.Activo = 0;
                        break;
                    case PopUpTypes.Reactivate:
                        oENTLugarEvento.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPLugarEvento.UpdateLugarEvento_Estatus(oENTLugarEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectLugarEvento();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp

        void ClearPopUpPanel(){
            try
            {

                // Limpiar formulario
                this.txtPopUpNombre.Text = "";
                this.ddlPopUpMunicipio.SelectedIndex = 0;
                this.txtPopUpColonia.Text = "";
                this.txtPopUpCalle.Text = "";
                this.txtPopUpNumeroExterior.Text = "";
                this.txtPopUpNumeroInterior.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;
                this.ckePopUpDescripcion.Text = "";

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddLugarEvento.Value = "";
                this.hddPopUpColoniaId.Value = "";

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlPopUpMunicipio.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes PopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddLugarEvento.Value = idItem.ToString();

                // Detalle de acción
                switch (PopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Lugar de Evento";
                        this.btnPopUpCommand.Text = "Crear Lugar de Evento";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Lugar de Evento";
                        this.btnPopUpCommand.Text = "Actualizar Lugar de Evento";
                        SelectLugarEvento_ForEdit(idItem);
                        break;

                    default:
                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {

                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
                if (this.hddPopUpColoniaId.Value.Trim() == "" || this.hddPopUpColoniaId.Value.Trim() == "0") { throw new Exception("* Es necesario seleccionar una colonia"); }
                if (this.txtPopUpCalle.Text.Trim() == "") { throw new Exception("* El campo [Calle] es requerido"); }
                if (this.txtPopUpNumeroExterior.Text.Trim() == "") { throw new Exception("* El campo [Número Exterior] es requerido"); }
                if (this.ddlPopUpStatus.SelectedIndex == 0) { throw new Exception("* El campo [Estatus] es requerido"); }

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
                SelectEstatus();
                SelectEstatus_PopUp();
                SelectMunicipio();

                // Estado inicial del formulario
                this.gvLugarEvento.DataSource = null;
                this.gvLugarEvento.DataBind();
                ClearPopUpPanel();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                // Filtrar información
                SelectLugarEvento();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvLugarEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String LugarEventoId = "";
            String NombreLugarEvento = "";
            String Activo = "";

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
                LugarEventoId = this.gvLugarEvento.DataKeys[e.Row.RowIndex]["LugarEventoId"].ToString();
                Activo = this.gvLugarEvento.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreLugarEvento = this.gvLugarEvento.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar LugarEvento [" + NombreLugarEvento + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " LugarEvento [" + NombreLugarEvento + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Imagen del botón [imgDelete]
                imgDelete.ImageUrl = "../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png";

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + "_Over.png';";

                // Puntero y Sombra en fila Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png';";

                // Puntero y Sombra en fila Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvLugarEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 LugarEventoId = 0;

            String strCommand = "";
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
                LugarEventoId = Int32.Parse(this.gvLugarEvento.DataKeys[intRow]["LugarEventoId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvLugarEvento.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, LugarEventoId);
                        break;
                    case "Eliminar":
                        UpdateLugarEvento_Estatus(LugarEventoId, PopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateLugarEvento_Estatus(LugarEventoId, PopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvLugarEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvLugarEvento, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }

        }



        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddLugarEvento.Value == "0"){

                    InsertLugarEvento();
                }else{

                    UpdateLugarEvento(Int32.Parse(this.hddLugarEvento.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpNombre.ClientID + "'); }", true);
            }
        }

        protected void ddlPopUpMunicipio_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Limpiado de controles
                this.txtPopUpColonia.Text = "";
                this.hddPopUpColoniaId.Value = "";

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlPopUpMunicipio.SelectedItem.Value;

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpColonia.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPopUpColonia.ClientID + "'); }", true);
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

    }
}