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
using Agenda.DataAccess.Object;
using Agenda.Entity.Object;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;

namespace Agenda.BusinessProcess.Object
{
    public class BPDocumento : BPBase
    {

        // Enumeraciones
		public enum RepositoryTypes { Invitacion, Evento }

        ///<remarks>
        ///   <name>BPDocumento.CloneFile</name>
		///   <create>22-Diciembre-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Clona un archivo prestablecio en el servidor, regresando la ruta en donde se guardó físicamente</summary>
        ///<param name="FileName">Nombre del archivo a clonar</param>
		///<param name="Seed">Semilla el directorio. ID de Expediente o Solicitud</param>
		///<param name="RepositoryType">Tipo de repositorio (Expediente o Solicitud)</param>
		///<returns>La ruta completa del directorio creado</returns>
		public String CloneFile( String FileName,  String Seed, RepositoryTypes RepositoryType){
            String Path = "";

			try{

                // Obtener la ruta física del contenedor de archivos
                if ( ConfigurationManager.AppSettings["Application.Repository.Virtual"].ToString() == "0" ){

                    Path = ConfigurationManager.AppSettings["Application.Repository"].ToString();
                }else{

                    Path = HttpContext.Current.Server.MapPath ( ConfigurationManager.AppSettings["Application.Repository"].ToString() );
                }


				// Armar el path
				switch(RepositoryType){
					case RepositoryTypes.Invitacion:

                        Path = Path + "I" + Seed + Convert.ToChar(92);
						break;

                    case RepositoryTypes.Evento:
                        Path = Path + "E" + Seed + Convert.ToChar(92);
						break;

					default:
						throw( new Exception("Tipo de repositorio inválido"));
				}

				// Validar existencia de la ruta
				if (!Directory.Exists(Path)) { Directory.CreateDirectory(Path); }

				// Validar la existencia del archivo
				if (File.Exists(Path + FileName)) { 

                    switch(RepositoryType){
					    case RepositoryTypes.Invitacion:
                            throw (new Exception("Ya existe éste archivo asociado a la invitación"));

                        case RepositoryTypes.Evento:
                            throw (new Exception("Ya existe éste archivo asociado al evento"));
				    }

                }

				// Clonar el archivo
                System.IO.File.Copy( HttpContext.Current.Server.MapPath ("~/Include/Image/Cuadernillo/" + FileName), Path + FileName);

			}catch (IOException ioEx){

				throw( new Exception( ioEx.Message + " [" + Path + FileName + "]" ) );

			} catch (Exception ex){

				throw( new Exception( ex.Message + " [" + Path + FileName + "]" ) );

			}

			// Directorio con nombre de archivo
			return Path + FileName;
		}

		///<remarks>
		///   <name>BPDocumento.UploadFile</name>
		///   <create>22-Diciembre-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Sube un archivo al servidor, regresando la ruta en donde se guardó físicamente</summary>
		///<param name="PostedFile">Archivo a subir</param>
		///<param name="Seed">Semilla el directorio. ID de Expediente o Solicitud</param>
		///<param name="RepositoryType">Tipo de repositorio (Expediente o Solicitud)</param>
		///<returns>La ruta completa del directorio creado</returns>
		public String UploadFile( HttpPostedFile PostedFile,  String Seed, RepositoryTypes RepositoryType){
            String Path = "";
            String FileName = "";

			try{

				// Nombre del archivo físico
				//FileName = PostedFile.FileName;
                FileName = new FileInfo(PostedFile.FileName).Name;

                // Obtener la ruta física del contenedor de archivos
                if ( ConfigurationManager.AppSettings["Application.Repository.Virtual"].ToString() == "0" ){

                    Path = ConfigurationManager.AppSettings["Application.Repository"].ToString();
                }else{

                    Path = HttpContext.Current.Server.MapPath ( ConfigurationManager.AppSettings["Application.Repository"].ToString() );
                }


				// Armar el path
				switch(RepositoryType){
					case RepositoryTypes.Invitacion:

                        Path = Path + "I" + Seed + Convert.ToChar(92);
						break;

                    case RepositoryTypes.Evento:
                        Path = Path + "E" + Seed + Convert.ToChar(92);
						break;

					default:
						throw( new Exception("Tipo de repositorio inválido"));
				}

				// Validar existencia de la ruta
				if (!Directory.Exists(Path)) { Directory.CreateDirectory(Path); }

				// Validar la existencia del archivo
				if (File.Exists(Path + FileName)) { 

                    switch(RepositoryType){
					    case RepositoryTypes.Invitacion:
                            throw (new Exception("Ya existe éste archivo asociado a la invitación"));

                        case RepositoryTypes.Evento:
                            throw (new Exception("Ya existe éste archivo asociado al evento"));
				    }

                }

				// Cargar el archivo
				PostedFile.SaveAs(Path + FileName);

			}catch (IOException ioEx){

				throw( new Exception( ioEx.Message + " [" + Path + FileName + "]" ) );

			} catch (Exception ex){

				throw( new Exception( ex.Message + " [" + Path + FileName + "]" ) );

			}

			// Directorio con nombre de archivo
			return Path + FileName;
		}

