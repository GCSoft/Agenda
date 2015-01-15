/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPUsuario
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de reglas de negocio de la aplicación con métodos relacionados con el usuario
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias manuales
using GCUtility.Security;
using GCUtility.Communication;
using Agenda.DataAccess.Object;
using Agenda.Entity.Object;
using System.Data;
using System.Web;

namespace Agenda.BusinessProcess.Object
{
    public class BPUsuario : BPBase
    {

        ///<remarks>
        ///   <name>BPUsuario.InsertUsuario</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un usuario</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertUsuario(ENTUsuario oENTUsuario){
            GCMail gcMail = new GCMail();
            GCEncryption gcEncryption = new GCEncryption();
            GCPassword gcPassword = new GCPassword();

            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            String sHTMLMessage = "";
            String Password = "";

            try
            {

                // Generar Password
                Password = gcPassword.Create();

                // Encriptar el Password
                oENTUsuario.Password = gcEncryption.EncryptString(Password, false);

                // Validación de creación de password
                if (oENTUsuario.Password.Trim() == "") { throw (new Exception("Se generó una excepción al crear el usuario. Por favor intente de nuevo")); }

                // Transacción en base de datos
                oENTResponse = oDAUsuario.InsertUsuario(oENTUsuario, this.ConnectionApplication, 300);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Configuración del correo
                sHTMLMessage = "<html>" +
                   "<head>" +
                      "<title>Agenda - Bienvenido al sistema</title>" +
                   "</head>" +
                   "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                      "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                         "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
							"<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
								"<tr style='height:20%;' valign='middle'>" +
									"<td style='font-weight:bold;'>" +
										"Bienvenido al sistema<br /><br />" +
										"<div style='border-bottom:1px solid #339933;'></div>" +
									"</td>" +
								"</tr>" +
								"<tr style='height:80%;' valign='top'>" +
									"<td>" +
										"El equipo de sistemas le da la bienvenida a la plataforma de Agenda. Los datos de acceso a la aplicaci&oacute;n son los siguientes<br /><br /><br />" +
                                        "<font style='font-size:15px; font-weight:bold;'>Usuario:</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font style='color:#000000; font-weight:bold;'>" + oENTUsuario.Email + "</font><br />" +
										"<font style='font-size:15px; font-weight:bold;'>Contrase&ntilde;a:</font>&nbsp;<font style='color:#000000; font-weight:bold;'>" + Password + "</font><br /><br /><br />" +
										"Puede acceder al sistema haciendo click <a href='" + this.ApplicationURL + "' style='color:#000000; font-weight:bold;'>aqui</a><br /><br /><br />" +
										"NOTA: Es recomendable que cambie su contrase&ntilde;a desde el men&uacute; Administraci&oacute;n/Cambio de Contrase&ntilde;a.<br /><br /><br /><br /><br />" +
                                        "Gracias por utilizar nuestros servicios inform&aacute;ticos<br /><br />" +s
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
                gcMail.Send("Agenda - Bienvenido al sistema", oENTUsuario.Email, "Bienvenido al sistema", sHTMLMessage);

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;

        }

        ///<remarks>
        ///   <name>BPUsuario.SelectUsuario</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Usuarios</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectUsuario(ENTUsuario oENTUsuario){
            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAUsuario.SelectUsuario(oENTUsuario, this.ConnectionApplication, 0);

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
        ///   <name>BPUsuario.selectUsuario_Autenticacion</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Valida las credenciales de un usuario para verificar el acceso al sistema. Si las credenciales son válidas configura la sesión.</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectUsuario_Autenticacion(ENTUsuario oENTUsuario){
            GCAuthentication gcAuthentication = new GCAuthentication();
            GCEncryption gcEncryption = new GCEncryption();

            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            HttpContext oContext;

