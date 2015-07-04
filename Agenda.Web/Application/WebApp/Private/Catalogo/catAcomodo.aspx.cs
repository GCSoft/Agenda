/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	catAcomodo
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
    public partial class catAcomodo : BPPage
    {
        

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertAcomodo(){
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTTipoAcomodo.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTTipoAcomodo.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTTipoAcomodo.Rank = 1;

                // Transacción
                oENTResponse = oBPTipoAcomodo.InsertTipoAcomodo(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectAcomodo_Paginado( true );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Tipo de Acomodo creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

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

        void SelectAcomodo_Paginado( Boolean Restart ){
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();
            String MessageDB = "";

            try
            {

                // Si se reinicia la consulta posicionarse en la página 1
                if ( Restart ) { this.lblPage.Text = "1"; }

                // Formulario
                oENTTipoAcomodo.Nombre = this.txtNombre.Text;
                oENTTipoAcomodo.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Paginación
                oENTTipoAcomodo.Paginacion.Page = Int32.Parse( this.lblPage.Text );
                oENTTipoAcomodo.Paginacion.PageSize = Int32.Parse( this.lblPageSize.Text );

                // Transacción
                oENTResponse = oBPTipoAcomodo.SelectTipoAcomodo_Paginado(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                if (oENTResponse.DataSetResponse.Tables[1].Rows.Count == 0) {

                    this.pnlPaginado.Visible = false;
                }else{

                    this.pnlPaginado.Visible = true;
                    this.lblPages.Text = Math.Ceiling( Double.Parse( oENTResponse.DataSetResponse.Tables[2].Rows[0]["TotalRows"].ToString() ) / Double.Parse( this.PageSize.ToString() ) ).ToString();
                }

                this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvAcomodo.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectAcomodo_ForEdit(Int32 TipoAcomodoId){
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.TipoAcomodoId = TipoAcomodoId;
                oENTTipoAcomodo.Nombre = "";
                oENTTipoAcomodo.Activo = 2;

                // Transacción
                oENTResponse = oBPTipoAcomodo.SelectTipoAcomodo(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateAcomodo(Int32 TipoAcomodoId){
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.TipoAcomodoId = TipoAcomodoId;
                oENTTipoAcomodo.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTTipoAcomodo.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTTipoAcomodo.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTTipoAcomodo.Rank = 1;

                // Transacción
                oENTResponse = oBPTipoAcomodo.UpdateTipoAcomodo(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectAcomodo_Paginado( false );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateAcomodo_Estatus(Int32 TipoAcomodoId, PopUpTypes AcomodoPopUpType){
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.TipoAcomodoId = TipoAcomodoId;
                switch (AcomodoPopUpType)
                {
                    case PopUpTypes.Delete:
                        oENTTipoAcomodo.Activo = 0;
                        break;
                    case PopUpTypes.Reactivate:
                        oENTTipoAcomodo.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPTipoAcomodo.UpdateTipoAcomodo_Estatus(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectAcomodo_Paginado( false );

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
                this.hddAcomodo.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes AcomodoPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddAcomodo.Value = idItem.ToString();

                // Detalle de acción
                switch (AcomodoPopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Tipo de Acomodo";
                        this.btnPopUpCommand.Text = "Crear Tipo de Acomodo";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Tipo de Acomodo";
                        this.btnPopUpCommand.Text = "Actualizar Tipo de Acomodo";
                        SelectAcomodo_ForEdit(idItem);
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

                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }
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

                // Configuración de paginado
                this.lblPageSize.Text = this.PageSize.ToString();
                this.pnlPaginado.Visible = false;

                // Llenado de controles
                SelectEstatus_PopUp();
                SelectEstatus();

                // Estado inicial del formulario
                SelectAcomodo_Paginado( true );
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
                SelectAcomodo_Paginado( true );

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

        protected void gvAcomodo_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String TipoAcomodoId = "";
            String NombreAcomodo = "";
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
                TipoAcomodoId = this.gvAcomodo.DataKeys[e.Row.RowIndex]["TipoAcomodoId"].ToString();
                Activo = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreAcomodo = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Tipo de Acomodo [" + NombreAcomodo + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Tipo de Acomodo [" + NombreAcomodo + "]";
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

        protected void gvAcomodo_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 TipoAcomodoId = 0;

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
                TipoAcomodoId = Int32.Parse(this.gvAcomodo.DataKeys[intRow]["TipoAcomodoId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvAcomodo.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, TipoAcomodoId);
                        break;
                    case "Eliminar":
                        UpdateAcomodo_Estatus(TipoAcomodoId, PopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateAcomodo_Estatus(TipoAcomodoId, PopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvAcomodo_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvAcomodo, ref this.hddSort, e.SortExpression);

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
                if (this.hddAcomodo.Value == "0"){

                    InsertAcomodo();
                }else{

                    UpdateAcomodo(Int32.Parse(this.hddAcomodo.Value));
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


        // Eventos del paginado

        protected void GridView_SelectPage(object sender, CommandEventArgs e){
            try
            {

                switch( e.CommandName ){

                    case "FirstPage":
                        this.lblPage.Text = "1";
                        break;

                    case "LastPage":
                        this.lblPage.Text = Int32.Parse ( "0" + this.lblPages.Text ).ToString();
                        break;

                    case "NextPage":
                        this.lblPage.Text = (Int32.Parse( "0" + this.lblPage.Text ) + 1).ToString();
                        if ( Int32.Parse( this.lblPage.Text ) > Int32.Parse( this.lblPages.Text ) ) { this.lblPage.Text = this.lblPages.Text; }
                        break;

                    case "PreviousPage":
                        this.lblPage.Text = ( Int32.Parse("0" + this.lblPage.Text ) - 1).ToString();
                        if ( this.lblPage.Text == "0" ) { this.lblPage.Text = "1"; }
                        break;
                }

                SelectAcomodo_Paginado( false );

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }


    }
}