		///<remarks>
		///   <name>BPDocumento.DeleteDocumento</name>
		///   <create>22-Diciembre-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Elimina un documento en la BD</summary>
		///<param name="oENTDocumento">Entidad de Documento con los parámetros necesarios para realizar la transacción</param>
		///<returns>Una entidad de respuesta</returns>
		public ENTResponse DeleteDocumento(ENTDocumento oENTDocumento){
			DADocumento oDADocumento = new DADocumento();
			ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

			try{

				// Transacción en base de datos
				oENTResponse = oDADocumento.DeleteDocumento(oENTDocumento, this.ConnectionApplication, 0);

				// Validación de error en consulta
				if (oENTResponse.GeneratesException) { return oENTResponse; }

				// Validación de mensajes de la BD
				oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa en Eventos)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTDocumento.RolId ){
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
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
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
		///   <name>BPDocumento.InsertDocumento</name>
		///   <create>22-Diciembre-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Guarda un documento en la BD</summary>
		///<param name="oENTDocumento">Entidad de Documento con los parámetros necesarios para realizar la transacción</param>
		///<returns>Una entidad de respuesta</returns>
		public ENTResponse InsertDocumento(ENTDocumento oENTDocumento){
			DADocumento oDADocumento = new DADocumento();
			ENTResponse oENTResponse = new ENTResponse();

            GCMail gcMail = new GCMail();
            String Contactos = "";
            String Dependencia = "";
            String HTMLMessage = "";

			try{

				// Transacción en base de datos
				oENTResponse = oDADocumento.InsertDocumento(oENTDocumento, this.ConnectionApplication, 0);

				// Validación de error en consulta
				if (oENTResponse.GeneratesException) { return oENTResponse; }

				// Validación de mensajes de la BD
				oENTResponse.MessageDB = oENTResponse.DataSetResponse.Tables[0].Rows[0]["Response"].ToString();

                // Envío de correo (se crea el listado sólo si la transacción fue exitosa en Eventos)
                if ( oENTResponse.DataSetResponse.Tables[2].Rows.Count > 0 ){

                    // Listado de direcciones
                    foreach (DataRow rowContacto in oENTResponse.DataSetResponse.Tables[2].Rows){
                        Contactos = (Contactos == "" ? rowContacto["Email"].ToString() : Contactos + "," + rowContacto["Email"].ToString());
                    }

                    // Determinar la dependencia
                    switch( oENTDocumento.RolId ){
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
                                                    Dependencia + " le notifica que " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Comentarios"].ToString().Trim().Replace(".", "").ToLower() + " <font style='color:#000000; font-style: italic; font-weight:bold;'>" + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString() + "</font>.<br /><br /><br />" +
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
		///   <name>BPDocumento.SelectDocumento_Path</name>
        ///   <create>22-Diciembre-2014</create>
		///   <author>Ruben.Cobos</author>
		///</remarks>
		///<summary>Consulta la ruta física en donde se almacenó un documento</summary>
		///<param name="oENTDocumento">Entidad de Documento con los parámetros necesarios para realizar la transacción</param>
		///<returns>Una entidad de respuesta</returns>
		public ENTResponse SelectDocumento_Path(ENTDocumento oENTDocumento){
			DADocumento oDADocumento = new DADocumento();
			ENTResponse oENTResponse = new ENTResponse();

			try{

				// Transacción en base de datos
				oENTResponse = oDADocumento.SelectDocumento_Path(oENTDocumento, this.ConnectionApplication, 0);

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
