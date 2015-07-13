/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre: wucFullCalendar
' Autor:  Ruben.Cobos
' Fecha:  26-Enero-2015
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using Agenda.BusinessProcess.Object;
using Agenda.Entity.Object;
using GCUtility.Function;
using GCUtility.Security;
using System.Data;

namespace Agenda.Web.Include.WebUserControls.FullCalendar
{
    public partial class wucFullCalendar : System.Web.UI.UserControl
    {

        // Utilerías
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        
        // Handlers

        public delegate void DelegateMonthEvent();
        public event DelegateMonthEvent ChangeMonth;


        // Propiedades

        ///<remarks>
        ///   <name>wucFullCalendar.PrioridadId</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna identificador único de la prioridad a filtrar</summary>
        public Int32 PrioridadId
        {
            get { return Int32.Parse(this.hddPrioridadId.Value); }
            set { this.hddPrioridadId.Value = value.ToString(); }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.Dependencia</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos de Logística (1), Dirección de Protocolo (2) o todas con un 0</summary>
        public Int16 Dependencia
        {
            get { return Int16.Parse(this.hddDependencia.Value); }
            set
            {

                if (value > 2) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddDependencia.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.EventoCancelado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que determina si se mostrarán los eventos cancelados</summary>
        public Int16 EventoCancelado
        {
            get { return Int16.Parse(this.hddEventoCancelado.Value); }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddEventoCancelado.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.EventoExpirado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que determina si se mostrarán los eventos expirados</summary>
        public Int16 EventoExpirado
        {
            get { return Int16.Parse(this.hddEventoExpirado.Value); }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddEventoExpirado.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.EventoNuevos</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que determina si se mostrarán los eventos nuevos</summary>
        public Int16 EventoNuevos
        {
            get { return Int16.Parse(this.hddEventoNuevos.Value); }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddEventoNuevos.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.EventoProceso</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que determina si se mostrarán los eventos en proceso</summary>
        public Int16 EventoProceso
        {
            get { return Int16.Parse(this.hddEventoProceso.Value); }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddEventoProceso.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.EventoRepresentado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que determina si se mostrarán los eventos representado</summary>
        public Int16 EventoRepresentado
        {
            get { return Int16.Parse(this.hddEventoRepresentado.Value); }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                this.hddEventoRepresentado.Value = value.ToString();
            }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.AnioActual</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el año actual configurado en el control</summary>
        public Int32 AnioActual
        {
            get { return Int32.Parse(this.hddCurrentYear.Value); }
            set { this.hddCurrentYear.Value = value.ToString(); }
        }

        ///<remarks>
        ///   <name>wucFullCalendar.MesActual</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el mes actual configurado en el control</summary>
        public Int32 MesActual
        {
            get { return Int32.Parse( this.hddCurrentMonth.Value ); }
            set { 

                if ( value > 12 ) { throw( new Exception("Fuera de rango") ); }
                if (value < 1) { throw (new Exception("Fuera de rango")); }

                this.hddCurrentMonth.Value = value.ToString();
            }
        }



        // Métodos privados

        private String GetQueryDate(){
            String Response;
            try
            {

                // Formato: 2014-12-05
                Response = this.AnioActual.ToString() + "-";
                Response = Response + ( this.MesActual > 9 ? this.MesActual.ToString() : "0" + this.MesActual.ToString() ) + "-";

                // Si hubo cambio de mes hay que validar que el día exista en el mes seleccionado
                if( DateTime.Now.Day > DateTime.DaysInMonth(this.AnioActual, this.MesActual) ){

                    Response = Response + DateTime.DaysInMonth(this.AnioActual, this.MesActual).ToString();
                }else{

                    Response = Response + ( DateTime.Now.Day > 9 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString() );
                }

            }catch(Exception){
                Response = "";
            }

            return Response;
        }

        private DataTable SelectEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            DataTable tblResponse = null;
            DataRow rowEstatusEvento;

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.PrioridadId = this.PrioridadId;
                oENTEvento.Mes = this.MesActual;
                oENTEvento.Anio = this.AnioActual;
                oENTEvento.Dependencia = this.Dependencia;

                // Listado de estatus a mostrar
                oENTEvento.DataTableEstatusEvento = new DataTable("DataTableEstatusEvento");
                oENTEvento.DataTableEstatusEvento.Columns.Add("EstatusEventoId", typeof(Int32));

                if( this.EventoNuevos == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "1";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( this.EventoProceso == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "2";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( this.EventoExpirado == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "3";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( this.EventoCancelado == 1 ){
                    rowEstatusEvento = oENTEvento.DataTableEstatusEvento.NewRow();
                    rowEstatusEvento["EstatusEventoId"] = "4";
                    oENTEvento.DataTableEstatusEvento.Rows.Add(rowEstatusEvento);
                }

                if( this.EventoRepresentado == 1 ){
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

            }catch (Exception ex){
                throw (ex);
            }

            return tblResponse;
        }


        // Métodos públicos

        public void ConstruirCalendario(){
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
                                                "title: '" + rowEvento["EventoNombre"].ToString() + "', " +
                                                "start: '" + rowEvento["EventoFechaInicio_ANSI"].ToString() + "T" + rowEvento["EventoHoraInicio_ANSI"].ToString() + "', " +
                                                "end: '" + rowEvento["EventoFechaFin_ANSI"].ToString() + "T" + rowEvento["EventoHoraFin_ANSI"].ToString() + "', " +
                                                "backgroundColor: '" + rowEvento["HexColor"].ToString() + "', " +
                                                "coordinacion: '" + rowEvento["Coordinacion"].ToString() + "', " +
                                                "icono: '../../../../Include/Image/Buttons/Prioridad" + rowEvento["PrioridadNombre"].ToString() + ".png', " +
                                                "llave:'" + gcEncryption.EncryptString(rowEvento["EventoId"].ToString() + "|4", true) + "', " +
                                                "gira: '" + rowEvento["Gira"].ToString() + "', " +
                                                "tooltip: '" + rowEvento["EventoNombre"].ToString() + "<br /><br />Estatus: " + rowEvento["EstatusEventoNombre"].ToString() + "<br />Dependencia: " + rowEvento["Dependencia"].ToString() + "<br /><br />Inicio: " + rowEvento["EventoFechaInicioTexto"].ToString() + "<br />Fin:     " + rowEvento["EventoFechaFinTexto"].ToString() + "' " +
                                            "} ";

                    // Fin del evento
                    Eventos = Eventos + ( EventoActual < TotalEventos ? ", " : " " );
                }

                // Construir objeto
                Calendario = "" +
                             "<script type='text/javascript'>" +
                                "$(document).ready(function () { " +
                                    "$('#calendar').fullCalendar({ " +
                                        "businessHours: { start: '06:00', end: '20:00', dow: [1, 2, 3, 4, 5, 6] }, " +
                                        "defaultDate: '" + GetQueryDate() + "', " +
                                        "editable: false, " +
                                        "eventBorderColor: '#675C9D', " +
                                        "eventLimit: true, " +
                                        "eventTextColor: '#FFFFFF', " +
                                        "header: { left: 'month,agendaWeek,agendaDay', center: 'title' }, " +
                                        "lang: 'es', " +
                                        "" +
                                        "events: [ " + Eventos + " ], " +
                                        "" +
                                        "eventRender: function (event, eventElement) { " +
                                            "var sCoordinacion = 'S/D'; " +
                                            "var sIcono = ''; " +
                                            "var sToolTip = ''; " +
                                            "" +
                                            "if (event.coordinacion) { sCoordinacion = event.coordinacion; } " +
                                            "if (event.icono) { sIcono = event.icono; } " +
                                            "if (event.tooltip) { sToolTip = CreateLineBreaks(event.tooltip); } " +
                                            "if (event.gira == '1') { sCoordinacion = 'GIRA'; } " +
                                            "" +
                                            "if (event.gira == '1') { " +
                                                "eventElement.find(" + Convert.ToChar(34) + "div.fc-content" + Convert.ToChar(34) + ").prepend(" + Convert.ToChar(34) + "<div style='border-bottom:solid 1px #FFF; height:19px; width:100%;'><div style='float:left;'>" + Convert.ToChar(34) + " + sCoordinacion + " + Convert.ToChar(34) + "</div></div>" + Convert.ToChar(34) + "); " +
                                            "} else { " +
                                                "eventElement.find(" + Convert.ToChar(34) + "div.fc-content" + Convert.ToChar(34) + ").prepend(" + Convert.ToChar(34) + "<div style='border-bottom:solid 1px #FFF; height:19px; width:100%;'><div style='float:left;'>" + Convert.ToChar(34) + " + sCoordinacion + " + Convert.ToChar(34) + "</div><div style='float:right;'><img src='" + Convert.ToChar(34) + " + sIcono + " + Convert.ToChar(34) + "' ></div></div>" + Convert.ToChar(34) + "); " +
                                            "}" +
                                            "eventElement.find(" + Convert.ToChar(34) + "div.fc-content" + Convert.ToChar(34) + ").css('cursor', 'pointer'); " +
                                            "eventElement.find(" + Convert.ToChar(34) + "div.fc-content" + Convert.ToChar(34) + ").prop('title', sToolTip); " +
                                        "}, " +
                                        "" +
                                        "eventClick: function (event) { " +
                                            "var sKey = ''; " +
                                            "" +
                                            "if (event.llave) { sKey = event.llave; } " +
                                            "" +
                                            "if (event.gira == '1') { window.location = " + Convert.ToChar(34) + "../Gira/girDetalleGira.aspx?key=" + Convert.ToChar(34) + " + sKey; } else { window.location = " + Convert.ToChar(34) + "eveDetalleEvento.aspx?key=" + Convert.ToChar(34) + " + sKey; }" +
                                            "" +
                                        "}, " +
                                        "" +
                                        "viewRender: function (view, element) { " +
                                            "var moment = $('#calendar').fullCalendar('getDate'); " +
                                            "" +
                                            "var BaseMonth = " + this.MesActual.ToString() + "; " +
                                            "" +
                                            "var Month = moment.month() + 1; " +
                                            "var Year = moment.year(); " +
                                            "" +
                                            "if (BaseMonth != Month) { " +
                                                "document.getElementById('" + this.hddCurrentMonth.ClientID + "').value = Month; " +
                                                "document.getElementById('" + this.hddCurrentYear.ClientID + "').value = Year; " +
                                                "" +
                                                "document.getElementById('" + this.MonthChangeEvent.ClientID + "').value = '1'; " +
                                                "__doPostBack(" + Convert.ToChar(34) + this.MonthChangeEvent.ClientID + Convert.ToChar(34) + ", " + Convert.ToChar(34) + Convert.ToChar(34) + "); " +
                                            "} " +
                                        "} " +
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


        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            
        }

        protected void MonthChangeEvent_ValueChanged(object sender, EventArgs e){
            try
            {

                // Generar evento
                if (ChangeMonth != null) { ChangeMonth(); }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

    }
}