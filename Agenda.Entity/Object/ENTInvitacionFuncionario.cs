/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTInvitacionFuncionario
' Autor: Ruben.Cobos
' Fecha: 19-Diciembre-2014
'
' Proposito:
'          Clase que modela el catálogo de InvitacionFuncionarioes de la aplicación
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
    public class ENTInvitacionFuncionario : ENTBase
    {

        private Int32       _InvitacionId;
        private Int32       _UsuarioId;
        private String      _Fecha;
        private Int16       _Activo;
        private DataTable   _DataTableUsuario;


        //Constructor

        public ENTInvitacionFuncionario()
        {
            _InvitacionId = 0;
            _UsuarioId = 0;
            _Fecha = "";
            _Activo = 0;
            _DataTableUsuario = null;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTInvitacionFuncionario.InvitacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la invitación en la que esta asociada el funcionario</summary>
        public Int32 InvitacionId
        {
            get { return _InvitacionId; }
            set { _InvitacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionFuncionario.UsuarioId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Usuario (funcionario) que esta asociado a la invitación</summary>
        public Int32 UsuarioId
        {
            get { return _UsuarioId; }
            set { _UsuarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionFuncionario.Fecha</name>
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
        ///   <name>ENTInvitacionFuncionario.Activo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacionFuncionario.DataTableUsuario</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene los ID's de las opciones de los usuarios (funcionarios) que estan asociados a la invitación</summary>
        public DataTable DataTableUsuario
        {
            get { return _DataTableUsuario; }
            set { _DataTableUsuario = value; }
        }

    }
}
