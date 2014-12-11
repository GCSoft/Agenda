/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatMenu
' Autor:	Ruben.Cobos
' Fecha:	27-Octubre-2013
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

namespace Agenda.Web.Application.WebApp.Private.SysCat
{
    public partial class scatMenu : BPPage
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum MenuPopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertMenu(){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTMenu.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTMenu.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTMenu.Rank = Int16.Parse(this.txtPopUpRank.Text);

                // Transacción
                oENTResponse = oBPMenu.InsertMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectMenu();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Menú creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMenu(){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();
            String MessageDB = "";

            try
            {

                // Formulario
                oENTMenu.Nombre = this.txtNombre.Text;
                oENTMenu.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPMenu.SelectMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                this.gvMenu.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvMenu.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMenu_ForEdit(Int32 MenuId){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.MenuId = MenuId;
                oENTMenu.Nombre = "";
                oENTMenu.Activo = 2;

                // Transacción
                oENTResponse = oBPMenu.SelectMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.txtPopUpRank.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Rank"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectStatus(){
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

        void SelectStatus_PopUp(){
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

        void UpdateMenu(Int32 MenuId){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.MenuId = MenuId;
                oENTMenu.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTMenu.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTMenu.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTMenu.Rank = Int16.Parse(this.txtPopUpRank.Text);

                // Transacción
                oENTResponse = oBPMenu.UpdateMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectMenu();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateMenu_Estatus(Int32 MenuId, MenuPopUpTypes MenuPopUpType){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.MenuId = MenuId;
                switch (MenuPopUpType)
                {
                    case MenuPopUpTypes.Delete:
                        oENTMenu.Activo = 0;
                        break;
                    case MenuPopUpTypes.Reactivate:
                        oENTMenu.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPMenu.UpdateMenu_Estatus(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectMenu();

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
                this.txtPopUpDescripcion.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddMenu.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(MenuPopUpTypes MenuPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddMenu.Value = idItem.ToString();

                // Detalle de acción
                switch (MenuPopUpType)
                {
                    case MenuPopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Menú";
                        this.btnPopUpCommand.Text = "Crear Menú";

                        break;

                    case MenuPopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Menú";
                        this.btnPopUpCommand.Text = "Actualizar Menú";
                        SelectMenu_ForEdit(idItem);
                        break;

                    default:
                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {

                // Nombre
                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }

                // Estatus
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
                SelectStatus_PopUp();
                SelectStatus();
                SelectMenu();

                // Estado inicial del formulario
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
                SelectMenu();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(MenuPopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgPopUp = null;

            String MenuId = "";
            String NombreMenu = "";
            String Activo = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgPopUp = (ImageButton)e.Row.FindControl("imgPopUp");

                // Datakeys
                MenuId = this.gvMenu.DataKeys[e.Row.RowIndex]["MenuId"].ToString();
                Activo = this.gvMenu.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreMenu = this.gvMenu.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Menú [" + NombreMenu + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Menú [" + NombreMenu + "]";
                imgPopUp.Attributes.Add("title", sTootlTip);

                // Imagen del botón [imgPopUp]
                imgPopUp.ImageUrl = "../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png";

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgPopUp.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + "_Over.png';";

                // Puntero y Sombra en fila Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgPopUp.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png';";

                // Puntero y Sombra en fila Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvMenu_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 MenuId = 0;

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
                MenuId = Int32.Parse(this.gvMenu.DataKeys[intRow]["MenuId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvMenu.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(MenuPopUpTypes.Update, MenuId);
                        break;
                    case "Eliminar":
                        UpdateMenu_Estatus(MenuId, MenuPopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateMenu_Estatus(MenuId, MenuPopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvMenu_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvMenu, ref this.hddSort, e.SortExpression);

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
                if (this.hddMenu.Value == "0"){

                    InsertMenu();
                }else{

                    UpdateMenu(Int32.Parse(this.hddMenu.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);
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