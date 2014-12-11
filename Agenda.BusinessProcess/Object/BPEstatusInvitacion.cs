/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPEstatusInvitacion
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
    public class BPEstatusInvitacion : BPBase
    {


        ///<remarks>
        ///   <name>BPEstatusInvitacion.SelectEstatusInvitacion</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Estatus de Invitacion</summary>
        ///<param name="oENTEstatusInvitacion">Entidad de Estatus de Invitacion con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEstatusInvitacion(ENTEstatusInvitacion oENTEstatusInvitacion){
            DAEstatusInvitacion oDAEstatusInvitacion = new DAEstatusInvitacion();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAEstatusInvitacion.SelectEstatusInvitacion(oENTEstatusInvitacion, this.ConnectionApplication, 0);

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
