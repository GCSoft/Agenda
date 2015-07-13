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
        private Int32   _GiraConfiguracionId;
        private Int32   _GiraContactoId;
        private Int32   _TipoGiraConfiguracionId;
        private Int32   _UsuarioId;
        private Int16   _Activo;
        private String  _GiraDetalle;
        private String  _GiraNombre;
        private String  _FechaGiraInicio;
        private String  _FechaGiraFin;
        private String  _HoraGiraFin;
        private String  _HoraGiraInicio;
        private String  _MotivoRechazo;
        private String  _ConfiguracionGrupo;
        private String  _ConfiguracionFechaInicio;
        private String  _ConfiguracionFechaFin;
        private String  _ConfiguracionHoraInicio;
        private String  _ConfiguracionHoraFin;
        private String  _ConfiguracionDetalle;
        private Int16   _ConfiguracionActivo;
        private String  _HelipuertoLugar;
        private String  _HelipuertoDomicilio;
        private String  _HelipuertoCoordenadas;
        private DataTable   _DataTableComiteRecepcion;
        private DataTable   _DataTableComiteHelipuerto;
        private DataTable   _DataTableMedioTraslado;
        private DataTable   _DataTableOrdenDia;
        private DataTable   _DataTableAcomodo;
        private ENTInvitacionContacto   _Contacto;
        private ENTEvento               _Evento;

        private Int16   _NotaInicioDocumento;
        private Int16   _NotaFinDocumento;
        private String  _NotaDocumento;


        //Constructor

        public ENTGira()
        {
            _GiraId = 0;
            _EstatusGiraId = 0;
            _GiraConfiguracionId = 0;
            _GiraContactoId = 0;
            _TipoGiraConfiguracionId = 0;
            _UsuarioId = 0;
            _Activo = 0;
            _GiraDetalle = "";
            _GiraNombre = "";
            _FechaGiraInicio = "";
            _FechaGiraFin = "";
            _HoraGiraFin = "";
            _HoraGiraInicio = "";
            _MotivoRechazo = "";
            _ConfiguracionGrupo = "";
            _ConfiguracionFechaInicio = "";
            _ConfiguracionFechaFin = "";
            _ConfiguracionHoraInicio = "";
            _ConfiguracionHoraFin = "";
            _ConfiguracionDetalle = "";
            _ConfiguracionActivo = 0;
            _HelipuertoLugar = "";
            _HelipuertoDomicilio = "";
            _HelipuertoCoordenadas = "";
            _DataTableComiteRecepcion = null;
            _DataTableComiteHelipuerto = null;
            _DataTableMedioTraslado = null;
            _DataTableOrdenDia = null;
            _DataTableAcomodo = null;
            _Contacto = new ENTInvitacionContacto();
            _Evento = new ENTEvento();

            _NotaInicioDocumento = 0;
            _NotaFinDocumento = 0;
            _NotaDocumento = "";
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
        ///   <name>ENTGira.GiraConfiguracionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la Configuracion asociada a la Gira</summary>
        public Int32 GiraConfiguracionId
        {
            get { return _GiraConfiguracionId; }
            set { _GiraConfiguracionId = value; }
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
        ///   <name>ENTGira.TipoGiraConfiguracionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del tipo de configuración asociado a la gira</summary>
        public Int32 TipoGiraConfiguracionId
        {
            get { return _TipoGiraConfiguracionId; }
            set { _TipoGiraConfiguracionId = value; }
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
        ///   <name>ENTGira.FechaGiraInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String FechaGiraInicio
        {
            get { return _FechaGiraInicio; }
            set { _FechaGiraInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.FechaGiraFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String FechaGiraFin
        {
            get { return _FechaGiraFin; }
            set { _FechaGiraFin = value; }
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
        ///   <name>ENTGira.ConfiguracionGrupo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el grupo al que pertenece una partida de configuración de una Gira</summary>
        public String ConfiguracionGrupo
        {
            get { return _ConfiguracionGrupo; }
            set { _ConfiguracionGrupo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionFechaInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String ConfiguracionFechaInicio
        {
            get { return _ConfiguracionFechaInicio; }
            set { _ConfiguracionFechaInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionFechaFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String ConfiguracionFechaFin
        {
            get { return _ConfiguracionFechaFin; }
            set { _ConfiguracionFechaFin = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionHoraInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la hora inicial en una configuracion de una Gira</summary>
        public String ConfiguracionHoraInicio
        {
            get { return _ConfiguracionHoraInicio; }
            set { _ConfiguracionHoraInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionHoraFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la hora final en una configuracion de una Gira</summary>
        public String ConfiguracionHoraFin
        {
            get { return _ConfiguracionHoraFin; }
            set { _ConfiguracionHoraFin = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionDetalle</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el detalle en una configuracion de una Gira</summary>
        public String ConfiguracionDetalle
        {
            get { return _ConfiguracionDetalle; }
            set { _ConfiguracionDetalle = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.ConfiguracionActivo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el registro de configuración está activo o no</summary>
        public Int16 ConfiguracionActivo
        {
            get { return _ConfiguracionActivo; }
            set { _ConfiguracionActivo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.HelipuertoLugar</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el lugar en donde se ubica el helipuerto</summary>
        public String HelipuertoLugar
        {
            get { return _HelipuertoLugar; }
            set { _HelipuertoLugar = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.HelipuertoDomicilio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el domicilio del helipuerto</summary>
        public String HelipuertoDomicilio
        {
            get { return _HelipuertoDomicilio; }
            set { _HelipuertoDomicilio = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.HelipuertoCoordenadas</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las coordenadas donde se ubica el helipuerto</summary>
        public String HelipuertoCoordenadas
        {
            get { return _HelipuertoCoordenadas; }
            set { _HelipuertoCoordenadas = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.DataTableComiteRecepcion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene el detalle del comité de recepción</summary>
        public DataTable DataTableComiteRecepcion
        {
            get { return _DataTableComiteRecepcion; }
            set { _DataTableComiteRecepcion = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.DataTableComiteHelipuerto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene el detalle del comité de recepción del helipuerto</summary>
        public DataTable DataTableComiteHelipuerto
        {
            get { return _DataTableComiteHelipuerto; }
            set { _DataTableComiteHelipuerto = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.DataTableMedioTraslado</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene los ID's de los medio sd etraslado seleccionadosr</summary>
        public DataTable DataTableMedioTraslado
        {
            get { return _DataTableMedioTraslado; }
            set { _DataTableMedioTraslado = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.DataTableOrdenDia</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene el detalle de la orden del día</summary>
        public DataTable DataTableOrdenDia
        {
            get { return _DataTableOrdenDia; }
            set { _DataTableOrdenDia = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.DataTableAcomodo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene el acomodo en un evento</summary>
        public DataTable DataTableAcomodo
        {
            get { return _DataTableAcomodo; }
            set { _DataTableAcomodo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.Contacto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la información del contacto del Evento</summary>
        public ENTInvitacionContacto Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.Evento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la información del Evento</summary>
        public ENTEvento Evento
        {
            get { return _Evento; }
            set { _Evento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaInicioDocumento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaInicioDocumento
        {
            get { return _NotaInicioDocumento; }
            set { _NotaInicioDocumento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaFinDocumento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaFinDocumento
        {
            get { return _NotaFinDocumento; }
            set { _NotaFinDocumento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaDocumento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String NotaDocumento
        {
            get { return _NotaDocumento; }
            set { _NotaDocumento = value; }
        } 

    }
}
