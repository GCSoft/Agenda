/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTSession
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que contiene las variables de operación del usuario que se autentica en la aplicación
'
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Referencias manuales
using System.Data;

namespace Agenda.Entity.Object
{
    public class ENTSession : ENTBase
    {

        private Int32       _UsuarioId;
        private Int32       _RolId;
        private Boolean     _TokenGenerado;
        private String      _Email;
        private String      _Nombre;
        private DataTable   _DataTableMenu;
        private DataTable   _DataTableSubMenu;
        private object      _Entity;



        //Constructor

        public ENTSession()
        {
            _UsuarioId = 0;
            _RolId = 0;
            _TokenGenerado = false;
            _Email = "";
            _Nombre = "";
            _DataTableMenu = null;
            _DataTableSubMenu = null;
            _Entity = null;
        }



        // Propiedades

        ///<remarks>
        ///   <name>ENTSession.UsuarioId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Usuario</summary>
        public Int32 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.RolId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Rol al que pertenece el usuario</summary>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.TokenGenerado</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor booleano que indica si el Usuario ya generó el Token de autenticación a la aplicación</summary>
        public Boolean TokenGenerado
        {
            get { return _TokenGenerado; }
            set { _TokenGenerado = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.Email</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el email/username del usuario</summary>
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.Nombre</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre(s) del usuario</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.DataTableMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable con las opciones del Menú válidas para el usuario</summary>
        public DataTable DataTableMenu
        {
            get { return _DataTableMenu; }
            set { _DataTableMenu = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.DataTableSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable con las opciones del SubMenú válidas para el usuario</summary>
        public DataTable DataTableSubMenu
        {
            get { return _DataTableSubMenu; }
            set { _DataTableSubMenu = value; }
        }

        ///<remarks>
        ///   <name>ENTSession.Entity</name>
        ///   <create>10-Julio-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un objeto que puede ser cualquier entidad, es usada para guardar los filtros de un formulario</summary>
        public object Entity
        {
            get { return _Entity; }
            set { _Entity = value; }
        }

    }
}
