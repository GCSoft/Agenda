/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTRol
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela el catálogo de Roles de la aplicación
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
    public class ENTRol : ENTBase
    {

        private Int32       _RolId;
        private String      _Nombre;
        private String      _Descripcion;
        private String      _FechaCreacion;
        private Int16       _Activo;
        private DataTable   _DataTableSubMenu;


        //Constructor

        public ENTRol()
        {
            _RolId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Activo = 0;
            _DataTableSubMenu = null;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTRol.RolId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Rol</summary>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTRol.Nombre</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Rol</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTRol.Descripcion</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la descripción/notas del registro</summary>
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        ///<remarks>
        ///   <name>ENTRol.FechaCreacion</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha de creación del registro</summary>
        public String FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        ///<remarks>
        ///   <name>ENTRol.Activo</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.DataTableSubMenu</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene los ID's de las opciones del SubMenú asociadas al Rol</summary>
        public DataTable DataTableSubMenu
        {
            get { return _DataTableSubMenu; }
            set { _DataTableSubMenu = value; }
        }

    }
}
