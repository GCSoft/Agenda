/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTPaginacion
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
    public class ENTPaginacion : ENTBase
    {

        private Int32   _Page;
        private Int32   _PageSize;


        //Constructor

        public ENTPaginacion()
        {
            _Page = 0;
            _PageSize = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTPaginacion.Page</name>
        ///   <create>04-Febrero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número de página</summary>
        public Int32 Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        ///<remarks>
        ///   <name>ENTPaginacion.PageSize</name>
        ///   <create>04-Febrero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tamaño de la página</summary>
        public Int32 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

    }
}
