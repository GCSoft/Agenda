/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: DASubMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de capa de acceso a datos de la aplicación con métodos relacionados con los Sub Menús
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
    public class DASubMenu : DABase
    {

        ///<remarks>
        ///   <name>DASubMenu.InsertSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Crea una nueva opción en el SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertSubMenu(ENTSubMenu oENTSubMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSubMenu_Ins", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ASPX", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.ASPX;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("URL", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.URL;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Rank;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Requerido", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Requerido;
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
        ///   <name>DASubMenu.SelectSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene un listado de SubMenus en base a los parámetros proporcionados</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para consultar la información</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectSubMenu(ENTSubMenu oENTSubMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSubMenu_Sel", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("RolId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.RolId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("SubMenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.SubMenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Activo;
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
        ///   <name>DASubMenu.UpdateSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSubMenu(ENTSubMenu oENTSubMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSubMenu_Upd", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SubMenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.SubMenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("MenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.MenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Descripcion", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.Descripcion;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Nombre", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.Nombre;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("ASPX", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.ASPX;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("URL", SqlDbType.VarChar);
            sqlPar.Value = oENTSubMenu.URL;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Activo;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Rank", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Rank;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Requerido", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Requerido;
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
        ///   <name>DASubMenu.UpdateSubMenu_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para crear el registro</param>
        ///<param name="sConnection">Cadena de conexión a la base de datos</param>
        ///<param name="iAlternateDBTimeout">Valor en milisegundos del Timeout en la consulta a la base de datos. 0 si se desea el Timeout por default</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSubMenu_Estatus(ENTSubMenu oENTSubMenu, String sConnection, Int32 iAlternateDBTimeout){
            SqlConnection sqlCnn = new SqlConnection(sConnection);
            SqlCommand sqlCom;
            SqlParameter sqlPar;
            SqlDataAdapter sqlDA;

            ENTResponse oENTResponse = new ENTResponse();

            // Configuración de objetos
            sqlCom = new SqlCommand("uspSubMenu_Upd_Estatus", sqlCnn);
            sqlCom.CommandType = CommandType.StoredProcedure;

            // Timeout alternativo en caso de ser solicitado
            if (iAlternateDBTimeout > 0) { sqlCom.CommandTimeout = iAlternateDBTimeout; }

            // Parametros
            sqlPar = new SqlParameter("SubMenuId", SqlDbType.Int);
            sqlPar.Value = oENTSubMenu.SubMenuId;
            sqlCom.Parameters.Add(sqlPar);

            sqlPar = new SqlParameter("Activo", SqlDbType.TinyInt);
            sqlPar.Value = oENTSubMenu.Activo;
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
