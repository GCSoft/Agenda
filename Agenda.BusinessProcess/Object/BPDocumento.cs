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
using Agenda.DataAccess.Object;
using Agenda.Entity.Object;
using System.Configuration;
using System.IO;
using System.Web;

namespace Agenda.BusinessProcess.Object
{
    public class BPDocumento : BPBase
    {

        // Enumeraciones
		public enum RepositoryTypes { Invitacion, Evento }

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
			String Path;
			String FileName;

			try{

				// Nombre del archivo físico
				FileName = PostedFile.FileName;

				// Armar el path
				switch(RepositoryType){
					case RepositoryTypes.Invitacion:
						Path = ConfigurationManager.AppSettings["Application.Repository"].ToString() + "I" + Seed + Convert.ToChar(92);
						break;

                    case RepositoryTypes.Evento:
						Path = ConfigurationManager.AppSettings["Application.Repository"].ToString() + "E" + Seed + Convert.ToChar(92);
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

				throw(ioEx);

			} catch (Exception ex){

				throw(ex);

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

			try{

				// Transacción en base de datos
				oENTResponse = oDADocumento.DeleteDocumento(oENTDocumento, this.ConnectionApplication, 0);

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

			try{

				// Transacción en base de datos
				oENTResponse = oDADocumento.InsertDocumento(oENTDocumento, this.ConnectionApplication, 0);

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
