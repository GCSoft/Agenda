/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	PrivateTemplate
' Autor:    Ruben.Cobos
' Fecha:    21-Octubre-2013
'
' Descripción:
'           MasterPage de las pantallas de trabajo en la sección Privada de la aplicación
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using Agenda.Entity.Object;

namespace Agenda.Web.Include.MasterPage
{
    public partial class PrivateTemplate : System.Web.UI.MasterPage
    {

        
        // Eventos de la página

		protected void Page_Load(object sender, EventArgs e){		
			ENTSession oENTSession = new ENTSession();

			try
			{

                // Validaciones
                if (this.IsPostBack) { return; }

				// Nombre de usuario
				oENTSession = (ENTSession)this.Session["oENTSession"];

                if (oENTSession == null)
                {
                    this.Response.Redirect("~/Index.aspx", false);
                }
                else
                {
                    this.lblUserName.Text = oENTSession.Titulo + " " + oENTSession.Nombre;
                }

			}catch (Exception){
				// Do Nothing
			}
		}

        protected void lnkSalir_Click(object sender, EventArgs e){
            this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappLogout.aspx");
		}


    }
}