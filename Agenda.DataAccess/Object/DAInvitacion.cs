/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DAInvitacion
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
    public class DAInvitacion : DABase
    {

        ///<remarks>
        ///   <name>DAInvitacion.DeleteInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un comentario existente en una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionComentario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionComentario_Del", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionComentarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionComentarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
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
        ///   <name>DAInvitacion.DeleteInvitacionContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionContacto(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionContacto_Del", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionContactoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
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
        ///   <name>DAInvitacion.DeleteInvitacionFuncionario</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un funcionario para una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteInvitacionFuncionario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionFuncionario_Del", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId_Nuevo", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId_Temp;
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
        ///   <name>DAInvitacion.InsertInvitacion</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea una nueva invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacion(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacion_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("CategoriaId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.CategoriaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConductoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.ConductoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstatusInvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.EstatusInvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Ramo", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Ramo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Representante", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Representante;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Responsable", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Responsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.EventoDetalle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.EventoNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionObservaciones", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.InvitacionObservaciones;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaEvento", SqlDbType.Date);
            sqlPar.Value = oENTInvitacion.FechaEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEvento", SqlDbType.Time);
            sqlPar.Value = oENTInvitacion.HoraEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Comentarios;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblFuncionario", SqlDbType.Structured);
            sqlPar.Value = oENTInvitacion.Funcionario.DataTableUsuario;
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
        ///   <name>DAInvitacion.InsertInvitacionContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto a la invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionContacto(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionContacto_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Comentarios;
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
        ///   <name>DAInvitacion.InsertInvitacionComentario</name>
        ///   <create>29-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea un nuevo comentario a un invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionComentario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionComentario_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ModuloId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.ModuloId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Comentario", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Comentario;
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
        ///   <name>DAInvitacion.InsertInvitacionFuncionario</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo funcionario para una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertInvitacionFuncionario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionFuncionario_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId_Nuevo", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId_Temp;
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
        ///   <name>DAInvitacion.SelectInvitacion</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Invitaciones en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacion(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacion_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EstatusInvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.EstatusInvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTInvitacion.FechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaFin", SqlDbType.Date);
            sqlPar.Value = oENTInvitacion.FechaFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nivel", SqlDbType.TinyInt);
            sqlPar.Value = oENTInvitacion.Nivel;
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
        ///   <name>DAInvitacion.SelectInvitacion_Detalle</name>
        ///   <create>22-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el detalle de una invitación en particular</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacion_Detalle(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacion_Sel_Detalle", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
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
        ///   <name>DAInvitacion.SelectInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de comentarios realizados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacionComentario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionComentario_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionComentarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionComentarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTInvitacion.Activo;
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
        ///   <name>DAInvitacion.SelectInvitacionContacto</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a una invitación en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectInvitacionContacto(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionContacto_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionContactoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTInvitacion.Activo;
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
        ///   <name>DAInvitacion.UpdateInvitacion_DatosEvento</name>
        ///   <create>09-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos del Evento de una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_DatosEvento(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacion_Upd_DatosEvento", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("LugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.EventoNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaEvento", SqlDbType.Date);
            sqlPar.Value = oENTInvitacion.FechaEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraEvento", SqlDbType.Time);
            sqlPar.Value = oENTInvitacion.HoraEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.EventoDetalle;
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
        ///   <name>DAInvitacion.UpdateInvitacion_DatosGenerales</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos generales de una invitación existente</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacion_DatosGenerales(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacion_Upd_DatosGenerales", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("CategoriaId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.CategoriaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConductoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.ConductoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("PrioridadId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.PrioridadId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Ramo", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Ramo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Representante", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Representante;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SecretarioId_Responsable", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.SecretarioId_Responsable;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionObservaciones", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.InvitacionObservaciones;
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
        ///   <name>DAInvitacion.UpdateInvitacionComentario</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza un comentario existente en una invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacionComentario(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionComentario_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionComentarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionComentarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Comentario", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Comentario;
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
        ///   <name>DAInvitacion.UpdateInvitacionContacto</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a la invitación</summary>
        ///<param name="oENTInvitacion">Entidad de Invitacion con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateInvitacionContacto(ENTInvitacion oENTInvitacion, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspInvitacionContacto_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("InvitacionContactoId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("InvitacionId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.InvitacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTInvitacion.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTInvitacion.Contacto.Comentarios;
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
