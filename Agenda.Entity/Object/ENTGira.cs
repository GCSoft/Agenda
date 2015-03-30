/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTGira
' Autor: Ruben.Cobos
' Fecha: 18-Diciembre-2014
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
    public class ENTGira : ENTBase
    {

        private Int32   _GiraId;
        private Int32   _EstatusGiraId;
        private Int32   _GiraContactoId;
        private Int32   _UsuarioId;
        private Int16   _Activo;
        private String  _GiraDetalle;
        private String  _GiraNombre;
        private String  _FechaGira;
        private String  _HoraGiraFin;
        private String  _HoraGiraInicio;
        private String  _MotivoRechazo;
        private ENTInvitacionContacto _Contacto;


        //Constructor

        public ENTGira()
        {
            _GiraId = 0;
            _EstatusGiraId = 0;
            _GiraContactoId = 0;
            _UsuarioId = 0;
            _Activo = 0;
            _GiraDetalle = "";
            _GiraNombre = "";
            _FechaGira = "";
            _HoraGiraFin = "";
            _HoraGiraInicio = "";
            _MotivoRechazo = "";
            _Contacto = new ENTInvitacionContacto();
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTGira.GiraId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Gira</summary>
        public Int32 GiraId
        {
            get { return _GiraId; }
            set { _GiraId = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.EstatusGiraId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del estatus del Gira</summary>
        public Int32 EstatusGiraId
        {
            get { return _EstatusGiraId; }
            set { _EstatusGiraId = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.GiraContactoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del contacto asociado a la Gira</summary>
        public Int32 GiraContactoId
        {
            get { return _GiraContactoId; }
            set { _GiraContactoId = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.UsuarioId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del usuario que realiza la transacción</summary>
        public Int32 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.Activo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el registro está activo o no</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.GiraDetalle</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el detalle del Gira</summary>
        public String GiraDetalle
        {
            get { return _GiraDetalle; }
            set { _GiraDetalle = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.GiraNombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Gira</summary>
        public String GiraNombre
        {
            get { return _GiraNombre; }
            set { _GiraNombre = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.FechaGira</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha en la que se realizará el Gira</summary>
        public String FechaGira
        {
            get { return _FechaGira; }
            set { _FechaGira = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.HoraGiraFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha final en la consulta de información</summary>
        public String HoraGiraFin
        {
            get { return _HoraGiraFin; }
            set { _HoraGiraFin = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.HoraGiraInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha inicial en la consulta de información</summary>
        public String HoraGiraInicio
        {
            get { return _HoraGiraInicio; }
            set { _HoraGiraInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.MotivoRechazo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el motivo de rechazo de una gira</summary>
        public String MotivoRechazo
        {
            get { return _MotivoRechazo; }
            set { _MotivoRechazo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Contacto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la información del contacto del Evento</summary>
        public ENTInvitacionContacto Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

    }
}
