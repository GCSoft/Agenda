﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPTipoAcomodo
' Autor: Ruben.Cobos
' Fecha: 09-Diciembre-2014
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
    public class BPTipoAcomodo : BPBase
    {

        ///<remarks>
        ///   <name>BPTipoAcomodo.InsertTipoAcomodo</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Contacto de Programa</summary>
        ///<param name="oENTTipoAcomodo">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertTipoAcomodo(ENTTipoAcomodo oENTTipoAcomodo){
            DATipoAcomodo oDATipoAcomodo = new DATipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoAcomodo.InsertTipoAcomodo(oENTTipoAcomodo, this.ConnectionApplication, 0);

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
        ///   <name>BPTipoAcomodo.SelectTipoAcomodo</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Tipos de Acomodo</summary>
        ///<param name="oENTTipoAcomodo">Entidad de Tipo de Acomodo con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectTipoAcomodo(ENTTipoAcomodo oENTTipoAcomodo){
            DATipoAcomodo oDATipoAcomodo = new DATipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoAcomodo.SelectTipoAcomodo(oENTTipoAcomodo, this.ConnectionApplication, 0);

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
        ///   <name>BPTipoAcomodo.SelectTipoAcomodo_Paginado</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Contacto de Programas de forma paginada</summary>
        ///<param name="oENTTipoAcomodo">Entidad de Contacto de Programa con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectTipoAcomodo_Paginado(ENTTipoAcomodo oENTTipoAcomodo){
            DATipoAcomodo oDATipoAcomodo = new DATipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoAcomodo.SelectTipoAcomodo_Paginado(oENTTipoAcomodo, this.ConnectionApplication, 0);

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
        ///   <name>BPTipoAcomodo.UpdateTipoAcomodo</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un Contacto de Programa existente</summary>
        ///<param name="oENTTipoAcomodo">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateTipoAcomodo(ENTTipoAcomodo oENTTipoAcomodo){
            DATipoAcomodo oDATipoAcomodo = new DATipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoAcomodo.UpdateTipoAcomodo(oENTTipoAcomodo, this.ConnectionApplication, 0);

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
        ///   <name>BPTipoAcomodo.UpdateTipoAcomodo_Estatus</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Contacto de Programa existente</summary>
        ///<param name="oENTTipoAcomodo">Entidad de Contacto de Programa con los parámetros necesarios para realizar la transacción</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateTipoAcomodo_Estatus(ENTTipoAcomodo oENTTipoAcomodo){
            DATipoAcomodo oDATipoAcomodo = new DATipoAcomodo();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoAcomodo.UpdateTipoAcomodo_Estatus(oENTTipoAcomodo, this.ConnectionApplication, 0);

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
