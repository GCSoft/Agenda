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

        private Int32   _EstadoId;
        private Int32   _EventoId;
        private Int32   _GiraId;
        private Int32   _GiraConfiguracionId;
        private String  _Fecha;


        //Constructor

        public ENTReporte()
        {
            _EstadoId = 0;
            _EventoId = 0;
            _GiraId = 0;
            _GiraConfiguracionId = 0;
            _Fecha = "";
        }


        
        // Propiedades

        ///<remarks>
        ///   <name>ENTReporte.EstadoId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 EstadoId
        {
            get { return _EstadoId; }
            set { _EstadoId = value; }
        }

        ///<remarks>
        ///   <name>ENTReporte.EventoId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 EventoId
        {
            get { return _EventoId; }
            set { _EventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTReporte.GiraId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 GiraId
        {
            get { return _GiraId; }
            set { _GiraId = value; }
        }

        ///<remarks>
        ///   <name>ENTReporte.GiraConfiguracionId</name>
        ///   <create>21-Octubre-2013</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 GiraConfiguracionId
        {
            get { return _GiraConfiguracionId; }
            set { _GiraConfiguracionId = value; }
        }
        
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
