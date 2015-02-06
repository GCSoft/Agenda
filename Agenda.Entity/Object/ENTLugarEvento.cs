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
        private Int32   _EstadoId;
        private Int32   _MunicipioId;
        private Int32   _ColoniaId;
        private String  _Nombre;
        private String  _Calle;
        private String  _NumeroExterior;
        private String  _NumeroInterior;
        private String  _Descripcion;
        private String  _FechaCreacion;
        private Int16   _Activo;
        private Int32   _Rank;
        private ENTPaginacion   _Paginacion;


        //Constructor

        public ENTLugarEvento()
        {
            _LugarEventoId = 0;
            _EstadoId = 0;
            _MunicipioId = 0;
            _ColoniaId = 0;
            _Nombre = "";
            _Calle = "";
            _NumeroExterior = "";
            _NumeroInterior = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Activo = 2;
            _Rank = 0;
            _Paginacion = new ENTPaginacion();
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
        ///   <name>ENTLugarEvento.EstadoId</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Estado en donde se ubica el Lugar del Evento</summary>
        public Int32 EstadoId
        {
            get { return _EstadoId; }
            set { _EstadoId = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.MunicipioId</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Municipio en donde se ubica el Lugar del Evento</summary>
        public Int32 MunicipioId
        {
            get { return _MunicipioId; }
            set { _MunicipioId = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.ColoniaId</name>
        ///   <create>08-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la Colonia en donde se ubica el Lugar del Evento</summary>
        public Int32 ColoniaId
        {
            get { return _ColoniaId; }
            set { _ColoniaId = value; }
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
        ///   <name>ENTLugarEvento.Calle</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre de la calle donde se ubica el Lugar del Evento</summary>
        public String Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.NumeroExterior</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número exterior sobre la calle donde se ubica el Lugar del Evento</summary>
        public String NumeroExterior
        {
            get { return _NumeroExterior; }
            set { _NumeroExterior = value; }
        }

        ///<remarks>
        ///   <name>ENTLugarEvento.NumeroInterior</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número interior sobre la calle donde se ubica el Lugar del Evento</summary>
        public String NumeroInterior
        {
            get { return _NumeroInterior; }
            set { _NumeroInterior = value; }
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

        ///<remarks>
        ///   <name>ENTLugarEvento.Paginacion</name>
        ///   <create>04-Febrero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la configuración de paginación</summary>
        public ENTPaginacion Paginacion
        {
            get { return _Paginacion; }
            set { _Paginacion = value; }
        }

    }
}
