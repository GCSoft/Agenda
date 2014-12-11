/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveCalendario
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveCalendario : BPPage
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

        
        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                SelectEstatusInvitacion();

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


    }
}