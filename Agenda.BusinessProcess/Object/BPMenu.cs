/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de reglas de negocio de la aplicación con métodos relacionados con el Menú
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
    public class BPMenu : BPBase
    {

        ///<remarks>
        ///   <name>BPMenu.InsertMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un Menu</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertMenu(ENTMenu oENTMenu){
            DAMenu oDAMenu = new DAMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAMenu.InsertMenu(oENTMenu, this.ConnectionApplication, 0);

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

        ///<remarks>
        ///   <name>BPMenu.SelectMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de Menus</summary>
        ///<param name="oENTMenu">Entidad de Menu con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectMenu(ENTMenu oENTMenu){
            DAMenu oDAMenu = new DAMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAMenu.SelectMenu(oENTMenu, this.ConnectionApplication, 0);

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

        ///<remarks>
        ///   <name>BPMenu.UpdateMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un Menú</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateMenu(ENTMenu oENTMenu){
            DAMenu oDAMenu = new DAMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAMenu.UpdateMenu(oENTMenu, this.ConnectionApplication, 0);

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

        ///<remarks>
        ///   <name>BPMenu.UpdateMenu_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un Menu</summary>
        ///<param name="oENTMenu">Entidad de Menu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateMenu_Estatus(ENTMenu oENTMenu){
            DAMenu oDAMenu = new DAMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDAMenu.UpdateMenu_Estatus(oENTMenu, this.ConnectionApplication, 0);

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
