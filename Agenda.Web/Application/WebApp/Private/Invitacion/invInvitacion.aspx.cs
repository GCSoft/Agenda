﻿/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	invInvitacion
' Autor:	Ruben.Cobos
' Fecha:	09-Diciembre-2014
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using GCUtility.Function;
using GCUtility.Security;
using Agenda.BusinessProcess.Object;
using Agenda.BusinessProcess.Page;
using Agenda.Entity.Object;
using System.Data;

namespace Agenda.Web.Application.WebApp.Private.Invitacion
{
    public partial class invInvitacion : BPPage
    {
        
        // Servicios

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSColonia(string prefixText, int count, string contextKey){
			BPColonia oBPColonia = new BPColonia();
			ENTColonia oENTColonia = new ENTColonia();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Colonia Where ColoniaId = @ColoniaId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @ColoniaId = 0
			//				End

			try
			{

				// Formulario
                oENTColonia.ColoniaId = 0;
                oENTColonia.MunicipioId = Int32.Parse(contextKey);
				oENTColonia.Nombre = prefixText;
                oENTColonia.Activo = 1;

				// Transacción
				oENTResponse = oBPColonia.SelectColonia(oENTColonia);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowColonia in oENTResponse.DataSetResponse.Tables[1].Rows){
					Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowColonia["Nombre"].ToString(), rowColonia["ColoniaId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Colonias
			return ServiceResponse;
		}

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSFuncionario(string prefixText, int count){
			BPUsuario oBPUsuario = new BPUsuario();
			ENTUsuario oENTUsuario = new ENTUsuario();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Funcionario Where FuncionarioId = @FuncionarioId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @FuncionarioId = 0
			//				End

			try
			{

				// Formulario
                oENTUsuario.RolId = 3;      // Rol de funcionario
                oENTUsuario.UsuarioId = 0;
                oENTUsuario.Email = "";
                oENTUsuario.Nombre = prefixText;
                oENTUsuario.Activo = 1;

				// Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowFuncionario in oENTResponse.DataSetResponse.Tables[1].Rows){
                    Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowFuncionario["NombreCompletoTitulo"].ToString(), rowFuncionario["UsuarioId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Funcionarios
			return ServiceResponse;
		}

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSLugarEvento(string prefixText, int count){
			BPLugarEvento oBPLugarEvento = new BPLugarEvento();
			ENTLugarEvento oENTLugarEvento = new ENTLugarEvento();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From LugarEvento Where LugarEventoId = @LugarEventoId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @LugarEventoId = 0
			//				End

			try
			{

				// Formulario
                oENTLugarEvento.LugarEventoId = 0;
				oENTLugarEvento.Nombre = prefixText;
                oENTLugarEvento.Activo = 1;

				// Transacción
				oENTResponse = oBPLugarEvento.SelectLugarEvento(oENTLugarEvento);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowLugarEvento in oENTResponse.DataSetResponse.Tables[1].Rows){
					Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowLugarEvento["Nombre"].ToString(), rowLugarEvento["LugarEventoId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de LugarEventos
			return ServiceResponse;
		}

        [System.Web.Script.Services.ScriptMethod()]
		[System.Web.Services.WebMethod]
		public static List<string> WSSecretario(string prefixText, int count){
			BPSecretario oBPSecretario = new BPSecretario();
			ENTSecretario oENTSecretario = new ENTSecretario();
			ENTResponse oENTResponse = new ENTResponse();

			List<String> ServiceResponse = new List<String>();
			String Item;

			// Errores conocidos:
			//		* El control toma el foco con el metodo JS Focus() sólo si es llamado con la función JS pageLoad() 
			//		* No se pudo encapsular en un WUC
			//		* Si se selecciona un nombre válido, enseguida se borra y se pone uno inválido, el control almacena el ID del nombre válido, se implemento el siguiente Script en la transacción
			//			If Not Exists ( Select 1 From Secretario Where SecretarioId = @SecretarioId And ( Nombre + ' ' + ApellidoPaterno  + ' ' +  IsNull(ApellidoMaterno, '') = @NombreTemporal ) )
			//				Begin
			//					Set @SecretarioId = 0
			//				End

			try
			{

				// Formulario
                oENTSecretario.SecretarioId = 0;
				oENTSecretario.Nombre = prefixText;
                oENTSecretario.Activo = 1;

				// Transacción
				oENTResponse = oBPSecretario.SelectSecretario(oENTSecretario);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

				// Configuración de arreglo de respuesta
				foreach (DataRow rowSecretario in oENTResponse.DataSetResponse.Tables[1].Rows){
                    Item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rowSecretario["NombreTituloPuesto"].ToString(), rowSecretario["SecretarioId"].ToString());
					ServiceResponse.Add(Item);
				}

			}catch (Exception){
				// Do Nothing
			}

			// Regresar listado de Secretarios
			return ServiceResponse;
		}


        
        // Utilerías

        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();
        GCParse gcParse = new GCParse();


        // Enumeraciones

        enum TransactionTypes { Aprobar, Declinar, Registrar }



        // Rutinas del programador

        void InsertInvitacion(){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            DataTable tblFuncionario;
            DataRow rowFuncionario;

            String JSScript = "";
            String Key = "";

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

                // Formulario

                #region TAB - Datos generales
                    oENTInvitacion.CategoriaId = Int32.Parse( this.ddlCategoria.SelectedItem.Value );
                    oENTInvitacion.ConductoId = Int32.Parse(this.ddlConducto.SelectedItem.Value);
                    oENTInvitacion.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                    oENTInvitacion.SecretarioId_Ramo = Int32.Parse(this.hddSecretarioRamoId.Value);
                    oENTInvitacion.SecretarioId_Responsable = Int32.Parse(this.hddResponsableId.Value);
                    oENTInvitacion.SecretarioId_Representante = ( this.hddRepresentanteId.Value.Trim() == "" || this.hddRepresentanteId.Value.Trim() == "0" ? 0 : Int32.Parse( this.hddRepresentanteId.Value ) );
                    oENTInvitacion.InvitacionObservaciones = this.ckeObservaciones.Text.Trim();
                #endregion

                #region TAB - Datos del evento
                    oENTInvitacion.EventoNombre = this.txtNombreEvento.Text.Trim();
                    oENTInvitacion.FechaEvento = this.wucCalendar.DisplayUTCDate;
                    oENTInvitacion.HoraEvento = this.wucTimer.DisplayUTCTime;
                    oENTInvitacion.LugarEventoId = Int32.Parse( this.hddLugarEventoId.Value );
                    oENTInvitacion.ColoniaId =  Int32.Parse( this.hddColoniaId.Value );
                    oENTInvitacion.Calle = this.txtCalle.Text.Trim();
                    oENTInvitacion.NumeroExterior = this.txtNumeroExterior.Text.Trim();
                    oENTInvitacion.NumeroInterior = this.txtNumeroInterior.Text.Trim();
                    oENTInvitacion.EventoDetalle = this.ckeDetalleEvento.Text.Trim();
                #endregion

                #region TAB - Contacto
                    oENTInvitacion.Contacto.Nombre = this.txtContactoNombre.Text.Trim();
                    oENTInvitacion.Contacto.Puesto = this.txtContactoPuesto.Text.Trim();
                    oENTInvitacion.Contacto.Organizacion = this.txtContactoOrganizacion.Text.Trim();
                    oENTInvitacion.Contacto.Telefono = this.txtContactoTelefono.Text.Trim();
                    oENTInvitacion.Contacto.Email = this.txtContactoEmail.Text.Trim();
                    oENTInvitacion.Contacto.Comentarios = this.ckeContactoComentarios.Text.Trim();
                #endregion

                #region TAB - Asociar funcionarios
                    
                    // Obtener DataTable del grid
                    tblFuncionario = gcParse.GridViewToDataTable(this.gvFuncionario, true);

                    // Configurar el contenedor
                    oENTInvitacion.Funcionario.DataTableUsuario = new DataTable("DataTableFuncionario");
                    oENTInvitacion.Funcionario.DataTableUsuario.Columns.Add("UsuarioId", typeof(Int32));

                    // Listado de usuarios(funcionarios) asociados
                    foreach (DataRow rowCurrentFuncionario in tblFuncionario.Rows){
                        rowFuncionario = oENTInvitacion.Funcionario.DataTableUsuario.NewRow();
                        rowFuncionario["UsuarioId"] = rowCurrentFuncionario["UsuarioId"];
                        oENTInvitacion.Funcionario.DataTableUsuario.Rows.Add(rowFuncionario);
                    }

                #endregion

                // Estatus
                oENTInvitacion.EstatusInvitacionId = 3; // Registrada

                // Transacción
                oENTResponse = oBPInvitacion.InsertInvitacion(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                LimpiaFormulario();

                // Llave encriptada
                Key = oENTResponse.DataSetResponse.Tables[1].Rows[0]["InvitacionId"].ToString() + "|1";
                Key = gcEncryption.EncryptString(Key, true);

                // Mensaje a desplegar y script
                JSScript = "function pageLoad(){ if( confirm('Se registro la invitación exitosamente. ¿Desea ir al detalle para continuar con la captura?') ) { window.location.href('eveDetalleInvitacion.aspx?key=" + Key + "'); } }";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), JSScript, true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void InsertInvitacion_Declinar(){
            ENTInvitacion oENTInvitacion = new ENTInvitacion();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPInvitacion oBPInvitacion = new BPInvitacion();

            DataTable tblFuncionario;
            DataRow rowFuncionario;

            String JSScript = "";
            String Key = "";

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTInvitacion.UsuarioId = oENTSession.UsuarioId;

                // Formulario

                #region TAB - Datos generales
                    oENTInvitacion.CategoriaId = Int32.Parse( this.ddlCategoria.SelectedItem.Value );
                    oENTInvitacion.ConductoId = Int32.Parse(this.ddlConducto.SelectedItem.Value);
                    oENTInvitacion.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                    oENTInvitacion.SecretarioId_Ramo = Int32.Parse(this.hddSecretarioRamoId.Value);
                    oENTInvitacion.SecretarioId_Responsable = Int32.Parse(this.hddResponsableId.Value);
                    oENTInvitacion.SecretarioId_Representante = ( this.hddRepresentanteId.Value.Trim() == "" || this.hddRepresentanteId.Value.Trim() == "0" ? 0 : Int32.Parse( this.hddRepresentanteId.Value ) );
                    oENTInvitacion.InvitacionObservaciones = this.ckeObservaciones.Text.Trim();
                #endregion

                #region TAB - Datos del evento
                    oENTInvitacion.EventoNombre = this.txtNombreEvento.Text.Trim();
                    oENTInvitacion.FechaEvento = this.wucCalendar.DisplayUTCDate;
                    oENTInvitacion.HoraEvento = this.wucTimer.DisplayUTCTime;
                    oENTInvitacion.LugarEventoId = Int32.Parse( this.hddLugarEventoId.Value );
                    oENTInvitacion.ColoniaId =  Int32.Parse( this.hddColoniaId.Value );
                    oENTInvitacion.Calle = this.txtCalle.Text.Trim();
                    oENTInvitacion.NumeroExterior = this.txtNumeroExterior.Text.Trim();
                    oENTInvitacion.NumeroInterior = this.txtNumeroInterior.Text.Trim();
                    oENTInvitacion.EventoDetalle = this.ckeDetalleEvento.Text.Trim();
                #endregion

                #region TAB - Contacto
                    oENTInvitacion.Contacto.Nombre = this.txtContactoNombre.Text.Trim();
                    oENTInvitacion.Contacto.Puesto = this.txtContactoPuesto.Text.Trim();
                    oENTInvitacion.Contacto.Organizacion = this.txtContactoOrganizacion.Text.Trim();
                    oENTInvitacion.Contacto.Telefono = this.txtContactoTelefono.Text.Trim();
                    oENTInvitacion.Contacto.Email = this.txtContactoEmail.Text.Trim();
                    oENTInvitacion.Contacto.Comentarios = this.ckeContactoComentarios.Text.Trim();
                #endregion

                #region TAB - Asociar funcionarios
                    
                    // Obtener DataTable del grid
                    tblFuncionario = gcParse.GridViewToDataTable(this.gvFuncionario, true);

                    // Configurar el contenedor
                    oENTInvitacion.Funcionario.DataTableUsuario = new DataTable("DataTableFuncionario");
                    oENTInvitacion.Funcionario.DataTableUsuario.Columns.Add("UsuarioId", typeof(Int32));

                    // Listado de usuarios(funcionarios) asociados
                    foreach (DataRow rowCurrentFuncionario in tblFuncionario.Rows){
                        rowFuncionario = oENTInvitacion.Funcionario.DataTableUsuario.NewRow();
                        rowFuncionario["UsuarioId"] = rowCurrentFuncionario["UsuarioId"];
                        oENTInvitacion.Funcionario.DataTableUsuario.Rows.Add(rowFuncionario);
                    }

                #endregion

                // Estatus
                oENTInvitacion.EstatusInvitacionId = 2; // Declinada
                
                // Transacción
                oENTResponse = oBPInvitacion.InsertInvitacion(oENTInvitacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Transacción exitosa
                LimpiaFormulario();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void LimpiaFormulario(){
            try
            {

                // TAB - Datos generales
                this.ddlCategoria.SelectedIndex = 0;
                this.ddlConducto.SelectedIndex = 0;
                this.ddlPrioridad.SelectedIndex = 0;

                this.txtSecretarioRamo.Text = "";
                this.hddSecretarioRamoId.Value = "";

                this.txtResponsable.Text = "";
                this.hddResponsableId.Value = "";

                this.txtRepresentante.Text = "";
                this.hddRepresentanteId.Value = "";

                this.ckeObservaciones.Text = "";

                // TAB - Datos del evento
                this.txtNombreEvento.Text = "";
                this.wucCalendar.SetDate(DateTime.Now);
                this.wucTimer.DisplayTime = "10:00 a.m.";

                this.txtLugarEvento.Text = "";
                this.hddLugarEventoId.Value = "";

                this.ddlMunicipio.SelectedIndex = 0;

                this.txtColonia.Text = "";
                this.hddColoniaId.Value = "";

                this.txtCalle.Text = "";
                this.txtNumeroExterior.Text = "";
                this.txtNumeroInterior.Text = "";
                this.ckeDetalleEvento.Text = "";

                // TAB - Contacto
                this.txtContactoNombre.Text = "";
                this.txtContactoPuesto.Text = "";
                this.txtContactoOrganizacion.Text = "";
                this.txtContactoTelefono.Text = "";
                this.txtContactoEmail.Text = "";
                this.ckeContactoComentarios.Text = "";

                // TAB - Asociar funcionarios
                this.txtFuncionario.Text = "";
                this.hddFuncionarioId.Value = "";

                this.gvFuncionario.DataSource = null;
                this.gvFuncionario.DataBind();

                // Foco y pestaña
                this.tabInvitacion.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectCategoria(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTCategoria oENTCategoria = new ENTCategoria();

            BPCategoria oBPCategoria = new BPCategoria();

            try
            {

                // Formulario
                oENTCategoria.CategoriaId = 0;
                oENTCategoria.Nombre = "";
                oENTCategoria.Activo = 1;

                // Transacción
                oENTResponse = oBPCategoria.SelectCategoria(oENTCategoria);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlCategoria.DataTextField = "Nombre";
                this.ddlCategoria.DataValueField = "CategoriaId";
                this.ddlCategoria.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlCategoria.DataBind();

                // Agregar Item de selección
                this.ddlCategoria.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectConducto(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTConducto oENTConducto = new ENTConducto();

            BPConducto oBPConducto = new BPConducto();

            try
            {

                // Formulario
                oENTConducto.ConductoId = 0;
                oENTConducto.Nombre = "";
                oENTConducto.Activo = 1;

                // Transacción
                oENTResponse = oBPConducto.SelectConducto(oENTConducto);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlConducto.DataTextField = "Nombre";
                this.ddlConducto.DataValueField = "ConductoId";
                this.ddlConducto.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlConducto.DataBind();

                // Agregar Item de selección
                this.ddlConducto.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMunicipio(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMunicipio oENTMunicipio = new ENTMunicipio();

            BPMunicipio oBPMunicipio = new BPMunicipio();

            try
            {

                // Formulario
                oENTMunicipio.EstadoId = 19; //Nuevo León
                oENTMunicipio.MunicipioId = 0;
                oENTMunicipio.Nombre = "";
                oENTMunicipio.Activo = 1;

                // Transacción
                oENTResponse = oBPMunicipio.SelectMunicipio(oENTMunicipio);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo de municipio
                this.ddlMunicipio.DataTextField = "Nombre";
                this.ddlMunicipio.DataValueField = "MunicipioId";
                this.ddlMunicipio.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMunicipio.DataBind();

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlMunicipio.SelectedItem.Value;

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectPrioridad(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTPrioridad oENTPrioridad = new ENTPrioridad();

            BPPrioridad oBPPrioridad = new BPPrioridad();

            try
            {

                // Formulario
                oENTPrioridad.PrioridadId = 0;
                oENTPrioridad.Nombre = "";
                oENTPrioridad.Activo = 1;

                // Transacción
                oENTResponse = oBPPrioridad.SelectPrioridad(oENTPrioridad);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlPrioridad.DataTextField = "Nombre";
                this.ddlPrioridad.DataValueField = "PrioridadId";
                this.ddlPrioridad.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlPrioridad.DataBind();

                // Agregar Item de selección
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Seleccione]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ValidarFormulario(TransactionTypes TransactionType){
            DataTable tblFuncionario;

            try
            {

                // TAB - Datos generales
                if ( this.ddlCategoria.SelectedIndex == 0 ) {
                    this.tabInvitacion.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Tipo de cita"));
                }

                if ( this.ddlConducto.SelectedIndex == 0 ) {
                    this.tabInvitacion.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Conducto"));
                }
                
                if ( this.ddlPrioridad.SelectedIndex == 0 ) {
                    this.tabInvitacion.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar una Prioridad"));
                }

                if( this.hddSecretarioRamoId.Value.Trim() == "" || this.hddSecretarioRamoId.Value.Trim() == "0" ){
                    this.tabInvitacion.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Secretario Ramo"));
                }

                if( this.hddResponsableId.Value.Trim() == "" || this.hddResponsableId.Value.Trim() == "0" ){
                    this.tabInvitacion.ActiveTabIndex = 0;
                    throw (new Exception("Es necesario seleccionar un Responsable"));
                }

                // TAB - Datos del evento
                if( this.txtNombreEvento.Text.Trim() == "" ){
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario ingresar un Nombre de evento"));
                }

                if ( !this.wucCalendar.IsValidDate() ) {
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw new Exception("El campo [Fecha del evento] es requerido");
                }

                if ( !this.wucTimer.IsValidTime() ) {
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw new Exception("El campo [Hora del evento] es requerido");
                }

                if( this.hddLugarEventoId.Value.Trim() == "" || this.hddLugarEventoId.Value.Trim() == "0" ){
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario seleccionar un Lugar del Evento"));
                }

                if( this.hddColoniaId.Value.Trim() == "" || this.hddColoniaId.Value.Trim() == "0" ){
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario seleccionar una Colonia"));
                }
                
                if( this.txtCalle.Text.Trim() == "" ){
                    this.tabInvitacion.ActiveTabIndex = 1;
                    throw (new Exception("Es necesario ingresar una Calle"));
                }

                // TAB - Contacto
                if( this.txtContactoNombre.Text.Trim() == "" ){
                    this.tabInvitacion.ActiveTabIndex = 2;
                    throw (new Exception("Es necesario ingresar un Nombre de contacto"));
                }

                if( this.txtContactoTelefono.Text.Trim() == "" ){
                    this.tabInvitacion.ActiveTabIndex = 2;
                    throw (new Exception("Es necesario ingresar un Teléfono del contacto"));
                }

                // TAB - Asociar funcionarios
                if ( TransactionType == TransactionTypes.Registrar ){

                    tblFuncionario = gcParse.GridViewToDataTable(this.gvFuncionario, false);
                    if( tblFuncionario.Rows.Count == 0 ){
                        this.tabInvitacion.ActiveTabIndex = 3;
                        throw (new Exception("Es necesario asociar por lo menos a un funcionario"));
                    }

                }

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                SelectCategoria();
                SelectConducto();
                SelectMunicipio();
                SelectPrioridad();

                // Estado inicial
                this.gvFuncionario.DataSource = null;
                this.gvFuncionario.DataBind();

                // Foco
                this.tabInvitacion.ActiveTabIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlCategoria.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }       

        protected void btnCancelar_Click(object sender, EventArgs e){
            try
            {

                LimpiaFormulario();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }

        protected void btnDeclinar_Click(object sender, EventArgs e){
            try
            {

                // Validar el formulario
                ValidarFormulario(TransactionTypes.Declinar);

                // Transacción
                InsertInvitacion_Declinar();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e){
            try
            {

                // Validar el formulario
                ValidarFormulario(TransactionTypes.Registrar);

                // Transacción
                InsertInvitacion();

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); }", true);
            }
        }



        // Eventos de panel - Ubicación

        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

				// Limpiado de controles
                this.txtColonia.Text = "";
                this.hddColoniaId.Value = "";

                // Configurar el context key del autosuggest de colonia
                autosuggestColonia.ContextKey = this.ddlMunicipio.SelectedItem.Value;

				// Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtColonia.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtColonia.ClientID + "'); }", true);
            }
        }



        // Eventos de panel - Funcionarios

        protected void btnAsociarFuncionario_Click(object sender, EventArgs e){
            BPUsuario oBPUsuario = new BPUsuario();
			ENTUsuario oENTUsuario = new ENTUsuario();
			ENTResponse oENTResponse = new ENTResponse();
            
            DataTable tblFuncionario;
            DataRow rowFuncionario;

            try
            {

                // Obtener DataTable del grid
                tblFuncionario = gcParse.GridViewToDataTable(this.gvFuncionario, false);

                // Validaciones
                if( this.hddFuncionarioId.Value.Trim() == "" || this.hddFuncionarioId.Value.Trim() == "0" ){ throw ( new Exception( "Es necesario seleccionar un funcionario" ) ); }
                if ( tblFuncionario.Select("UsuarioId='" + this.hddFuncionarioId.Value.Trim() + "'" ).Length > 0 ){ throw ( new Exception( "Ya ha selecionado este funcionario" ) ); }

                // Formulario
                oENTUsuario.RolId = 3;      // Rol de funcionario
                oENTUsuario.UsuarioId = Int32.Parse(this.hddFuncionarioId.Value.Trim());
                oENTUsuario.Email = "";
                oENTUsuario.Nombre = "";
                oENTUsuario.Activo = 1;

				// Transacción
                oENTResponse = oBPUsuario.SelectUsuario(oENTUsuario);

				// Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Agregar un nuevo elemento
                rowFuncionario = tblFuncionario.NewRow();
                rowFuncionario["UsuarioId"] = oENTResponse.DataSetResponse.Tables[1].Rows[0]["UsuarioId"].ToString();
                rowFuncionario["Nombre"] = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Nombre"].ToString();
                rowFuncionario["NombreCompletoTitulo"] = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NombreCompletoTitulo"].ToString();
                rowFuncionario["Puesto"] = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Puesto"].ToString();
                tblFuncionario.Rows.Add(rowFuncionario);

                // Actualizar Grid
                this.gvFuncionario.DataSource = tblFuncionario;
                this.gvFuncionario.DataBind();

                // Nueva captura
                this.txtFuncionario.Text = "";
                this.hddFuncionarioId.Value = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);

            }catch (Exception ex){
                this.txtFuncionario.Text = "";
                this.hddFuncionarioId.Value = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }

        protected void gvFuncionario_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblFuncionario;

            String strCommand = "";
            String UsuarioId = "";
            Int32 intRow = 0;

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el evento RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                UsuarioId = this.gvFuncionario.DataKeys[intRow]["UsuarioId"].ToString();

                // Acción
                switch (strCommand){

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblFuncionario = gcParse.GridViewToDataTable(this.gvFuncionario, true);

                        // Remover el elemento
                        tblFuncionario.Rows.Remove( tblFuncionario.Select("UsuarioId=" + UsuarioId )[0] );

                        // Actualizar Grid
                        this.gvFuncionario.DataSource = tblFuncionario;
                        this.gvFuncionario.DataBind();

                        // Nueva captura
                        this.txtFuncionario.Text = "";
                        this.hddFuncionarioId.Value = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtFuncionario.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }

        protected void gvFuncionario_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;

            String UsuarioId = "";
            String FuncionarioNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                UsuarioId = this.gvFuncionario.DataKeys[e.Row.RowIndex]["UsuarioId"].ToString();
                FuncionarioNombre = this.gvFuncionario.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Desasociar funcionario [" + FuncionarioNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvFuncionario_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvFuncionario, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtFuncionario.ClientID + "'); }", true);
            }
        }

    }
}