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
using System.Data;

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveCalendarioCompleto : System.Web.UI.Page
    {


        // Utilerías
        GCEncryption gcEncryption = new GCEncryption();

        // Variables publicas
        String QueryDate;


        // Funciones del programador

		String GetKey(String sKey) {
			String Response = "";

			try{

				Response = gcEncryption.DecryptString(sKey, true);

			}catch(Exception){
				Response = "";
			}

			return Response;
		}

        DataTable SelectEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();
            ENTSession oENTSession = new ENTSession();
            ENTFiltroCalendario oENTFiltroCalendario;

            BPEvento oBPEvento = new BPEvento();

            DataTable tblResponse = null;
            DataRow rowEstatusEvento;

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Recuperar formulario de sesión
                if (oENTSession.Entity == null) { return null; }
                if (oENTSession.Entity.GetType().Name != "ENTFiltroCalendario") { return null; }

                // Obtener Formulario
                oENTFiltroCalendario = (ENTFiltroCalendario)oENTSession.Entity;

                // Vaciado de datos
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.PrioridadId = oENTFiltroCalendario.PrioridadId;
                oENTEvento.Mes = oENTFiltroCalendario.MesActual;
                oENTEvento.Anio = oENTFiltroCalendario.AnioActual;
                oENTEvento.Dependencia = oENTFiltroCalendario.Dependencia;

                // Listado de estatus a mostrar
                oENTEvento.DataTableEstatusEvento = new DataTable("DataTableEstatusEvento");
                oENTEvento.DataTableEstatusEvento.Columns.Add("EstatusEventoId", typeof(Int32));

                if( oENTFiltroCalendario.EventoNuevos == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "1";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( oENTFiltroCalendario.EventoProceso == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "2";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( oENTFiltroCalendario.EventoExpirado == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "3";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( oENTFiltroCalendario.EventoCancelado == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "4";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( oENTFiltroCalendario.EventoRepresentado == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "5";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                // Transacción
                oENTResponse = oBPEvento.SelectEvento_Calendario(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Resultado
                tblResponse = oENTResponse.DataSetResponse.Tables[1];

                // Obtener fecha del proceso
                GetQueryDate(oENTFiltroCalendario.AnioActual, oENTFiltroCalendario.MesActual);

            }catch (Exception ex){
                throw (ex);
            }

            return tblResponse;
        }


        // Métodos públicos

        void GetQueryDate( Int32 AnioActual, Int32 MesActual ){
            try
            {

                // Formato: 2014-12-05
                QueryDate = AnioActual.ToString() + "-";
                QueryDate = QueryDate + ( MesActual > 9 ? MesActual.ToString() : "0" + MesActual.ToString() ) + "-";

                // Si hubo cambio de mes hay que validar que el día exista en el mes seleccionado
                if( DateTime.Now.Day > DateTime.DaysInMonth(AnioActual, MesActual) ){

                    QueryDate = QueryDate + DateTime.DaysInMonth(AnioActual, MesActual).ToString();
                }else{

                    QueryDate = QueryDate + ( DateTime.Now.Day > 9 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString() );
                }

            }catch(Exception){
                QueryDate = "";
            }
        }

        void ConstruirCalendario(){
            DataTable tblEventos;
            Int32 TotalEventos = 0;
            Int32 EventoActual = 0;

            String Calendario;
            String Eventos = "";

            try
            {

                // Listado de eventos con los parámetros configurados
                tblEventos = SelectEvento();

                // Tota de eventos registrados
                TotalEventos = tblEventos.Rows.Count;

                // Obtener el total de eventos
                foreach( DataRow rowEvento in tblEventos.Rows ){

                    // Apuntador al evento actual
                    EventoActual = EventoActual + 1;

                    // Armar el evento
                    Eventos = Eventos + "{ " +
                                                "id: " + rowEvento["EventoId"].ToString() + ", " +
                                                "title: '', " +
                                                "start: '" + rowEvento["EventoFecha_ANSI"].ToString() + "T" + rowEvento["EventoHoraInicio_ANSI"].ToString() + "', " +
                                                "end: '" + rowEvento["EventoFecha_ANSI"].ToString() + "T" + rowEvento["EventoHoraFin_ANSI"].ToString() + "', " +
                                                "backgroundColor: '" + rowEvento["HexColor"].ToString() + "', " +
                                                "description: '" + rowEvento["EventoNombre"].ToString() + "'" +
                                            "} ";

                    // Fin del evento
                    Eventos = Eventos + ( EventoActual < TotalEventos ? ", " : " " );
                }

                // Construir objeto
                Calendario = "" +
                             "<script type='text/javascript'>" +
                                "$(document).ready(function () { " +
                                    "$('#calendar').fullCalendar({ " +
                                        "defaultDate: '" + QueryDate + "', " +
                                        "displayEventEnd: true, " +
                                        "editable: false, " +
                                        "eventLimit: false, " +
                                        "eventBorderColor: '#675C9D', " +
                                        "eventTextColor: '#FFFFFF', " +
                                        "header: { left: '', center: 'title', right: '' }, " +
                                        "height: 'auto', " +
                                        "lang: 'es', " +
                                        "" +
                                        "events: [ " + Eventos + " ], " +
                                        "" +
                                        "eventRender: function (event, element) { " +
                                            "element.find('.fc-content').after(" + Convert.ToChar(34) + "<div style='font-family:Arial; font-size:11px; text-align:justify;'>" + Convert.ToChar(34) + " + event.description + " + Convert.ToChar(34) + "</div>" + Convert.ToChar(34) + ");" +
                                        "}" +
                                        "" +
                                    "}); " +
                                    "" +
                                "}); " +
                                "" +
                            "</script>";

                //  Vaciado de objeto
                this.litCalendar.Text = Calendario;   

            }catch (Exception ex){
                throw (ex);
            }
        }

       

        // Eventos de la página ( por orden de aparición)

        protected void Page_Load(object sender, EventArgs e){
            String Key = "";

            try
            {

                // Validaciones
                if ( this.Request.QueryString["key"] == null ) { return; }
                Key = GetKey( this.Request.QueryString["key"].ToString() );
                if (Key != "1") { return; }

                // Construir calendario
                ConstruirCalendario();

            }catch (Exception){
                // Do Nothing
            }
        }


    }
}