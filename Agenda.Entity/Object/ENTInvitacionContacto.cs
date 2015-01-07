/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTInvitacionContacto
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
    public class ENTInvitacionContacto : ENTBase
    {

        private Int32   _InvitacionContactoId;
        private String  _Nombre;
        private String  _Puesto;
        private String  _Organizacion;
        private String  _Telefono;
        private String  _Email;
        private String  _Comentarios;
        private String  _Fecha;


        //Constructor

        public ENTInvitacionContacto()
        {
            _InvitacionContactoId = 0;
            _Nombre = "";
            _Puesto = "";
            _Organizacion = "";
            _Telefono = "";
            _Email = "";
            _Comentarios = "";
            _Fecha = "";
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTInvitacionContacto.InvitacionContactoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Contacto de la Invitacion/Evento</summary>
        public Int32 InvitacionContactoId
        {
            get { return _InvitacionContactoId; }
            set { _InvitacionContactoId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Nombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Contacto de la Invitación/Evento</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Puesto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el puesto del Contacto de la Invitación/Evento</summary>
        public String Puesto
        {
            get { return _Puesto; }
            set { _Puesto = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Organizacion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la organización para la cual trabaja el Contacto de la Invitación/Evento</summary>
        public String Organizacion
        {
            get { return _Organizacion; }
            set { _Organizacion = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Telefono</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número de teléfono que tiene el Contacto de la Invitación/Evento</summary>
        public String Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Email</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la dirección de correo electrónico que tiene el Contacto de la Invitación/Evento</summary>
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Comentarios</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna los comentarios sobre la información del contacto de la Invitación/Evento</summary>
        public String Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionContacto.Fecha</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha de creación del registro</summary>
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

       
    }
}
