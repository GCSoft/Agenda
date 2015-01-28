/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DAEvento
' Autor: Ruben.Cobos
' Fecha: 22-Diciembre-2014
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
    public class DAEvento : DABase
    {

        ///<remarks>
        ///   <name>DAEvento.SelectEvento</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Eventos en base a los parámetros proporcionados</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstatusEventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EstatusEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaFin", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nivel", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Nivel;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Dependencia", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Dependencia;
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
        ///   <name>DAEvento.SelectEvento_Calendario</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Eventos en base a los parámetros proporcionados el cual se deplegará en el calendario</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento_Calendario(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Sel_Calendario", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Mes", SqlDbType.Int);
            sqlPar.Value = oENTEvento.Mes;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Anio", SqlDbType.Int);
            sqlPar.Value = oENTEvento.Anio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Dependencia", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Dependencia;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblEstatusEvento", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableEstatusEvento;
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
        ///   <name>DAEvento.SelectEvento_Detalle</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el detalle de una invitación en particular</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEvento_Detalle(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Sel_Detalle", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
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
        ///   <name>DAEvento.UpdateEvento_Cancelar</name>
        ///   <create>12-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un evento existente a cancelada</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_Cancelar(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Upd_Cancelar", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MotivoRechazo", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.MotivoRechazo;
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
        ///   <name>DAEvento.UpdateEvento_DatosEvento</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos del Evento de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_DatosEvento(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Upd_DatosEvento", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaEvento", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEventoInicio", SqlDbType.Time);
            sqlPar.Value = oENTEvento.HoraEventoInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEventoFin", SqlDbType.Time);
            sqlPar.Value = oENTEvento.HoraEventoFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoDetalle;
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
        ///   <name>DAEvento.UpdateEvento_DatosGenerales</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos generales de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_DatosGenerales(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Upd_DatosGenerales", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("CategoriaId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.CategoriaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConductoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.ConductoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Ramo", SqlDbType.Int);
            sqlPar.Value = oENTEvento.SecretarioId_Ramo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Representante", SqlDbType.Int);
            sqlPar.Value = oENTEvento.SecretarioId_Representante;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Responsable", SqlDbType.Int);
            sqlPar.Value = oENTEvento.SecretarioId_Responsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoObservaciones", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoObservaciones;
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
        ///   <name>DAEvento.UpdateEventoContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a la invitación</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEventoContacto(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoContacto_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EventoContactoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Contacto.Comentarios;
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
