/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DALugarEvento
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
    public class DALugarEvento : DABase
    {

        ///<remarks>
        ///   <name>DALugarEvento.InsertLugarEvento</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Lugar de Evento</summary>
        ///<param name="oENTLugarEvento">Entidad de Lugar de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertLugarEvento(ENTLugarEvento oENTLugarEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspLugarEvento_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("ColoniaId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.ColoniaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Calle", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Calle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NumeroExterior", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.NumeroExterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NumeroInterior", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.NumeroInterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTLugarEvento.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.Rank;
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
        ///   <name>DALugarEvento.SelectLugarEvento</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Lugares de Evento en base a los parámetros proporcionados</summary>
        ///<param name="oENTLugarEvento">Entidad de LugarEvento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectLugarEvento(ENTLugarEvento oENTLugarEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspLugarEvento_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstadoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.EstadoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MunicipioId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.MunicipioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ColoniaId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.ColoniaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTLugarEvento.Activo;
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
        ///   <name>DALugarEvento.SelectLugarEvento_Paginado</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado paginado de Lugares de Evento en base a los parámetros proporcionados</summary>
        ///<param name="oENTLugarEvento">Entidad de LugarEvento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectLugarEvento_Paginado(ENTLugarEvento oENTLugarEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspLugarEvento_Sel_Paginado", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstadoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.EstadoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MunicipioId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.MunicipioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ColoniaId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.ColoniaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTLugarEvento.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Page", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.Paginacion.Page;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PageSize", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.Paginacion.PageSize;
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
        ///   <name>DALugarEvento.UpdateLugarEvento</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un Lugar de Evento existente</summary>
        ///<param name="oENTLugarEvento">Entidad de Lugar de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateLugarEvento(ENTLugarEvento oENTLugarEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspLugarEvento_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ColoniaId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.ColoniaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Calle", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Calle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NumeroExterior", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.NumeroExterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NumeroInterior", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.NumeroInterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTLugarEvento.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTLugarEvento.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.Rank;
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
        ///   <name>DALugarEvento.UpdateLugarEvento_Estatus</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Lugar de Evento existente</summary>
        ///<param name="oENTLugarEvento">Entidad de Lugar de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateLugarEvento_Estatus(ENTLugarEvento oENTLugarEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspLugarEvento_Upd_Estatus", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTLugarEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTLugarEvento.Activo;
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
