/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTUsuario
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
'
' Proposito:
'          Clase que modela el catálogo de Usuarios de la aplicación
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTUsuario : ENTBase
    {

        private Int32   _UsuarioId;
        private Int32   _RolId;
        private Int32   _TituloId;
        private String  _FechaCreacion;
        private String  _Nombre;
        private String  _ApellidoPaterno;
        private String  _ApellidoMaterno;
        private String  _Descripcion;
        private String  _Email;
        private String  _Password;
        private String  _PasswordAnterior;
        private String  _Puesto;
        private Int16   _Activo;



        //Constructor

        public ENTUsuario()
        {
            _UsuarioId = 0;
            _RolId = 0;
            _TituloId = 0;
            _FechaCreacion = "";
            _Nombre = "";
            _ApellidoPaterno = "";
            _ApellidoMaterno = "";
            _Descripcion = "";
            _Email = "";
            _Password = "";
            _PasswordAnterior = "";
            _Puesto = "";
            _Activo = 0;
        }



        // Propiedades

        ///<remarks>
        ///   <name>ENTUsuario.UsuarioId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Usuario</summary>
        public Int32 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.RolId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de Rol de Usuario</summary>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.TituloId</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del título que tiene el Usuario</summary>
        public Int32 TituloId
        {
            get { return _TituloId; }
            set { _TituloId = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.FechaCreacion</name>
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
        ///   <name>ENTUsuario.Nombre</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre(s) del usuario</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.ApellidoPaterno</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el apellido del padre del usuario</summary>
        public String ApellidoPaterno
        {
            get { return _ApellidoPaterno; }
            set { _ApellidoPaterno = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.ApellidoMaterno</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el apellido de la madre del usuario</summary>
        public String ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.Descripcion</name>
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
        ///   <name>ENTUsuario.Email</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el email/username del usuario</summary>
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.Password</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el password encriptado</summary>
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.PasswordAnterior</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el password anterior encriptado del usuario. Utilizado en cambio de contraseña.</summary>
        public String PasswordAnterior
        {
            get { return _PasswordAnterior; }
            set { _PasswordAnterior = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.Password</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el puesto del usuario</summary>
        public String Puesto
        {
            get { return _Puesto; }
            set { _Puesto = value; }
        }

        ///<remarks>
        ///   <name>ENTUsuario.Activo</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

    }
}
