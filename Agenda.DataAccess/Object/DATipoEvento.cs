/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DATipoEvento
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
using System.Data;
using System.Data.SqlClient;
using Agenda.Entity.Object;

namespace Agenda.DataAccess.Object
{
    public class DATipoEvento : DABase
    {

        ///<remarks>
        ///   <name>DATipoEvento.SelectTipoEvento</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Tipos de Evento en base a los parámetros proporcionados</summary>
        ///<param name="oENTTipoEvento">Entidad de Tipo de Evento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectTipoEvento(ENTTipoEvento oENTTipoEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspTipoEvento_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("TipoEventoId", SqlDbType.Int);
            sqlPar.Value = oENTTipoEvento.TipoEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTTipoEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTTipoEvento.Activo;
            sqlCom.Parameters.Add(sqlPar);

            // Inicializaciones
            oENTResponse.DataSetResponse = new DataSet();
            sqlDA = new SqlDataAdapter(sqlCom);

            // Transacción
            try
            {

                sqlCnn.Open();
                sqlDA.Fill(oENTResponse.DataSetResponse);
                sqlCnn.Close();

            }catch (SqlException sqlEx){

                oENTResponse.ExceptionRaised(sqlEx.Message);
            }catch (Exception ex){

                oENTResponse.ExceptionRaised(ex.Message);
            }finally{

                if (sqlCnn.State == ConnectionState.Open) { sqlCnn.Close(); }
                sqlCnn.Dispose();
            }

            // Resultado
            return oENTResponse;
        }

    }
}
