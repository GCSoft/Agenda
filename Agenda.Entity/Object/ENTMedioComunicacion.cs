/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTMedioComunicacion
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
    public class ENTMedioComunicacion : ENTBase
    {

        private Int32 _MedioComunicacionId;
        private String _Nombre;
        private String _Descripcion;
        private String _FechaCreacion;
        private Int16 _Activo;
        private Int32 _Rank;


        //Constructor

        public ENTMedioComunicacion()
        {
            _MedioComunicacionId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Activo = 2;
            _Rank = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTMedioComunicacion.MedioComunicacionId</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Medio de Comunicacion</summary>
        public Int32 MedioComunicacionId
        {
            get { return _MedioComunicacionId; }
            set { _MedioComunicacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTMedioComunicacion.Nombre</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Medio de Comunicacion</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTMedioComunicacion.Descripcion</name>
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
        ///   <name>ENTMedioComunicacion.FechaCreacion</name>
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
        ///   <name>ENTMedioComunicacion.Activo</name>
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
        ///   <name>ENTMedioComunicacion.Rank</name>
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
