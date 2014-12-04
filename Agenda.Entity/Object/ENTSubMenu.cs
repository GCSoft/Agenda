/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTSubMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela el catálogo de Sub Menús de la aplicación
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTSubMenu : ENTBase
    {

        // Definiciones
        private Int32   _SubMenuId;
        private Int32   _MenuId;
        private Int32   _RolId;
        private String  _Nombre;
        private String  _Descripcion;
        private String  _ASPX;
        private String  _URL;
        private String  _FechaCreacion;
        private Int16   _Activo;
        private Int16   _Rank;
        private Int16   _Requerido;


        //Constructor

        public ENTSubMenu()
        {
            _SubMenuId = 0;
            _MenuId = 0;
            _RolId = 0;
            _Nombre = "";
            _Descripcion = "";
            _ASPX = "";
            _URL = "";
            _FechaCreacion = "";
            _Activo = 0;
            _Rank = 0;
            _Requerido = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTSubMenu.SubMenuId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de SubMenu</summary>
        public Int32 SubMenuId
        {
            get { return _SubMenuId; }
            set { _SubMenuId = value; }
        }

        ///<remarks>
        ///   <name>ENTMenu.MenuId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Menu</summary>
        public Int32 MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        ///<remarks>
        ///   <name>ENTMenu.RolId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Rol al que pertenece el usuario que consulta el listado de SubMenús</summary>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTSubMenu.Nombre</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del SubMenu</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTSubMenu.Descripcion</name>
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
        ///   <name>ENTSubMenu.ASPX</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre de la Pagina aspx del SubMenú</summary>
        public String ASPX
        {
            get { return _ASPX; }
            set { _ASPX = value; }
        }

        ///<remarks>
        ///   <name>ENTSubMenu.URL</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la dirección relativa completa de la Pagina aspx del SubMenú</summary>
        public String URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        ///<remarks>
        ///   <name>ENTSubMenu.FechaCreacion</name>
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
        ///   <name>ENTSubMenu.Activo</name>
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
        ///   <name>ENTSubMenu.Rank</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        ///<remarks>
        ///   <name>ENTSubMenu.Requerido</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el SubMenú es requerido o no al momento de diseñar un rol</summary>
        public Int16 Requerido
        {
            get { return _Requerido; }
            set { _Requerido = value; }
        }

    }
}