            try
            {

                // Encriptar el password
                oENTUsuario.Password = gcEncryption.EncryptString(oENTUsuario.Password, false);

                // Consulta a base de datos
                oENTResponse = oDAUsuario.SelectUsuario_Autenticacion(oENTUsuario, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Configurar de entidad de sesión
                oENTSession.UsuarioId = int.Parse(oENTResponse.DataSetResponse.Tables[1].Rows[0]["UsuarioId"].ToString());
                oENTSession.RolId = int.Parse(oENTResponse.DataSetResponse.Tables[1].Rows[0]["RolId"].ToString());
                oENTSession.Email = oENTUsuario.Email;
                oENTSession.Nombre = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                oENTSession.Titulo = oENTResponse.DataSetResponse.Tables[1].Rows[0]["TituloNombre"].ToString();
                oENTSession.DataTableSubMenu = oENTResponse.DataSetResponse.Tables[2];
                oENTSession.DataTableMenu = oENTResponse.DataSetResponse.Tables[3];

                // Autenticar el usuario
                gcAuthentication.AuthenticateUser(oENTSession.Email, this.SessionTimeout);
                oENTSession.TokenGenerado = true;

                // Variable de sesión con los datos del usuario
                oContext = HttpContext.Current;
                oContext.Session["oENTSession"] = oENTSession;

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;

        }

        ///<remarks>
        ///   <name>BPUsuario.SelectUsuario_RecuperaContrasena</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Recupera la contraseña de un usuario enviándola por correo electrónico</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectUsuario_RecuperaContrasena(ENTUsuario oENTUsuario){
            GCMail gcMail = new GCMail();
            GCEncryption gcEncryption = new GCEncryption();

            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            String Password = "";
            String NombreUsuario = "";
            String sHTMLMessage = "";

            try
            {

                // Consulta a base de datos
                oENTResponse = oDAUsuario.SelectUsuario_RecuperaContrasena(oENTUsuario, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

                // Validación de mensajes de la BD
                oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();
                if (oENTResponse.MessageDB != "") { return oENTResponse; }

                // Obtener el nombre de usuario y password
                NombreUsuario = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                Password = gcEncryption.DecryptString(oENTResponse.DataSetResponse.Tables[1].Rows[0]["Password"].ToString(), false);

                // Configuración del correo
                sHTMLMessage = "" +
                    "<head>" +
                      "<title>Agenda - Recuperaci&oacute;n de contrase&ntilde;a</title>" +
                   "</head>" +
                   "<body style='height:100%; margin:0px; padding:0px; width:100%;'>" +
                      "<div style='clear:both; height:80%; text-align:center; width:100%;'>" +
                         "<div style='clear:both; height:70%; margin:0px auto; position:relative; top:10%; width:90%;'>" +
                            "<table style='color:#339933; height:100%; font-family:Arial; font-size:12px; text-align:left; width:100%;'>" +
                                "<tr style='height:20%;' valign='middle'>" +
                                    "<td style='font-weight:bold;'>" +
                                        "Recuperaci&oacute;n de contrase&ntilde;a<br /><br />" +
                                        "<div style='border-bottom:1px solid #339933;'></div>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr style='height:80%;' valign='top'>" +
                                    "<td>" +
                                        "Usted ha solicitado la recuperaci&oacute;n de informaci&oacute;n de su usuario al sistema de Agenda. Los datos de acceso a la aplicaci&oacute;n son los siguientes:<br /><br /><br />" +
                                        "<font style='font-size:15px; font-weight:bold;'>Usuario:</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font style='color:#000000; font-weight:bold;'>" + oENTUsuario.Email + "</font><br />" +
                                        "<font style='font-size:15px; font-weight:bold;'>Contrase&ntilde;a:</font>&nbsp;<font style='color:#000000; font-weight:bold;'>" + Password + "</font><br /><br /><br />" +
                                        "Puede acceder al sistema haciendo click <a href='" + this.ApplicationURL + "' style='color:#000000; font-weight:bold;'>aqui</a><br /><br /><br />" +
                                        "Gracias por utilizar nuestros servicios inform&aacute;ticos<br /><br />" +
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
                gcMail.Send("Agenda - Recuperación de contraseña", oENTUsuario.Email, "Recuperación de contraseña", sHTMLMessage);

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;

        }

        ///<remarks>
        ///   <name>BPUsuario.UpdateUsuario</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un usuario</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateUsuario(ENTUsuario oENTUsuario){
            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAUsuario.UpdateUsuario(oENTUsuario, this.ConnectionApplication, 0);

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
        ///   <name>BPUsuario.UpdateUsuario_ActualizaContrasena</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la contraseña de un usuario</summary>
        ///<param name="oENTUsuario">Entidad de usuario con los parámetros necesarios para actualizar la contraseña</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateUsuario_ActualizaContrasena(ENTUsuario oENTUsuario){
            GCEncryption gcEncryption = new GCEncryption();

            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();


            try
            {

                // Encriptar el passwords
                oENTUsuario.Password = gcEncryption.EncryptString(oENTUsuario.Password, false);
                oENTUsuario.PasswordAnterior = gcEncryption.EncryptString(oENTUsuario.PasswordAnterior, false);

                // Transacción en base de datos
                oENTResponse = oDAUsuario.UpdateUsuario_ActualizaContrasena(oENTUsuario, this.ConnectionApplication, 0);

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
        ///   <name>BPUsuario.UpdateUsuario_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un Usuario</summary>
        ///<param name="oENTUsuario">Entidad de Usuario con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateUsuario_Estatus(ENTUsuario oENTUsuario){
            DAUsuario oDAUsuario = new DAUsuario();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAUsuario.UpdateUsuario_Estatus(oENTUsuario, this.ConnectionApplication, 0);

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
