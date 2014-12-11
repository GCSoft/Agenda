﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:   wucCalendar
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

namespace Agenda.Web.Include.WebUserControls
{
    public partial class wucCalendar : System.Web.UI.UserControl
    {

        // Enumeraciones
        private enum DateTypes { BeginDate, EndDate }


        // Propiedades

        ///<remarks>
        ///   <name>wucCalendar.BeginDate</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene la fecha seleccionada en formato universal agregando hora, minuto y segundo al iniciar el día</summary>
        public String BeginDate
        {
            get { return GetDate(DateTypes.BeginDate); }
        }

        ///<remarks>
        ///   <name>wucCalendar.DisplayDate</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/asigna la fecha desplegada en el control</summary>
        public String DisplayDate
        {
            get { return this.txtCanvas.Text; }
        }

        ///<remarks>
        ///   <name>wucCalendar.EndDate</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene la fecha seleccionada en formato universal agregando hora, minuto y segundo al finalizar el día</summary>
        public String EndDate
        {
            get { return GetDate(DateTypes.EndDate); }
        }



        // Metodos privados

        ///<remarks>
        ///   <name>wucCalendar.SetDate</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Estable el calendario en una hora determinada</summary>
        ///<param name="dtDate">Fecha a establecer el calendario</param>
        public void SetDate( DateTime dtDate ){
            this.ceManager.SelectedDate = dtDate;
        }



        // Metodos privados

        private String GetDate(DateTypes DateType){
            String sReturnDate = "";

            try
            {

                // Fecha en formato universal
                sReturnDate = this.txtCanvas.Text.Split(new Char[] { '/' })[2] + "-" + this.txtCanvas.Text.Split(new Char[] { '/' })[1] + "-" + this.txtCanvas.Text.Split(new Char[] { '/' })[0];

                // Hora, minuto y segundo
                switch (DateType)
                {
                    case DateTypes.BeginDate:
                        sReturnDate = sReturnDate + " " + "00:00";
                        break;

                    case DateTypes.EndDate:
                        sReturnDate = sReturnDate + " " + "23:59";
                        break;
                }

            }catch (Exception ex){
                throw (ex);
            }

            return sReturnDate;
        }



        // Eventos del control

        protected void Page_Load(object sender, EventArgs e){

            // Deshabilitar la validación no-intrusiva por el manejo de JQuery
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            
            // Mantener estado
            if (this.txtCanvas.Text != "") { this.ceManager.SelectedDate = DateTime.Parse(this.txtCanvas.Text); }

            // Validaciones
            if (this.IsPostBack) { return; }

            // Fecha actual
            if (this.ceManager.SelectedDate == null) { this.ceManager.SelectedDate = DateTime.Now; }
        }

    
    }
}