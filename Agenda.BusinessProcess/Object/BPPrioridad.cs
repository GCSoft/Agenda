/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPPrioridad
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
    public class BPPrioridad : BPBase
    {

        ///<remarks>
        ///   <name>BPPrioridad.SelectPrioridad</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Prioridades</summary>
        ///<param name="oENTPrioridad">Entidad de Prioridad con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectPrioridad(ENTPrioridad oENTPrioridad){
            DAPrioridad oDAPrioridad = new DAPrioridad();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAPrioridad.SelectPrioridad(oENTPrioridad, this.ConnectionApplication, 0);

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
