/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	sappNotificacion
' Autor:	Ruben.Cobos
' Fecha:	21-Octubre-2013
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using GCUtility.Security;
using Agenda.BusinessProcess.Page;
using Agenda.Entity.Object;

namespace Agenda.Web.Application.WebApp.Private.SysApp
{
    public partial class sappNotificacion : NonMenuPage
    {

        // Utilerías
        GCEncryption gcEncryption = new GCEncryption();


        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Mensajes por Default
                this.litEncabezado.Text = "Estimado Usuario, ha ocurrido un problema al querer ingresar a la página que solicitó, a continuación se detalla:";
                this.lblNotificacion.Text = "No tiene permisos de acceder a esta página";

                // Aviso
                this.lblNotificacion.Text = (this.Request.QueryString["key"] == null ? this.lblNotificacion.Text : gcEncryption.DecryptString(this.Request.QueryString["key"], true));

                // Nombre de usuario
                oENTSession = (ENTSession)this.Session["oENTSession"];
                this.litEncabezado.Text = "&nbsp;Estimado <b>" + oENTSession.Nombre + " </b>, ha ocurrido un problema al querer ingresar a la página que solicitó, a continuación se detalla:";

            }catch (Exception){
                // Do Nothing
            }
        }

        protected void lnkRegresar_Click(object sender, EventArgs e){
            if (this.lblNotificacion.Text.Substring(0, 5) == "[V04]"){

                // Se inactivó la compañía con el usuario dentro
                this.Response.Redirect("~/Application/WebApp/Private/SysApp/saLogout.aspx");
            }else{

                // Acceso prohibido común
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "parent.parent.window.location.href = '../../../../Application/WebApp/Private/AppIndex.aspx';", true);
            }
        }

    }
}