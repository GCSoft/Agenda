/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DAGira
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
    public class DAGira : DABase
    {

        ///<remarks>
        ///   <name>DAGira.DeleteGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>elimina lógicamente una Configuración asociada a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteGiraConfiguracion(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraConfiguracion_Del", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
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
        ///   <name>DAGira.DeleteGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Elimina lógicamente un contacto asociado a una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse DeleteGiraContacto(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraContacto_Del", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraContactoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
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
        ///   <name>DAGira.InsertGira</name>
        ///   <create>25-Marzo-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea una nueva Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGira(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGira_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("EstatusGiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.EstatusGiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.GiraDetalle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.GiraNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaGiraInicio", SqlDbType.Date);
            sqlPar.Value = oENTGira.FechaGiraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaGiraFin", SqlDbType.Date);
            sqlPar.Value = oENTGira.FechaGiraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraGiraInicio", SqlDbType.Time);
            sqlPar.Value = oENTGira.HoraGiraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraGiraFin", SqlDbType.Time);
            sqlPar.Value = oENTGira.HoraGiraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Comentarios;
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
        ///   <name>DAGira.InsertGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia una nueva Configuración a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGiraConfiguracion(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraConfiguracion_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TipoGiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.TipoGiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionGrupo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.ConfiguracionGrupo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionFechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTGira.ConfiguracionFechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionFechaFin", SqlDbType.Date);
            sqlPar.Value = oENTGira.ConfiguracionFechaFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionHoraInicio", SqlDbType.Time);
            sqlPar.Value = oENTGira.ConfiguracionHoraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionHoraFin", SqlDbType.Time);
            sqlPar.Value = oENTGira.ConfiguracionHoraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.ConfiguracionDetalle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoLugar", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoLugar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoDomicilio", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoDomicilio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoCoordenadas", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoCoordenadas;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoLugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoMedioComunicacionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.MedioComunicacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoAcomodoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.TipoAcomodoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoVestimentaId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.TipoVestimentaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoLugarArribo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.LugarArribo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoMontaje", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.TipoMontaje;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoAforo", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.Aforo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoCaracteristicasInvitados", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.CaracteristicasInvitados;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposa", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.Esposa;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaSi", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaSi;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaNo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaNo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaConfirma", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaConfirma;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoVestimentaOtro", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.TipoVestimentaOtro;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoMenu", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.Menu;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoPronosticoClima", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.PronosticoClima;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoAccionRealizar", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.AccionRealizar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionActivo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.ConfiguracionActivo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblAcompanaHelipuerto", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableAcompanaHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteHelipuerto", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableComiteHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblMedioTraslado", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableMedioTraslado;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteRecepcion", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableComiteRecepcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblOrdenDia", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableOrdenDia;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblAcomodo", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioEvento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinEvento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaEvento", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioComite", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinComite", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaComite", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioOrden", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinOrden", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaOrden", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioAcomodo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinAcomodo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaAcomodo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaAcomodo;
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
        ///   <name>DAGira.InsertGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Asocia un nuevo contacto a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertGiraContacto(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraContacto_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Comentarios;
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
        ///   <name>DAGira.SelectGira_Detalle</name>
        ///   <create>25-Marzo-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene el detalle de una gira en particular</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGira_Detalle(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGira_Sel_Detalle", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
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
        ///   <name>DAGira.SelectGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Configuraciones asociadas a una Gira en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGiraConfiguracion(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraConfiguracion_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Activo;
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
        ///   <name>DAGira.SelectGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de contactos asociados a una Gira en particular en base a los parámetros proporcionados</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectGiraContacto(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraContacto_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraContactoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Activo;
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
        ///   <name>DAGira.UpdateGira_Cancelar</name>
        ///   <create>12-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza el estatus de un Gira existente a cancelada</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGira_Cancelar(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGira_Upd_Cancelar", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MotivoRechazo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.MotivoRechazo;
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
        ///   <name>DAGira.UpdateGira_DatosGira</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la sección de datos de una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGira_DatosGira(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGira_Upd_DatosGira", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraNombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.GiraNombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaGiraInicio", SqlDbType.Date);
            sqlPar.Value = oENTGira.FechaGiraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("FechaGiraFin", SqlDbType.Date);
            sqlPar.Value = oENTGira.FechaGiraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraGiraInicio", SqlDbType.Time);
            sqlPar.Value = oENTGira.HoraGiraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HoraGiraFin", SqlDbType.Time);
            sqlPar.Value = oENTGira.HoraGiraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.GiraDetalle;
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
        ///   <name>DAGira.UpdateGiraConfiguracion</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza una Configuración existente y asociada a la Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGiraConfiguracion(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraConfiguracion_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("TipoGiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.TipoGiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionGrupo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.ConfiguracionGrupo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionFechaInicio", SqlDbType.Date);
            sqlPar.Value = oENTGira.ConfiguracionFechaInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionFechaFin", SqlDbType.Date);
            sqlPar.Value = oENTGira.ConfiguracionFechaFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionHoraInicio", SqlDbType.Time);
            sqlPar.Value = oENTGira.ConfiguracionHoraInicio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionHoraFin", SqlDbType.Time);
            sqlPar.Value = oENTGira.ConfiguracionHoraFin;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionDetalle", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.ConfiguracionDetalle;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoLugar", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoLugar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoDomicilio", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoDomicilio;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("HelipuertoCoordenadas", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.HelipuertoCoordenadas;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoLugarEventoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.LugarEventoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoMedioComunicacionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.MedioComunicacionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoAcomodoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.TipoAcomodoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoVestimentaId", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.TipoVestimentaId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoLugarArribo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.LugarArribo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoMontaje", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.TipoMontaje;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoAforo", SqlDbType.Int);
            sqlPar.Value = oENTGira.Evento.Aforo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoCaracteristicasInvitados", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.CaracteristicasInvitados;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposa", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.Esposa;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaSi", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaSi;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaNo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaNo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoEsposaConfirma", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.EsposaConfirma;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoTipoVestimentaOtro", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.TipoVestimentaOtro;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoMenu", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.Menu;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoPronosticoClima", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.PronosticoClima;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("EventoAccionRealizar", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.AccionRealizar;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ConfiguracionActivo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.ConfiguracionActivo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblAcompanaHelipuerto", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableAcompanaHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteHelipuerto", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableComiteHelipuerto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblMedioTraslado", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableMedioTraslado;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblComiteRecepcion", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableComiteRecepcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblOrdenDia", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableOrdenDia;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("tblAcomodo", SqlDbType.Structured);
            sqlPar.Value = oENTGira.DataTableAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioEvento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinEvento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaEvento", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaEvento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioComite", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinComite", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaComite", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaComite;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioOrden", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinOrden", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaOrden", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioAcomodo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaInicioAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinAcomodo", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.Evento.NotaFinAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaAcomodo", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Evento.NotaAcomodo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaInicioDocumento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.NotaInicioDocumento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaFinDocumento", SqlDbType.TinyInt);
            sqlPar.Value = oENTGira.NotaFinDocumento;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NotaDocumento", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.NotaDocumento;
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
        ///   <name>DAGira.UpdateGiraContacto</name>
        ///   <create>30-Marzo-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un contacto asociado a una Gira</summary>
        ///<param name="oENTGira">Entidad de Gira con los parámetros necesarios para realizar la transacción</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateGiraContacto(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraContacto_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("GiraContactoId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraContactoId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Puesto;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Organizacion", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Organizacion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Telefono", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Telefono;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Email", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Email;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Contacto_Comentarios", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Contacto.Comentarios;
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
        ///   <name>DAGira.UpdateGiraComiteHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraComiteHelipuerto_Item(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraComiteHelipuerto_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTGira.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTGira.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Puesto;
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
        ///   <name>DAGira.UpdateGiraAcompanaHelipuerto_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraAcompanaHelipuerto_Item(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraAcompanaHelipuerto_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTGira.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTGira.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Puesto;
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
        ///   <name>DAGira.UpdateGiraComiteRecepcion_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraComiteRecepcion_Item(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraComiteRecepcion_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTGira.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTGira.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Puesto;
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
        ///   <name>DAGira.UpdateGiraOrdenDia_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraOrdenDia_Item(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraOrdenDia_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTGira.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTGira.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Detalle", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Nombre;
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
        ///   <name>DAGira.UpdateGiraAcomodo_Item</name>
        ///   <create>07-Enero-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public ENTResponse UpdateGiraAcomodo_Item(ENTGira oENTGira, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspGiraAcomodo_Upd_Item", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("UsuarioId", SqlDbType.Int);
            sqlPar.Value = oENTGira.UsuarioId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("GiraConfiguracionId", SqlDbType.Int);
            sqlPar.Value = oENTGira.GiraConfiguracionId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("OrdenAnterior", SqlDbType.Int);
            sqlPar.Value = oENTGira.OrdenAnterior;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("NuevoOrden", SqlDbType.Int);
            sqlPar.Value = oENTGira.NuevoOrden;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Puesto", SqlDbType.VarChar);
            sqlPar.Value = oENTGira.Puesto;
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
