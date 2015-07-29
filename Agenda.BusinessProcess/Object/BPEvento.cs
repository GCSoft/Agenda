/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPEvento
' Autor: Ruben.Cobos
' Fecha: 22-Diciembre-2014
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias manuales
using GCUtility.Communication;
using GCUtility.Security;
using Agenda.DataAccess.Object;
using Agenda.Entity.Object;
using System.Data;
using System.Web;

namespace Agenda.BusinessProcess.Object
{
    public class BPEvento : BPBase
    {

        ///<remarks>
        ///   <name>BPEvento.DeleteEventoContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a un evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteEventoContacto(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.DeleteEventoContacto(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.InsertEvento</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertEvento(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.InsertEvento(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento agendado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento agendado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que ha sido agendado el evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTEvento.EventoNombre + "</font> para el Gobernador con fecha inicial de <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTEvento.FechaInicio + "</font>.<br /><br /><br />" +
                                                    (oENTEvento.Comentario.Trim() == "" ? "" : "Notas adicionales:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTEvento.Comentario.Trim() + "</font>") +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento agendado", Contactos, "Evento agendado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.InsertEventoContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto a un evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertEventoContacto(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.InsertEventoContacto(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.SelectEvento</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Eventos en base a los parámetros proporcionados</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.SelectEvento(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.SelectEvento_Calendario</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Eventos en base a los parámetros proporcionados el cual se deplegará en el calendario</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento_Calendario(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.SelectEvento_Calendario(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.SelectEvento_Detalle</name>
        ///   <create>24-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el detalle de un evento en particular</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento_Detalle(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.SelectEvento_Detalle(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.SelectEventoContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEventoContacto(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.SelectEventoContacto(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_Cancelar</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un evento existente a cancelada</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_Cancelar(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_Cancelar(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento cancelado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento cancelado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se ha cancelado el evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Motivo de cancelación:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento cancelado", Contactos, "Evento cancelado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_Configuracion</name>
        ///   <create>28-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de configuración de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_Configuracion(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_Configuracion(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se han actualizado los datos del programa del evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }        

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_DatosEvento</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos del Evento de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_DatosEvento(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_DatosEvento(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se han actualizado los datos del evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_DatosGenerales</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos generales de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_DatosGenerales(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_DatosGenerales(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se han actualizado los datos generales del evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }        

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_EliminarRepresentante</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina la representación de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_EliminarRepresentante(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_EliminarRepresentante(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a un evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEventoContacto(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoContacto(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEvento_Reactivar</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEvento_Reactivar(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_Reactivar(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento reactivado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento reactivado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se ha reactivado el evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento reactivado", Contactos, "Evento reactivado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }



        ///<remarks>
        ///   <name>BPEvento.UpdateEventoComiteHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoComiteHelipuerto_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoComiteHelipuerto_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoComiteRecepcion_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoComiteRecepcion_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoComiteRecepcion_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoOrdenDia_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoOrdenDia_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoOrdenDia_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoAcomodo_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoAcomodo_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoAcomodo_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoListadoAdicional_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoListadoAdicional_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoListadoAdicional_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoResponsableEvento_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoResponsable_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoResponsable_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPEvento.UpdateEventoResponsableLogistica_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoResponsableLogistica_Item(ENTEvento oENTEvento){
            DAEvento oDAEvento = new DAEvento();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEventoResponsableLogistica_Item(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[3].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTEvento.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
                            break;

                        case 5:

                            Dependencia = "La coordinaci&oacute;n de direcci&oacute;n de protocolo";
                            break;

                        default:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                    }

                    #region Cuerpo de Correo (HTML)

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                                "<head>" +
                                    "<title>Agenda - Evento actualizado</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Evento actualizado<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
                                                    "Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                    "</div>" +
                                    "<div style='background:#339933; clear:both; height:20%; text-align:left; width:100%;'>" +
                                    "<div style='height:5%;'></div>" +
                                    "<div style='height:90%;'>" +
                                        "<table style='color:#FFFFFF; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:100%;' valign='middle'>" +
                                                "<td style='text-align:center; float:left; width:20%;'>" +
                                                    "<img src='" + this.MailLogo + "' height='120px' width='92px' />" +
                                                "</td>" +
                                                "<td style='text-align:justify; float:left; vertical-align: middle; width:70%;'>" +
                                                    "<div style='text-align:center; width:90%;'><font style='font-family:Arial; font-size:9px;'>Powered By GCSoft</font><br /><br /></div>" +
                                                    "<font style='font-family:Arial; font-size:10px;'>Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.</font><br />" +
                                                "</td>" +
                                                "<td></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div>" +
                                    "<div style='height:5%;'></div>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

                    #endregion

                    // Enviar correo
                    gcMail.Send("Agenda - Evento actualizado", Contactos, "Evento actualizado", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

    }
}
