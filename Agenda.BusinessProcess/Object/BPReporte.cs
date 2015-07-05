/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPReporte
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
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
    public class BPReporte : BPBase
    {


        ///<remarks>
        ///   <name>BPReporte.SelectReporte_AgendaDiaria</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el reporte de Agenda Diaria</summary>
        ///<param name="oENTReporte">Entidad de Reporte con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectReporte_AgendaDiaria(ENTReporte oENTReporte){
            DAReporte oDAReporte = new DAReporte();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAReporte.SelectReporte_AgendaDiaria(oENTReporte, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

        ///<remarks>
        ///   <name>BPReporte.SelectReporte_InvitacionPrensa</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el reporte de Invitaciones a la prensa</summary>
        ///<param name="oENTReporte">Entidad de Reporte con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectReporte_InvitacionPrensa(ENTReporte oENTReporte){
            DAReporte oDAReporte = new DAReporte();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAReporte.SelectReporte_InvitacionPrensa(oENTReporte, this.ConnectionApplication, 0);

                // Validación de error en consulta
                if (oENTResponse.GeneratesException) { return oENTResponse; }

            }catch (Exception ex){
                oENTResponse.ExceptionRaised(ex.Message);
            }

            // Resultado
            return oENTResponse;
        }

    }
}
