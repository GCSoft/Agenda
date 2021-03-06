﻿/*---------------------------------------------------------------------------------------------------------------------------------
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
            set { this.txtCanvas.Text = SetStandarTime(value); }
		}

        ///<remarks>
        ///   <name>wucTimer.DisplayUTCTime</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/asigna la hora desplegada en el control en formato universal</summary>
        public String DisplayUTCTime
        {
            //get { return GetStandarTime(this.txtCanvas.Text); }
            get { return this.txtCanvas.Text; }
        }



        // Métodos públicos

        ///<remarks>
        ///   <name>wucTimer.IsValidTime</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Determina si la hora contenida en el control es válida</summary>
        ///<param name="dtDate">Fecha a establecer el calendario</param>
        public Boolean IsValidTime( ref String ErrorDetail){
            Boolean Response = false;

            //String CurrentTime;
            //Int32 TempNumber;

            try
            {

                //// Valor seleccionado
                //CurrentTime = this.txtCanvas.Text;

                //// Validaciones
                //if( CurrentTime.Length != 8 ){ throw(new Exception("Longitud inválida")); }

                //TempNumber = Int32.Parse(CurrentTime.Substring(0, 2));
                //if ( TempNumber > 12 || TempNumber < 0 ) { throw (new Exception("Hora inválida")); }

                //TempNumber = Int32.Parse(CurrentTime.Substring(3, 2));
                //if ( TempNumber > 59 || TempNumber < 0 ) { throw (new Exception("Minuto inválido")); }

                //if ( CurrentTime.Substring(2, 1) != ":" ) { throw (new Exception("Separador inválido 1")); }
                //if ( CurrentTime.Substring(5, 1) != " " ) { throw (new Exception("Separador inválido 2")); }
                //if ( CurrentTime.Substring(6, 2) != "AM" && CurrentTime.Substring(6, 2) != "PM" ) { throw (new Exception("Identificador inválido")); }

                // Hora válida
                Response = true;

            }catch(Exception ex){
                ErrorDetail = ex.Message;
                Response = false;
            }

            return Response;
        }



        // Métodos privados

        private String GetStandarTime(String Input){
			String sTime = "";

			try{

				// Obtener la hora
				if (Input.Substring(6, 2) == "AM"){

					sTime = Input.Substring(0, 2);
				}else {
					sTime = ( Int32.Parse( Input.Substring(0, 2)) + 12).ToString();
					if (sTime == "24") { sTime = "12"; }
				}

				// Obtener los minutos
				sTime = sTime + Input.Substring(2, 3);

				// Hora universal
				return sTime;

			}catch(Exception ex){
				throw(ex);
			}
		}

        private String SetStandarTime(String Input){
			String sTime = "";

			try{

                // Validar formato de hora
                if( Input.IndexOf("AM") > 0 || Input.IndexOf("PM") > 0 ){

                    // Ajuste para los casos -> 012:45PM
                    if( Input.IndexOf(":") > 2 ){
                        Input = Input.Substring( Input.IndexOf(":") - 2 );
                    }

                    // Horas
                    if( Input.IndexOf("AM") > 0 ){

                        Input = Input.Replace("AM", "").Trim();
                        sTime = Input.Substring(0, 2);
                        if (sTime == "12") { sTime = "00"; }

                    }else{

                        Input = Input.Replace("PM", "").Trim();
                        sTime = (Int32.Parse(Input.Substring(0, 2)) + 12).ToString();
                        if (sTime == "24") { sTime = "12"; }
                    }

                    // Minutos
                    sTime = sTime + Input.Substring(2, 3);

                }else{

                    sTime = Input;
                }
                
				// Hora universal
				return sTime;

			}catch(Exception ex){
				throw(ex);
			}
		}



		// Eventos del control

		protected void Page_Load(object sender, EventArgs e){

            // Validaciones
            if (this.IsPostBack) { return; }

            // Hora demo
            if (this.txtCanvas.Text == "") { this.txtCanvas.Text = "10:00"; }

		}

    }
}