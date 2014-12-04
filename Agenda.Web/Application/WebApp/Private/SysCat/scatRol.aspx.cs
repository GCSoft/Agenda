/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatRol
' Autor:    Ruben.Cobos
' Fecha:    27-Octubre-2013
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
    public partial class scatRol : BPPage
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Enumeraciones
        enum RolPopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertRol(){
            ENTRol oENTRol = new ENTRol();
            ENTResponse oENTResponse = new ENTResponse();

            BPRol oBPRol = new BPRol();

            DataTable tblSelected;
            DataRow rowSelected;

            try
            {

                // Formulario
                oENTRol.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTRol.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTRol.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);

                // Listado de SubMenús asociados
                tblSelected = (DataTable)this.ViewState["DataTableSubMenu"];
                oENTRol.DataTableSubMenu = new DataTable("DataTableSubMenu");
                oENTRol.DataTableSubMenu.Columns.Add("SubMenuId", typeof(Int32));

                foreach (DataRow rowCurrentSelected in tblSelected.Select("Seleccionado = 1")){
                    rowSelected = oENTRol.DataTableSubMenu.NewRow();
                    rowSelected["SubMenuId"] = rowCurrentSelected["SubMenuId"];
                    oENTRol.DataTableSubMenu.Rows.Add(rowSelected);
                }

                // Transacción
                oENTResponse = oBPRol.InsertRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectRol();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Rol creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMenu_PopUp(){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.Activo = 1;

                // Transacción
                oENTResponse = oBPMenu.SelectMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPopUpMenu.DataTextField = "Nombre";
                this.ddlPopUpMenu.DataValueField = "MenuId";
                this.ddlPopUpMenu.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUpMenu.DataBind();

                // Agregar Item de selección
                this.ddlPopUpMenu.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectRol(){
            ENTRol oENTRol = new ENTRol();
            ENTResponse oENTResponse = new ENTResponse();

            BPRol oBPRol = new BPRol();
            String MessageDB = "";

            try
            {

                // Formulario
                oENTRol.Nombre = this.txtNombre.Text;
                oENTRol.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPRol.SelectRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                this.gvRol.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvRol.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectRol_ForEdit(Int32 RolId){
            ENTRol oENTRol = new ENTRol();
            ENTResponse oENTResponse = new ENTResponse();

            BPRol oBPRol = new BPRol();

            DataTable DataTableSubMenu = null;

            try
            {

                // Formulario
                oENTRol.RolId = RolId;
                oENTRol.Nombre = "";
                oENTRol.Activo = 2;

                // Transacción
                oENTResponse = oBPRol.SelectRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.ddlPopUpMenu.SelectedIndex = 0;

                // SubMenus asociados al Rol
                DataTableSubMenu = (DataTable)this.ViewState["DataTableSubMenu"];
                foreach (DataRow rowAssociated in oENTResponse.DataSetResponse.Tables[3].Rows)
                {
                    if (DataTableSubMenu.Select("SubMenuId = " + rowAssociated["SubMenuId"]).Length == 1)
                    {
                        DataTableSubMenu.Select("SubMenuId = " + rowAssociated["SubMenuId"])[0]["Seleccionado"] = "1";
                    }
                }

                // Actualizar ViewState
                this.ViewState["DataTableSubMenu"] = DataTableSubMenu;
                this.ViewState["DataTableSubMenu_Filtered"] = this.ViewState["DataTableSubMenu"];

                // Llenado de controles
                this.gvPopUpSubMenu.DataSource = DataTableSubMenu;
                this.gvPopUpSubMenu.DataBind();

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

        void SelectSubMenu_ForViewState(){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

            BPSubMenu oBPSubMenu = new BPSubMenu();

            DataTable DataTableSubMenu = null;
            DataRow rowSubMenu = null;

            try
            {

                // Información de Sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTSubMenu.RolId = oENTSession.RolId;
                oENTSubMenu.SubMenuId = 0;
                oENTSubMenu.MenuId = 0;
                oENTSubMenu.Nombre = "";
                oENTSubMenu.Activo = 1;

                // Transacción
                oENTResponse = oBPSubMenu.SelectSubMenu(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');", true); }

                // Definición del DataTable
                DataTableSubMenu = new DataTable("DataTableSubMenu");
                DataTableSubMenu.Columns.Add("MenuId", typeof(Int32));
                DataTableSubMenu.Columns.Add("SubMenuId", typeof(Int32));
                DataTableSubMenu.Columns.Add("NombreMenu", typeof(String));
                DataTableSubMenu.Columns.Add("NombreSubMenu", typeof(String));
                DataTableSubMenu.Columns.Add("Seleccionado", typeof(Int32));
                DataTableSubMenu.Columns.Add("Requerido", typeof(Int32));

                // Llenado de DataTable
                foreach (DataRow rowCurrentSubMenu in oENTResponse.DataSetResponse.Tables[1].Rows){

                    rowSubMenu = DataTableSubMenu.NewRow();
                    rowSubMenu["MenuId"] = rowCurrentSubMenu["MenuId"];
                    rowSubMenu["SubMenuId"] = rowCurrentSubMenu["SubMenuId"];
                    rowSubMenu["NombreMenu"] = rowCurrentSubMenu["NombreMenu"];
                    rowSubMenu["NombreSubMenu"] = rowCurrentSubMenu["NombreSubMenu"];
                    rowSubMenu["Seleccionado"] = rowCurrentSubMenu["Requerido"];
                    rowSubMenu["Requerido"] = rowCurrentSubMenu["Requerido"];
                    DataTableSubMenu.Rows.Add(rowSubMenu);
                }

                // Almacenar en ViewState
                this.ViewState["DataTableSubMenu"] = DataTableSubMenu;
                this.ViewState["DataTableSubMenu_Filtered"] = this.ViewState["DataTableSubMenu"];

                // Llenado de controles
                this.gvPopUpSubMenu.DataSource = DataTableSubMenu;
                this.gvPopUpSubMenu.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateRol(Int32 RolId){
            ENTRol oENTRol = new ENTRol();
            ENTResponse oENTResponse = new ENTResponse();

            BPRol oBPRol = new BPRol();

            DataTable tblSelected;
            DataRow rowSelected;

            try
            {

                // Formulario
                oENTRol.RolId = RolId;
                oENTRol.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTRol.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTRol.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);

                // Listado de SubMenús asociados
                tblSelected = (DataTable)this.ViewState["DataTableSubMenu"];
                oENTRol.DataTableSubMenu = new DataTable("DataTableSubMenu");
                oENTRol.DataTableSubMenu.Columns.Add("SubMenuId", typeof(Int32));

                foreach (DataRow rowCurrentSelected in tblSelected.Select("Seleccionado = 1")){
                    rowSelected = oENTRol.DataTableSubMenu.NewRow();
                    rowSelected["SubMenuId"] = rowCurrentSelected["SubMenuId"];
                    oENTRol.DataTableSubMenu.Rows.Add(rowSelected);
                }

                // Transacción
                oENTResponse = oBPRol.UpdateRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectRol();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Rol actualizado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateRol_Estatus(Int32 RolId, RolPopUpTypes RolPopUpType){
            ENTRol oENTRol = new ENTRol();
            ENTResponse oENTResponse = new ENTResponse();

            BPRol oBPRol = new BPRol();

            try
            {

                // Formulario
                oENTRol.RolId = RolId;
                switch (RolPopUpType){
                    case RolPopUpTypes.Delete:
                        oENTRol.Activo = 0;
                        break;
                    case RolPopUpTypes.Reactivate:
                        oENTRol.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPRol.UpdateRol_Estatus(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectRol();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp

        void ClearPopUpPanel(){
            DataTable DataTableSubMenu = null;

            try
            {

                // Limpiar formulario
                this.txtPopUpNombre.Text = "";
                this.txtPopUpDescripcion.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;
                this.ddlPopUpMenu.SelectedIndex = 0;

                // Limpiar grid
                if (this.ViewState["DataTableSubMenu"] != null){

                    DataTableSubMenu = (DataTable)this.ViewState["DataTableSubMenu"];
                    for (int i = 1; i < DataTableSubMenu.Rows.Count; i++) { DataTableSubMenu.Rows[i]["Seleccionado"] = DataTableSubMenu.Rows[i]["Requerido"]; }
                    this.ViewState["DataTableSubMenu"] = DataTableSubMenu;
                    this.ViewState["DataTableSubMenu_Filtered"] = DataTableSubMenu;
                }

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddRol.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(RolPopUpTypes RolPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddRol.Value = idItem.ToString();

                // Detalle de acción
                switch (RolPopUpType){

                    case RolPopUpTypes.Insert:

                        this.lblPopUpTitle.Text = "Nuevo Rol";
                        this.btnPopUpCommand.Text = "Crear Rol";
                        break;

                    case RolPopUpTypes.Update:

                        this.lblPopUpTitle.Text = "Edición de Rol";
                        this.btnPopUpCommand.Text = "Actualizar Rol";
                        SelectRol_ForEdit(idItem);
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
                SelectStatus();
                SelectStatus_PopUp();
                SelectRol();
                SelectMenu_PopUp();
                SelectSubMenu_ForViewState();

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
                SelectRol();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(RolPopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void chkPopUpIncluir_Changed(object sender, EventArgs e){
            GridViewRow oGridViewRow = null;
            CheckBox oCheckBox = null;
            String SubMenuId = "";

            DataTable DataTableSubMenu = (DataTable)this.ViewState["DataTableSubMenu"];
            DataTable DataTableSubMenu_Filtered = (DataTable)this.ViewState["DataTableSubMenu_Filtered"];

            try
            {

                // Controles
                oCheckBox = (CheckBox)sender;
                oGridViewRow = (GridViewRow)oCheckBox.NamingContainer;

                // Datakeys
                SubMenuId = this.gvPopUpSubMenu.DataKeys[oGridViewRow.RowIndex]["SubMenuId"].ToString();

                // Actualizar DataTables
                DataTableSubMenu.Select("SubMenuId=" + SubMenuId)[0]["Seleccionado"] = (oCheckBox.Checked ? 1 : 0);
                DataTableSubMenu_Filtered.Select("SubMenuId=" + SubMenuId)[0]["Seleccionado"] = (oCheckBox.Checked ? 1 : 0);

                // Actualizar ViewState
                this.ViewState["DataTableSubMenu"] = DataTableSubMenu;
                this.ViewState["DataTableSubMenu_Filtered"] = DataTableSubMenu_Filtered;

                // Actualizar grid
                this.gvPopUpSubMenu.DataSource = DataTableSubMenu_Filtered;
                this.gvPopUpSubMenu.DataBind();

                // Foco al checkbox pulsado
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + oCheckBox.ClientID + "');", true);

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);
            }
        }

        protected void ddlPopUpMenu_SelectedIndexChanged(object sender, EventArgs e){
            DataTable DataTableSubMenu = (DataTable)this.ViewState["DataTableSubMenu"];
            DataTable DataTableSubMenu_Filtered = (DataTable)this.ViewState["DataTableSubMenu_Filtered"];

            DataRow rowSubMenu_Filtered = null;

            try
            {

                // Depurar DataTable de filtrado
                DataTableSubMenu_Filtered.Rows.Clear();

                // Filtrar
                if (this.ddlPopUpMenu.SelectedIndex == 0){

                    DataTableSubMenu_Filtered = DataTableSubMenu;

                }else{

                    foreach (DataRow rowCurrentSubMenu in DataTableSubMenu.Select("MenuId=" + this.ddlPopUpMenu.SelectedValue)){
                        rowSubMenu_Filtered = DataTableSubMenu_Filtered.NewRow();
                        rowSubMenu_Filtered["MenuId"] = rowCurrentSubMenu["MenuId"];
                        rowSubMenu_Filtered["SubMenuId"] = rowCurrentSubMenu["SubMenuId"];
                        rowSubMenu_Filtered["NombreMenu"] = rowCurrentSubMenu["NombreMenu"];
                        rowSubMenu_Filtered["NombreSubMenu"] = rowCurrentSubMenu["NombreSubMenu"];
                        rowSubMenu_Filtered["Seleccionado"] = rowCurrentSubMenu["Seleccionado"];
                        rowSubMenu_Filtered["Requerido"] = rowCurrentSubMenu["Requerido"];
                        DataTableSubMenu_Filtered.Rows.Add(rowSubMenu_Filtered);
                    }

                }

                // Actualizar ViewState
                this.ViewState["DataTableSubMenu_Filtered"] = DataTableSubMenu_Filtered;

                // Actualizar grid
                this.gvPopUpSubMenu.DataSource = DataTableSubMenu_Filtered;
                this.gvPopUpSubMenu.DataBind();

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                // Filtrar información
                SelectRol();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvRol_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgPopUp = null;

            String RolId = "";
            String NombreRol = "";
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
                RolId = this.gvRol.DataKeys[e.Row.RowIndex]["RolId"].ToString();
                Activo = this.gvRol.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreRol = this.gvRol.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Seguridad
                if (RolId == "1"){

                    imgEdit.Visible = false;
                    imgPopUp.Visible = false;
                }else{

                    // Tooltip Edición
                    sTootlTip = "Editar Rol [" + NombreRol + "]";
                    imgEdit.Attributes.Add("title", sTootlTip);

                    // Tooltip PopUp
                    sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Rol [" + NombreRol + "]";
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
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvRol_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 RolId = 0;

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
                RolId = Int32.Parse(this.gvRol.DataKeys[intRow]["RolId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp"){
                    strCommand = (this.gvRol.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar");
                }

                // Acción
                switch (strCommand){
                    case "Editar":

                        SetPanel(RolPopUpTypes.Update, RolId);
                        break;

                    case "Eliminar":

                        UpdateRol_Estatus(RolId, RolPopUpTypes.Delete);
                        break;

                    case "Reactivar":

                        UpdateRol_Estatus(RolId, RolPopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvRol_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvRol, ref this.hddSort, e.SortExpression);

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
                if (this.hddRol.Value == "0"){

                    InsertRol();
                }else{

                    UpdateRol(Int32.Parse(this.hddRol.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpNombre.ClientID + "');", true);
            }
        }

        protected void gvPopUpSubMenu_RowDataBound(object sender, GridViewRowEventArgs e){
            CheckBox oCheckBox = null;
            String SubMenuId = "";
            String Seleccionado = "";
            String Requerido = "";

             try
            {
                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Datakeys
                SubMenuId = this.gvPopUpSubMenu.DataKeys[e.Row.RowIndex]["SubMenuId"].ToString();
                Seleccionado = this.gvPopUpSubMenu.DataKeys[e.Row.RowIndex]["Seleccionado"].ToString();
                Requerido = this.gvPopUpSubMenu.DataKeys[e.Row.RowIndex]["Requerido"].ToString();

                // CheckBox
                oCheckBox = (CheckBox)e.Row.FindControl("chkPopUpIncluir");

                // Estado de CheckBox y Opciones requeridas
                if (Seleccionado == "1") { oCheckBox.Checked = true; }
                if (Requerido == "1") { oCheckBox.Checked = true; oCheckBox.Enabled = false; }

                // Seguridad
                if (SubMenuId == "1" || SubMenuId == "2") { oCheckBox.Checked = false; oCheckBox.Enabled = false; }

                // Sombra en fila Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp';");

                /// Sombra en fila Out
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp';");

            }catch (Exception ex){
                 throw (ex);
            }
        }

        protected void gvPopUpSubMenu_Sorting(object sender, GridViewSortEventArgs e){
            DataTable DataTableSubMenu_Filtered = (DataTable)this.ViewState["DataTableSubMenu_Filtered"];
            DataView viewSubMenu_Filtered = new DataView(DataTableSubMenu_Filtered);

            try
            {

                // Determinar ordenamiento
                this.hddSortPopUp.Value = (this.hddSortPopUp.Value == e.SortExpression ? e.SortExpression + " DESC" : e.SortExpression);

                // Ordenar vista
                viewSubMenu_Filtered.Sort = this.hddSortPopUp.Value;

                // Vaciar datos
                this.gvPopUpSubMenu.DataSource = viewSubMenu_Filtered;
                this.gvPopUpSubMenu.DataBind();

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