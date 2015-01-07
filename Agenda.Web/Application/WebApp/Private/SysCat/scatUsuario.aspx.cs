/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:   scatUsuario
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
    public partial class scatUsuario : BPPage
    {

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Enumeraciones
        enum UsuarioPopUpTypes { Delete, Insert, Reactivate, Update }



        // Funciones recursivas

        DataTable DeleteDataTableRow(DataTable tblData, String sFilterField, String sFilterValue){

            foreach (DataRow oRow in tblData.Rows){
                if (oRow[sFilterField].ToString() == sFilterValue){

                    tblData.Rows.Remove(oRow);
                    DeleteDataTableRow(tblData, sFilterField, sFilterValue);
                    break;
                }
            }

            return tblData;

        }



        // Rutinas del programador

        void InsertUsuario(){
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            BPUsuario oBPUsuario = new BPUsuario();

            try
            {

                // Formulario
                oENTUsuario.RolId = Int32.Parse(this.ddlPopUpRol.SelectedValue);
                oENTUsuario.TituloId = Int32.Parse(this.ddlPopUpTitulo.SelectedValue);
                oENTUsuario.ApellidoMaterno = this.txtPopUpApellidoMaterno.Text.Trim();
                oENTUsuario.ApellidoPaterno = this.txtPopUpApellidoPaterno.Text.Trim();
                oENTUsuario.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTUsuario.Email = this.txtPopUpEmail.Text.Trim();
                oENTUsuario.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTUsuario.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTUsuario.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);

                // Transacción
                oENTResponse = oBPUsuario.InsertUsuario(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectUsuario();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Usuario creado con éxito!. La contraseña fue enviada por correo electrónico. Favor de revisar el correo no deseado.'); focusControl('" + this.ddlRol.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectRol(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTRol oENTRol = new ENTRol();
            ENTSession oENTSession;

            BPRol oBPRol = new BPRol();

            DataTable tblRol;

            try
            {

                // Formulario
                oENTRol.RolId = 0;
                oENTRol.Nombre = "";
                oENTRol.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPRol.SelectRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Seguridad
                oENTSession = (ENTSession)this.Session["oENTSession"];
                tblRol = (oENTSession.RolId != 1 ? DeleteDataTableRow(oENTResponse.DataSetResponse.Tables[1], "RolId", "1") : oENTResponse.DataSetResponse.Tables[1]);

                // Llenado de combo
                this.ddlRol.DataTextField = "Nombre";
                this.ddlRol.DataValueField = "RolId";
                this.ddlRol.DataSource = tblRol;
                this.ddlRol.DataBind();

                // Agregar Item de selección
                this.ddlRol.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectRol_PopUp(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTRol oENTRol = new ENTRol();
            ENTSession oENTSession;

            BPRol oBPRol = new BPRol();

            DataTable tblRol;

            try
            {

                // Estado inicial
                this.lblPopUpMessage.Text = "";

                // Formulario
                oENTRol.RolId = 0;
                oENTRol.Nombre = "";
                oENTRol.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPRol.SelectRol(oENTRol);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { this.lblPopUpMessage.Text = oENTResponse.MessageDB; }

                // Seguridad
                oENTSession = (ENTSession)this.Session["oENTSession"];
                tblRol = (oENTSession.RolId != 1 ? DeleteDataTableRow(oENTResponse.DataSetResponse.Tables[1], "RolId", "1") : oENTResponse.DataSetResponse.Tables[1]);

                // Llenado de combo
                this.ddlPopUpRol.DataTextField = "Nombre";
                this.ddlPopUpRol.DataValueField = "RolId";
                this.ddlPopUpRol.DataSource = tblRol;
                this.ddlPopUpRol.DataBind();

                // Agregar Item de selección
                this.ddlPopUpRol.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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

        void SelectUsuario(){
            ENTSession oENTSession;
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            BPUsuario oBPUsuario = new BPUsuario();

            DataTable tblUsuario;
            String MessageDB = "";

            try
            {

                // Formulario
                oENTUsuario.RolId = Int32.Parse(this.ddlRol.SelectedItem.Value);
                oENTUsuario.Email = this.txtEmail.Text;
                oENTUsuario.Nombre = this.txtNombre.Text;
                oENTUsuario.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Seguridad
                oENTSession = (ENTSession)this.Session["oENTSession"];
                tblUsuario = (oENTSession.RolId != 1 ? DeleteDataTableRow(oENTResponse.DataSetResponse.Tables[1], "RolId", "1") : oENTResponse.DataSetResponse.Tables[1]);

                // Llenado de controles
                this.gvUsuario.DataSource = tblUsuario;
                this.gvUsuario.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectUsuario_ForEdit(Int32 UsuarioId){
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            BPUsuario oBPUsuario = new BPUsuario();

            try
            {

                // Formulario
                oENTUsuario.RolId = 0;
                oENTUsuario.UsuarioId = UsuarioId;
                oENTUsuario.Email = "";
                oENTUsuario.Nombre = "";
                oENTUsuario.Activo = 2;

                // Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.ddlPopUpRol.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["RolId"].ToString();
                this.ddlPopUpTitulo.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["TituloId"].ToString();
                this.txtPopUpPuesto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Puesto"].ToString();
                this.txtPopUpEmail.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Email"].ToString();
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.txtPopUpApellidoPaterno.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ApellidoPaterno"].ToString();
                this.txtPopUpApellidoMaterno.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ApellidoMaterno"].ToString();
                this.txtPopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateUsuario(Int32 UsuarioId){
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            BPUsuario oBPUsuario = new BPUsuario();

            try
            {

                // Formulario
                oENTUsuario.UsuarioId = UsuarioId;
                oENTUsuario.RolId = Int32.Parse(this.ddlPopUpRol.SelectedValue);
                oENTUsuario.TituloId = Int32.Parse(this.ddlPopUpTitulo.SelectedValue);
                oENTUsuario.ApellidoMaterno = this.txtPopUpApellidoMaterno.Text.Trim();
                oENTUsuario.ApellidoPaterno = this.txtPopUpApellidoPaterno.Text.Trim();
                oENTUsuario.Descripcion = this.txtPopUpDescripcion.Text.Trim();
                oENTUsuario.Email = this.txtPopUpEmail.Text.Trim();
                oENTUsuario.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTUsuario.Puesto = this.txtPopUpPuesto.Text.Trim();
                oENTUsuario.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);

                // Transacción
                oENTResponse = oBPUsuario.UpdateUsuario(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectUsuario();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Información actualizada con éxito!'); focusControl('" + this.ddlRol.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateUsuario_Estatus(Int32 UsuarioId, UsuarioPopUpTypes UsuarioPopUpType){
            ENTUsuario oENTUsuario = new ENTUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            BPUsuario oBPUsuario = new BPUsuario();

            try
            {

                // Formulario
                oENTUsuario.UsuarioId = UsuarioId;
                switch (UsuarioPopUpType){
                    case UsuarioPopUpTypes.Delete:

                        oENTUsuario.Activo = 0;
                        break;

                    case UsuarioPopUpTypes.Reactivate:

                        oENTUsuario.Activo = 1;
                        break;

                    default:

                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPUsuario.UpdateUsuario_Estatus(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectUsuario();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp

        void ClearPopUpPanel(){
            try
            {

                // Limpiar formulario
                this.ddlPopUpRol.SelectedIndex = 0;
                this.ddlPopUpTitulo.SelectedIndex = 0;
                this.txtPopUpPuesto.Text = "";
                this.txtPopUpEmail.Text = "";
                this.txtPopUpNombre.Text = "";
                this.txtPopUpApellidoPaterno.Text = "";
                this.txtPopUpApellidoMaterno.Text = "";
                this.txtPopUpDescripcion.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddUsuario.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(UsuarioPopUpTypes UsuarioPopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddUsuario.Value = idItem.ToString();

                // Detalle de acción
                switch (UsuarioPopUpType){
                    case UsuarioPopUpTypes.Insert:

                        this.lblPopUpTitle.Text = "Nuevo Usuario";
                        this.btnPopUpCommand.Text = "Crear Usuario";
                        break;

                    case UsuarioPopUpTypes.Update:

                        this.lblPopUpTitle.Text = "Edición de Usuario";
                        this.btnPopUpCommand.Text = "Actualizar Usuario";
                        SelectUsuario_ForEdit(idItem);
                        break;

                    default:

                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlPopUpRol.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {

                // Rol
                if (this.ddlPopUpRol.SelectedIndex == 0) { throw new Exception("* El campo [Rol] es requerido"); }

                // Título
                if (this.ddlPopUpTitulo.SelectedIndex == 0) { throw new Exception("* El campo [Título] es requerido"); }

                // Email
                if (this.txtPopUpEmail.Text.Trim() == "") { throw new Exception("* El campo [Email] es requerido"); }

                // Nombre
                if (this.txtPopUpNombre.Text.Trim() == "") { throw new Exception("* El campo [Nombre] es requerido"); }

                // Apellido Paterno
                if (this.txtPopUpApellidoPaterno.Text.Trim() == "") { throw new Exception("* El campo [Apellido Paterno] es requerido"); }

                // Estatus
                if (this.ddlPopUpStatus.SelectedIndex == 0) { throw new Exception("* El campo [Estatus] es requerido"); }

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            // Validación. Solo la primera vez que se ejecuta la página
            if (this.IsPostBack) { return; }

            // Lógica de la página
            try
            {

                // Llenado de controles
                SelectStatus();
                SelectStatus_PopUp();
                SelectRol();
                SelectRol_PopUp();
                SelectTitulo_PopUp();
                SelectUsuario();

                // Estado inicial del formulario
                ClearPopUpPanel();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlRol.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                // Filtrar información
                SelectUsuario();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(UsuarioPopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }

        protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgPopUp = null;

            String UsuarioId = "";
            String NombreUsuario = "";
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
                UsuarioId = this.gvUsuario.DataKeys[e.Row.RowIndex]["UsuarioId"].ToString();
                Activo = this.gvUsuario.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreUsuario = this.gvUsuario.DataKeys[e.Row.RowIndex]["NombreCompleto"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Usuario [" + NombreUsuario + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Usuario [" + NombreUsuario + "]";
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

        protected void gvUsuario_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 UsuarioId = 0;

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
                UsuarioId = Int32.Parse(this.gvUsuario.DataKeys[intRow]["UsuarioId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvUsuario.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand){
                    case "Editar":

                        SetPanel(UsuarioPopUpTypes.Update, UsuarioId);
                        break;

                    case "Eliminar":

                        UpdateUsuario_Estatus(UsuarioId, UsuarioPopUpTypes.Delete);
                        break;

                    case "Reactivar":

                        UpdateUsuario_Estatus(UsuarioId, UsuarioPopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }

        protected void gvUsuario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvUsuario, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }


        
        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddUsuario.Value == "0")
                {

                    InsertUsuario();
                }
                else
                {

                    UpdateUsuario(Int32.Parse(this.hddUsuario.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlPopUpRol.ClientID + "');", true);
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlRol.ClientID + "');", true);
            }
        }

    }
}