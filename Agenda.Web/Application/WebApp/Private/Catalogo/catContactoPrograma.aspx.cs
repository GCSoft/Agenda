/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	catContactoPrograma
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
    public partial class catContactoPrograma : BPPage
    {
        

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }



        // Rutinas del programador

        void InsertContactoPrograma(){
            ENTContactoPrograma oENTContactoPrograma = new ENTContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            BPContactoPrograma oBPContactoPrograma = new BPContactoPrograma();

            try
            {

                // Formulario
                oENTContactoPrograma.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTContactoPrograma.Correo = this.txtPopUpCorreo.Text.Trim();
                oENTContactoPrograma.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTContactoPrograma.Rank = 1;

                // Transacción
                oENTResponse = oBPContactoPrograma.InsertContactoPrograma(oENTContactoPrograma);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectContactoPrograma_Paginado( true );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Contacto de Programa creado con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

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

        void SelectContactoPrograma_Paginado( Boolean Restart ){
            ENTContactoPrograma oENTContactoPrograma = new ENTContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            BPContactoPrograma oBPContactoPrograma = new BPContactoPrograma();
            String MessageDB = "";

            try
            {

                // Si se reinicia la consulta posicionarse en la página 1
                if ( Restart ) { this.lblPage.Text = "1"; }

                // Formulario
                oENTContactoPrograma.Nombre = this.txtNombre.Text;
                oENTContactoPrograma.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Paginación
                oENTContactoPrograma.Paginacion.Page = Int32.Parse( this.lblPage.Text );
                oENTContactoPrograma.Paginacion.PageSize = Int32.Parse( this.lblPageSize.Text );

                // Transacción
                oENTResponse = oBPContactoPrograma.SelectContactoPrograma_Paginado(oENTContactoPrograma);

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

                this.gvContactoPrograma.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvContactoPrograma.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectContactoPrograma_ForEdit(Int32 ContactoProgramaId){
            ENTContactoPrograma oENTContactoPrograma = new ENTContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            BPContactoPrograma oBPContactoPrograma = new BPContactoPrograma();

            try
            {

                // Formulario
                oENTContactoPrograma.ContactoProgramaId = ContactoProgramaId;
                oENTContactoPrograma.Nombre = "";
                oENTContactoPrograma.Activo = 2;

                // Transacción
                oENTResponse = oBPContactoPrograma.SelectContactoPrograma(oENTContactoPrograma);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpCorreo.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Correo"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateContactoPrograma(Int32 ContactoProgramaId){
            ENTContactoPrograma oENTContactoPrograma = new ENTContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            BPContactoPrograma oBPContactoPrograma = new BPContactoPrograma();

            try
            {

                // Formulario
                oENTContactoPrograma.ContactoProgramaId = ContactoProgramaId;
                oENTContactoPrograma.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTContactoPrograma.Correo = this.txtPopUpCorreo.Text.Trim();
                oENTContactoPrograma.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTContactoPrograma.Rank = 1;

                // Transacción
                oENTResponse = oBPContactoPrograma.UpdateContactoPrograma(oENTContactoPrograma);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectContactoPrograma_Paginado( false );

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateContactoPrograma_Estatus(Int32 ContactoProgramaId, PopUpTypes ContactoProgramaPopUpType){
            ENTContactoPrograma oENTContactoPrograma = new ENTContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            BPContactoPrograma oBPContactoPrograma = new BPContactoPrograma();

            try
            {

                // Formulario
                oENTContactoPrograma.ContactoProgramaId = ContactoProgramaId;
                switch (ContactoProgramaPopUpType)
                {
                    case PopUpTypes.Delete:
                        oENTContactoPrograma.Activo = 0;
                        break;
                    case PopUpTypes.Reactivate:
                        oENTContactoPrograma.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPContactoPrograma.UpdateContactoPrograma_Estatus(oENTContactoPrograma);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectContactoPrograma_Paginado( false );

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
                this.txtPopUpCorreo.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddContactoPrograma.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes ContactoProgramaPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddContactoPrograma.Value = idItem.ToString();

                // Detalle de acción
                switch (ContactoProgramaPopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Contacto de Programa";
                        this.btnPopUpCommand.Text = "Crear Contacto de Programa";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Contacto de Programa";
                        this.btnPopUpCommand.Text = "Actualizar Contacto de Programa";
                        SelectContactoPrograma_ForEdit(idItem);
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
                SelectContactoPrograma_Paginado( true );
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
                SelectContactoPrograma_Paginado( true );

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

        protected void gvContactoPrograma_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String ContactoProgramaId = "";
            String NombreContactoPrograma = "";
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
                ContactoProgramaId = this.gvContactoPrograma.DataKeys[e.Row.RowIndex]["ContactoProgramaId"].ToString();
                Activo = this.gvContactoPrograma.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreContactoPrograma = this.gvContactoPrograma.DataKeys[e.Row.RowIndex]["NombreCorreo"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Contacto de Programa [" + NombreContactoPrograma + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Contacto de Programa [" + NombreContactoPrograma + "]";
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

        protected void gvContactoPrograma_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 ContactoProgramaId = 0;

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
                ContactoProgramaId = Int32.Parse(this.gvContactoPrograma.DataKeys[intRow]["ContactoProgramaId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvContactoPrograma.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, ContactoProgramaId);
                        break;
                    case "Eliminar":
                        UpdateContactoPrograma_Estatus(ContactoProgramaId, PopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateContactoPrograma_Estatus(ContactoProgramaId, PopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }

        protected void gvContactoPrograma_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvContactoPrograma, ref this.hddSort, e.SortExpression);

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
                if (this.hddContactoPrograma.Value == "0"){

                    InsertContactoPrograma();
                }else{

                    UpdateContactoPrograma(Int32.Parse(this.hddContactoPrograma.Value));
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

                SelectContactoPrograma_Paginado( false );

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtNombre.ClientID + "');", true);
            }
        }


    }
}