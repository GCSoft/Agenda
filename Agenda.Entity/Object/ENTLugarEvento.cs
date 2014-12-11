/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTLugarEvento
' Autor: Ruben.Cobos
' Fecha: 09-Diciembre-2014
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTLugarEvento : ENTBase
    {

        private Int32   _LugarEventoId;
        private String  _Nombre;
        private String  _Descripcion;
        private String  _FechaCreacion;
        private Int16   _Activo;
        private Int32   _Rank;


        //Constructor

        public ENTLugarEvento()
        {
            _LugarEventoId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Activo = 2;
            _Rank = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTLugarEvento.LugarEventoId</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Lugar del Evento</summary>
        public Int32 LugarEventoId
        {
            get { return _LugarEventoId; }
            set { _LugarEventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.Nombre</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Lugar del Evento</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.Descripcion</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la descripción/notas del registro</summary>
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.FechaCreacion</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha de creación del registro</summary>
        public String FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.Activo</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.Rank</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de posicionamiento del registro en un ordenado</summary>
        public Int32 Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

    }
}
