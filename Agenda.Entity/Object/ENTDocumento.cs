/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTDocumento
' Autor: Ruben.Cobos
' Fecha: 19-Diciembre-2014
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTDocumento : ENTBase
    {

        private Int32   _DocumentoId;
        private Int32   _DocumentoExtensionId;
        private Int32   _EventoId;
        private Int32   _InvitacionId;
        private Int32   _ModuloId;
        private Int32   _RolId;
        private Int32   _TipoDocumentoId;
        private Int32   _UsuarioId;
        private String  _Nombre;
        private String  _Extension;
        private String  _Descripcion;
        private String  _Fecha;
        private String  _Ruta;


        //Constructor

        public ENTDocumento()
        {
            _DocumentoId = 0;
            _DocumentoExtensionId = 0;
            _EventoId = 0;
            _InvitacionId = 0;
            _ModuloId = 0;
            _RolId = 0;
            _TipoDocumentoId = 0;
            _UsuarioId = 0;
            _Nombre = "";
            _Extension = "";
            _Descripcion = "";
            _Fecha = "";
            _Ruta = "";
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTDocumento.DocumentoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Documento</summary>
        public Int32 DocumentoId
        {
            get { return _DocumentoId; }
            set { _DocumentoId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.DocumentoExtensionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la extensión del Documento</summary>
        public Int32 DocumentoExtensionId
        {
            get { return _DocumentoExtensionId; }
            set { _DocumentoExtensionId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.EventoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del evento asociado al documento</summary>
        public Int32 EventoId
        {
            get { return _EventoId; }
            set { _EventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.InvitacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la invitación asociada al documento</summary>
        public Int32 InvitacionId
        {
            get { return _InvitacionId; }
            set { _InvitacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.ModuloId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del módulo desde donde se cargó el documento</summary>
        public Int32 ModuloId
        {
            get { return _ModuloId; }
            set { _ModuloId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.RolId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.TipoDocumentoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del tipo de documento que se carga</summary>
        public Int32 TipoDocumentoId
        {
            get { return _TipoDocumentoId; }
            set { _TipoDocumentoId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.UsuarioId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del usuario que carga el documento</summary>
        public Int32 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.Nombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Documento</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.Extension</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la Extension del Documento</summary>
        public String Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.Descripcion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la descripción/notas del registro</summary>
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.Fecha</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha de creación del registro</summary>
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        ///<remarks>
        ///   <name>ENTDocumento.Ruta</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la ruta física en donde se depositó el documento</summary>
        public String Ruta
        {
            get { return _Ruta; }
            set { _Ruta = value; }
        }

    }
}
