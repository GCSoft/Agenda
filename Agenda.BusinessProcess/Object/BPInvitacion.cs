/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPInvitacion
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
    public class BPInvitacion : BPBase
    {

        ///<remarks>
        ///   <name>BPInvitacion.DeleteInvitacionContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionContacto(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.DeleteInvitacionContacto(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.DeleteInvitacionFuncionario</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un funcionario para una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionFuncionario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.DeleteInvitacionFuncionario(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.InsertInvitacion</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea una nueva invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacion(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            GCEncryption gcEncryption = new GCEncryption();

            String Contactos = "";
            String HTMLMessage = "";
            String Key = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.InsertInvitacion(oENTInvitacion, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Validaciones de invitación
                if (oENTResponse.DataSetResponse.Tables[2].Rows.Count == 0) { oENTResponse.MessageDB = "No se detectaron direcciones de correo electrónico para el envío de la notificación, la invitación se " + (oENTInvitacion.EstatusInvitacionId == 2 ? "declinó" : "creó") + " de todas formas "; }
                if (oENTResponse.MessageDB != "") { return oENTResponse; }    

                // Obtener el listado de direcciones a donde se enviará la notificación
                foreach( DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows ){ Contactos = ( Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString() ); }

                // Determinar el tipo de correo a enviar
                if( oENTInvitacion.EstatusInvitacionId == 2 ){

                    #region Declinada

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
                                                    "La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que la invitaci&oacute;n al evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTInvitacion.EventoNombre + "</font> ha sido rechazada.<br /><br /><br />" +
                                                    ( oENTInvitacion.MotivoRechazo.Trim() == "" ? "" : "El motivo de rechazo fue el siguiente:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTInvitacion.MotivoRechazo.Trim() + "</font>" ) +
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

                }else{

                    #region Registrada

                        // Llave encriptada
                        Key = "1|" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["InvitacionId"].ToString();
                        Key = gcEncryption.EncryptString(Key, false);

                        // Configuración del correo
                        HTMLMessage = "" +
                            "<html>" +
                               "<head>" +
                                  "<title>Agenda - Valoraci&oacute;n de evento</title>" +
                               "</head>" +
                               "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                  "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                     "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
							            "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
								            "<tr style='height:20%;' valign='middle'>" +
									            "<td style='font-weight:bold;'>" +
										            "Valoraci&oacute;n de evento<br /><br />" +
										            "<div style='border-bottom:1px solid #339933;'></div>" +
									            "</td>" +
								            "</tr>" +
								            "<tr style='height:80%;' valign='top'>" +
									            "<td>" +
                                                    "La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que ha sido asociado a la invitaci&oacute;n del evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTInvitacion.EventoNombre + "</font> para su valoraci&oacute;n.<br /><br /><br />" +
										            "Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "?key=" + Key + "'>aqui</a><br /><br /><br /><br /><br />" +
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

                        // Enviar correo
                        gcMail.Send("Agenda - Valoración de evento", Contactos, "Valoración de evento", HTMLMessage);

                    #endregion

                }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPInvitacion.InsertInvitacionContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto a la invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionContacto(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.InsertInvitacionContacto(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.InsertInvitacionComentario</name>
        ///   <create>29-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo comentario a un invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionComentario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.InsertInvitacionComentario(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.InsertInvitacionFuncionario</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo funcionario para una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionFuncionario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            GCEncryption gcEncryption = new GCEncryption();

            String Contactos = "";
            String EventoNombre = "";
            String HTMLMessage = "";
            String Key = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.InsertInvitacionFuncionario(oENTInvitacion, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Validaciones de invitación
                if (oENTResponse.DataSetResponse.Tables[1].Rows.Count == 0) { oENTResponse.MessageDB = "No se detectaron direcciones de correo electrónico para el envío de la notificación, el funcionario se asoció de todas formas "; }
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Correo
                Contactos = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Email"].ToString();
                EventoNombre = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();

                #region Enviar correo

                    // Llave encriptada
                    Key = "1|" + oENTInvitacion.InvitacionId.ToString();
                    Key = gcEncryption.EncryptString(Key, false);

                    // Configuración del correo
                    HTMLMessage = "" +
                            "<html>" +
                               "<head>" +
                                  "<title>Agenda - Valoraci&oacute;n de evento</title>" +
                               "</head>" +
                               "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                  "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                     "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Valoraci&oacute;n de evento<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    "La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que ha sido asociado a la invitaci&oacute;n del evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + EventoNombre + "</font> para su valoraci&oacute;n.<br /><br /><br />" +
                                                    "Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "?key=" + Key + "'>aqui</a><br /><br /><br /><br /><br />" +
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

                    // Enviar correo
                    gcMail.Send("Agenda - Valoración de evento", Contactos, "Valoración de evento", HTMLMessage);

                #endregion

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPInvitacion.SelectInvitacion</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Invitaciones en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacion(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.SelectInvitacion(oENTInvitacion, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPInvitacion.SelectInvitacion_Detalle</name>
        ///   <create>24-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el detalle de una invitación en particular</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacion_Detalle(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.SelectInvitacion_Detalle(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.SelectInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de comentarios realizados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacionComentario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.SelectInvitacionComentario(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.SelectInvitacionContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacionContacto(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.SelectInvitacionContacto(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.UpdateInvitacion_DatosEvento</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos del Evento de una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_DatosEvento(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.UpdateInvitacion_DatosEvento(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.UpdateInvitacion_Aprobar</name>
        ///   <create>13-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de una invitación existente a aprobada</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_Aprobar(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            GCEncryption gcEncryption = new GCEncryption();

            String Contactos = "";
            String HTMLMessage = "";
            String EventoNombre = "";
            String Fecha = "";
            String Key = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.UpdateInvitacion_Aprobar(oENTInvitacion, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Nombre del evento
                EventoNombre = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                Fecha = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();

                // Obtener el listado de direcciones a donde se enviará la notificación y enviar correo
                if( oENTResponse.DataSetResponse.Tables[4].Rows[0]["CorreoRepresentado"].ToString() != "" ){

                    Contactos = oENTResponse.DataSetResponse.Tables[4].Rows[0]["CorreoRepresentado"].ToString();

                    #region Correo

                    // Configuración del correo
                    HTMLMessage = "" +
                        "<html>" +
                            "<head>" +
                                "<title>Agenda - Representación de Evento</title>" +
                            "</head>" +
                            "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                    "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                    "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                        "<tr style='height:20%;' valign='middle'>" +
                                            "<td style='font-weight:bold;'>" +
                                                "Representación de Evento<br /><br />" +
                                                "<div style='border-bottom:1px solid #339933;'></div>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr style='height:80%;' valign='top'>" +
                                            "<td>" +
                                                "Estimado(a) <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[4].Rows[0]["NombreRepresentado"].ToString() + "</font>:<br /><br /><br />La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que fue seleccionado(a) para representar al C. Gobernado en el evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + EventoNombre + "</font> el cual se llevará a cabo el <font style='color:#000000; font-style: italic; font-weight:bold;'>" + Fecha + "</font> en <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[4].Rows[0]["LugarEvento"].ToString() + "</font>." +
                                                "<br /><br /><br /><br /><br />" +
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


                    // Enviar correo
                    gcMail.Send("Agenda - Representación de Evento", Contactos, "Representación de Evento", HTMLMessage);

                    #endregion

                }else{

                    // Validación de envío de correo
                    if (oENTInvitacion.Notificacion == 3) { return oENTResponse; }

                    // Validaciones de invitación
                    if (oENTResponse.DataSetResponse.Tables[2].Rows.Count == 0 && oENTResponse.DataSetResponse.Tables[3].Rows.Count == 0) { oENTResponse.MessageDB = "No se detectaron direcciones de correo electrónico para el envío de la notificación, la invitación se aprobó de todas formas "; }
                    if (oENTResponse.MessageDB != "") { return oENTResponse; }

                    // Listado de correos
                    switch( oENTInvitacion.Notificacion ){
                        case 1: // Logística

                            foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows) {
                                Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                            }

                            break;

                        case 2: // Dirección de protocolo

                            foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[3].Rows) {
                                Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                            }

                            break;
                    }

                    // Llave encriptada
                    Key = "2|" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoId"].ToString();
                    Key = gcEncryption.EncryptString(Key, false);

                    #region Correo

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
                                                    "La coordinaci&oacute;n de relaciones p&uacute;blicas le notifica que la invitaci&oacute;n al evento <font style='color:#000000; font-style: italic; font-weight:bold;'>" + EventoNombre + "</font> ha sido agendada para el Gobernador con fecha de <font style='color:#000000; font-style: italic; font-weight:bold;'>" + Fecha + "</font>.<br /><br /><br />" +
                                                    ( oENTInvitacion.Comentario.Trim() == "" ? "" : "Notas adicionales:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTInvitacion.Comentario.Trim() + "</font>" ) +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho evento haciendo click <a href='" + this.ApplicationURLInvitation + "?key=" + Key + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    

                        // Enviar correo
                        gcMail.Send("Agenda - Evento agendado", Contactos, "Evento agendado", HTMLMessage);

                    #endregion
                
                }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPInvitacion.UpdateInvitacion_Declinar</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de una invitación existente a declinada</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_Declinar(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
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
                oENTResponse = oDAInvitacion.UpdateInvitacion_Declinar(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.UpdateInvitacion_DatosGenerales</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos generales de una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_DatosGenerales(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.UpdateInvitacion_DatosGenerales(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.UpdateInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un comentario existente en una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacionComentario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.UpdateInvitacionComentario(oENTInvitacion, this.ConnectionApplication, 0);

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
        ///   <name>BPInvitacion.UpdateInvitacionContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a la invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacionContacto(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.UpdateInvitacionContacto(oENTInvitacion, this.ConnectionApplication, 0);

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
