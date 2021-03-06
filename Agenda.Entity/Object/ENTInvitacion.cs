﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Clase: ENTInvitacion
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
    public class ENTInvitacion : ENTBase
    {

        private Int32   _InvitacionId;
        private Int32   _CategoriaId;
        private Int32   _ColoniaId;
        private Int32   _ConductoId;
        private Int32   _EstatusInvitacionId;
        private Int32   _InvitacionComentarioId;
        private Int32   _InvitacionContactoId;
        private Int32   _LugarEventoId;
        private Int32   _ModuloId;
        private Int32   _PrioridadId;
        private Int32   _RespuestaEvaluacionId;
        private Int32   _SecretarioId_Ramo;
        private Int32   _SecretarioId_Representante;
        private Int32   _SecretarioId_Responsable;
        private Int32   _UsuarioId;
        private Int32   _UsuarioId_Temp;
        private String  _EventoDetalle;
        private String  _EventoNombre;
        private String  _InvitacionObservaciones;
        private String  _Calle;
        private String  _Comentario;
        private String  _MotivoRechazo;
        private String  _NumeroExterior;
        private String  _NumeroInterior;
        private String  _RepresentanteNombre;
        private String  _RepresentanteCargo;
        private String  _RepresentanteTelefonoOficina;
        private String  _RepresentanteTelefonoMovil;
        private String  _RepresentanteTelefonoParticular;
        private String  _RepresentanteTelefonoOtro;
        private String  _FechaEvento;
        private String  _FechaFin;
        private String  _FechaInicio;
        private String  _HoraEventoInicio;
        private String  _HoraEventoFin;
        private Int16   _Activo;
        private Int16   _Nivel;
        private Int16   _Notificacion;
        private ENTInvitacionContacto       _Contacto;
        private ENTInvitacionFuncionario    _Funcionario;
        private String  _PalabraClave;


        //Constructor

        public ENTInvitacion()
        {
            _InvitacionId = 0;
            _CategoriaId = 0;
            _ColoniaId = 0;
            _ConductoId = 0;
            _EstatusInvitacionId = 0;
            _InvitacionComentarioId = 0;
            _InvitacionContactoId = 0;
            _LugarEventoId = 0;
            _ModuloId = 0;
            _PrioridadId = 0;
            _RespuestaEvaluacionId = 0;
            _SecretarioId_Ramo = 0;
            _SecretarioId_Representante = 0;
            _SecretarioId_Responsable = 0;
            _UsuarioId = 0;
            _UsuarioId_Temp = 0;
            _EventoDetalle = "";
            _EventoNombre = "";
            _InvitacionObservaciones = "";
            _Calle = "";
            _Comentario = "";
            _MotivoRechazo = "";
            _NumeroExterior = "";
            _NumeroInterior = "";
            _RepresentanteNombre = "";
            _RepresentanteCargo = "";
            _RepresentanteTelefonoOficina = "";
            _RepresentanteTelefonoMovil = "";
            _RepresentanteTelefonoParticular = "";
            _RepresentanteTelefonoOtro = "";
            _FechaEvento = "";
            _FechaFin = "";
            _FechaInicio = "";
            _HoraEventoInicio = "";
            _HoraEventoFin = "";
            _Activo = 2;
            _Nivel = 0;
            _Notificacion = 0;
            _Contacto = new ENTInvitacionContacto();
            _Funcionario = new ENTInvitacionFuncionario();
            _PalabraClave = "";
        }


        // Propiedades

        ///<remarks>
        ///   <name>ENTInvitacion.InvitacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la Invitacion</summary>
        public Int32 InvitacionId
        {
            get { return _InvitacionId; }
            set { _InvitacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.CategoriaId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de la categoría asociada a la invitación</summary>
        public Int32 CategoriaId
        {
            get { return _CategoriaId; }
            set { _CategoriaId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.ColoniaId</name>
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
        ///   <name>ENTInvitacion.ConductoId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del conducto por el cual llego la invitación</summary>
        public Int32 ConductoId
        {
            get { return _ConductoId; }
            set { _ConductoId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.EstatusInvitacionId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del estatus de la invitación</summary>
        public Int32 EstatusInvitacionId
        {
            get { return _EstatusInvitacionId; }
            set { _EstatusInvitacionId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.InvitacionComentarioId</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de un comentario hecho sobre la Invitacion</summary>
        public Int32 InvitacionComentarioId
        {
            get { return _InvitacionComentarioId; }
            set { _InvitacionComentarioId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.InvitacionContactoId</name>
        ///   <create>07-Enero-2015</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único de un contacto de la Invitacion</summary>
        public Int32 InvitacionContactoId
        {
            get { return _InvitacionContactoId; }
            set { _InvitacionContactoId = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.LugarEventoId</name>
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
        ///   <name>ENTInvitacion.ModuloId</name>
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
        ///   <name>ENTInvitacion.PrioridadId</name>
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
        ///   <name>ENTInvitacion.RespuestaEvaluacionId</name>
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
        ///   <name>ENTInvitacion.SecretarioId_Ramo</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el identificador único del ramo de la secretaría (secretario) al que pertenece la invitación</summary>
        public Int32 SecretarioId_Ramo
        {
            get { return _SecretarioId_Ramo; }
            set { _SecretarioId_Ramo = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.SecretarioId_Representante</name>
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
        ///   <name>ENTInvitacion.SecretarioId_Responsable</name>
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
        ///   <name>ENTInvitacion.UsuarioId</name>
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
        ///   <name>ENTInvitacion.UsuarioId_Temp</name>
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
        ///   <name>ENTInvitacion.EventoDetalle</name>
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
        ///   <name>ENTInvitacion.EventoNombre</name>
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
        ///   <name>ENTInvitacion.InvitacionObservaciones</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna las observaciones de la invitación</summary>
        public String InvitacionObservaciones
        {
            get { return _InvitacionObservaciones; }
            set { _InvitacionObservaciones = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.Calle</name>
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
        ///   <name>ENTInvitacion.Comentario</name>
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
        ///   <name>ENTInvitacion.MotivoRechazo</name>
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
        ///   <name>ENTInvitacion.NumeroExterior</name>
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
        ///   <name>ENTInvitacion.RepresentanteNombre</name>
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
        ///   <name>ENTInvitacion.RepresentanteCargo</name>
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
        ///   <name>ENTInvitacion.RepresentanteTelefonoOficina</name>
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
        ///   <name>ENTInvitacion.RepresentanteTelefonoMovil</name>
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
        ///   <name>ENTInvitacion.RepresentanteTelefonoParticular</name>
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
        ///   <name>ENTInvitacion.RepresentanteTelefonoOtro</name>
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
        ///   <name>ENTInvitacion.NumeroInterior</name>
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
        ///   <name>ENTInvitacion.FechaEvento</name>
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
        ///   <name>ENTInvitacion.FechaFin</name>
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
        ///   <name>ENTInvitacion.FechaInicio</name>
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
        ///   <name>ENTInvitacion.HoraEventoInicio</name>
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
        ///   <name>ENTInvitacion.HoraEventoFin</name>
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
        ///   <name>ENTInvitacion.Activo</name>
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
        ///   <name>ENTInvitacion.Nivel</name>
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
        ///   <name>ENTInvitacion.Notificacion</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna el tipo de notificación por correo cuando se aprueba una invitación. 1-> Logística, 2-> Dirección de Protocolo, 3-> Ninguno</summary>
        public Int16 Notificacion
        {
            get { return _Notificacion; }
            set { _Notificacion = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.Contacto</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la información del contacto de la Invitación/Evento</summary>
        public ENTInvitacionContacto Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.Funcionario</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna la información del funcionario(s) asociado(s) a la Invitación/Evento</summary>
        public ENTInvitacionFuncionario Funcionario
        {
            get { return _Funcionario; }
            set { _Funcionario = value; }
        }

        ///<remarks>
        ///   <name>ENTInvitacion.PalabraClave</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene/Asigna una palabra/palabras clave en la consulta de información</summary>
        public String PalabraClave
        {
            get { return _PalabraClave; }
            set { _PalabraClave = value; }
        }

    }
}
