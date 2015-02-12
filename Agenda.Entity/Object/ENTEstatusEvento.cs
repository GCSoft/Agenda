/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTEstatusEvento
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
    public class ENTEstatusEvento : ENTBase
    {

        private Int32   _EstatusEventoId;
        private Int32   _RolId;
        private String  _Nombre;
        private String  _Descripcion;
        private String  _FechaCreacion;
        private Int16   _Operativo;
        private Int32   _Rank;


        //Constructor

        public ENTEstatusEvento()
        {
            _EstatusEventoId = 0;
            _RolId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Operativo = 2;
            _Rank = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTEstatusEvento.EstatusEventoId</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Estatus de una Evento</summary>
        public Int32 EstatusEventoId
        {
            get { return _EstatusEventoId; }
            set { _EstatusEventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusEvento.RolId</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Rol del usuario que realiza una consulta</summary>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusEvento.Nombre</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Estatus de una Evento</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusEvento.Descripcion</name>
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
        ///   <name>ENTEstatusEvento.FechaCreacion</name>
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
        ///   <name>ENTEstatusEvento.Operativo</name>
        ///   <create>23-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el registro solicitado es necesario para el flujo operativo</summary>
        public Int16 Operativo
        {
            get { return _Operativo; }
            set { _Operativo = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusEvento.Rank</name>
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
