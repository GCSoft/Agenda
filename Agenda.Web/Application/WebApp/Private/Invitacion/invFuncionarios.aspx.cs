/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invFuncionarios
' Autor:	Ruben.Cobos
' Fecha:	22-Diciembre-2014
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
using Agenda.Entity.Object;
using Agenda.BusinessProcess.Object;
using System.Data;

namespace Agenda.Web.Application.WebApp.Private.Invitacion
{
    public partial class invFuncionarios : System.Web.UI.Page
    {


        // Servicios

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSFuncionario(string prefixText, int count){
			BPUsuario oBPUsuario = new BPUsuario();
			ENTUsuario oENTUsuario = new ENTUsuario();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Funcionario Where FuncionarioId = @FuncionarioId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @FuncionarioId = 0
			//				End

			try
			{

				// Formulario
                oENTUsuario.RolId = 3;      // Rol de funcionario
                oENTUsuario.UsuarioId = 0;
                oENTUsuario.Email = "";
                oENTUsuario.Nombre = prefixText;
                oENTUsuario.Activo = 1;

				// Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowFuncionario in oENTResponse.DataSetResponse.Tables[1].Rows){
                    Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowFuncionario["NombreCompletoTitulo"].ToString(), rowFuncionario["UsuarioId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Funcionarios
			return ServiceResponse;
		}

        
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

		String GetKey(String sKey) {
			String Response = "";

			try{

				Response = gcEncryption.DecryptString(sKey, true);

			}catch(Exception){
				Response = "";
			}

			return Response;
		}


        // Rutinas el programador

        void DeleteInvitacionFuncionario(Int32 UsuarioId){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);
                oENTInvitacion.UsuarioId_Temp = UsuarioId;

                // Transacción
                oENTResponse = oBPInvitacion.DeleteInvitacionFuncionario(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar formulario
                SelectInvitacion();

                // Limpiar autosuggest
                this.txtFuncionario.Text = "";
                this.hddFuncionarioId.Value = "";

                // Mensaje de usuario
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('Funcionario eliminado con éxito!'); focusControl('" + this.txtFuncionario.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void InsertInvitacionFuncionario(){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                // Validaciones
                if (this.hddFuncionarioId.Value.Trim() == "" || this.hddFuncionarioId.Value.Trim() == "0") { throw (new Exception("Es necesario seleccionar un Funcionario")); }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

                // Formulario
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);
                oENTInvitacion.UsuarioId_Temp = Int32.Parse(this.hddFuncionarioId.Value);

                // Transacción
                oENTResponse = oBPInvitacion.InsertInvitacionFuncionario(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Actualizar formulario
                SelectInvitacion();

                // Limpiar autosuggest
                this.txtFuncionario.Text = "";
                this.hddFuncionarioId.Value = "";

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('Funcionario asociado con éxito'); focusControl('" + this.txtFuncionario.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectInvitacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTInvitacion oENTInvitacion = new ENTInvitacion();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            try
            {

                // Formulario
                oENTInvitacion.InvitacionId = Int32.Parse(this.hddInvitacionId.Value);

                // Transacción
                oENTResponse = oBPInvitacion.SelectInvitacion_Detalle(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Carátula compacta
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraInicioTexto"].ToString() + " al " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraFinTexto"].ToString();

                // Funcionarios
                this.gvFuncionario.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvFuncionario.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            String Key = "";

			try
            {

				// Validaciones de llamada
				if (Page.IsPostBack) { return; }
                if (this.Request.QueryString["key"] == null) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

				// Validaciones de parámetros
				Key = GetKey(this.Request.QueryString["key"].ToString());
				if (Key == "") { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }
				if (Key.ToString().Split(new Char[] { '|' }).Length != 2) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

                // Obtener InvitacionId
                this.hddInvitacionId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

				// Carátula
                SelectInvitacion();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e){
			try
            {

                InsertInvitacionFuncionario();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

				// Llave encriptada
                sKey = this.hddInvitacionId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("invDetalleInvitacion.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
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
                switch (strCommand)
                {

                    case "Eliminar":
                        DeleteInvitacionFuncionario(UsuarioId);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }

        protected void gvFuncionario_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String UsuarioId = "";
            String Nombre = "";

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
                Nombre = this.gvFuncionario.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Delete
                sTootlTip = "Eliminar Funcionario [" + Nombre + "] de la invitación";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvFuncionario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvFuncionario, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }


    }
}