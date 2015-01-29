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

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

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
        ///<summary>Obtiene el detalle de una invitación en particular</summary>
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
            GCEncryption gcEncryption = new GCEncryption();

            String Contactos = "";
            String HTMLMessage = "";
            String EventoNombre = "";
            String MotivoRechazo = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_Cancelar(oENTEvento, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Validaciones de invitación
                if (oENTResponse.DataSetResponse.Tables[2].Rows.Count == 0) { oENTResponse.MessageDB = "No se detectaron direcciones de correo electrónico para el envío de la notificación, la invitación se creó de todas formas "; }
                if (oENTResponse.MessageDB != "") { return oENTResponse; }    

                // Obtener el listado de direcciones a donde se enviará la notificación
                foreach( DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows ){ Contactos = ( Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString() ); }

                // Nombre del evento y motivo de rechazo
                EventoNombre = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                MotivoRechazo = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString().Trim();

                #region Correo

                    // Configuración del correo
                    HTMLMessage = "" +
                            "<html>" +
                               "<head>" +
                                  "<title>Agenda - Invitaci&oacute;n declinada</title>" +
                               "</head>" +
                               "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                  "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                     "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Invitaci&oacute;n declinada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    "La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que la invitaci&oacute;n al evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + EventoNombre + "</font> ha sido rechazada.<br /><br /><br />" +
                                                    ( MotivoRechazo == "" ? "" : "El motivo de rechazo fue el siguiente:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" +MotivoRechazo + "</font>" ) +
                                                    "<br /><br /><br /><br /><br />Gracias por utilizar nuestros servicios inform&aacute;ticos.<br /><br />" +
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

                        // Enviar correo
                        gcMail.Send("Agenda - Invitación declinada", Contactos, "Invitación declinada", HTMLMessage);

                #endregion

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
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

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_Configuracion(oENTEvento, this.ConnectionApplication, 0);

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

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_DatosEvento(oENTEvento, this.ConnectionApplication, 0);

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

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEvento.UpdateEvento_DatosGenerales(oENTEvento, this.ConnectionApplication, 0);

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


    }
}
