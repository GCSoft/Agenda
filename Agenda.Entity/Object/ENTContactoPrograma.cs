/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTContactoPrograma
' Autor: Ruben.Cobos
' Fecha: 18-Diciembre-2014
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTContactoPrograma
    {

        private Int32   _ContactoProgramaId;
        private String  _Nombre;
        private String  _Correo;
        private String  _FechaCreacion;
        private Int16   _Activo;
        private Int32   _Rank;
        private ENTPaginacion   _Paginacion;


        //Constructor

        public ENTContactoPrograma()
        {
            _ContactoProgramaId = 0;
            _Nombre = "";
            _Correo = "";
            _FechaCreacion = "";
            _Activo = 2;
            _Rank = 0;
            _Paginacion = new ENTPaginacion();
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTContactoPrograma.ContactoProgramaId</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del Contacto de Programa</summary>
        public Int32 ContactoProgramaId
        {
            get { return _ContactoProgramaId; }
            set { _ContactoProgramaId = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.Nombre</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del Contacto de Programa</summary>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.Correo</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el puesto que tiene el Contacto de Programa</summary>
        public String Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.FechaCreacion</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha de creación del registro</summary>
        public String FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.Activo</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el control de baja lógica de registro</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.Rank</name>
        ///   <create>18-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de posicionamiento del registro en un ordenado</summary>
        public Int32 Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        ///<remarks>
        ///   <name>ENTContactoPrograma.Paginacion</name>
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
