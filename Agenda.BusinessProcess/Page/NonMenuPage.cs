/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BusinessProcess.NonMenuPage
' Autor: Ruben.Cobos
' Fecha: 21-Noviembre-2014
'
' Proposito:
'       Sobreescritura del método Page_PreLoad la cual se utilizará como clase padre de las paginas del proyecto que no se visualizan en el menú,
'       implementa la seguridad de la aplicación.
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias manuales
using Agenda.Entity.Object;
using Agenda.DataAccess.Object;
using System.Configuration;

namespace Agenda.BusinessProcess.Page
{
    public class NonMenuPage : System.Web.UI.Page
    {

        // Asignación de evento PreLoad

        override protected void OnInit(EventArgs e){
            this.PreLoad += new System.EventHandler(this.Override_PagePreLoad);
        }


        // Evento PreLoad

        protected void Override_PagePreLoad(object sender, EventArgs e){
            ENTSession oENTSession;

            // Validación. Solo la primera vez que entre a la página
            if (this.IsPostBack) { return; }

            // Sesión
            if (this.Session["oENTSession"] == null) { this.Response.Redirect("~/Index.aspx", true); }

            // Información de Sesión
            oENTSession = (ENTSession)this.Session["oENTSession"];

            // Token generado
            if (!oENTSession.TokenGenerado){ this.Response.Redirect("~/Index.aspx", true); }

            // Deshabilitar caché
            this.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        }

    }
}
