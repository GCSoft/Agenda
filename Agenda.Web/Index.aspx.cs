/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	Index
' Autor:	Ruben.Cobos
' Fecha:	14-Octubre-2014
'
' Descripción:
'				Canaliza la aplicación al módulo de autenticación correspondiente
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agenda.Web
{
    public partial class Index : System.Web.UI.Page
    {
        
        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e) {

            Response.Redirect("Application/WebApp/Public/frmLogin.aspx", false);
        }

    }
}