/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	wucTimer
' Autor:	Ruben.Cobos
' Fecha:	20-Agosto-2013
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agenda.Web.Include.WebUserControls
{
    public partial class wucTimer : System.Web.UI.UserControl
    {
       
        // Propiedades

		///<remarks>
		///   <name>wucTimer.DisplayTime</name>
		///   <create>20-Agosto-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Obtiene/Asigna la hora desplegada en el control</summary>
		public String DisplayTime
		{
			get { return this.txtCanvas.Text; }
			set { this.txtCanvas.Text = value; }
		}


		// Eventos del control

		protected void Page_Load(object sender, EventArgs e){

            // Deshabilitar la validación no-intrusiva por el manejo de JQuery
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            // Validaciones
            if (this.IsPostBack) { return; }

            // Hora demo
            this.txtCanvas.Text = "10:00 a.m.";

		}

    }
}