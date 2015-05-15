/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTReporte
' Autor: Ruben.Cobos
' Fecha: 21-Octubre-2013
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
    public class ENTReporte : ENTBase
    {
        
        private String _Fecha;


        //Constructor

        public ENTReporte()
        {
            _Fecha = "";
        }


        
        // Propiedades
        
        ///<remarks>
        ///   <name>ENTReporte.Fecha</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la Fecha del Reporte</summary>
        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }


    }
}
