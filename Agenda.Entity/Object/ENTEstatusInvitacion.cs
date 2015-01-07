/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTEstatusInvitacion
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
    public class ENTEstatusInvitacion : ENTBase
    {

        private Int32   _EstatusInvitacionId;
        private String  _Nombre;
        private String  _Descripcion;
        private String  _FechaCreacion;
        private Int16   _Operativo;
        private Int32   _Rank;


        //Constructor

        public ENTEstatusInvitacion()
        {
            _EstatusInvitacionId = 0;
            _Nombre = "";
            _Descripcion = "";
            _FechaCreacion = "";
            _Operativo = 2;
            _Rank = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTEstatusInvitacion.EstatusInvitacionId</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Estatus de una Invitacion</summary>
        public Int32 EstatusInvitacionId
        {
            get { return _EstatusInvitacionId; }
            set { _EstatusInvitacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusInvitacion.Nombre</name>
        ///   <create>09-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Estatus de una Invitacion</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTEstatusInvitacion.Descripcion</name>
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
        ///   <name>ENTEstatusInvitacion.FechaCreacion</name>
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
        ///   <name>ENTEstatusInvitacion.Operativo</name>
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
        ///   <name>ENTEstatusInvitacion.Rank</name>
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
