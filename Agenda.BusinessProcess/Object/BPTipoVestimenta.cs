/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPTipoVestimenta
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
    public class BPTipoVestimenta : BPBase
    {

        ///<remarks>
        ///   <name>BPTipoVestimenta.SelectTipoVestimenta</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Tipo de Vestimenta</summary>
        ///<param name="oENTTipoVestimenta">Entidad de Tipo de Evento con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectTipoVestimenta(ENTTipoVestimenta oENTTipoVestimenta){
            DATipoVestimenta oDATipoVestimenta = new DATipoVestimenta();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDATipoVestimenta.SelectTipoVestimenta(oENTTipoVestimenta, this.ConnectionApplication, 0);

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
