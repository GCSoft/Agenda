/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatSubMenu
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
    public partial class scatSubMenu : BPPage
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Enumeraciones
        enum SubMenuPopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertSubMenu(){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPSubMenu oBPSubMenu = new BPSubMenu();

            try
            {

                // Formulario
                oENTSubMenu.MenuId = Int32.Parse(this.ddlPopUpMenu.SelectedValue);
                oENTSubMenu.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTSubMenu.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTSubMenu.ASPX = this.txtPopUpPageName.Text.Trim();
                oENTSubMenu.URL = this.txtPopUpURL.Text.Trim();
                oENTSubMenu.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTSubMenu.Rank =  Int16.Parse(this.txtPopUpRank.Text);
                oENTSubMenu.Requerido = Int16.Parse(this.ddlPopUpRequired.SelectedValue);

                // Transacción
                oENTResponse = oBPSubMenu.InsertSubMenu(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectSubMenu();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Menú creado con éxito! Recuerde agregarlo en los roles de los clientes.'); focusControl('" + this.ddlMenu.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectRequired_PopUp(){
            try
            {

                // Opciones de DropDownList
                this.ddlPopUpRequired.Items.Insert(0, new ListItem("[Seleccione]", "2"));
                this.ddlPopUpRequired.Items.Insert(1, new ListItem("Si", "1"));
                this.ddlPopUpRequired.Items.Insert(2, new ListItem("No", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMenu(){
            ENTMenu oENTMenu = new ENTMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPMenu oBPMenu = new BPMenu();

            try
            {

                // Formulario
                oENTMenu.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPMenu.SelectMenu(oENTMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlMenu.DataTextField = "Nombre";
                this.ddlMenu.DataValueField = "MenuId";
                this.ddlMenu.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMenu.DataBind();

                // Agregar Item de selección
                this.ddlMenu.Items.Insert(0, new ListItem("[Todos]", "0"));

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
                oENTMenu.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

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
                this.ddlPopUpMenu.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectSubMenu(){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

            BPSubMenu oBPSubMenu = new BPSubMenu();
            String MessageDB = "";

            try
            {

                // Información de Sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTSubMenu.RolId = oENTSession.RolId;
                oENTSubMenu.SubMenuId = 0;
                oENTSubMenu.MenuId = Int32.Parse(this.ddlMenu.SelectedItem.Value);
                oENTSubMenu.Nombre = this.txtNombre.Text;
                oENTSubMenu.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPSubMenu.SelectSubMenu(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                this.gvSubMenu.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvSubMenu.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectSubMenu_ForEdit(Int32 SubMenuId){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

            BPSubMenu oBPSubMenu = new BPSubMenu();

            try
            {

                // Información de Sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTSubMenu.RolId = oENTSession.RolId;
                oENTSubMenu.SubMenuId = SubMenuId;
                oENTSubMenu.MenuId = 0;
                oENTSubMenu.Nombre = "";
                oENTSubMenu.Activo = 2;

                // Transacción
                oENTResponse = oBPSubMenu.SelectSubMenu(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.ddlPopUpMenu.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MenuId"].ToString();
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NombreSubMenu"].ToString();
                this.txtPopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();
                this.txtPopUpPageName.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ASPX"].ToString();
                this.txtPopUpURL.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["URL"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.txtPopUpRank.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Rank"].ToString();
                this.ddlPopUpRequired.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Requerido"].ToString();

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

        void UpdateSubMenu(Int32 SubMenuId){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPSubMenu oBPSubMenu = new BPSubMenu();

            try
            {

                // Formulario
                oENTSubMenu.SubMenuId = SubMenuId;
                oENTSubMenu.MenuId = Int32.Parse(this.ddlPopUpMenu.SelectedValue);
                oENTSubMenu.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTSubMenu.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTSubMenu.ASPX = this.txtPopUpPageName.Text.Trim();
                oENTSubMenu.URL = this.txtPopUpURL.Text.Trim();
                oENTSubMenu.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTSubMenu.Rank = Int16.Parse(this.txtPopUpRank.Text);
                oENTSubMenu.Requerido = Int16.Parse(this.ddlPopUpRequired.SelectedValue);

                // Transacción
                oENTResponse = oBPSubMenu.UpdateSubMenu(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectSubMenu();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.ddlMenu.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateSubMenu_Estatus(Int32 SubMenuId, SubMenuPopUpTypes SubMenuPopUpType){
            ENTSubMenu oENTSubMenu = new ENTSubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            BPSubMenu oBPSubMenu = new BPSubMenu();

            try
            {

                // Formulario
                oENTSubMenu.SubMenuId = SubMenuId;
                switch (SubMenuPopUpType){
                    case SubMenuPopUpTypes.Delete:

                        oENTSubMenu.Activo = 0;
                        break;

                    case SubMenuPopUpTypes.Reactivate:

                        oENTSubMenu.Activo = 1;
                        break;

                    default:

                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPSubMenu.UpdateSubMenu_Estatus(oENTSubMenu);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectSubMenu();

            }catch (Exception ex){
                throw (ex);
            }
        }

        

        // Rutinas del PopUp

        void ClearPopUpPanel(){
            try
            {

                // Limpiar formulario
                this.ddlPopUpMenu.SelectedIndex = 0;
                this.txtPopUpNombre.Text = "";
                this.txtPopUpDescripcion.Text = "";
                this.txtPopUpPageName.Text = "";
                this.txtPopUpURL.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;
                this.ddlPopUpRequired.SelectedIndex = 0;

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddSubMenu.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(SubMenuPopUpTypes SubMenuPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddSubMenu.Value = idItem.ToString();

                // Detalle de acción
                switch (SubMenuPopUpType){
                    case SubMenuPopUpTypes.Insert:

                        this.lblPopUpTitle.Text = "Nuevo SubMenú";
                        this.btnPopUpCommand.Text = "Crear SubMenú";
                        break;

                    case SubMenuPopUpTypes.Update:

                        this.lblPopUpTitle.Text = "Edición de SubMenú";
                        this.btnPopUpCommand.Text = "Actualizar SubMenú";
                        SelectSubMenu_ForEdit(idItem);
                        break;

                    default:

                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlPopUpMenu.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {

                // Menú
                if (this.ddlPopUpMenu.SelectedIndex == 0) { throw new Exception("* El campo [Menú] es requerido"); }

                // Nombre
                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }

                // ASPX
                if (this.txtPopUpPageName.Text.Trim() == "") { throw new Exception("* El campo [ASPX] es requerido"); }

                // URL
                if (this.txtPopUpURL.Text.Trim() == "") { throw new Exception("* El campo [URL] es requerido"); }

                // Estatus
                if (this.ddlPopUpStatus.SelectedIndex == 0) { throw new Exception("* El campo [Estatus] es requerido"); }

                // Requerido
                if (this.ddlPopUpRequired.SelectedIndex == 0) { throw new Exception("* El campo [Requerido] es requerido"); }

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
                SelectMenu();
                SelectSubMenu();

                SelectStatus_PopUp();
                SelectMenu_PopUp();
                SelectRequired_PopUp();

                // Estado inicial del formulario
                ClearPopUpPanel();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlMenu.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                // Filtrar información
                SelectSubMenu();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(SubMenuPopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }

        protected void gvSubMenu_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgPopUp = null;

            String SubMenuId = "";
            String MenuNombre = "";
            String SubMenuNombre = "";
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
                SubMenuId = this.gvSubMenu.DataKeys[e.Row.RowIndex]["SubMenuId"].ToString();
                Activo = this.gvSubMenu.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                MenuNombre = this.gvSubMenu.DataKeys[e.Row.RowIndex]["NombreMenu"].ToString();
                SubMenuNombre = this.gvSubMenu.DataKeys[e.Row.RowIndex]["NombreSubMenu"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar SubMenú [" + MenuNombre + "/" + SubMenuNombre + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " SubMenú [" + MenuNombre + "/" + SubMenuNombre + "]";
                imgPopUp.Attributes.Add("title", sTootlTip);

                // Imagen del botón [imgPopUp]
                imgPopUp.ImageUrl = "../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png";

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgPopUp.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + "_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgPopUp.ClientID + "').src='../../../../Include/Image/Buttons/" + (Activo == "1" ? "Delete" : "Restore") + ".png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvSubMenu_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 SubMenuId = 0;

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
                SubMenuId = Int32.Parse(this.gvSubMenu.DataKeys[intRow]["SubMenuId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp"){ strCommand = (this.gvSubMenu.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand){
                    case "Editar":

                        SetPanel(SubMenuPopUpTypes.Update, SubMenuId);
                        break;

                    case "Eliminar":

                        UpdateSubMenu_Estatus(SubMenuId, SubMenuPopUpTypes.Delete);
                        break;

                    case "Reactivar":

                        UpdateSubMenu_Estatus(SubMenuId, SubMenuPopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }

        protected void gvSubMenu_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvSubMenu, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }


        
        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddSubMenu.Value == "0"){

                    InsertSubMenu();
                }else{

                    UpdateSubMenu(Int32.Parse(this.hddSubMenu.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlPopUpMenu.ClientID + "');", true);
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlMenu.ClientID + "');", true);
            }
        }



    }
}