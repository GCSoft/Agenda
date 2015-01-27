/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTFiltroCalendario
' Autor: Ruben.Cobos
' Fecha: 26-Enero-2015
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity.Object
{
    public class ENTFiltroCalendario : ENTBase
    {

        private Int32   _PrioridadId;
        private Int32   _AnioActual;
        private Int16   _Dependencia;
        private Int16   _EventoCancelado;
        private Int16   _EventoExpirado;
        private Int16   _EventoNuevos;
        private Int16   _EventoProceso;
        private Int16   _EventoRepresentado;
        private Int32   _MesActual;


        //Constructor

        public ENTFiltroCalendario()
        {
            _PrioridadId = 0;
            _AnioActual = 0;
            _Dependencia = 0;
            _EventoCancelado = 0;
            _EventoExpirado = 0;
            _EventoNuevos = 0;
            _EventoProceso = 0;
            _EventoRepresentado = 0;
            _MesActual = 0;
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTFiltroCalendario.PrioridadId</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la prioridad seleccionada</summary>
        public Int32 PrioridadId
        {
            get { return _PrioridadId; }
            set { _PrioridadId = value; }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.AnioActual</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el año en una consulta</summary>
        public Int32 AnioActual
        {
            get { return _AnioActual; }
            set { _AnioActual = value; }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.Dependencia</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos de Logística (1), Dirección de Protocolo (2) o todas con un 0</summary>
        public Int16 Dependencia
        {
            get { return _Dependencia; }
            set { _Dependencia = value; }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.EventoCancelado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos cancelados</summary>
        public Int16 EventoCancelado
        {
            get { return _EventoCancelado; }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                _EventoCancelado = value;
            }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.EventoExpirado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos expirados</summary>
        public Int16 EventoExpirado
        {
            get { return _EventoExpirado; }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                _EventoExpirado = value;
            }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.EventoNuevos</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos nuevos</summary>
        public Int16 EventoNuevos
        {
            get { return _EventoNuevos; }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                _EventoNuevos = value;
            }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.EventoProceso</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos en proceso</summary>
        public Int16 EventoProceso
        {
            get { return _EventoProceso; }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                _EventoProceso = value;
            }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.EventoRepresentado</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se mostrarán los eventos representados</summary>
        public Int16 EventoRepresentado
        {
            get { return _EventoRepresentado; }
            set
            {

                if (value > 1) { throw (new Exception("Fuera de rango")); }
                if (value < 0) { throw (new Exception("Fuera de rango")); }

                _EventoRepresentado = value;
            }
        }

        ///<remarks>
        ///   <name>ENTFiltroCalendario.MesActual</name>
        ///   <create>26-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el mes en una consulta</summary>
        public Int32 MesActual
        {
            get { return _MesActual; }
            set
            {

                if (value > 12) { throw (new Exception("Fuera de rango")); }
                if (value < 1) { throw (new Exception("Fuera de rango")); }

                _MesActual = value;
            }
        }

    }
}
