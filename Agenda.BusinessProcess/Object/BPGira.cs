/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPGira
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
    public class BPGira : BPBase
    {

        ///<remarks>
        ///   <name>BPGira.DeleteGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>elimina lógicamente una Configuración asociada a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteGiraConfiguracion(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.DeleteGiraConfiguracion(oENTGira, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Configuración de gira eliminada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Configuración de gira eliminada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Configuración de gira eliminada", Contactos, "Configuración de gira eliminada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.DeleteGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteGiraContacto(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.DeleteGiraContacto(oENTGira, this.ConnectionApplication, 0);

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
        ///   <name>BPGira.InsertGira</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGira(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.InsertGira(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira agendada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira agendada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que ha sido agendado la gira <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTGira.GiraNombre + "</font> para el Gobernador con fecha inicial de <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTGira.FechaGiraInicio + "</font>.<br /><br /><br />" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicha gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira agendada", Contactos, "Gira agendada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.InsertGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia una nueva Configuración a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGiraConfiguracion(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.InsertGiraConfiguracion(oENTGira, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Configuración de gira creada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Configuración de gira creada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Configuración de gira creada", Contactos, "Configuración de gira creada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.InsertGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto a una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGiraContacto(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.InsertGiraContacto(oENTGira, this.ConnectionApplication, 0);

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
        ///<summary>Obtiene el detalle de una gira en particular</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGira_Detalle(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.SelectGira_Detalle(oENTGira, this.ConnectionApplication, 0);

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
        ///   <name>BPGira.SelectGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Configuracions asociados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGiraConfiguracion(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.SelectGiraConfiguracion(oENTGira, this.ConnectionApplication, 0);

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
        ///   <name>BPGira.SelectGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGiraContacto(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.SelectGiraContacto(oENTGira, this.ConnectionApplication, 0);

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
        ///   <name>BPGira.UpdateGira_Cancelar</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Gira existente a cancelada</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGira_Cancelar(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGira_Cancelar(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira cancelada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira cancelada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se ha cancelado la gira <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Motivo de cancelación:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira cancelada", Contactos, "Gira cancelada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGira_DatosGira</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos de una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGira_DatosGira(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGira_DatosGira(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se han actualizado los datos de la gira <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza una Configuración existente y asociada a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGiraConfiguracion(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraConfiguracion(oENTGira, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Configuración de gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Configuración de gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Configuración de gira actualizada", Contactos, "Configuración de gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a un Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGiraContacto(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraContacto(oENTGira, this.ConnectionApplication, 0);

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
        ///   <name>BPGira.UpdateGira_Reactivar</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGira_Reactivar(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGira_Reactivar(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira reactivada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira reactivada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que se ha reactivado la gira <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira reactivada", Contactos, "Gira reactivada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }



        ///<remarks>
        ///   <name>BPGira.UpdateGiraComiteHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraComiteHelipuerto_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraComiteHelipuerto_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraAcompanaHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraAcompanaHelipuerto_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraAcompanaHelipuerto_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraDespedidaHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraDespedidaHelipuerto_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraDespedidaHelipuerto_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraComiteRecepcion_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraComiteRecepcion_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraComiteRecepcion_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraOrdenDia_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraOrdenDia_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraOrdenDia_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPGira.UpdateGiraAcomodo_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraAcomodo_Item(ENTGira oENTGira){
            DAGira oDAGira = new DAGira();
            ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAGira.UpdateGiraAcomodo_Item(oENTGira, this.ConnectionApplication, 0);

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
                    switch( oENTGira.RolId ){
                        case 1:
                        case 2:

                            Dependencia = "La coordinaci&oacute;n de relaciones p&uacute;blicas";
                            break;

                        case 4:

                            Dependencia = "La coordinaci&oacute;n de log&iacute;stica";
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
                                    "<title>Agenda - Gira actualizada</title>" +
                                "</head>" +
                                "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                                    "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                                        "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                                        "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                            "<tr style='height:20%;' valign='middle'>" +
                                                "<td style='font-weight:bold;'>" +
                                                    "Gira actualizada<br /><br />" +
                                                    "<div style='border-bottom:1px solid #339933;'></div>" +
                                                "</td>" +
                                            "</tr>" +
                                            "<tr style='height:80%;' valign='top'>" +
                                                "<td>" +
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["GiraNombre"].ToString() + "</font>.<br /><br /><br />" +
                                                    "Información previa al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Antes"].ToString() + "</font><br /><br /><br />" +
                                                    "Información posterior al cambio:<br /><br /><font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[2].Rows[0]["Despues"].ToString() + "</font>" +
                                                    "<br /><br /><br /><br /><br />Puede acceder al detalle de dicho Gira haciendo click <a href='" + this.ApplicationURLInvitation + "'>aqui</a><br /><br /><br /><br /><br />" +
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
                    gcMail.Send("Agenda - Gira actualizada", Contactos, "Gira actualizada", HTMLMessage);

                }

            }catch (Exception){
                // oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

    }
}
