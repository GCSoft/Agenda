﻿/*---------------------------------------------------------------------------------------------------------------------------------
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
        ///   <name>BPInvitacion.DeleteInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un comentario existente en una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionComentario(ENTInvitacion oENTInvitacion){
            DAInvitacion oDAInvitacion = new DAInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAInvitacion.DeleteInvitacionComentario(oENTInvitacion, this.ConnectionApplication, 0);

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
                if (oENTResponse.DataSetResponse.Tables[2].Rows.Count == 0) { oENTResponse.MessageDB = "No se detectaron direcciones de correo electrónico para el envío de la notificación, la invitación se creó de todas formas "; }
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
                              "<title>Agenda - Invitación declianada</title>" +
                           "</head>" +
                           "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                              "<div style='clear:both; height:90%; text-align:center; width:100%;'>" +
                                 "<div style='height:80%; clear: both; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                    "<table border='0px;' cellpadding='0' cellspacing='0' style='height:100%; width:100%;'>" +
                                       "<tr>" +
                                          "<td colspan='2' valign='middle' style='color:#549CC6; font-family:Arial; font-size:12px; font-weight:bold; text-align:left;'>Agenda - Invitacion declinada</td>" +
                                       "</tr>" +
                                             "<tr><td colspan='3'><div style='border-bottom:1px solid #549CC6;'></div></td></tr>" +
                                       "<tr style='height:10px'><td colspan='3'></td></tr>" +
                                       "<tr>" +
                                          "<td colspan='2' valign='top' style='font-family:Arial; font-size:12px;'>" +
                                             "Se le notifica que la solicitud de invitación al evento ha sido declinada, el  motivo ha sido el siguiente:<br><br>" +
                                             "<table border='0px' cellpadding='0' cellspacing ='0' class='Text' style='height:100%; width:100%'>" +
                                                "<tr style='height:10px'><td></td></tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:12px; text-align:left;'>" +
                                                         "<br>Motivo" +
                                                   "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:12px; text-align:left;'>" +
                                                      "<br><br><br>Gracias por utilizar nuestros servicios informáticos" +
                                                   "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:9px; text-align:center;'>" +
                                                      "<br><br>Powered By GCSoft" +
                                                   "</td>" +
                                                "</tr>" +
                                             "</table>" +
                                          "</td>" +
                                       "</tr>" +
                                       "<tr style='height:20px'><td colspan='3'></td></tr>" +
                                       "<tr style='height:20px'><td colspan='3'></td></tr>" +
                                       "<tr style='height:1px'><td colspan='3' style='background:#000063 repeat-x;'></td></tr>" +
                                       "<tr style='height:60px; vertical-align:top;'>" +
                                          "<td colspan='2' style='font-family:Arial; font-size:10px; color: #180A3B; text-align:justify; vertical-align:middle;'>" +
                                             "Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.<br/><br/>Nota: Los acentos y caracteres especiales fueron omitidos para su correcta lectura en cualquier medio electronico.<br/>" +
                                          "</td>" +
                                          "<td></td>" +
                                       "</tr>" +
                                             "<tr><td colspan='3'></td></tr>" +
                                    "</table>" +
                                 "</div>" +
                              "</div>" +
                           "</body>" +
                        "</html>";

                        // Enviar correo
                        gcMail.Send("Agenda - Invitación creada", Contactos, "Agenda - Invitación creada", HTMLMessage);

                    #endregion

                }else{

                    #region Registrada

                        // Llave encriptada
                        Key = oENTResponse.DataSetResponse.Tables[1].Rows[0]["InvitacionId"].ToString() + "|2";
                        Key = gcEncryption.EncryptString(Key, true);

                        // Configuración del correo
                        HTMLMessage = "" +
                           "<html>" +
                           "<head>" +
                              "<title>Agenda - Invitación creada</title>" +
                           "</head>" +
                           "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                              "<div style='clear:both; height:90%; text-align:center; width:100%;'>" +
                                 "<div style='height:80%; clear: both; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                    "<table border='0px;' cellpadding='0' cellspacing='0' style='height:100%; width:100%;'>" +
                                       "<tr>" +
                                          "<td colspan='2' valign='middle' style='color:#549CC6; font-family:Arial; font-size:12px; font-weight:bold; text-align:left;'>Agenda - Invitacion creada</td>" +
                                       "</tr>" +
                                             "<tr><td colspan='3'><div style='border-bottom:1px solid #549CC6;'></div></td></tr>" +
                                       "<tr style='height:10px'><td colspan='3'></td></tr>" +
                                       "<tr>" +
                                          "<td colspan='2' valign='top' style='font-family:Arial; font-size:12px;'>" +
                                             "Usted ha sido asociado en una nueva invitación<br><br>" +
                                             "<table border='0px' cellpadding='0' cellspacing ='0' class='Text' style='height:100%; width:100%'>" +
                                                "<tr style='height:10px'><td></td></tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:12px; text-align:left;'>" +
                                                         "<br>Puede acceder al sistema haciendo click <a href='" + this.ApplicationURLInvitation + "key=" + Key + "'>aqui</a>" +
                                                   "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:12px; text-align:left;'>" +
                                                      "<br><br><br>Gracias por utilizar nuestros servicios informáticos" +
                                                   "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                   "<td style='font-family:Arial; font-size:9px; text-align:center;'>" +
                                                      "<br><br>Powered By GCSoft" +
                                                   "</td>" +
                                                "</tr>" +
                                             "</table>" +
                                          "</td>" +
                                       "</tr>" +
                                       "<tr style='height:20px'><td colspan='3'></td></tr>" +
                                       "<tr style='height:20px'><td colspan='3'></td></tr>" +
                                       "<tr style='height:1px'><td colspan='3' style='background:#000063 repeat-x;'></td></tr>" +
                                       "<tr style='height:60px; vertical-align:top;'>" +
                                          "<td colspan='2' style='font-family:Arial; font-size:10px; color: #180A3B; text-align:justify; vertical-align:middle;'>" +
                                             "Este correo electronico es confidencial y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o tomar ninguna accion basada en este correo electronico o cualquier otra informacion incluida en el, favor de notificar al remitente de inmediato mediante el reenvio de este correo electronico y borrar a continuacion totalmente este correo electronico y sus anexos.<br/><br/>Nota: Los acentos y caracteres especiales fueron omitidos para su correcta lectura en cualquier medio electronico.<br/>" +
                                          "</td>" +
                                          "<td></td>" +
                                       "</tr>" +
                                             "<tr><td colspan='3'></td></tr>" +
                                    "</table>" +
                                 "</div>" +
                              "</div>" +
                           "</body>" +
                        "</html>";

                        // Enviar correo
                        gcMail.Send("Agenda - Invitación creada", Contactos, "Agenda - Invitación creada", HTMLMessage);

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

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

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
