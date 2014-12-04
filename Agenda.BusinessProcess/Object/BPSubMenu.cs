/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: BPSubMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela la capa de reglas de negocio de la aplicación con métodos relacionados con el SubMenú
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
    public class BPSubMenu : BPBase
    {

        ///<remarks>
        ///   <name>BPSubMenu.InsertSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse InsertSubMenu(ENTSubMenu oENTSubMenu){
            DASubMenu oDASubMenu = new DASubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDASubMenu.InsertSubMenu(oENTSubMenu, this.ConnectionApplication, 0);

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
        ///   <name>BPSubMenu.SelectSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Consulta el catálogo de SubMenus</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los filtros necesarios para la consulta</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse SelectSubMenu(ENTSubMenu oENTSubMenu){
            DASubMenu oDASubMenu = new DASubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDASubMenu.SelectSubMenu(oENTSubMenu, this.ConnectionApplication, 0);

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
        ///   <name>BPSubMenu.UpdateSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Actualiza la información de un SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSubMenu(ENTSubMenu oENTSubMenu){
            DASubMenu oDASubMenu = new DASubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDASubMenu.UpdateSubMenu(oENTSubMenu, this.ConnectionApplication, 0);

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
        ///   <name>BPSubMenu.UpdateSubMenu_Estatus</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Activa/inactiva un SubMenu</summary>
        ///<param name="oENTSubMenu">Entidad de SubMenu con los parámetros necesarios para actualizar su información</param>
        ///<returns>Una entidad de respuesta</returns>
        public ENTResponse UpdateSubMenu_Estatus(ENTSubMenu oENTSubMenu){
            DASubMenu oDASubMenu = new DASubMenu();
            ENTResponse oENTResponse = new ENTResponse();

            try
            {

                // Transacción en base de datos
                oENTResponse = oDASubMenu.UpdateSubMenu_Estatus(oENTSubMenu, this.ConnectionApplication, 0);

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
