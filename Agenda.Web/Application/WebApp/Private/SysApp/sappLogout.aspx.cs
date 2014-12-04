/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:   sappLogout
' Autor:    Ruben.Cobos
' Fecha:    21-Octubre-2013
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using Agenda.BusinessProcess.Page;

namespace Agenda.Web.Application.WebApp.Private.SysApp
{
    public partial class sappLogout : NonMenuPage
    {

        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            this.Session.Abandon();
            this.Response.Redirect("~/Index.aspx", true);
        }


    }
}