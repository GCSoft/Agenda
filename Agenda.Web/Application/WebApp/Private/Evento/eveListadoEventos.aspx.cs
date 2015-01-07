/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatEvento
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveListadoEventos : BPPage
    {
       
         // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Rutinas del programador

        void SelectEstatusInvitacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEstatusInvitacion oENTEstatusInvitacion = new ENTEstatusInvitacion();

            BPEstatusInvitacion oBPEstatusInvitacion = new BPEstatusInvitacion();

            try
            {

                // Formulario
                oENTEstatusInvitacion.EstatusInvitacionId = 0;
                oENTEstatusInvitacion.Nombre = "";

                // Transacción
                oENTResponse = oBPEstatusInvitacion.SelectEstatusInvitacion(oENTEstatusInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlEstatusInvitacion.DataTextField = "Nombre";
                this.ddlEstatusInvitacion.DataValueField = "EstatusInvitacionId";
                this.ddlEstatusInvitacion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlEstatusInvitacion.DataBind();

                // Agregar Item de selección
                this.ddlEstatusInvitacion.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEventos(){
            try
            {

                // Estado inicial
                this.gvEvento.DataSource = null;
                this.gvEvento.DataBind();

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
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Todas]", "0"));

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
                SelectEstatusInvitacion();
                SelectPrioridad();
                SelectEventos();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlEstatusInvitacion.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "');", true);
            }
        }

        protected void ddlEstatusInvitacion_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "');", true);
            }
        }

        protected void gvEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;
            ImageButton imgPopUp = null;

            String EventoId = "";
            String NombreEvento = "";
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
                EventoId = this.gvEvento.DataKeys[e.Row.RowIndex]["EventoId"].ToString();
                Activo = this.gvEvento.DataKeys[e.Row.RowIndex]["Activo"].ToString();
                NombreEvento = this.gvEvento.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Editar Menú [" + NombreEvento + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Tooltip PopUp
                sTootlTip = (Activo == "1" ? "Eliminar" : "Reactivar") + " Menú [" + NombreEvento + "]";
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

        protected void gvEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 EventoId = 0;

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
                EventoId = Int32.Parse(this.gvEvento.DataKeys[intRow]["EventoId"].ToString());

                // Reajuste de Command
                if (strCommand == "PopUp") { strCommand = (this.gvEvento.DataKeys[intRow]["Activo"].ToString() == "0" ? "Reactivar" : "Eliminar"); }

                // Acción
                switch (strCommand)
                {
                    case "Editar":
                        break;
                    case "Eliminar":
                        break;
                    case "Reactivar":
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "');", true);
            }
        }

        protected void gvEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvEvento, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusInvitacion.ClientID + "');", true);
            }

        }

    }
}