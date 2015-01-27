/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	AppIndex
' Autor:	Ruben.Cobos
' Fecha:	21-Noviembre-2014
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
using Agenda.BusinessProcess.Object;
using Agenda.BusinessProcess.Page;
using Agenda.Entity.Object;

namespace Agenda.Web.Application.WebApp.Private
{
    public partial class AppIndex : NonMenuPage
    {

        // Utilerías
        GCJavascript gcJavascript = new GCJavascript();

       
         // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Obtener sesión
                oENTSession = (ENTSession)Session["oENTSession"];

                // Canalizar al usuario por rol
                switch (oENTSession.RolId){
				    case 1: // System Administrator
                        // Do Nothing
					    break;

                    case 2: // Administrador
                        this.Response.Redirect("Evento/eveCalendario.aspx", false);
                        break;

                    case 3: // Funcionario
                        this.Response.Redirect("Invitacion/invListado.aspx", false);
                        break;

                    case 4: // Logística
                    case 5: // Dirección de Protocolo
                        this.Response.Redirect("Evento/eveCalendario.aspx", false);
                        break;

				    default:
                        // Do Nothing
					    break;
			    }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

    }
}