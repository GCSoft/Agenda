/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DASecretario
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
using System.Data;
using System.Data.SqlClient;
using Agenda.Entity.Object;

namespace Agenda.DataAccess.Object
{
    public class DASecretario : DABase
    {

        ///<remarks>
        ///   <name>DASecretario.InsertSecretario</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Secretario</summary>
        ///<param name="oENTSecretario">Entidad de Secretario con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertSecretario(ENTSecretario oENTSecretario, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSecretario_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("TituloId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.TituloId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSecretario.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.Rank;
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

        ///<remarks>
        ///   <name>DASecretario.SelectSecretario</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Secretarios en base a los parámetros proporcionados</summary>
        ///<param name="oENTSecretario">Entidad de Secretario con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectSecretario(ENTSecretario oENTSecretario, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSecretario_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SecretarioId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.SecretarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSecretario.Activo;
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

        ///<remarks>
        ///   <name>DASecretario.SelectSecretario_Paginado</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado paginado de Secretarios en base a los parámetros proporcionados</summary>
        ///<param name="oENTSecretario">Entidad de Secretario con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectSecretario_Paginado(ENTSecretario oENTSecretario, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSecretario_Sel_Paginado", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SecretarioId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.SecretarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSecretario.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Page", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.Paginacion.Page;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PageSize", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.Paginacion.PageSize;
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

        ///<remarks>
        ///   <name>DASecretario.UpdateSecretario</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un Secretario existente</summary>
        ///<param name="oENTSecretario">Entidad de Secretario con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSecretario(ENTSecretario oENTSecretario, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSecretario_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SecretarioId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.SecretarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TituloId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.TituloId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTSecretario.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSecretario.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.Rank;
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

        ///<remarks>
        ///   <name>DASecretario.UpdateSecretario_Estatus</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Secretario existente</summary>
        ///<param name="oENTSecretario">Entidad de Secretario con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSecretario_Estatus(ENTSecretario oENTSecretario, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSecretario_Upd_Estatus", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SecretarioId", SqlDbType.Int);
            sqlPar.Value = oENTSecretario.SecretarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSecretario.Activo;
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
