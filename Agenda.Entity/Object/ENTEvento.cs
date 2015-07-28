/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTEvento
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
    public class ENTEvento : ENTBase
    {

        private Int32   _EventoId;
        private Int32   _CategoriaId;
        private Int32   _ColoniaId;
        private Int32   _ConductoId;
        private Int32   _EstatusEventoId;
        private Int32   _EventoComentarioId;
        private Int32   _EventoContactoId;
        private Int32   _LugarEventoId;
        private Int32   _MedioComunicacionId;
        private Int32   _MedioTrasladoId;
        private Int32   _ModuloId;
        private Int32   _PrioridadId;
        private Int32   _RespuestaEvaluacionId;
        private Int32   _SecretarioId_Ramo;
        private Int32   _SecretarioId_Representante;
        private Int32   _SecretarioId_Responsable;
        private Int32   _TipoAcomodoId;
        private Int32   _TipoVestimentaId;
        private Int32   _UsuarioId;
        private Int32   _UsuarioId_Temp;
        private Int32   _RolId;
        private String  _AccionRealizar;
        private String  _AcomodoObservaciones;
        private String  _EventoDetalle;
        private String  _EventoNombre;
        private String  _EventoObservaciones;
        private String  _Calle;
        private String  _CaracteristicasInvitados;
        private String  _Comentario;
        private String  _LugarArribo;
        private String  _Menu;
        private String  _MotivoRechazo;
        private String  _NumeroExterior;
        private String  _NumeroInterior;
        private String  _PronosticoClima;
        private String  _ProtocoloInvitacionA;
        private String  _ProtocoloResponsableEvento;
        private String  _ProtocoloBandera;
        private String  _ProtocoloLeyenda;
        private String  _ProtocoloResponsable;
        private String  _ProtocoloSonido;
        private String  _ProtocoloResponsableSonido;
        private String  _ProtocoloDesayuno;
        private String  _ProtocoloSillas;
        private String  _ProtocoloMesas;
        private String  _ProtocoloPresentacion;
        private String  _RepresentanteNombre;
        private String  _RepresentanteCargo;
        private String  _RepresentanteTelefonoOficina;
        private String  _RepresentanteTelefonoMovil;
        private String  _RepresentanteTelefonoParticular;
        private String  _RepresentanteTelefonoOtro;
        private String  _TemperaturaMaxima;
        private String  _TemperaturaMinima;
        private String  _TipoMontaje;
        private String  _TipoVestimentaOtro;
        private String  _FechaEvento;
        private String  _FechaFin;
        private String  _FechaInicio;
        private String  _HelipuertoLugar;
        private String  _HelipuertoDomicilio;
        private String  _HelipuertoCoordenadas;
        private String  _HoraEventoInicio;
        private String  _HoraEventoFin;
        private Int16   _Activo;
        private Int32   _Aforo;
        private Int32   _Anio;
        private Int16   _ComiteHelipuerto;
        private Int16   _Dependencia;
        private Int32   _Mes;
        private Int16   _Esposa;
        private Int16   _EsposaSi;
        private Int16   _EsposaNo;
        private Int16   _EsposaConfirma;
        private Int16   _Logistica;
        private Int16   _Nivel;
        private Int16   _Notificacion;
        private Int16   _PropuestaAcomodo;
        private Int16   _Protocolo;
        private DataTable   _DataTableAcomodo;
        private DataTable   _DataTableComiteHelipuerto;
        private DataTable   _DataTableComiteRecepcion;
        private DataTable   _DataTableEstatusEvento;
        private DataTable   _DataTableMedioTraslado;
        private DataTable   _DataTableOrdenDia;
        private DataTable   _DataTableResponsable;
        private DataTable   _DataTableResponsableLogistica;
        private ENTInvitacionContacto _Contacto;

        private Int16   _NotaInicioEvento;
        private Int16   _NotaFinEvento;
        private String  _NotaEvento;

        private Int16   _NotaInicioComite;
        private Int16   _NotaFinComite;
        private String  _NotaComite;

        private Int16   _NotaInicioOrden;
        private Int16   _NotaFinOrden;
        private String  _NotaOrden;

        private Int16   _NotaInicioAcomodo;
        private Int16   _NotaFinAcomodo;
        private String  _NotaAcomodo;

        private Int16   _NotaInicioDocumento;
        private Int16   _NotaFinDocumento;
        private String  _NotaDocumento;

        private String  _PalabraClave;

        private Int16   _ListadoAdicional;
        private String  _ListadoAdicionalTitulo;
        private DataTable   _DataTableListadoAdicional;

        private Int32   _OrdenAnterior;
        private Int32   _NuevoOrden;
        private String  _Nombre;
        private String  _Puesto;
        private Int16   _Separador;


        //Constructor

        public ENTEvento()
        {
            _EventoId = 0;
            _CategoriaId = 0;
            _ColoniaId = 0;
            _ConductoId = 0;
            _EstatusEventoId = 0;
            _EventoComentarioId = 0;
            _EventoContactoId = 0;
            _LugarEventoId = 0;
            _MedioComunicacionId = 0;
            _MedioTrasladoId = 0;
            _ModuloId = 0;
            _PrioridadId = 0;
            _RespuestaEvaluacionId = 0;
            _SecretarioId_Ramo = 0;
            _SecretarioId_Representante = 0;
            _SecretarioId_Responsable = 0;
            _TipoAcomodoId = 0;
            _TipoVestimentaId = 0;
            _UsuarioId = 0;
            _UsuarioId_Temp = 0;
            _RolId = 0;
            _AccionRealizar = "";
            _AcomodoObservaciones = "";
            _EventoDetalle = "";
            _EventoNombre = "";
            _EventoObservaciones = "";
            _Calle = "";
            _CaracteristicasInvitados = "";
            _Comentario = "";
            _LugarArribo = "";
            _Menu = "";
            _MotivoRechazo = "";
            _NumeroExterior = "";
            _NumeroInterior = "";
            _PronosticoClima = "";
            _ProtocoloInvitacionA = "";
            _ProtocoloResponsableEvento = "";
            _ProtocoloBandera = "";
            _ProtocoloLeyenda = "";
            _ProtocoloResponsable = "";
            _ProtocoloSonido = "";
            _ProtocoloResponsableSonido = "";
            _ProtocoloDesayuno = "";
            _ProtocoloSillas = "";
            _ProtocoloMesas = "";
            _ProtocoloPresentacion = "";
            _RepresentanteNombre = "";
            _RepresentanteCargo = "";
            _RepresentanteTelefonoOficina = "";
            _RepresentanteTelefonoMovil = "";
            _RepresentanteTelefonoParticular = "";
            _RepresentanteTelefonoOtro = "";
            _TemperaturaMaxima = "";
            _TemperaturaMinima = "";
            _TipoMontaje = "";
            _TipoVestimentaOtro = "";
            _FechaEvento = "";
            _FechaFin = "";
            _FechaInicio = "";
            _HelipuertoLugar = "";
            _HelipuertoDomicilio = "";
            _HelipuertoCoordenadas = "";
            _HoraEventoInicio = "";
            _HoraEventoFin = "";
            _Activo = 2;
            _Aforo = 0;
            _Anio = 0;
            _ComiteHelipuerto = 0;
            _Dependencia = 0;
            _Mes = 0;
            _Esposa = 0;
            _EsposaSi = 0;
            _EsposaNo = 0;
            _EsposaConfirma = 0;
            _Logistica = 0;
            _Nivel = 0;
            _Notificacion = 0;
            _PropuestaAcomodo = 0;
            _Protocolo = 0;
            _DataTableAcomodo = null;
            _DataTableComiteHelipuerto = null;
            _DataTableComiteRecepcion = null;
            _DataTableEstatusEvento = null;
            _DataTableMedioTraslado = null;
            _DataTableOrdenDia = null;
            _DataTableResponsable = null;
            _DataTableResponsableLogistica = null;
            _Contacto = new ENTInvitacionContacto();

            _NotaInicioEvento = 0;
            _NotaFinEvento = 0;
            _NotaEvento = "";

            _NotaInicioComite = 0;
            _NotaFinComite = 0;
            _NotaComite = "";

            _NotaInicioOrden = 0;
            _NotaFinOrden = 0;
            _NotaOrden = "";

            _NotaInicioAcomodo = 0;
            _NotaFinAcomodo = 0;
            _NotaAcomodo = "";

            _PalabraClave = "";

            _ListadoAdicional = 0;
            _ListadoAdicionalTitulo = "";
            _DataTableListadoAdicional = null;

            _NotaInicioDocumento = 0;
            _NotaFinDocumento = 0;
            _NotaDocumento = "";

            _OrdenAnterior = 0;
            _NuevoOrden = 0;
            _Nombre = "";
            _Puesto = "";
            _Separador = 0;

        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTEvento.EventoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del evento</summary>
        public Int32 EventoId
        {
            get { return _EventoId; }
            set { _EventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.CategoriaId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la categoría asociada a el evento</summary>
        public Int32 CategoriaId
        {
            get { return _CategoriaId; }
            set { _CategoriaId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ColoniaId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la colonia en donde se realizará el evento</summary>
        public Int32 ColoniaId
        {
            get { return _ColoniaId; }
            set { _ColoniaId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ConductoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del conducto por el cual llego el evento</summary>
        public Int32 ConductoId
        {
            get { return _ConductoId; }
            set { _ConductoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EstatusEventoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del estatus del evento</summary>
        public Int32 EstatusEventoId
        {
            get { return _EstatusEventoId; }
            set { _EstatusEventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EventoComentarioId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de un comentario hecho sobre el evento</summary>
        public Int32 EventoComentarioId
        {
            get { return _EventoComentarioId; }
            set { _EventoComentarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EventoContactoId</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de un contacto del evento</summary>
        public Int32 EventoContactoId
        {
            get { return _EventoContactoId; }
            set { _EventoContactoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.LugarEventoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del lugar del evento</summary>
        public Int32 LugarEventoId
        {
            get { return _LugarEventoId; }
            set { _LugarEventoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.MedioComunicacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del medio de comunicación del evento</summary>
        public Int32 MedioComunicacionId
        {
            get { return _MedioComunicacionId; }
            set { _MedioComunicacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.MedioTrasladoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del medio de traslado al evento</summary>
        public Int32 MedioTrasladoId
        {
            get { return _MedioTrasladoId; }
            set { _MedioTrasladoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ModuloId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del módulo desde donde se realiza la transacción</summary>
        public Int32 ModuloId
        {
            get { return _ModuloId; }
            set { _ModuloId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.PrioridadId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la prioridad del evento</summary>
        public Int32 PrioridadId
        {
            get { return _PrioridadId; }
            set { _PrioridadId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RespuestaEvaluacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la respuesta de evaluación del evento</summary>
        public Int32 RespuestaEvaluacionId
        {
            get { return _RespuestaEvaluacionId; }
            set { _RespuestaEvaluacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.SecretarioId_Ramo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del ramo de la secretaría (secretario) al que pertenece el evento</summary>
        public Int32 SecretarioId_Ramo
        {
            get { return _SecretarioId_Ramo; }
            set { _SecretarioId_Ramo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.SecretarioId_Representante</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del representante (secretario) que acudirá al evento (vacío si asiste el gobernador)</summary>
        public Int32 SecretarioId_Representante
        {
            get { return _SecretarioId_Representante; }
            set { _SecretarioId_Representante = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.SecretarioId_Responsable</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del ramo de la secretaría (secretario) responsable del evento</summary>
        public Int32 SecretarioId_Responsable
        {
            get { return _SecretarioId_Responsable; }
            set { _SecretarioId_Responsable = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TipoAcomodoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del tipo de acomodo en el evento</summary>
        public Int32 TipoAcomodoId
        {
            get { return _TipoAcomodoId; }
            set { _TipoAcomodoId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TipoVestimentaId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del tipo de vestimenta al que hay que acudir al evento</summary>
        public Int32 TipoVestimentaId
        {
            get { return _TipoVestimentaId; }
            set { _TipoVestimentaId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.UsuarioId</name>
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
        ///   <name>ENTEvento.UsuarioId_Temp</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del usuario para una transacción alterna</summary>
        public Int32 UsuarioId_Temp
        {
            get { return _UsuarioId_Temp; }
            set { _UsuarioId_Temp = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RolId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 RolId
        {
            get { return _RolId; }
            set { _RolId = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.AccionRealizar</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la acción a realizar en el evento</summary>
        public String AccionRealizar
        {
            get { return _AccionRealizar; }
            set { _AccionRealizar = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.AcomodoObservaciones</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las observaciones en el acomodo</summary>
        public String AcomodoObservaciones
        {
            get { return _AcomodoObservaciones; }
            set { _AcomodoObservaciones = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EventoDetalle</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el detalle del evento</summary>
        public String EventoDetalle
        {
            get { return _EventoDetalle; }
            set { _EventoDetalle = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EventoNombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del evento</summary>
        public String EventoNombre
        {
            get { return _EventoNombre; }
            set { _EventoNombre = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EventoObservaciones</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las observaciones del evento</summary>
        public String EventoObservaciones
        {
            get { return _EventoObservaciones; }
            set { _EventoObservaciones = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Calle</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la calle en donde se realizará el evento</summary>
        public String Calle
        {
            get { return _Calle; }
            set { _Calle = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.CaracteristicasInvitados</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las caracteristicas de los invitados al evento</summary>
        public String CaracteristicasInvitados
        {
            get { return _CaracteristicasInvitados; }
            set { _CaracteristicasInvitados = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Comentario</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un comentario sobre una invitación realizado por un funcionario en particular</summary>
        public String Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.LugarArribo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el lugar de arribo al evento</summary>
        public String LugarArribo
        {
            get { return _LugarArribo; }
            set { _LugarArribo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Menu</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el menú a servir en el evento</summary>
        public String Menu
        {
            get { return _Menu; }
            set { _Menu = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.MotivoRechazo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el motivo de rechazo de una invitación en particular</summary>
        public String MotivoRechazo
        {
            get { return _MotivoRechazo; }
            set { _MotivoRechazo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.NumeroExterior</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número exterior del predio en donde se realizará el evento</summary>
        public String NumeroExterior
        {
            get { return _NumeroExterior; }
            set { _NumeroExterior = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.NumeroInterior</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el número interior del predio en donde se realizará el evento</summary>
        public String NumeroInterior
        {
            get { return _NumeroInterior; }
            set { _NumeroInterior = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.PronosticoClima</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna pronóstico del clima que se espera el día del evento</summary>
        public String PronosticoClima
        {
            get { return _PronosticoClima; }
            set { _PronosticoClima = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloInvitacionA</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloInvitacionA" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloInvitacionA
        {
            get { return _ProtocoloInvitacionA; }
            set { _ProtocoloInvitacionA = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloResponsableEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el Responsable del Evento en el programa de dirección de protocolo</summary>
        public String ProtocoloResponsableEvento
        {
            get { return _ProtocoloResponsableEvento; }
            set { _ProtocoloResponsableEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloBandera</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloBandera" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloBandera
        {
            get { return _ProtocoloBandera; }
            set { _ProtocoloBandera = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloLeyenda</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloLeyenda" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloLeyenda
        {
            get { return _ProtocoloLeyenda; }
            set { _ProtocoloLeyenda = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloResponsable</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloResponsable" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloResponsable
        {
            get { return _ProtocoloResponsable; }
            set { _ProtocoloResponsable = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloSonido</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloSonido" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloSonido
        {
            get { return _ProtocoloSonido; }
            set { _ProtocoloSonido = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloResponsableSonido</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloResponsableSonido" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloResponsableSonido
        {
            get { return _ProtocoloResponsableSonido; }
            set { _ProtocoloResponsableSonido = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloDesayuno</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloDesayuno" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloDesayuno
        {
            get { return _ProtocoloDesayuno; }
            set { _ProtocoloDesayuno = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloSillas</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloSillas" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloSillas
        {
            get { return _ProtocoloSillas; }
            set { _ProtocoloSillas = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloMesas</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloMesas" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloMesas
        {
            get { return _ProtocoloMesas; }
            set { _ProtocoloMesas = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ProtocoloPresentacion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el valor de "ProtocoloPresentacion" usado en el programa de dirección de protocolo</summary>
        public String ProtocoloPresentacion
        {
            get { return _ProtocoloPresentacion; }
            set { _ProtocoloPresentacion = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteNombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nombre del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteNombre
        {
            get { return _RepresentanteNombre; }
            set { _RepresentanteNombre = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteCargo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el cargo del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteCargo
        {
            get { return _RepresentanteCargo; }
            set { _RepresentanteCargo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteTelefonoOficina</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el teléfono de oficina del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteTelefonoOficina
        {
            get { return _RepresentanteTelefonoOficina; }
            set { _RepresentanteTelefonoOficina = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteTelefonoMovil</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el teléfono movil del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteTelefonoMovil
        {
            get { return _RepresentanteTelefonoMovil; }
            set { _RepresentanteTelefonoMovil = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteTelefonoParticular</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el teléfono particular del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteTelefonoParticular
        {
            get { return _RepresentanteTelefonoParticular; }
            set { _RepresentanteTelefonoParticular = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.RepresentanteTelefonoOtro</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna otro teléfono del representante del gobernador propuesto en una evaluación de una invitación por parte d eun funcionario</summary>
        public String RepresentanteTelefonoOtro
        {
            get { return _RepresentanteTelefonoOtro; }
            set { _RepresentanteTelefonoOtro = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TemperaturaMaxima</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna pronóstico de la temperatura máxima del clima que se espera el día del evento</summary>
        public String TemperaturaMaxima
        {
            get { return _TemperaturaMaxima; }
            set { _TemperaturaMaxima = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TemperaturaMinima</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna pronóstico de la temperatura mínima del clima que se espera el día del evento</summary>
        public String TemperaturaMinima
        {
            get { return _TemperaturaMinima; }
            set { _TemperaturaMinima = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TipoMontaje</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tipo de montaje en el evento</summary>
        public String TipoMontaje
        {
            get { return _TipoMontaje; }
            set { _TipoMontaje = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.TipoVestimentaOtro</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tipo de vestimenta cuando se selecciona la opción de otro</summary>
        public String TipoVestimentaOtro
        {
            get { return _TipoVestimentaOtro; }
            set { _TipoVestimentaOtro = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.FechaEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha en la que se realizará el evento</summary>
        public String FechaEvento
        {
            get { return _FechaEvento; }
            set { _FechaEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.FechaFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha final en la consulta de información</summary>
        public String FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.FechaInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la fecha inicial en la consulta de información</summary>
        public String FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.HelipuertoLugar</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el lugar del Helipuerto provisional</summary>
        public String HelipuertoLugar
        {
            get { return _HelipuertoLugar; }
            set { _HelipuertoLugar = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.HelipuertoDomicilio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el domicilio del Helipuerto provisional</summary>
        public String HelipuertoDomicilio
        {
            get { return _HelipuertoDomicilio; }
            set { _HelipuertoDomicilio = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.HelipuertoCoordenadas</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las coordenadas del Helipuerto provisional</summary>
        public String HelipuertoCoordenadas
        {
            get { return _HelipuertoCoordenadas; }
            set { _HelipuertoCoordenadas = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.HoraEventoInicio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la hora inicial en la que se realizará el evento</summary>
        public String HoraEventoInicio
        {
            get { return _HoraEventoInicio; }
            set { _HoraEventoInicio = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.HoraEventoFin</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la hora final en la que se realizará el evento</summary>
        public String HoraEventoFin
        {
            get { return _HoraEventoFin; }
            set { _HoraEventoFin = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Activo</name>
        ///   <create>30-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el registro se encuentra activo</summary>
        public Int16 Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Aforo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el aforo esperado en el evento</summary>
        public Int32 Aforo
        {
            get { return _Aforo; }
            set { _Aforo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Anio</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el año a consultar</summary>
        public Int32 Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ComiteHelipuerto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se incluye el comité del helipuerto</summary>
        public Int16 ComiteHelipuerto
        {
            get { return _ComiteHelipuerto; }
            set { _ComiteHelipuerto = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Dependencia</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tipo de dependencia a consultar. 0-> Ambos, 1-> Logística, 2-> Dirección de Protocolo</summary>
        public Int16 Dependencia
        {
            get { return _Dependencia; }
            set { _Dependencia = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Mes</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el mes a consultar</summary>
        public Int32 Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Esposa</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si la esposa del gobernador fue invitada</summary>
        public Int16 Esposa
        {
            get { return _Esposa; }
            set { _Esposa = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EsposaSi</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si la esposa del gobernador acudirá al evento</summary>
        public Int16 EsposaSi
        {
            get { return _EsposaSi; }
            set { _EsposaSi = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EsposaNo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si la esposa del gobernador no acudirá al evento</summary>
        public Int16 EsposaNo
        {
            get { return _EsposaNo; }
            set { _EsposaNo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.EsposaConfirma</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si la esposa del gobernador está pendiente por confirmar si acudirá o no al evento</summary>
        public Int16 EsposaConfirma
        {
            get { return _EsposaConfirma; }
            set { _EsposaConfirma = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Logistica</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el evento fue creado por logística</summary>
        public Int16 Logistica
        {
            get { return _Logistica; }
            set { _Logistica = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Nivel</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el nivel de la transacción</summary>
        public Int16 Nivel
        {
            get { return _Nivel; }
            set { _Nivel = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Notificacion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tipo de notificación por correo cuando se aprueba una invitación. 1-> Logística, 2-> Dirección de Protocolo, 3-> Ambos y 4-> Ninguno</summary>
        public Int16 Notificacion
        {
            get { return _Notificacion; }
            set { _Notificacion = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.PropuestaAcomodo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si se incluirá la propuesta de acomodo en el cuadernillo</summary>
        public Int16 PropuestaAcomodo
        {
            get { return _PropuestaAcomodo; }
            set { _PropuestaAcomodo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Protocolo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un valor que determina si el evento fue creado por la dirección de protocolo</summary>
        public Int16 Protocolo
        {
            get { return _Protocolo; }
            set { _Protocolo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.DataTableAcomodo</name>
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
        ///   <name>ENTEvento.DataTableComiteHelipuerto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene el detalle del comité de recepción del helipuerto provisional</summary>
        public DataTable DataTableComiteHelipuerto
        {
            get { return _DataTableComiteHelipuerto; }
            set { _DataTableComiteHelipuerto = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.DataTableComiteRecepcion</name>
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
        ///   <name>ENTEvento.DataTableEstatusEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene los ID's de estatus a mostrar</summary>
        public DataTable DataTableEstatusEvento
        {
            get { return _DataTableEstatusEvento; }
            set { _DataTableEstatusEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.DataTableMedioTraslado</name>
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
        ///   <name>ENTEvento.DataTableOrdenDia</name>
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
        ///   <name>ENTEvento.DataTableResponsable</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene al responsable del evento</summary>
        public DataTable DataTableResponsable
        {
            get { return _DataTableResponsable; }
            set { _DataTableResponsable = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.DataTableResponsableLogistica</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna un DataTable el cual contiene al responsable de logística del evento</summary>
        public DataTable DataTableResponsableLogistica
        {
            get { return _DataTableResponsableLogistica; }
            set { _DataTableResponsableLogistica = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Contacto</name>
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
        ///   <name>ENTGira.NotaInicioEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaInicioEvento
        {
            get { return _NotaInicioEvento; }
            set { _NotaInicioEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaFinEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaFinEvento
        {
            get { return _NotaFinEvento; }
            set { _NotaFinEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaEvento</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String NotaEvento
        {
            get { return _NotaEvento; }
            set { _NotaEvento = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaInicioComite</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaInicioComite
        {
            get { return _NotaInicioComite; }
            set { _NotaInicioComite = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaFinComite</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaFinComite
        {
            get { return _NotaFinComite; }
            set { _NotaFinComite = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaComite</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String NotaComite
        {
            get { return _NotaComite; }
            set { _NotaComite = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaInicioOrden</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaInicioOrden
        {
            get { return _NotaInicioOrden; }
            set { _NotaInicioOrden = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaFinOrden</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaFinOrden
        {
            get { return _NotaFinOrden; }
            set { _NotaFinOrden = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaOrden</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String NotaOrden
        {
            get { return _NotaOrden; }
            set { _NotaOrden = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaInicioAcomodo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaInicioAcomodo
        {
            get { return _NotaInicioAcomodo; }
            set { _NotaInicioAcomodo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaFinAcomodo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 NotaFinAcomodo
        {
            get { return _NotaFinAcomodo; }
            set { _NotaFinAcomodo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.NotaAcomodo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String NotaAcomodo
        {
            get { return _NotaAcomodo; }
            set { _NotaAcomodo = value; }
        }

        ///<remarks>
        ///   <name>ENTGira.PalabraClave</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String PalabraClave
        {
            get { return _PalabraClave; }
            set { _PalabraClave = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ListadoAdicional</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 ListadoAdicional
        {
            get { return _ListadoAdicional; }
            set { _ListadoAdicional = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.ListadoAdicionalTitulo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String ListadoAdicionalTitulo
        {
            get { return _ListadoAdicionalTitulo; }
            set { _ListadoAdicionalTitulo = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.DataTableListadoAdicional</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public DataTable DataTableListadoAdicional
        {
            get { return _DataTableListadoAdicional; }
            set { _DataTableListadoAdicional = value; }
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

        ///<remarks>
        ///   <name>ENTEvento.OrdenAnterior</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 OrdenAnterior
        {
            get { return _OrdenAnterior; }
            set { _OrdenAnterior = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.NuevoOrden</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int32 NuevoOrden
        {
            get { return _NuevoOrden; }
            set { _NuevoOrden = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Nombre</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Puesto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public String Puesto
        {
            get { return _Puesto; }
            set { _Puesto = value; }
        }

        ///<remarks>
        ///   <name>ENTEvento.Separador</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        public Int16 Separador
        {
            get { return _Separador; }
            set { _Separador = value; }
        }

    }
}
