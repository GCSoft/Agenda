/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPRespuestaEvaluacion
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
    public class BPRespuestaEvaluacion : BPBase
    {

        ///<remarks>
        ///   <name>BPRespuestaEvaluacion.SelectRespuestaEvaluacion</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Respuestas de Evaluación en base a los parámetros proporcionados</summary>
        ///<param name="oENTRespuestaEvaluacion">Entidad de Respuesta de Evaluación con los parámetros necesarios para consultar la información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectRespuestaEvaluacion(ENTRespuestaEvaluacion oENTRespuestaEvaluacion){
            DARespuestaEvaluacion oDARespuestaEvaluacion = new DARespuestaEvaluacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDARespuestaEvaluacion.SelectRespuestaEvaluacion(oENTRespuestaEvaluacion, this.ConnectionApplication, 0);

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
