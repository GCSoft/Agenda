/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DAMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de capa de acceso a datos de la aplicación con métodos relacionados con los Menús
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
    public class DAMenu : DABase
    {

        ///<remarks>
        ///   <name>DAMenu.InsertMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea una nueva opción en el Menu</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertMenu(ENTMenu oENTMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspMenu_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTMenu.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Rank;
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
        ///   <name>DAMenu.SelectMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de Menus en base a los parámetros proporcionados</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectMenu(ENTMenu oENTMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspMenu_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Activo;
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
        ///   <name>DAMenu.UpdateMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un Menú</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateMenu(ENTMenu oENTMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspMenu_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTMenu.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Rank;
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
        ///   <name>DAMenu.UpdateMenu_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un Menu</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateMenu_Estatus(ENTMenu oENTMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspMenu_Upd_Estatus", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTMenu.Activo;
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
