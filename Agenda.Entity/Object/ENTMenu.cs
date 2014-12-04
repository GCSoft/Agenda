/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTMenu
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela el catálogo de Menús de la aplicación
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTMenu : ENTBase
    {

        private Int32   _MenuId;
        private String  _Nombre;
        private String  _Descripcion;
        private String  _FechaCreacion;
        private Int16   _Activo;
        private Int16   _Rank;


        //Constructor

        public ENTMenu(){
            _MenuId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Activo = 0;
            _Rank = 0;
        }



        // Propiedades

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
        ///   <name>ENTMenu.Nombre</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Menu</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTMenu.Descripcion</name>
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
        ///   <name>ENTMenu.FechaCreacion</name>
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
        ///   <name>ENTMenu.Activo</name>
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
        ///   <name>ENTMenu.Rank</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el Rank del Menu</summary>
        public Int16 Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

    }
}
