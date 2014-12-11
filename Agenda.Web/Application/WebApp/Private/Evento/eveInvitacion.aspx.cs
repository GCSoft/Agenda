/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveInvitacion
' Autor:	Ruben.Cobos
' Fecha:	09-Diciembre-2014
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveInvitacion : BPPage
    {
        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


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

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSLugarEvento(string prefixText, int count){
			BPLugarEvento oBPLugarEvento = new BPLugarEvento();
			ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From LugarEvento Where LugarEventoId = @LugarEventoId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @LugarEventoId = 0
			//				End

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
					Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowLugarEvento["Nombre"].ToString(), rowLugarEvento["LugarEventoId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de LugarEventos
			return ServiceResponse;
		}



        // Rutinas del programador

        void SelectCategoria(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTCategoria oENTCategoria = new ENTCategoria();

            BPCategoria oBPCategoria = new BPCategoria();

            try
            {

                // Formulario
                oENTCategoria.CategoriaId = 0;
                oENTCategoria.Nombre = "";
                oENTCategoria.Activo = 1;

                // Transacción
                oENTResponse = oBPCategoria.SelectCategoria(oENTCategoria);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlCategoria.DataTextField = "Nombre";
                this.ddlCategoria.DataValueField = "CategoriaId";
                this.ddlCategoria.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlCategoria.DataBind();

                // Agregar Item de selección
                this.ddlCategoria.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectFuncionario(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTUsuario oENTUsuario = new ENTUsuario();

            BPUsuario oBPUsuario = new BPUsuario();

            try
            {

                // Formulario
                oENTUsuario.UsuarioId = 0;
                oENTUsuario.Nombre = "";
                oENTUsuario.Activo = 1;

                // Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlUsuario.DataTextField = "Nombre";
                this.ddlUsuario.DataValueField = "UsuarioId";
                this.ddlUsuario.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlUsuario.DataBind();

                // Agregar Item de selección
                this.ddlUsuario.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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
                this.ddlMunicipio.DataTextField = "Nombre";
                this.ddlMunicipio.DataValueField = "MunicipioId";
                this.ddlMunicipio.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMunicipio.DataBind();

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlMunicipio.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectPrioridad(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTPrioridad oENTPrioridad = new ENTPrioridad();

            BPPrioridad oBPPrioridad = new BPPrioridad();

            try
            {

                // Formulario
                oENTPrioridad.PrioridadId = 0;
                oENTPrioridad.Nombre = "";
                oENTPrioridad.Activo = 1;

                // Transacción
                oENTResponse = oBPPrioridad.SelectPrioridad(oENTPrioridad);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPrioridad.DataTextField = "Nombre";
                this.ddlPrioridad.DataValueField = "PrioridadId";
                this.ddlPrioridad.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPrioridad.DataBind();

                // Agregar Item de selección
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoEvento oENTTipoEvento = new ENTTipoEvento();

            BPTipoEvento oBPTipoEvento = new BPTipoEvento();

            try
            {

                // Formulario
                oENTTipoEvento.TipoEventoId = 0;
                oENTTipoEvento.Nombre = "";
                oENTTipoEvento.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoEvento.SelectTipoEvento(oENTTipoEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoEvento.DataTextField = "Nombre";
                this.ddlTipoEvento.DataValueField = "TipoEventoId";
                this.ddlTipoEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoEvento.DataBind();

                // Agregar Item de selección
                this.ddlTipoEvento.Items.Insert(0, new ListItem("[Seleccione]", "0"));

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
                SelectCategoria();
                SelectMunicipio();
                SelectPrioridad();
                SelectTipoEvento();
                SelectFuncionario();

                // Estado inicial
                this.gvFuncionario.DataSource = null;
                this.gvFuncionario.DataBind();

                // Foco
                this.tabInvitacion.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void btnAprobar_Click(object sender, EventArgs e){
            try
            {

                

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e){
            try
            {

                

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e){
            try
            {

                

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }



        // Eventos de panel - Ubicación

        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Limpiado de controles
                this.txtColonia.Text = "";
                this.hddColoniaId.Value = "";

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlMunicipio.SelectedItem.Value;

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtColonia.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtColonia.ClientID + "'); }", true);
            }
        }



        // Eventos de panel - Funcionarios

        protected void btnAsociarFuncionario_Click(object sender, EventArgs e){
            try
            {



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void gvFuncionario_RowCommand(object sender, GridViewCommandEventArgs e){
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
                UsuarioId = Int32.Parse(this.gvFuncionario.DataKeys[intRow]["UsuarioId"].ToString());

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // TODO: Eliminar
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlUsuario.ClientID + "'); }", true);
            }
        }

        protected void gvFuncionario_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String UsuarioId = "";
            String FuncionarioNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                UsuarioId = this.gvFuncionario.DataKeys[e.Row.RowIndex]["UsuarioId"].ToString();
                FuncionarioNombre = this.gvFuncionario.DataKeys[e.Row.RowIndex]["FuncionarioNombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Desasociar funcionario [" + FuncionarioNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_PopUp" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvFuncionario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvFuncionario, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.ddlUsuario.ClientID + "'); }", true);
            }
        }

    }
}