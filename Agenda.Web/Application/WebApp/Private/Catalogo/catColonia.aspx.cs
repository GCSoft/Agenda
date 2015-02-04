/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	catColonia
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
    public partial class catColonia : BPPage
    {
        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Enumeraciones
        enum PopUpTypes { Delete, Insert, Reactivate, Update }


        // Rutinas del programador

        void InsertColonia(){
            ENTColonia oENTColonia = new ENTColonia();
            ENTResponse oENTResponse = new ENTResponse();

            BPColonia oBPColonia = new BPColonia();

            try
            {

                // Formulario
                oENTColonia.MunicipioId = Int32.Parse(this.ddlPopUpMunicipio.SelectedValue);
                oENTColonia.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTColonia.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTColonia.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTColonia.Rank = 1;

                // Transacción
                oENTResponse = oBPColonia.InsertColonia(oENTColonia);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectColonia();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Colonia creada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstado(){
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
                this.ddlEstado.DataTextField = "Nombre";
                this.ddlEstado.DataValueField = "EstadoId";
                this.ddlEstado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlEstado.DataBind();

                // Elemento extra
                this.ddlPopUpStatus.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstado_PopUp(){
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
                this.ddlPopUpEstado.DataTextField = "Nombre";
                this.ddlPopUpEstado.DataValueField = "EstadoId";
                this.ddlPopUpEstado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPopUpEstado.DataBind();

                // Elemento extra
                this.ddlPopUpEstado.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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

        void SelectColonia(){
            ENTColonia oENTColonia = new ENTColonia();
            ENTResponse oENTResponse = new ENTResponse();

            BPColonia oBPColonia = new BPColonia();
            String MessageDB = "";

            try
            {

                // Formulario
                oENTColonia.ColoniaId = 0;
                oENTColonia.EstadoId = 0;
                oENTColonia.MunicipioId = 0;
                oENTColonia.Nombre = this.txtNombre.Text;
                oENTColonia.Activo = Int16.Parse(this.ddlStatus.SelectedItem.Value);

                // Transacción
                oENTResponse = oBPColonia.SelectColonia(oENTColonia);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                if (oENTResponse.MessageDB != "") { MessageDB = "alert('" + gcJavascript.ClearText(oENTResponse.MessageDB) + "');"; }

                // Llenado de controles
                this.gvColonia.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.gvColonia.DataBind();

                // Mensaje al usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), MessageDB, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectColonia_ForEdit(Int32 ColoniaId){
            ENTColonia oENTColonia = new ENTColonia();
            ENTResponse oENTResponse = new ENTResponse();

            BPColonia oBPColonia = new BPColonia();

            try
            {

                // Formulario
                oENTColonia.ColoniaId = ColoniaId;
                oENTColonia.EstadoId = 0;
                oENTColonia.MunicipioId = 0;
                oENTColonia.Nombre = "";
                oENTColonia.Activo = 2;

                // Transacción
                oENTResponse = oBPColonia.SelectColonia(oENTColonia);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Mensaje de la BD
                this.lblPopUpMessage.Text = oENTResponse.MessageDB;

                // Llenado de formulario
                this.ddlPopUpEstado.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstadoId"].ToString();
                SelectMunicipio_PopUp();

                this.ddlPopUpMunicipio.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MunicipioId"].ToString();
                this.txtPopUpNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                this.ddlPopUpStatus.SelectedValue = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Activo"].ToString();
                this.ckePopUpDescripcion.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Descripcion"].ToString();

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
                oENTMunicipio.EstadoId = Int32.Parse( this.ddlEstado.SelectedValue );
                oENTMunicipio.MunicipioId = 0;
                oENTMunicipio.Nombre = "";
                oENTMunicipio.Activo = 1;

                // Transacción
                oENTResponse = oBPMunicipio.SelectMunicipio(oENTMunicipio);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo de municipio
                this.ddlMunicipio.DataTextField = "Nombre";
                this.ddlMunicipio.DataValueField = "MunicipioId";
                this.ddlMunicipio.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMunicipio.DataBind();

                // Elemento extra
                this.ddlMunicipio.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMunicipio_PopUp(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMunicipio oENTMunicipio = new ENTMunicipio();

            BPMunicipio oBPMunicipio = new BPMunicipio();

            try
            {

                // Formulario
                oENTMunicipio.EstadoId = Int32.Parse(this.ddlPopUpEstado.SelectedValue);
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

                // Elemento extra
                this.ddlPopUpMunicipio.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateColonia(Int32 ColoniaId){
            ENTColonia oENTColonia = new ENTColonia();
            ENTResponse oENTResponse = new ENTResponse();

            BPColonia oBPColonia = new BPColonia();

            try
            {

                // Formulario
                oENTColonia.ColoniaId = ColoniaId;
                oENTColonia.MunicipioId = Int32.Parse(this.ddlPopUpMunicipio.SelectedValue);
                oENTColonia.Nombre = this.txtPopUpNombre.Text.Trim();
                oENTColonia.Activo = Int16.Parse(this.ddlPopUpStatus.SelectedValue);
                oENTColonia.Descripcion = this.ckePopUpDescripcion.Text.Trim();
                oENTColonia.Rank = 1;

                // Transacción
                oENTResponse = oBPColonia.UpdateColonia(oENTColonia);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                ClearPopUpPanel();

                // Actualizar grid
                SelectColonia();

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('Colonia actualizada con éxito!'); focusControl('" + this.txtNombre.ClientID + "');", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateColonia_Estatus(Int32 ColoniaId, PopUpTypes PopUpType){
            ENTColonia oENTColonia = new ENTColonia();
            ENTResponse oENTResponse = new ENTResponse();

            BPColonia oBPColonia = new BPColonia();

            try
            {

                // Formulario
                oENTColonia.ColoniaId = ColoniaId;
                switch (PopUpType)
                {
                    case PopUpTypes.Delete:
                        oENTColonia.Activo = 0;
                        break;
                    case PopUpTypes.Reactivate:
                        oENTColonia.Activo = 1;
                        break;
                    default:
                        throw new Exception("Opción inválida");
                }

                // Transacción
                oENTResponse = oBPColonia.UpdateColonia_Estatus(oENTColonia);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar datos
                SelectColonia();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Rutinas del PopUp

        void ClearPopUpPanel(){
            try
            {

                // Limpiar formulario
                this.ddlPopUpEstado.SelectedIndex = 0;
                this.ddlPopUpMunicipio.Items.Clear();
                this.ddlMunicipio.Items.Insert(0, new ListItem("[Seleccione]", "0"));


                this.txtPopUpNombre.Text = "";
                this.ddlPopUpStatus.SelectedIndex = 0;
                this.ckePopUpDescripcion.Text = "";

                // Estado incial de controles
                this.pnlPopUp.Visible = false;
                this.lblPopUpTitle.Text = "";
                this.btnPopUpCommand.Text = "";
                this.lblPopUpMessage.Text = "";
                this.hddColonia.Value = "";

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetPanel(PopUpTypes PopUpType, Int32 idItem = 0){
            try
            {

                // Acciones comunes
                this.pnlPopUp.Visible = true;
                this.hddColonia.Value = idItem.ToString();

                // Detalle de acción
                switch (PopUpType)
                {
                    case PopUpTypes.Insert:
                        this.lblPopUpTitle.Text = "Nuevo Colonia";
                        this.btnPopUpCommand.Text = "Crear Colonia";

                        break;

                    case PopUpTypes.Update:
                        this.lblPopUpTitle.Text = "Edición de Colonia";
                        this.btnPopUpCommand.Text = "Actualizar Colonia";
                        SelectColonia_ForEdit(idItem);
                        break;

                    default:
                        throw (new Exception("Opción inválida"));
                }

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlPopUpEstado.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidatePopUpForm(){
            try
            {

                if (this.ddlPopUpMunicipio.SelectedIndex == 0) { throw new Exception("* El campo [Municipio] es requerido"); }
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

                // Llenado de controles
                SelectEstado();
                SelectEstado_PopUp();
                SelectEstatus();
                SelectEstatus_PopUp();
                SelectMunicipio();
                SelectMunicipio_PopUp();

                // Estado inicial del formulario
                this.gvColonia.DataSource = null;
                this.gvColonia.DataBind();
                ClearPopUpPanel();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlEstado.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                // Filtrar información
                SelectColonia();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
            try
            {

                // Nuevo registro
                SetPanel(PopUpTypes.Insert);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }
        }

        protected void gvColonia_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgDelete = null;

            String ColoniaId = "";
            String NombreColonia = "";
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
                ColoniaId = this.gvColonia.DataKeys[e.Row.RowIndex]["ColoniaId"].ToString();
                Activo = this.gvColonia.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreColonia = this.gvColonia.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Colonia [" + NombreColonia + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Colonia [" + NombreColonia + "]";
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

        protected void gvColonia_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 ColoniaId = 0;

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
                ColoniaId = Int32.Parse(this.gvColonia.DataKeys[intRow]["ColoniaId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvColonia.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        SetPanel(PopUpTypes.Update, ColoniaId);
                        break;
                    case "Eliminar":
                        UpdateColonia_Estatus(ColoniaId, PopUpTypes.Delete);
                        break;
                    case "Reactivar":
                        UpdateColonia_Estatus(ColoniaId, PopUpTypes.Reactivate);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }
        }

        protected void gvColonia_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvColonia, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }

        }



        // Eventos del PopUp

        protected void btnPopUpCommand_Click(object sender, EventArgs e){
            try
            {

                // Validar formulario
                ValidatePopUpForm();

                // Determinar acción
                if (this.hddColonia.Value == "0"){

                    InsertColonia();
                }else{

                    UpdateColonia(Int32.Parse(this.hddColonia.Value));
                }

            }catch (Exception ex){
                this.lblPopUpMessage.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlPopUpEstado.ClientID + "'); }", true);
            }
        }

        protected void ddlPopUpEstado_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Actualizar municipios
                SelectMunicipio_PopUp();

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlPopUpMunicipio.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlPopUpEstado.ClientID + "'); }", true);
            }
        }

        protected void imgCloseWindow_Click(object sender, ImageClickEventArgs e){
            try
            {

                // Cancelar transacción
                ClearPopUpPanel();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstado.ClientID + "');", true);
            }
        }

    }
}