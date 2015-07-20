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
        ///   <name>DAEvento.DeleteEventoContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a un evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteEventoContacto(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoContacto_Del", sqlCnn);
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
        ///   <name>DAEvento.InsertEvento</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo Evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertEvento(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("CategoriaId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.CategoriaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConductoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.ConductoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstatusEventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EstatusEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Ramo", SqlDbType.Int);
            sqlPar.Value = oENTEvento.SecretarioId_Ramo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Responsable", SqlDbType.Int);
            sqlPar.Value = oENTEvento.SecretarioId_Responsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoDetalle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoObservaciones", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.EventoObservaciones;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaFin", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEventoInicio", SqlDbType.Time);
            sqlPar.Value = oENTEvento.HoraEventoInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEventoFin", SqlDbType.Time);
            sqlPar.Value = oENTEvento.HoraEventoFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MotivoRechazo", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.MotivoRechazo;
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

            sqlPar = new SqlParameter("Logistica", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Logistica;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Protocolo", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Protocolo;
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
        ///   <name>DAEvento.InsertEventoContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto al evento</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertEventoContacto(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoContacto_Ins", sqlCnn);
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

            sqlPar = new SqlParameter("PalabraClave", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.PalabraClave;
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
        ///<summary>Obtiene el detalle de un evento en particular</summary>
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
        ///   <name>DAEvento.SelectEventoContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a un evento en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectEventoContacto(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoContacto_Sel", sqlCnn);
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

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Activo;
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
        ///   <name>DAEvento.UpdateEvento_Configuracion</name>
        ///   <create>28-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de configuración de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_Configuracion(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Upd_Configuracion", sqlCnn);
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

            sqlPar = new SqlParameter("TipoAcomodoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.TipoAcomodoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TipoVestimentaId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.TipoVestimentaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MedioComunicacionId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.MedioComunicacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TipoVestimentaOtro", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.TipoVestimentaOtro;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PronosticoClima", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.PronosticoClima;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TemperaturaMinima", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.TemperaturaMinima;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TemperaturaMaxima", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.TemperaturaMaxima;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Aforo", SqlDbType.Int);
            sqlPar.Value = oENTEvento.Aforo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TipoMontaje", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.TipoMontaje;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("LugarArribo", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.LugarArribo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Esposa", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Esposa;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EsposaSi", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.EsposaSi;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EsposaNo", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.EsposaNo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EsposaConfirma", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.EsposaConfirma;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PropuestaAcomodo", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.PropuestaAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("AcomodoObservaciones", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.AcomodoObservaciones;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("AccionRealizar", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.AccionRealizar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("CaracteristicasInvitados", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.CaracteristicasInvitados;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Menu", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Menu;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ComiteHelipuerto", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.ComiteHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoLugar", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.HelipuertoLugar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoDomicilio", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.HelipuertoDomicilio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoCoordenadas", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.HelipuertoCoordenadas;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloInvitacionA", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloInvitacionA;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloResponsableEvento", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloResponsableEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloBandera", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloBandera;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloLeyenda", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloLeyenda;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloResponsable", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloResponsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloSonido", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloSonido;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloResponsableSonido", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloResponsableSonido;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloDesayuno", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloDesayuno;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloSillas", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloSillas;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloMesas", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloMesas;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ProtocoloPresentacion", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ProtocoloPresentacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblMedioTraslado", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableMedioTraslado;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteHelipuerto", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableComiteHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteRecepcion", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableComiteRecepcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblOrdenDia", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableOrdenDia;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblAcomodo", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblResponsable", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableResponsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblResponsableLogistica", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableResponsableLogistica;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ListadoAdicional", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.ListadoAdicional;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ListadoAdicionalTitulo", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.ListadoAdicionalTitulo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblListadoAdicional", SqlDbType.Structured);
            sqlPar.Value = oENTEvento.DataTableListadoAdicional;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioDocumento", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.NotaInicioDocumento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinDocumento", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.NotaFinDocumento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaDocumento", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.NotaDocumento;
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

            sqlPar = new SqlParameter("FechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaFin", SqlDbType.Date);
            sqlPar.Value = oENTEvento.FechaFin;
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
        ///   <name>DAEvento.UpdateEvento_EliminarRepresentante</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina la representación de un evento existente</summary>
        ///<param name="oENTEvento">Entidad de Evento con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateEvento_EliminarRepresentante(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEvento_Upd_EliminarRepresentante", sqlCnn);
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
        ///   <name>DAEvento.UpdateEventoContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a un evento</summary>
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



        ///<remarks>
        ///   <name>DAEvento.UpdateEventoComiteHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoComiteHelipuerto_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoComiteHelipuerto_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
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
        ///   <name>DAEvento.UpdateEventoComiteRecepcion_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoComiteRecepcion_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoComiteRecepcion_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Separador", SqlDbType.TinyInt);
            sqlPar.Value = oENTEvento.Separador;
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
        ///   <name>DAEvento.UpdateEventoOrdenDia_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoOrdenDia_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoOrdenDia_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Detalle", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
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
        ///   <name>DAEvento.UpdateEventoAcomodo_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoAcomodo_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoAcomodo_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
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
        ///   <name>DAEvento.UpdateEventoListadoAdicional_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoListadoAdicional_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoListadoAdicional_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
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
        ///   <name>DAEvento.UpdateEventoResponsableEvento_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoResponsable_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoResponsable_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
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
        ///   <name>DAEvento.UpdateEventoResponsableLogistica_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateEventoResponsableLogistica_Item(ENTEvento oENTEvento, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspEventoResponsableLogistica_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoId", SqlDbType.Int);
            sqlPar.Value = oENTEvento.EventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTEvento.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTEvento.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto", SqlDbType.VarChar);
            sqlPar.Value = oENTEvento.Puesto;
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
