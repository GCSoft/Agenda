/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPContactoPrograma
' Autor: Ruben.Cobos
' Fecha: 18-Diciembre-2014
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
using System.Data;
using System.Web;

namespace Agenda.BusinessProcess.Object
{
    public class BPContactoPrograma : BPBase
    {

        ///<remarks>
        ///   <name>BPContactoPrograma.InsertContactoPrograma</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Contacto de Programa</summary>
        ///<param name="oENTContactoPrograma">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertContactoPrograma(ENTContactoPrograma oENTContactoPrograma){
            DAContactoPrograma oDAContactoPrograma = new DAContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAContactoPrograma.InsertContactoPrograma(oENTContactoPrograma, this.ConnectionApplication, 0);

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
        ///   <name>BPContactoPrograma.SelectContactoPrograma</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Contacto de Programas</summary>
        ///<param name="oENTContactoPrograma">Entidad de Contacto de Programa con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectContactoPrograma(ENTContactoPrograma oENTContactoPrograma){
            DAContactoPrograma oDAContactoPrograma = new DAContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAContactoPrograma.SelectContactoPrograma(oENTContactoPrograma, this.ConnectionApplication, 0);

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
        ///   <name>BPContactoPrograma.SelectContactoPrograma_Paginado</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Contacto de Programas de forma paginada</summary>
        ///<param name="oENTContactoPrograma">Entidad de Contacto de Programa con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectContactoPrograma_Paginado(ENTContactoPrograma oENTContactoPrograma){
            DAContactoPrograma oDAContactoPrograma = new DAContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAContactoPrograma.SelectContactoPrograma_Paginado(oENTContactoPrograma, this.ConnectionApplication, 0);

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
        ///   <name>BPContactoPrograma.UpdateContactoPrograma</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un Contacto de Programa existente</summary>
        ///<param name="oENTContactoPrograma">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateContactoPrograma(ENTContactoPrograma oENTContactoPrograma){
            DAContactoPrograma oDAContactoPrograma = new DAContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAContactoPrograma.UpdateContactoPrograma(oENTContactoPrograma, this.ConnectionApplication, 0);

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
        ///   <name>BPContactoPrograma.UpdateContactoPrograma_Estatus</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Contacto de Programa existente</summary>
        ///<param name="oENTContactoPrograma">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateContactoPrograma_Estatus(ENTContactoPrograma oENTContactoPrograma){
            DAContactoPrograma oDAContactoPrograma = new DAContactoPrograma();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAContactoPrograma.UpdateContactoPrograma_Estatus(oENTContactoPrograma, this.ConnectionApplication, 0);

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
