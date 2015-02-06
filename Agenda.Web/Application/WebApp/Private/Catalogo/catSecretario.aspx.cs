/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	catSecretario
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
    public partial class catSecretario : BPPage
    {
       
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertSecretario(){
            ENTSecretario oENTSecretario = new ENTSecretario();
            ENTResponse oENTResponse = new ENTResponse();

            BPSecretario oBPSecretario = new BPSecretario();

            try
            {

                // Formulario
                oENTSecretario.TituloId = Int16.Parse(this.ddlPopUpTitulo.SelectedValue);
                oENTSecretario.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTSecretario.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTSecretario.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTSecretario.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTSecretario.Rank = 1;

                // Transacción
                oENTResponse = oBPSecretario.InsertSecretario(oENTSecretario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectSecretario_Paginado( true );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Secretario creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

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

        void SelectSecretario_Paginado( Boolean Restart ){
            ENTSecretario oENTSecretario = new ENTSecretario();
            ENTResponse oENTResponse = new ENTResponse();

            BPSecretario oBPSecretario = new BPSecretario();
            String MessageDB = "";

            try
            {

                // Si se reinicia la consulta posicionarse en la página 1
                if ( Restart ) { this.lblPage.Text = "1"; }

                // Formulario
                oENTSecretario.Nombre = this.txtNombre.Text;
                oENTSecretario.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Paginación
                oENTSecretario.Paginacion.Page = Int32.Parse( this.lblPage.Text );
                oENTSecretario.Paginacion.PageSize = Int32.Parse( this.lblPageSize.Text );

                // Transacción
                oENTResponse = oBPSecretario.SelectSecretario_Paginado(oENTSecretario);

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

                this.gvSecretario.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvSecretario.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectSecretario_ForEdit(Int32 SecretarioId){
            ENTSecretario oENTSecretario = new ENTSecretario();
            ENTResponse oENTResponse = new ENTResponse();

            BPSecretario oBPSecretario = new BPSecretario();

            try
            {

                // Formulario
                oENTSecretario.SecretarioId = SecretarioId;
                oENTSecretario.Nombre = "";
                oENTSecretario.Activo = 2;

                // Transacción
                oENTResponse = oBPSecretario.SelectSecretario(oENTSecretario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.ddlPopUpTitulo.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["TituloId"].ToString();
                this.txtPopUpPuesto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Puesto"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.ckePopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTitulo_PopUp(){
            ENTTitulo oENTTitulo = new ENTTitulo();
            ENTResponse oENTResponse = new ENTResponse();

            BPTitulo oBPTitulo = new BPTitulo();

            try
            {

                // Formulario
                oENTTitulo.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPTitulo.SelectTitulo(oENTTitulo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPopUpTitulo.DataTextField = "Nombre";
                this.ddlPopUpTitulo.DataValueField = "TituloId";
                this.ddlPopUpTitulo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUpTitulo.DataBind();

                // Agregar Item de selección
                this.ddlPopUpTitulo.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateSecretario(Int32 SecretarioId){
            ENTSecretario oENTSecretario = new ENTSecretario();
            ENTResponse oENTResponse = new ENTResponse();

            BPSecretario oBPSecretario = new BPSecretario();

            try
            {

                // Formulario
                oENTSecretario.SecretarioId = SecretarioId;
                oENTSecretario.TituloId = Int16.Parse(this.ddlPopUpTitulo.SelectedValue);
                oENTSecretario.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTSecretario.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTSecretario.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTSecretario.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTSecretario.Rank = 1;

                // Transacción
                oENTResponse = oBPSecretario.UpdateSecretario(oENTSecretario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectSecretario_Paginado( false );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateSecretario_Estatus(Int32 SecretarioId, PopUpTypes SecretarioPopUpType){
            ENTSecretario oENTSecretario = new ENTSecretario();
            ENTResponse oENTResponse = new ENTResponse();

            BPSecretario oBPSecretario = new BPSecretario();

            try
            {

                // Formulario
                oENTSecretario.SecretarioId = SecretarioId;
                switch (SecretarioPopUpType)
                {
                    case PopUpTypes.Delete:
                        oENTSecretario.Activo = 0;
                        break;
                    case PopUpTypes.Reactivate:
                        oENTSecretario.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPSecretario.UpdateSecretario_Estatus(oENTSecretario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectSecretario_Paginado( false );

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
                this.ddlPopUpTitulo.SelectedIndex = 0;
                this.txtPopUpPuesto.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;
                this.ckePopUpDescripcion.Text = "";

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddSecretario.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes SecretarioPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddSecretario.Value = idItem.ToString();

                // Detalle de acción
                switch (SecretarioPopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Secretario";
                        this.btnPopUpCommand.Text = "Crear Secretario";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Secretario";
                        this.btnPopUpCommand.Text = "Actualizar Secretario";
                        SelectSecretario_ForEdit(idItem);
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
                if (this.ddlPopUpTitulo.SelectedIndex == 0) { throw new Exception("* El campo [Título] es requerido"); }
                if (this.txtPopUpPuesto.Text.Trim() == "") { throw new Exception("* El campo [Puesto] es requerido"); }
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
                SelectTitulo_PopUp();

                // Estado inicial del formulario
                SelectSecretario_Paginado( true );
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
                SelectSecretario_Paginado( true );

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

        protected void gvSecretario_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String SecretarioId = "";
            String NombreSecretario = "";
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
                SecretarioId = this.gvSecretario.DataKeys[e.Row.RowIndex]["SecretarioId"].ToString();
                Activo = this.gvSecretario.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreSecretario = this.gvSecretario.DataKeys[e.Row.RowIndex]["TituloNombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Secretario [" + NombreSecretario + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Secretario [" + NombreSecretario + "]";
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

        protected void gvSecretario_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 SecretarioId = 0;

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
                SecretarioId = Int32.Parse(this.gvSecretario.DataKeys[intRow]["SecretarioId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvSecretario.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, SecretarioId);
                        break;
                    case "Eliminar":
                        UpdateSecretario_Estatus(SecretarioId, PopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateSecretario_Estatus(SecretarioId, PopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvSecretario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvSecretario, ref this.hddSort, e.SortExpression);

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
                if (this.hddSecretario.Value == "0"){

                    InsertSecretario();
                }else{

                    UpdateSecretario(Int32.Parse(this.hddSecretario.Value));
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

                SelectSecretario_Paginado( false );

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

    }
}