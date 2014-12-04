/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPRol
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de reglas de negocio de la aplicación con métodos relacionados con los roles
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
    public class BPRol : BPBase
    {

        ///<remarks>
        ///   <name>BPRol.InsertRol</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un Rol</summary>
        ///<param name="oENTRol">Entidad de Rol con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertRol(ENTRol oENTRol){
            DARol oDARol = new DARol();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDARol.InsertRol(oENTRol, this.ConnectionApplication, 0);

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
        ///   <name>BPRol.SelectRol</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Rols</summary>
        ///<param name="oENTRol">Entidad de Rol con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectRol(ENTRol oENTRol){
            DARol oDARol = new DARol();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDARol.SelectRol(oENTRol, this.ConnectionApplication, 0);

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
        ///   <name>BPRol.UpdateRol</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un Rol</summary>
        ///<param name="oENTRol">Entidad de Rol con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateRol(ENTRol oENTRol){
            DARol oDARol = new DARol();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDARol.UpdateRol(oENTRol, this.ConnectionApplication, 0);

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
        ///   <name>BPRol.UpdateRol_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un Rol</summary>
        ///<param name="oENTRol">Entidad de Rol con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateRol_Estatus(ENTRol oENTRol){
            DARol oDARol = new DARol();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDARol.UpdateRol_Estatus(oENTRol, this.ConnectionApplication, 0);

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
