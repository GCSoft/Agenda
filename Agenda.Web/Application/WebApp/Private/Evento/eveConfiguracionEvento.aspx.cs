/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveConfiguracionEvento
' Autor:	Ruben.Cobos
' Fecha:	27-Enero-2015
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
using Agenda.Entity.Object;
using Agenda.BusinessProcess.Object;
using System.Data;
using System.IO;

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveConfiguracionEvento : System.Web.UI.Page
    {
       
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();
        GCParse gcParse = new GCParse();
        GCNumber gcNumber = new GCNumber();

        
        // Variables de control de GridView en PostBack al cargar imágenes
        DataTable tblComiteHelipuerto_Public;
        DataTable tblComiteRecepcion_Public;
        DataTable tblOrdenDia_Public;
        DataTable tblAcomodo_Public;
        DataTable tblListadoAdicional_Public;
        DataTable tblResponsableEvento_Public;
        DataTable tblResponsableLogistica_Public;


        // Funciones del programador

		String GetKey(String sKey) {
			String Response = "";

			try{

				Response = gcEncryption.DecryptString(sKey, true);

			}catch(Exception){
				Response = "";
			}

			return Response;
		}

        Boolean ValidaFormulario(){
            DataTable tblTemporal;
            DataRow rowTemporal;

            Int32 AccordionSelectedIndex;
            Int32 ValidateNumber;

            Boolean Response = false;
            String CurrentClientID = this.ddlTipoVestimenta.ClientID;

            try
            {

                #region "Sección: Nombre del evento"

                    // Expander el acordeón
                    AccordionSelectedIndex = this.acrdNombreEvento.SelectedIndex;
                    this.acrdNombreEvento.SelectedIndex = 0;

                    //// Medios de traslado seleccionados
                    //tblTemporal = new DataTable("DataTableMedioTraslado");
                    //tblTemporal.Columns.Add("MedioTrasladoId", typeof(Int32));
                    //for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
                    //    if(this.chklMedioTraslado.Items[k].Selected){
                    //        rowTemporal = tblTemporal.NewRow();
                    //        rowTemporal["MedioTrasladoId"] = this.chklMedioTraslado.Items[k].Value;
                    //        tblTemporal.Rows.Add(rowTemporal);
                    //    }
                    //}
                
                    //// Validaciones
                    //CurrentClientID = this.ddlTipoVestimenta.ClientID;
                    //if (this.ddlTipoVestimenta.SelectedItem.Value == "6" && this.txtTipoVestimentaOtro.Text.Trim() == "") { throw (new Exception("Es necesario ïngresar el tipo de vestimenta")); }

                    //CurrentClientID = this.chklMedioTraslado.ClientID;
                    //if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario seleccionar un medio de traslado")); }

                    CurrentClientID = this.txtAforo.ClientID;
                    if ( Int32.TryParse(this.txtAforo.Text, out ValidateNumber) == false) { throw (new Exception("La cantidad en Aforo debe de ser numérica")); }

                    //CurrentClientID = this.txtTipoMontaje.ClientID;
                    //if (this.txtTipoMontaje.Text.Trim() == "") { throw (new Exception("Es necesario determinar el tipo de montaje")); }

                    //CurrentClientID = this.txtAccionRealizar.ClientID;
                    //if (this.txtAccionRealizar.Text.Trim() == "") { throw (new Exception("Es necesario determinar la acción a realizar")); }
                    
                    // Estado original del acordeón
                    this.acrdNombreEvento.SelectedIndex = AccordionSelectedIndex;
                    
                #endregion

                //#region "Comité de Recepción en el Helipuerto Provisional"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdComiteHelipuerto.SelectedIndex;
                //    this.acrdComiteHelipuerto.SelectedIndex = 0;

                //    if ( this.ddlComiteHelipuerto.SelectedItem.Value == "1" ){

                //        CurrentClientID = this.txtComiteHelipuertoLugar.ClientID;
                //        if (this.txtComiteHelipuertoLugar.Text.Trim() == "") { throw (new Exception("Es necesario determinar el lugar del helipuerto provisional")); }

                //        CurrentClientID = this.txtComiteHelipuertoDomicilio.ClientID;
                //        if (this.txtComiteHelipuertoDomicilio.Text.Trim() == "") { throw (new Exception("Es necesario determinar el domicilio del helipuerto provisional")); }

                //        CurrentClientID = this.txtComiteHelipuertoCoordenadas.ClientID;
                //        if (this.txtComiteHelipuertoCoordenadas.Text.Trim() == "") { throw (new Exception("Es necesario determinar las coordenadas del helipuerto provisional")); }

                //        // Comité de recepción en helipuerto
                //        tblTemporal = null;
                //        tblTemporal = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);
                    
                //        // Validaciones
                //        CurrentClientID = this.txtComiteHelipuertoNombre.ClientID;
                //        if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el Comité de Recepción en el Helipuerto")); }

                //    }

                //    // Estado original del acordeón
                //    this.acrdComiteHelipuerto.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                //#region "Comité de recepción"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdComiteRecepcion.SelectedIndex;
                //    this.acrdComiteRecepcion.SelectedIndex = 0;

                //    // Comité de recepción
                //    tblTemporal = null;
                //    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);
                    
                //    // Validaciones
                //    CurrentClientID = this.txtComiteRecepcionNombre.ClientID;
                //    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el Comité de Recepción")); }

                //    // Estado original del acordeón
                //    this.acrdComiteRecepcion.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                //#region "Orden del día"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdOrdenDia.SelectedIndex;
                //    this.acrdOrdenDia.SelectedIndex = 0;

                //    // Comité de recepción
                //    tblTemporal = null;
                //    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                //    // Validaciones
                //    CurrentClientID = this.txtOrdenDiaDetalle.ClientID;
                //    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar la Orden del Día")); }

                //    // Estado original del acordeón
                //    this.acrdOrdenDia.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                //#region "Acomodo"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdAcomodo.SelectedIndex;
                //    this.acrdAcomodo.SelectedIndex = 0;

                //    // Comité de recepción
                //    tblTemporal = null;
                //    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                //    // Validaciones
                //    CurrentClientID = this.txtAcomodoNombre.ClientID;
                //    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario capturar el acomodo al evento")); }

                //    // Estado original del acordeón
                //    this.acrdAcomodo.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                //#region "Responsable del evento"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdResponsable.SelectedIndex;
                //    this.acrdResponsable.SelectedIndex = 0;

                //    // Comité de recepción
                //    tblTemporal = null;
                //    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);
                    
                //    // Validaciones
                //    CurrentClientID = this.txtResponsableEventoNombre.ClientID;
                //    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario ingresar por lo menos un responsable del evento")); }

                //    // Estado original del acordeón
                //    this.acrdResponsable.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                //#region "Responsable de logística"

                //    // Expander el acordeón
                //    AccordionSelectedIndex = this.acrdResponsableLogistica.SelectedIndex;
                //    this.acrdResponsableLogistica.SelectedIndex = 0;

                //    // Comité de recepción
                //    tblTemporal = null;
                //    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);
                    
                //    // Validaciones
                //    CurrentClientID = this.txtResponsableLogisticaNombre.ClientID;
                //    if (tblTemporal.Rows.Count == 0) { throw (new Exception("Es necesario ingresar por lo menos un responsable de logística")); }

                //    // Estado original del acordeón
                //    this.acrdResponsableLogistica.SelectedIndex = AccordionSelectedIndex;

                //#endregion

                // Validacion exitosa
                Response = true;

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + CurrentClientID + "'); }", true);
            }

            return Response;

        }


        // Rutinas el programador

        void DeleteDocumento(Int32 DocumentoId){
			ENTDocumento oENTDocumento = new ENTDocumento();
			ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

			BPDocumento oBPDocumento = new BPDocumento();

			try
			{

                // Obtener Sesion
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTDocumento.UsuarioId = oENTSession.UsuarioId;
                oENTDocumento.RolId = oENTSession.RolId;

				// Formulario
				oENTDocumento.DocumentoId = DocumentoId;
                oENTDocumento.ModuloId = 2; // Evento

				// Consultar información del archivo
				oENTResponse = oBPDocumento.SelectDocumento_Path(oENTDocumento);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

				// Eliminar físicamente el archivo
                if (File.Exists(oENTResponse.DataSetResponse.Tables[1].Rows[0]["Ruta"].ToString())) { File.Delete(oENTResponse.DataSetResponse.Tables[1].Rows[0]["Ruta"].ToString()); }

				// Eliminar la referencia del archivo en la base de datos
				oENTResponse = oBPDocumento.DeleteDocumento(oENTDocumento);

				// Errores y Warnings
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

				// Refrescar el formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnAgregarImagenMontaje.ClientID + "'); }", true);

			}catch ( IOException ioEx){

				throw (ioEx);
			}catch (Exception ex){

				throw (ex);
			}
		}

        void InhabilitarEdicion( ref GridView GridControl, ref Label LabelControl){
            ImageButton imgEdit = null;

            try
            {

                // Ocultar botón de edición
                foreach( GridViewRow oRow in GridControl.Rows ){

                    imgEdit = (ImageButton)oRow.FindControl("imgEdit");
                    if ( imgEdit != null ){ imgEdit.Visible = false; }
                }

                // Mostrar leyenda
                LabelControl.Visible = true;

            }catch(Exception ex){
                throw(ex);
            }
        }

        void InsertDocumento(){
            ENTDocumento oENTDocumento = new ENTDocumento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession;

            BPDocumento oBPDocumento = new BPDocumento();

            try
            {

                // Validaciones
                if ( this.rblTipoDocumento.SelectedItem.Value == "3" ){

                    // Sólo para carga de archivos
                    if (this.fupDocumento.PostedFile == null) { throw (new Exception("Es necesario seleccionar una imágen")); }
                    if (!this.fupDocumento.HasFile) { throw (new Exception("Es necesario seleccionar una imágen")); }
                    if (this.fupDocumento.PostedFile.ContentLength == 0) { throw (new Exception("Es necesario seleccionar una imágen")); }
                }
				
				 // Obtener Sesion
				oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTDocumento.UsuarioId = oENTSession.UsuarioId;
                oENTDocumento.RolId = oENTSession.RolId;

				// Formulario
                oENTDocumento.InvitacionId = 0;
				oENTDocumento.EventoId = Int32.Parse( this.hddEventoId.Value );
				oENTDocumento.ModuloId = 2; // Evento
                oENTDocumento.TipoDocumentoId = 3; // Imágen de Montaje
                

                if ( this.rblTipoDocumento.SelectedItem.Value == "3" ){

                    oENTDocumento.Extension = Path.GetExtension(this.fupDocumento.PostedFile.FileName);
                    oENTDocumento.Nombre = this.fupDocumento.FileName;
                    oENTDocumento.Ruta = oBPDocumento.UploadFile(this.fupDocumento.PostedFile, this.hddEventoId.Value, BPDocumento.RepositoryTypes.Evento);

                    oENTDocumento.Descripcion = "Imagen de montaje cargada desde archivo";

                } else {

                    switch( this.rblTipoDocumento.SelectedItem.Value ){
                        case "1":

                            oENTDocumento.Extension = "png";
                            oENTDocumento.Nombre = "Montaje1.png";
                            oENTDocumento.Descripcion = "Imagen de montaje precargada, Diseño 1, auditorio";
                            break;

                        case "2":

                            oENTDocumento.Extension = "png";
                            oENTDocumento.Nombre = "Montaje2.png";
                            oENTDocumento.Descripcion = "Imagen de montaje precargada, Diseño 2, mesa";
                            break;

                    }
                    oENTDocumento.Ruta = oBPDocumento.CloneFile(oENTDocumento.Nombre, this.hddEventoId.Value, BPDocumento.RepositoryTypes.Evento);

                }

				// Transacción
				oENTResponse = oBPDocumento.InsertDocumento(oENTDocumento);

				// Validaciones
				if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
				if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Refrescar el formulario
                SelectEvento();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnAgregarImagenMontaje.ClientID + "'); }", true);

            }catch (Exception ex){
                throw (ex);
            }
        }

        void ReorderGrid_ListadoAdicional(){
            Int32 NewOrder = 1;

            try
            {

                foreach(GridViewRow rowListadoAdicional in this.gvListadoAdicional.Rows){

                    if( this.gvListadoAdicional.DataKeys[rowListadoAdicional.RowIndex]["Separador"].ToString() == "0" ){

                        rowListadoAdicional.Cells[1].Text = NewOrder.ToString();
                        NewOrder = NewOrder + 1;

                    }
                }


            }catch(Exception){
                // Do Nothing
            }
        }

        void RecoveryGridState(){
            try
            {

				// Recuperar los grid's que hayan sufrido cambios
                if( this.lblComiteHelipuerto.Visible ){
                    this.gvComiteHelipuerto.DataSource = tblComiteHelipuerto_Public;
                    this.gvComiteHelipuerto.DataBind();
                }

                if( this.lblComiteRecepcion.Visible ){
                    this.gvComiteRecepcion.DataSource = tblComiteRecepcion_Public;
                    this.gvComiteRecepcion.DataBind();
                }

                if( this.lblOrdenDia.Visible ){
                    this.gvOrdenDia.DataSource = tblOrdenDia_Public;
                    this.gvOrdenDia.DataBind();
                }

                if( this.lblAcomodo.Visible ){
                    this.gvAcomodo.DataSource = tblAcomodo_Public;
                    this.gvAcomodo.DataBind();
                }

                if( this.lblListadoAdicional.Visible ){
                    this.gvListadoAdicional.DataSource = tblListadoAdicional_Public;
                    this.gvListadoAdicional.DataBind();
                }

                if( this.lblResponsableEvento.Visible ){
                    this.gvResponsableEvento.DataSource = tblResponsableEvento_Public;
                    this.gvResponsableEvento.DataBind();
                }

                if( this.lblResponsableLogistica.Visible ){
                    this.gvResponsableLogistica.DataSource = tblResponsableLogistica_Public;
                    this.gvResponsableLogistica.DataBind();
                }

            }catch (Exception){
                // Do Nothing
            }
        }

        void SaveGridState(){
            try
            {

                // Limpiar variables
                tblComiteHelipuerto_Public = null;
                tblComiteRecepcion_Public = null;
                tblOrdenDia_Public = null;
                tblAcomodo_Public = null;
                tblListadoAdicional_Public = null;
                tblResponsableEvento_Public = null;
                tblResponsableLogistica_Public = null;

                // Obtener los grid's que hayan sufrido cambios
                if( this.lblComiteHelipuerto.Visible ){
                    tblComiteHelipuerto_Public = gcParse.GridViewToDataTable( this.gvComiteHelipuerto, true );
                }

                if( this.lblComiteRecepcion.Visible ){
                    tblComiteRecepcion_Public = gcParse.GridViewToDataTable( this.gvComiteRecepcion, true );
                }

                if( this.lblOrdenDia.Visible ){
                    tblOrdenDia_Public = gcParse.GridViewToDataTable( this.gvOrdenDia, true );
                }

                if( this.lblAcomodo.Visible ){
                    tblAcomodo_Public = gcParse.GridViewToDataTable( this.gvAcomodo, true );
                }

                if( this.lblListadoAdicional.Visible ){
                    tblListadoAdicional_Public = gcParse.GridViewToDataTable( this.gvListadoAdicional, true );
                }

                if( this.lblResponsableEvento.Visible ){
                    tblResponsableEvento_Public = gcParse.GridViewToDataTable( this.gvResponsableEvento, true );
                }

                if( this.lblResponsableLogistica.Visible ){
                    tblResponsableLogistica_Public = gcParse.GridViewToDataTable( this.gvResponsableLogistica, true );
                }
				

            }catch (Exception){
                // Do Nothing
            }
        }

        void SelectComiteHelipuerto(){
            try
            {

                // Agregar Item de selección
                this.ddlComiteHelipuerto.Items.Insert(0, new ListItem("Si", "1"));
                this.ddlComiteHelipuerto.Items.Insert(0, new ListItem("No", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();

            BPEvento oBPEvento = new BPEvento();

            try
            {

                // Formulario
                oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);

                // Transacción
                oENTResponse = oBPEvento.SelectEvento_Detalle(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Carátula compacta
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraInicioTexto"].ToString() + " al " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHoraFinTexto"].ToString();

                // Comentario en cuadernillo
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "0" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "0" ){ this.rblNotaDocumento.SelectedIndex = 0; }
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "1" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "0" ){ this.rblNotaDocumento.SelectedIndex = 1; }
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "0" && oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "1" ){ this.rblNotaDocumento.SelectedIndex = 2; }
                this.txtNotaDocumento.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString();

                // Sección: Nombre del evento
                if ( oENTResponse.DataSetResponse.Tables[6].Rows.Count > 0 ){

                    this.ddlTipoVestimenta.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString();
                    this.ddlMedioComunicacion.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionId"].ToString();

                    for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
					    if( oENTResponse.DataSetResponse.Tables[7].Select("MedioTrasladoId=" + this.chklMedioTraslado.Items[k].Value).Length > 0 ){
                            this.chklMedioTraslado.Items[k].Selected = true;
					    }
				    }

                    this.txtTipoVestimentaOtro.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaOtro"].ToString();
                    this.txtPronostico.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["PronosticoClima"].ToString();
                    this.txtTemperaturaMinima.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMinima"].ToString();
                    this.txtTemperaturaMaxima.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMaxima"].ToString();
                    this.txtLugarArribo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["LugarArribo"].ToString();
                    this.txtAforo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString();
                    this.txtTipoMontaje.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString();

                    if ( oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString() == "1" ){

                        this.chkEsposaInvitada.Checked = true;
                        this.rblConfirmacionEsposa.Enabled = true;

                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaSi"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "1"; }
                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaNo"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "2"; }
                        if (oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaConfirma"].ToString() == "1") { this.rblConfirmacionEsposa.SelectedValue = "3"; }
                    
                    }else{

                        this.chkEsposaInvitada.Checked = false;
                        this.rblConfirmacionEsposa.Enabled = false;
                        this.rblConfirmacionEsposa.ClearSelection();
                    }

                    this.txtAccionRealizar.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["AccionRealizar"].ToString();
                    this.txtCaracteristicasInvitados.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["CaracteristicasInvitados"].ToString();
                    this.txtMenu.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["Menu"].ToString();


                    this.ddlTipoAcomodo.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoId"].ToString();
                    this.ddlPropuestaAcomodo.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["PropuestaAcomodo"].ToString();
                    this.txtAcomodoObservaciones.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["AcomodoObservaciones"].ToString();

                    this.ddlListadoAdicional.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ListadoAdicional"].ToString();
                    //this.txtListadoAdicionalTitulo.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ListadoAdicionalTitulo"].ToString();

                    this.ddlComiteHelipuerto.SelectedValue = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ComiteHelipuerto"].ToString();
                    this.txtComiteHelipuertoLugar.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoLugar"].ToString();
                    this.txtComiteHelipuertoDomicilio.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoDomicilio"].ToString();
                    this.txtComiteHelipuertoCoordenadas.Text = oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoCoordenadas"].ToString();

                }

                // Sección: Comité de Recepción en el Helipuerto Provisional
                this.gvComiteHelipuerto.DataSource = oENTResponse.DataSetResponse.Tables[14];
                this.gvComiteHelipuerto.DataBind();

                // Sección: Comité de recepción
                this.gvComiteRecepcion.DataSource = oENTResponse.DataSetResponse.Tables[8];
                this.gvComiteRecepcion.DataBind();

                // Sección: Orden del día
                this.gvOrdenDia.DataSource = oENTResponse.DataSetResponse.Tables[9];
                this.gvOrdenDia.DataBind();

                // Sección: Acomodo
                this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[10];
                this.gvAcomodo.DataBind();

                // Sección: Listado adicional
                this.gvListadoAdicional.DataSource = oENTResponse.DataSetResponse.Tables[15];
                this.gvListadoAdicional.DataBind();

                // Modificar manualmente el orden del listado adicional
                ReorderGrid_ListadoAdicional();

                // Sección: Responsable del evento
                this.gvResponsableEvento.DataSource = oENTResponse.DataSetResponse.Tables[11];
                this.gvResponsableEvento.DataBind();

                // Sección: Responsable de logística
                this.gvResponsableLogistica.DataSource = oENTResponse.DataSetResponse.Tables[12];
                this.gvResponsableLogistica.DataBind();

                // Documentos
                for (int i = oENTResponse.DataSetResponse.Tables[3].Rows.Count - 1; i >= 0; i--) {
                    DataRow dr = oENTResponse.DataSetResponse.Tables[3].Rows[i];
                    if (dr["TipoDocumentoId"].ToString() != "3") { dr.Delete(); }
                }
                this.gvImagenMontaje.DataSource = oENTResponse.DataSetResponse.Tables[3];
                this.gvImagenMontaje.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectListadoAdicional(){
            try
            {

                // Agregar Item de selección
                this.ddlListadoAdicional.Items.Insert(0, new ListItem("Si", "1"));
                this.ddlListadoAdicional.Items.Insert(0, new ListItem("No", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMedioComunicacion(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMedioComunicacion oENTMedioComunicacion = new ENTMedioComunicacion();

            BPMedioComunicacion oBPMedioComunicacion = new BPMedioComunicacion();

            try
            {

                // Formulario
                oENTMedioComunicacion.MedioComunicacionId = 0;
                oENTMedioComunicacion.Nombre = "";
                oENTMedioComunicacion.Activo = 1;

                // Transacción
                oENTResponse = oBPMedioComunicacion.SelectMedioComunicacion(oENTMedioComunicacion);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlMedioComunicacion.DataTextField = "Nombre";
                this.ddlMedioComunicacion.DataValueField = "MedioComunicacionId";
                this.ddlMedioComunicacion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlMedioComunicacion.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectMedioTraslado(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTMedioTraslado oENTMedioTraslado = new ENTMedioTraslado();

            BPMedioTraslado oBPMedioTraslado = new BPMedioTraslado();

            try
            {

                // Formulario
                oENTMedioTraslado.MedioTrasladoId = 0;
                oENTMedioTraslado.Nombre = "";
                oENTMedioTraslado.Activo = 1;

                // Transacción
                oENTResponse = oBPMedioTraslado.SelectMedioTraslado(oENTMedioTraslado);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.chklMedioTraslado.DataTextField = "Nombre";
                this.chklMedioTraslado.DataValueField = "MedioTrasladoId";
                this.chklMedioTraslado.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.chklMedioTraslado.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectPropuestaAcomodo(){
            try
            {

                // Agregar Item de selección
                this.ddlPropuestaAcomodo.Items.Insert(0, new ListItem("Si", "1"));
                this.ddlPropuestaAcomodo.Items.Insert(0, new ListItem("No", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoAcomodo(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoAcomodo oENTTipoAcomodo = new ENTTipoAcomodo();

            BPTipoAcomodo oBPTipoAcomodo = new BPTipoAcomodo();

            try
            {

                // Formulario
                oENTTipoAcomodo.TipoAcomodoId = 0;
                oENTTipoAcomodo.Nombre = "";
                oENTTipoAcomodo.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoAcomodo.SelectTipoAcomodo(oENTTipoAcomodo);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoAcomodo.DataTextField = "Nombre";
                this.ddlTipoAcomodo.DataValueField = "TipoAcomodoId";
                this.ddlTipoAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoAcomodo.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectTipoVestimenta(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTTipoVestimenta oENTTipoVestimenta = new ENTTipoVestimenta();

            BPTipoVestimenta oBPTipoVestimenta = new BPTipoVestimenta();

            try
            {

                // Formulario
                oENTTipoVestimenta.TipoVestimentaId = 0;
                oENTTipoVestimenta.Nombre = "";
                oENTTipoVestimenta.Activo = 1;

                // Transacción
                oENTResponse = oBPTipoVestimenta.SelectTipoVestimenta(oENTTipoVestimenta);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlTipoVestimenta.DataTextField = "Nombre";
                this.ddlTipoVestimenta.DataValueField = "TipoVestimentaId";
                this.ddlTipoVestimenta.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlTipoVestimenta.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void UpdateEvento_Configuracion(){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            DataTable tblTemporal;
            DataRow rowTemporal;

            try
            {

                // Evento
                oENTEvento.EventoId = Int32.Parse( this.hddEventoId.Value );

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.RolId = oENTSession.RolId;

                #region "Sección: Nombre del evento"

                    // Formulario
                    oENTEvento.TipoVestimentaId = Int32.Parse(this.ddlTipoVestimenta.SelectedItem.Value);
                    oENTEvento.MedioComunicacionId = Int32.Parse(this.ddlMedioComunicacion.SelectedItem.Value);
                    oENTEvento.TipoVestimentaOtro = this.txtTipoVestimentaOtro.Text.Trim();
                    oENTEvento.PronosticoClima = this.txtPronostico.Text.Trim();
                    oENTEvento.TemperaturaMinima = this.txtTemperaturaMinima.Text.Trim();
                    oENTEvento.TemperaturaMaxima = this.txtTemperaturaMaxima.Text.Trim();
                    oENTEvento.Aforo = Int32.Parse(this.txtAforo.Text.Trim());
                    oENTEvento.TipoMontaje = this.txtTipoMontaje.Text.Trim();
                    oENTEvento.LugarArribo = this.txtLugarArribo.Text.Trim();
                    oENTEvento.Esposa = Int16.Parse( ( this.chkEsposaInvitada.Checked ? 1 : 0 ).ToString() );
                    oENTEvento.EsposaSi = Int16.Parse( ( this.rblConfirmacionEsposa.SelectedValue == "1" ? 1 : 0 ).ToString() );
                    oENTEvento.EsposaNo = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "2" ? 1 : 0).ToString());
                    oENTEvento.EsposaConfirma = Int16.Parse((this.rblConfirmacionEsposa.SelectedValue == "3" ? 1 : 0).ToString());
                    oENTEvento.AccionRealizar = this.txtAccionRealizar.Text.Trim();
                    oENTEvento.CaracteristicasInvitados = this.txtCaracteristicasInvitados.Text.Trim();
                    oENTEvento.Menu = this.txtMenu.Text.Trim();
                    oENTEvento.ProtocoloInvitacionA = "";
                    oENTEvento.ProtocoloResponsableEvento = "";
                    oENTEvento.ProtocoloBandera = "";
                    oENTEvento.ProtocoloLeyenda = "";
                    oENTEvento.ProtocoloResponsable = "";
                    oENTEvento.ProtocoloSonido = "";
                    oENTEvento.ProtocoloResponsableSonido = "";
                    oENTEvento.ProtocoloDesayuno = "";
                    oENTEvento.ProtocoloSillas = "";
                    oENTEvento.ProtocoloMesas = "";
                    oENTEvento.ProtocoloPresentacion = "";

                    // Medios de traslado seleccionados
                    oENTEvento.DataTableMedioTraslado = new DataTable("DataTableMedioTraslado");
                    oENTEvento.DataTableMedioTraslado.Columns.Add("MedioTrasladoId", typeof(Int32));
                    for (int k = 0; k < this.chklMedioTraslado.Items.Count; k++) {
					    if(this.chklMedioTraslado.Items[k].Selected){
                            rowTemporal = oENTEvento.DataTableMedioTraslado.NewRow();
                            rowTemporal["MedioTrasladoId"] = this.chklMedioTraslado.Items[k].Value;
                            oENTEvento.DataTableMedioTraslado.Rows.Add(rowTemporal);
					    }
				    }

                #endregion

                #region "Sección: Comité de Recepción en el Helipuerto Provisional"

                    oENTEvento.ComiteHelipuerto = Int16.Parse(this.ddlComiteHelipuerto.SelectedItem.Value);
                    oENTEvento.HelipuertoLugar = this.txtComiteHelipuertoLugar.Text.Trim();
                    oENTEvento.HelipuertoDomicilio = this.txtComiteHelipuertoDomicilio.Text.Trim();
                    oENTEvento.HelipuertoCoordenadas = this.txtComiteHelipuertoCoordenadas.Text.Trim();

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);
                    
                    oENTEvento.DataTableComiteHelipuerto = new DataTable("DataTableComiteHelipuerto");
                    oENTEvento.DataTableComiteHelipuerto.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableComiteHelipuerto.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableComiteHelipuerto.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteHelipuerto in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableComiteHelipuerto.NewRow();
                        rowTemporal["Orden"] = rowComiteHelipuerto["Orden"];
                        rowTemporal["Nombre"] = rowComiteHelipuerto["Nombre"];
                        rowTemporal["Puesto"] = rowComiteHelipuerto["Puesto"];
                        oENTEvento.DataTableComiteHelipuerto.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Comité de recepción"
                    
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);
                    
                    oENTEvento.DataTableComiteRecepcion = new DataTable("DataTableComiteRecepcion");
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Puesto", typeof(String));
                    oENTEvento.DataTableComiteRecepcion.Columns.Add("Separador", typeof(Int16));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableComiteRecepcion.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        rowTemporal["Separador"] = "0";
                        oENTEvento.DataTableComiteRecepcion.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Orden del día"
                    
                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvOrdenDia, true);
                    
                    oENTEvento.DataTableOrdenDia = new DataTable("DataTableOrdenDia");
                    oENTEvento.DataTableOrdenDia.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableOrdenDia.Columns.Add("Detalle", typeof(String));
                    oENTEvento.DataTableOrdenDia.Columns.Add("Columna3", typeof(String)); // El DataType es de 3 columnas
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableOrdenDia.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Detalle"] = rowComiteRecepcion["Detalle"];
                        rowTemporal["Columna3"] = "";
                        oENTEvento.DataTableOrdenDia.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Acomodo"
                    
                    oENTEvento.TipoAcomodoId = Int32.Parse(this.ddlTipoAcomodo.SelectedItem.Value);
                    oENTEvento.PropuestaAcomodo = Int16.Parse(this.ddlPropuestaAcomodo.SelectedItem.Value);
                    oENTEvento.AcomodoObservaciones = this.txtAcomodoObservaciones.Text.Trim();

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvAcomodo, true);
                    
                    oENTEvento.DataTableAcomodo = new DataTable("DataTableAcomodo");
                    oENTEvento.DataTableAcomodo.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableAcomodo.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableAcomodo.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowComiteRecepcion in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableAcomodo.NewRow();
                        rowTemporal["Orden"] = rowComiteRecepcion["Orden"];
                        rowTemporal["Nombre"] = rowComiteRecepcion["Nombre"];
                        rowTemporal["Puesto"] = rowComiteRecepcion["Puesto"];
                        oENTEvento.DataTableAcomodo.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Listado adicional"

                    oENTEvento.ListadoAdicional = Int16.Parse(this.ddlListadoAdicional.SelectedItem.Value);
                    oENTEvento.ListadoAdicionalTitulo = ""; // this.txtListadoAdicionalTitulo.Text.Trim();

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvListadoAdicional, true);

                    oENTEvento.DataTableListadoAdicional = new DataTable("DataTableListadoAdicional");
                    oENTEvento.DataTableListadoAdicional.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableListadoAdicional.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableListadoAdicional.Columns.Add("Puesto", typeof(String));
                    oENTEvento.DataTableListadoAdicional.Columns.Add("Separador", typeof(Int16));
                    
                    foreach( DataRow rowComiteListadoAdicional in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableListadoAdicional.NewRow();
                        rowTemporal["Orden"] = rowComiteListadoAdicional["Orden"];
                        rowTemporal["Nombre"] = rowComiteListadoAdicional["Nombre"];
                        rowTemporal["Puesto"] = rowComiteListadoAdicional["Puesto"];
                        rowTemporal["Separador"] = rowComiteListadoAdicional["Separador"];
                        oENTEvento.DataTableListadoAdicional.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Responsable del evento"

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);
                    
                    oENTEvento.DataTableResponsable = new DataTable("DataTableResponsable");
                    oENTEvento.DataTableResponsable.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableResponsable.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableResponsable.Columns.Add("Puesto", typeof(String));
                    
                    foreach( DataRow rowResponsable in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableResponsable.NewRow();
                        rowTemporal["Orden"] = rowResponsable["Orden"];
                        rowTemporal["Nombre"] = rowResponsable["Nombre"];
                        rowTemporal["Puesto"] = rowResponsable["Puesto"];
                        oENTEvento.DataTableResponsable.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Responsable de logística"

                    tblTemporal = null;
                    tblTemporal = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);
                    
                    oENTEvento.DataTableResponsableLogistica = new DataTable("DataTableResponsableLogistica");
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Orden", typeof(Int32));
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Nombre", typeof(String));
                    oENTEvento.DataTableResponsableLogistica.Columns.Add("Contacto", typeof(String));
                    
                    foreach( DataRow rowResponsableLogistica in tblTemporal.Rows ){

                        rowTemporal = oENTEvento.DataTableResponsableLogistica.NewRow();
                        rowTemporal["Orden"] = rowResponsableLogistica["Orden"];
                        rowTemporal["Nombre"] = rowResponsableLogistica["Nombre"];
                        rowTemporal["Contacto"] = rowResponsableLogistica["Contacto"];
                        oENTEvento.DataTableResponsableLogistica.Rows.Add(rowTemporal);
                    }

                #endregion

                #region "Sección: Nota en Documento"

                    oENTEvento.NotaInicioDocumento = Int16.Parse((rblNotaDocumento.SelectedIndex == 0 ? 0 : (rblNotaDocumento.SelectedIndex == 1 ? 1 : 0)).ToString());
                    oENTEvento.NotaFinDocumento = Int16.Parse((rblNotaDocumento.SelectedIndex == 0 ? 0 : (rblNotaDocumento.SelectedIndex == 1 ? 0 : 1)).ToString());
                    oENTEvento.NotaDocumento = this.txtNotaDocumento.Text.Trim();

                #endregion


                // Transacción
                oENTResponse = oBPEvento.UpdateEvento_Configuracion(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

            }catch (Exception ex){
                throw (ex);
            }
        }



        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            String Key = "";

			try
            {

				// Validaciones de llamada
				if (Page.IsPostBack) { return; }
                if (this.Request.QueryString["key"] == null) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

				// Validaciones de parámetros
				Key = GetKey(this.Request.QueryString["key"].ToString());
				if (Key == "") { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }
				if (Key.ToString().Split(new Char[] { '|' }).Length != 2) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

                // Obtener EventoId
                this.hddEventoId.Value = Key.ToString().Split(new Char[] { '|' })[0];

				// Obtener Sender
                this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

                // Llenado de controles
                SelectTipoVestimenta();
                SelectMedioTraslado();
                SelectMedioComunicacion();
                SelectTipoAcomodo();
                SelectPropuestaAcomodo();
                SelectComiteHelipuerto();
                SelectListadoAdicional();
                
				// Carátula y formulario
                SelectEvento();

                // Limpiar popups
                ClearPopUp_ComiteHelipuertoPanel();
                ClearPopUp_ComiteRecepcionPanel();
                ClearPopUp_OrdenDiaPanel();
                ClearPopUp_AcomodoPanel();
                ClearPopUp_ListadoAdicionalPanel();
                ClearPopUp_ResponsableEventoPanel();
                ClearPopUp_ResponsableLogisticaPanel();

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPronostico.ClientID + "'); }", true);

            }catch (Exception ex){
                this.btnActualizar.Enabled = false;
                this.btnActualizar.CssClass = "Button_General_Disabled";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPronostico.ClientID + "'); }", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

                // Validar formulario
                if ( !ValidaFormulario() ) { return; }

                // Actualizar los datos generales
                UpdateEvento_Configuracion();

				// Regresar
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPronostico.ClientID + "'); }", true);
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
			String sKey = "";

			try
            {

				// Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
				sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDetalleEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPronostico.ClientID + "'); }", true);
            }
		}

        protected void chkEsposaInvitada_CheckedChanged(object sender, EventArgs e){
            try
            {

                if( this.chkEsposaInvitada.Checked ){

                    this.rblConfirmacionEsposa.Enabled = true;
                    this.rblConfirmacionEsposa.Items[0].Selected = true;
                }else{

                    this.rblConfirmacionEsposa.Enabled = false;
                    this.rblConfirmacionEsposa.ClearSelection();
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPronostico.ClientID + "'); }", true);
            }
        }

        protected void ddlTipoVestimenta_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                switch( this.ddlTipoVestimenta.SelectedItem.Value ){
                    case "6":   // Otro
                        
                        this.txtTipoVestimentaOtro.Enabled = true;
                        this.txtTipoVestimentaOtro.CssClass = "Textbox_General";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtTipoVestimentaOtro.ClientID + "'); }", true);
                        break;

                    default:

                        this.txtTipoVestimentaOtro.Text = "";
                        this.txtTipoVestimentaOtro.Enabled = false;
                        this.txtTipoVestimentaOtro.CssClass = "Textbox_Disabled";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlMedioComunicacion.ClientID + "'); }", true);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtPronostico.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Comité de Recepción en el Helipuerto Provisional

        protected void btnAgregarComiteHelipuerto_Click(object sender, EventArgs e){
            DataTable tblComiteHelipuerto;
            DataRow rowComiteHelipuerto;

            try
            {

                // Obtener DataTable del grid
                tblComiteHelipuerto = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, false);

                // Validaciones
                if ( this.txtComiteHelipuertoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                // if ( this.txtComiteHelipuertoPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowComiteHelipuerto = tblComiteHelipuerto.NewRow();
                rowComiteHelipuerto["Orden"] = (tblComiteHelipuerto.Rows.Count + 1).ToString();
                rowComiteHelipuerto["Nombre"] = this.txtComiteHelipuertoNombre.Text.Trim();
                rowComiteHelipuerto["Puesto"] = this.txtComiteHelipuertoPuesto.Text.Trim();
                tblComiteHelipuerto.Rows.Add(rowComiteHelipuerto);

                // Actualizar Grid
                this.gvComiteHelipuerto.DataSource = tblComiteHelipuerto;
                this.gvComiteHelipuerto.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvComiteHelipuerto, ref this.lblComiteHelipuerto);

                // Nueva captura
                this.txtComiteHelipuertoNombre.Text = "";
                this.txtComiteHelipuertoPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteHelipuerto_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblComiteHelipuerto;

            String strCommand = "";
            Int32 intRow = 0;

            String Orden = "";
            String Nombre = "";
            String Puesto = "";

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el evento RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                Orden = this.gvComiteHelipuerto.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvComiteHelipuerto.DataKeys[intRow]["Nombre"].ToString();
                Puesto = this.gvComiteHelipuerto.DataKeys[intRow]["Puesto"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_ComiteHelipuertoPanel(Orden, Nombre, Puesto);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblComiteHelipuerto = gcParse.GridViewToDataTable(this.gvComiteHelipuerto, true);

                        // Remover el elemento
                        tblComiteHelipuerto.Rows.Remove( tblComiteHelipuerto.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowComiteHelipuerto in tblComiteHelipuerto.Rows ){

                            tblComiteHelipuerto.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvComiteHelipuerto.DataSource = tblComiteHelipuerto;
                        this.gvComiteHelipuerto.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvComiteHelipuerto, ref this.lblComiteHelipuerto);

                        // Nueva captura
                        this.txtComiteHelipuertoNombre.Text = "";
                        this.txtComiteHelipuertoPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteHelipuerto_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String ComiteHelipuertoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvComiteHelipuerto.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ComiteHelipuertoNombre = this.gvComiteHelipuerto.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ComiteHelipuertoNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                sTootlTip = "Editar a [" + ComiteHelipuertoNombre + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvComiteHelipuerto_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvComiteHelipuerto, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Comité de recepción

        protected void btnAgregarComiteRecepcion_Click(object sender, EventArgs e){
            DataTable tblComiteRecepcion;
            DataRow rowComiteRecepcion;

            try
            {

                // Obtener DataTable del grid
                tblComiteRecepcion = gcParse.GridViewToDataTable(this.gvComiteRecepcion, false);

                // Validaciones
                if ( this.txtComiteRecepcionNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtComiteRecepcionPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowComiteRecepcion = tblComiteRecepcion.NewRow();
                rowComiteRecepcion["Orden"] = (tblComiteRecepcion.Rows.Count + 1).ToString();
                rowComiteRecepcion["Nombre"] = this.txtComiteRecepcionNombre.Text.Trim();
                rowComiteRecepcion["Puesto"] = this.txtComiteRecepcionPuesto.Text.Trim();
                tblComiteRecepcion.Rows.Add(rowComiteRecepcion);

                // Actualizar Grid
                this.gvComiteRecepcion.DataSource = tblComiteRecepcion;
                this.gvComiteRecepcion.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvComiteRecepcion, ref this.lblComiteRecepcion);

                // Nueva captura
                this.txtComiteRecepcionNombre.Text = "";
                this.txtComiteRecepcionPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteRecepcion_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblComiteRecepcion;

            String strCommand = "";
            String Orden = "";
            String Nombre = "";
            String Puesto = "";
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
                Orden = this.gvComiteRecepcion.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvComiteRecepcion.DataKeys[intRow]["Nombre"].ToString();
                Puesto = this.gvComiteRecepcion.DataKeys[intRow]["Puesto"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_ComiteRecepcionPanel(Orden, Nombre, Puesto);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblComiteRecepcion = gcParse.GridViewToDataTable(this.gvComiteRecepcion, true);

                        // Remover el elemento
                        tblComiteRecepcion.Rows.Remove( tblComiteRecepcion.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowComiteRecepcion in tblComiteRecepcion.Rows ){

                            tblComiteRecepcion.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvComiteRecepcion.DataSource = tblComiteRecepcion;
                        this.gvComiteRecepcion.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvComiteRecepcion, ref this.lblComiteRecepcion);

                        // Nueva captura
                        this.txtComiteRecepcionNombre.Text = "";
                        this.txtComiteRecepcionPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }

        protected void gvComiteRecepcion_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String ComiteRecepcionNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvComiteRecepcion.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ComiteRecepcionNombre = this.gvComiteRecepcion.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ComiteRecepcionNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvComiteRecepcion_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvComiteRecepcion, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Orden del día

        protected void btnAgregarOrdenDia_Click(object sender, EventArgs e){
            DataTable tblOrdenDia;
            DataRow rowOrdenDia;

            try
            {

                // Obtener DataTable del grid
                tblOrdenDia = gcParse.GridViewToDataTable(this.gvOrdenDia, false);

                // Validaciones
                if (this.txtOrdenDiaDetalle.Text.Trim() == "") { throw (new Exception("Es necesario ingresar el detalle de la orden del día")); }

                // Agregar un nuevo elemento
                rowOrdenDia = tblOrdenDia.NewRow();
                rowOrdenDia["Orden"] = (tblOrdenDia.Rows.Count + 1).ToString();
                rowOrdenDia["Detalle"] = this.txtOrdenDiaDetalle.Text.Trim();
                tblOrdenDia.Rows.Add(rowOrdenDia);

                // Actualizar Grid
                this.gvOrdenDia.DataSource = tblOrdenDia;
                this.gvOrdenDia.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvOrdenDia, ref this.lblOrdenDia);

                // Nueva captura
                this.txtOrdenDiaDetalle.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }

        protected void gvOrdenDia_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblOrdenDia;

            String strCommand = "";
            String Orden = "";
            String Detalle = "";
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
                Orden = this.gvOrdenDia.DataKeys[intRow]["Orden"].ToString();
                Detalle = this.gvOrdenDia.DataKeys[intRow]["Detalle"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_OrdenDiaPanel(Orden, Detalle);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblOrdenDia = gcParse.GridViewToDataTable(this.gvOrdenDia, true);

                        // Remover el elemento
                        tblOrdenDia.Rows.Remove( tblOrdenDia.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowOrdenDia in tblOrdenDia.Rows ){

                            tblOrdenDia.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvOrdenDia.DataSource = tblOrdenDia;
                        this.gvOrdenDia.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvOrdenDia, ref this.lblOrdenDia);

                        // Nueva captura
                        this.txtOrdenDiaDetalle.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }

        protected void gvOrdenDia_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

                // Datakeys
                Orden = this.gvOrdenDia.DataKeys[e.Row.RowIndex]["Orden"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar la posición [" + Orden + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvOrdenDia_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvOrdenDia, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Acomodo

        protected void btnAgregarAcomodo_Click(object sender, EventArgs e){
            DataTable tblAcomodo;
            DataRow rowAcomodo;

            try
            {

                // Obtener DataTable del grid
                tblAcomodo = gcParse.GridViewToDataTable(this.gvAcomodo, false);

                // Validaciones
                if ( this.txtAcomodoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtAcomodoPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowAcomodo = tblAcomodo.NewRow();
                rowAcomodo["Orden"] = (tblAcomodo.Rows.Count + 1).ToString();
                rowAcomodo["Nombre"] = this.txtAcomodoNombre.Text.Trim();
                rowAcomodo["Puesto"] = this.txtAcomodoPuesto.Text.Trim();
                tblAcomodo.Rows.Add(rowAcomodo);

                // Actualizar Grid
                this.gvAcomodo.DataSource = tblAcomodo;
                this.gvAcomodo.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvAcomodo, ref this.lblAcomodo);

                // Nueva captura
                this.txtAcomodoNombre.Text = "";
                this.txtAcomodoPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvAcomodo_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblAcomodo;

            String strCommand = "";
            String Orden = "";
            String Nombre = "";
            String Puesto = "";
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
                Orden = this.gvAcomodo.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvAcomodo.DataKeys[intRow]["Nombre"].ToString();
                Puesto = this.gvAcomodo.DataKeys[intRow]["Puesto"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_AcomodoPanel(Orden, Nombre, Puesto);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblAcomodo = gcParse.GridViewToDataTable(this.gvAcomodo, true);

                        // Remover el elemento
                        tblAcomodo.Rows.Remove( tblAcomodo.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowAcomodo in tblAcomodo.Rows ){

                            tblAcomodo.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvAcomodo.DataSource = tblAcomodo;
                        this.gvAcomodo.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvAcomodo, ref this.lblAcomodo);

                        // Nueva captura
                        this.txtAcomodoNombre.Text = "";
                        this.txtAcomodoPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvAcomodo_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String AcomodoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                AcomodoNombre = this.gvAcomodo.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + AcomodoNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvAcomodo_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvAcomodo, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Imagen de Montaje

        protected void btnAgregarImagenMontaje_Click(object sender, EventArgs e){
            try
            {

                this.lblImagenMontaje.Text = "";
                SaveGridState();
                InsertDocumento();
                RecoveryGridState();
                this.rblTipoDocumento.SelectedIndex = 0;

            }catch (Exception ex){
                this.lblImagenMontaje.Text = ex.Message;
                this.rblTipoDocumento.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnAgregarImagenMontaje.ClientID + "'); }", true);
            }
        }

        protected void gvImagenMontaje_RowCommand(object sender, GridViewCommandEventArgs e){
			String CommandName = "";
			String DocumentoId = "";
			String sKey = "";

			Int32 iRow = 0;

            try
            {
                // Opción seleccionada
                CommandName = e.CommandName.ToString();

                // Se dispara el evento RowCommand en el ordenamiento
                if (CommandName == "Sort") { return; }

                // Fila
                iRow = Convert.ToInt32(e.CommandArgument.ToString());

                // DataKeys
                DocumentoId = gvImagenMontaje.DataKeys[iRow]["DocumentoId"].ToString();

                // Acción
                switch (CommandName){
                    case "Visualizar":

						sKey = gcEncryption.EncryptString(DocumentoId, true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "window.open('" + ResolveUrl("~/Include/Handler/Documento.ashx") + "?key=" + sKey + "');", true);
                        break;

                    case "Borrar":
                        SaveGridState();
                        DeleteDocumento(Int32.Parse(DocumentoId));
                        RecoveryGridState();
                        this.rblTipoDocumento.SelectedIndex = 0;
                        break;
                }

            }catch (Exception ex){
                this.lblImagenMontaje.Text = ex.Message;
                this.rblTipoDocumento.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.btnAgregarImagenMontaje.ClientID + "'); }", true);
            }
		}

		protected void gvImagenMontaje_RowDataBound(object sender, GridViewRowEventArgs e){
			ImageButton imgView = null;
			ImageButton imgDelete = null;

			String DocumentoId = "";
            String ModuloId = "";
            String UsuarioId = "";
			String NombreDocumento = "";
			String Icono = "";

			String sImagesAttributes = "";
			String sToolTip = "";

			try
			{
				
				// Validación de que sea fila 
				if (e.Row.RowType != DataControlRowType.DataRow) { return; }

				// Obtener objetos
				imgView = (ImageButton)e.Row.FindControl("imgView");
				imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

				// Datakeys
				DocumentoId = this.gvImagenMontaje.DataKeys[e.Row.RowIndex]["DocumentoId"].ToString();
                ModuloId = this.gvImagenMontaje.DataKeys[e.Row.RowIndex]["ModuloId"].ToString();
                UsuarioId = this.gvImagenMontaje.DataKeys[e.Row.RowIndex]["UsuarioId"].ToString();
				Icono = this.gvImagenMontaje.DataKeys[e.Row.RowIndex]["Icono"].ToString();
				NombreDocumento = this.gvImagenMontaje.DataKeys[e.Row.RowIndex]["NombreDocumento"].ToString();

				// ToolTip Visualizar
				sToolTip = "Visualizar [" + NombreDocumento + "]";
                imgView.Attributes.Add("title", sToolTip);
				imgView.Attributes.Add("style", "cursor:hand;");
				imgView.ImageUrl = "~/Include/Image/File/" + Icono;

                // Tooltip Eliminar
                sToolTip = "Eliminar [" + NombreDocumento + "]";
                imgDelete.Attributes.Add("title", sToolTip);
                imgDelete.Attributes.Add("style", "cursor:hand;");

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                //sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                //sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

			}catch (Exception ex){
				throw (ex);
			}
		}

		protected void gvImagenMontaje_Sorting(object sender, GridViewSortEventArgs e){
			try
			{

				gcCommon.SortGridView(ref this.gvImagenMontaje, ref this.hddSort, e.SortExpression);

			}catch (Exception ex){
                this.lblImagenMontaje.Text = ex.Message;
			}
		}



        // Eventos de Sección: Listado adicional

        protected void btnAgregarListadoAdicional_Click(object sender, EventArgs e){
            DataTable tblListadoAdicional;
            DataRow rowListadoAdicional;

            try
            {

                // Obtener DataTable del grid
                tblListadoAdicional = gcParse.GridViewToDataTable(this.gvListadoAdicional, false);

                // Validaciones
                if ( this.txtListadoAdicionalNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtListadoAdicionalPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowListadoAdicional = tblListadoAdicional.NewRow();
                rowListadoAdicional["Orden"] = (tblListadoAdicional.Rows.Count + 1).ToString();
                rowListadoAdicional["Nombre"] = this.txtListadoAdicionalNombre.Text.Trim();
                rowListadoAdicional["Puesto"] = this.txtListadoAdicionalPuesto.Text.Trim();
                rowListadoAdicional["Separador"] = "0";
                tblListadoAdicional.Rows.Add(rowListadoAdicional);

                // Actualizar Grid
                this.gvListadoAdicional.DataSource = tblListadoAdicional;
                this.gvListadoAdicional.DataBind();

                // Modificar manualmente el orden
                ReorderGrid_ListadoAdicional();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvListadoAdicional, ref this.lblListadoAdicional);

                // Nueva captura
                this.txtListadoAdicionalNombre.Text = "";
                this.txtListadoAdicionalPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);
            }
        }

        protected void btnAgregarListadoAdicional_Separador_Click(object sender, EventArgs e){
            DataTable tblListadoAdicional;
            DataRow rowListadoAdicional;

            try
            {

                // Obtener DataTable del grid
                tblListadoAdicional = gcParse.GridViewToDataTable(this.gvListadoAdicional, false);

                // Validaciones
                if ( this.txtListadoAdicionalNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtListadoAdicionalPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowListadoAdicional = tblListadoAdicional.NewRow();
                rowListadoAdicional["Orden"] = (tblListadoAdicional.Rows.Count + 1).ToString();
                rowListadoAdicional["Nombre"] = this.txtListadoAdicionalNombre.Text.Trim();
                rowListadoAdicional["Puesto"] = this.txtListadoAdicionalPuesto.Text.Trim();
                rowListadoAdicional["Separador"] = "1";
                tblListadoAdicional.Rows.Add(rowListadoAdicional);

                // Actualizar Grid
                this.gvListadoAdicional.DataSource = tblListadoAdicional;
                this.gvListadoAdicional.DataBind();

                // Modificar manualmente el orden
                ReorderGrid_ListadoAdicional();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvListadoAdicional, ref this.lblListadoAdicional);

                // Nueva captura
                this.txtListadoAdicionalNombre.Text = "";
                this.txtListadoAdicionalPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);
            }
        }

        protected void gvListadoAdicional_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblListadoAdicional;

            String strCommand = "";
            String Orden = "";
            String Nombre = "";
            String Puesto = "";
            String Separador = "";
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
                Orden = this.gvListadoAdicional.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvListadoAdicional.DataKeys[intRow]["Nombre"].ToString();
                Puesto = this.gvListadoAdicional.DataKeys[intRow]["Puesto"].ToString();
                Separador = this.gvListadoAdicional.DataKeys[intRow]["Separador"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_ListadoAdicionalPanel(Orden, Nombre, Puesto, Separador);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblListadoAdicional = gcParse.GridViewToDataTable(this.gvListadoAdicional, true);

                        // Remover el elemento
                        tblListadoAdicional.Rows.Remove( tblListadoAdicional.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowListadoAdicional in tblListadoAdicional.Rows ){

                            tblListadoAdicional.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvListadoAdicional.DataSource = tblListadoAdicional;
                        this.gvListadoAdicional.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvListadoAdicional, ref this.lblListadoAdicional);

                        // Nueva captura
                        this.txtListadoAdicionalNombre.Text = "";
                        this.txtListadoAdicionalPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);
            }
        }

        protected void gvListadoAdicional_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String ListadoAdicionalNombre = "";
            String Separador = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvListadoAdicional.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ListadoAdicionalNombre = this.gvListadoAdicional.DataKeys[e.Row.RowIndex]["Nombre"].ToString();
                Separador = this.gvListadoAdicional.DataKeys[e.Row.RowIndex]["Separador"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ListadoAdicionalNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Configuración de la fila
                if ( Separador == "1" ){

                    // Dejar sólo una fila
                    e.Row.Cells[1].Text = e.Row.Cells[2].Text;
                    e.Row.Cells[1].ColumnSpan = 3;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    // Atributos de la fila
                    e.Row.CssClass = "Grid_Row_Over_PopUp";
                    imgDelete.ImageUrl = "~/Include/Image/Buttons/Delete_Over.png";
                    imgEdit.ImageUrl = "~/Include/Image/Buttons/Edit_Over.png";

                }else{

                    // Atributos Over
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                    sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                    e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                    // Atributos Out
                    sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                    sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                    e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);
                }

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvListadoAdicional_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvListadoAdicional, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Responsable Evento

        protected void btnAgregarResponsableEvento_Click(object sender, EventArgs e){
            DataTable tblResponsableEvento;
            DataRow rowResponsableEvento;

            try
            {

                // Obtener DataTable del grid
                tblResponsableEvento = gcParse.GridViewToDataTable(this.gvResponsableEvento, false);

                // Validaciones
                if ( this.txtResponsableEventoNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtResponsableEventoPuesto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowResponsableEvento = tblResponsableEvento.NewRow();
                rowResponsableEvento["Orden"] = (tblResponsableEvento.Rows.Count + 1).ToString();
                rowResponsableEvento["Nombre"] = this.txtResponsableEventoNombre.Text.Trim();
                rowResponsableEvento["Puesto"] = this.txtResponsableEventoPuesto.Text.Trim();
                tblResponsableEvento.Rows.Add(rowResponsableEvento);

                // Actualizar Grid
                this.gvResponsableEvento.DataSource = tblResponsableEvento;
                this.gvResponsableEvento.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvResponsableEvento, ref this.lblResponsableEvento);

                // Nueva captura
                this.txtResponsableEventoNombre.Text = "";
                this.txtResponsableEventoPuesto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblResponsableEvento;

            String strCommand = "";
            String Orden = "";
            String Nombre = "";
            String Puesto = "";
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
                Orden = this.gvResponsableEvento.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvResponsableEvento.DataKeys[intRow]["Nombre"].ToString();
                Puesto = this.gvResponsableEvento.DataKeys[intRow]["Puesto"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_ResponsableEventoPanel(Orden, Nombre, Puesto);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblResponsableEvento = gcParse.GridViewToDataTable(this.gvResponsableEvento, true);

                        // Remover el elemento
                        tblResponsableEvento.Rows.Remove( tblResponsableEvento.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowResponsableEvento in tblResponsableEvento.Rows ){

                            tblResponsableEvento.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvResponsableEvento.DataSource = tblResponsableEvento;
                        this.gvResponsableEvento.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvResponsableEvento, ref this.lblResponsableEvento);

                        // Nueva captura
                        this.txtResponsableEventoNombre.Text = "";
                        this.txtResponsableEventoPuesto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String ResponsableEventoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvResponsableEvento.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ResponsableEventoNombre = this.gvResponsableEvento.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ResponsableEventoNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvResponsableEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvResponsableEvento, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);
            }
        }



        // Eventos de Sección: Responsable Evento Logística

        protected void btnAgregarResponsableLogistica_Click(object sender, EventArgs e){
            DataTable tblResponsableLogistica;
            DataRow rowResponsableLogistica;

            try
            {

                // Obtener DataTable del grid
                tblResponsableLogistica = gcParse.GridViewToDataTable(this.gvResponsableLogistica, false);

                // Validaciones
                if ( this.txtResponsableLogisticaNombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }
                //if ( this.txtResponsableLogisticaContacto.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un puesto")); }

                // Agregar un nuevo elemento
                rowResponsableLogistica = tblResponsableLogistica.NewRow();
                rowResponsableLogistica["Orden"] = (tblResponsableLogistica.Rows.Count + 1).ToString();
                rowResponsableLogistica["Nombre"] = this.txtResponsableLogisticaNombre.Text.Trim();
                rowResponsableLogistica["Contacto"] = this.txtResponsableLogisticaContacto.Text.Trim();
                tblResponsableLogistica.Rows.Add(rowResponsableLogistica);

                // Actualizar Grid
                this.gvResponsableLogistica.DataSource = tblResponsableLogistica;
                this.gvResponsableLogistica.DataBind();

                // Inhabilitar edición
                InhabilitarEdicion(ref this.gvResponsableLogistica, ref this.lblResponsableLogistica);

                // Nueva captura
                this.txtResponsableLogisticaNombre.Text = "";
                this.txtResponsableLogisticaContacto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableLogistica_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable tblResponsableLogistica;

            String strCommand = "";
            String Orden = "";
            String Nombre = "";
            String Contacto = "";
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
                Orden = this.gvResponsableLogistica.DataKeys[intRow]["Orden"].ToString();
                Nombre = this.gvResponsableLogistica.DataKeys[intRow]["Nombre"].ToString();
                Contacto = this.gvResponsableLogistica.DataKeys[intRow]["Contacto"].ToString();

                // Acción
                switch (strCommand){

                    case "Editar":

                        // PopUp de Editar
                        SetPopUp_ResponsableLogisticaPanel(Orden, Nombre, Contacto);
                        break;

                    case "Eliminar":

                        // Obtener DataTable del grid
                        tblResponsableLogistica = gcParse.GridViewToDataTable(this.gvResponsableLogistica, true);

                        // Remover el elemento
                        tblResponsableLogistica.Rows.Remove( tblResponsableLogistica.Select("Orden=" + Orden )[0] );

                        // Reordenar los Items restantes
                        intRow = 0;
                        foreach( DataRow rowResponsableLogistica in tblResponsableLogistica.Rows ){

                            tblResponsableLogistica.Rows[intRow]["Orden"] = (intRow + 1);
                            intRow = intRow + 1;
                        }

                        // Actualizar Grid
                        this.gvResponsableLogistica.DataSource = tblResponsableLogistica;
                        this.gvResponsableLogistica.DataBind();

                        // Inhabilitar edición
                        InhabilitarEdicion(ref this.gvResponsableLogistica, ref this.lblResponsableLogistica);

                        // Nueva captura
                        this.txtResponsableLogisticaNombre.Text = "";
                        this.txtResponsableLogisticaContacto.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);

                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }

        protected void gvResponsableLogistica_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgDelete = null;
            ImageButton imgEdit = null;

            String Orden = "";
            String ResponsableLogisticaNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                Orden = this.gvResponsableLogistica.DataKeys[e.Row.RowIndex]["Orden"].ToString();
                ResponsableLogisticaNombre = this.gvResponsableLogistica.DataKeys[e.Row.RowIndex]["Nombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Eliminar a [" + ResponsableLogisticaNombre + "]";
                imgDelete.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete_Over.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over_PopUp'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgDelete.ClientID + "').src='../../../../Include/Image/Buttons/Delete.png';";
                sImagesAttributes = sImagesAttributes + " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='Grid_Row_PopUp'; " + sImagesAttributes);

                // Configurar columna
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#675C9D");

            }catch (Exception ex){
                throw (ex);
            }

        }

        protected void gvResponsableLogistica_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvResponsableLogistica, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);
            }
        }


        #region PopUp - Comite Helipuerto
            
            
            // Rutinas

            void ClearPopUp_ComiteHelipuertoPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_ComiteHelipuerto.Visible = false;
                    this.lblPopUp_ComiteHelipuertoTitle.Text = "";
                    this.btnPopUp_ComiteHelipuertoCommand.Text = "";
                    this.lblPopUp_ComiteHelipuertoMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ComiteHelipuertoPanel(String Orden, String Nombre, String Puesto){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ComiteHelipuerto.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_ComiteHelipuertoTitle.Text = "Edición de elemento";
                    this.btnPopUp_ComiteHelipuertoCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpComiteHelipuerto_OrdenAnterior.Text = Orden;
                    this.txtPopUpComiteHelipuerto_Orden.Text = Orden;
                    this.txtPopUpComiteHelipuerto_Nombre.Text = Nombre;
                    this.txtPopUpComiteHelipuerto_Puesto.Text = Puesto;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpComiteHelipuerto_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ComiteHelipuerto(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpComiteHelipuerto_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpComiteHelipuerto_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpComiteHelipuerto_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpComiteHelipuerto_Puesto.Text.Trim();

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoComiteHelipuerto_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ComiteHelipuertoPanel();

                    // Actualizar listado
                    this.gvComiteHelipuerto.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvComiteHelipuerto.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteHelipuertoNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ComiteHelipuertoCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpComiteHelipuerto_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpComiteHelipuerto_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpComiteHelipuerto_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpComiteHelipuerto_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_ComiteHelipuerto();

                }catch (Exception ex){
                    this.lblPopUp_ComiteHelipuertoMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpComiteHelipuerto_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ComiteHelipuerto_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ComiteHelipuertoPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - Comite Recepcion
            
            
            // Rutinas

            void ClearPopUp_ComiteRecepcionPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_ComiteRecepcion.Visible = false;
                    this.lblPopUp_ComiteRecepcionTitle.Text = "";
                    this.btnPopUp_ComiteRecepcionCommand.Text = "";
                    this.lblPopUp_ComiteRecepcionMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ComiteRecepcionPanel(String Orden, String Nombre, String Puesto){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ComiteRecepcion.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_ComiteRecepcionTitle.Text = "Edición de elemento";
                    this.btnPopUp_ComiteRecepcionCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpComiteRecepcion_OrdenAnterior.Text = Orden;
                    this.txtPopUpComiteRecepcion_Orden.Text = Orden;
                    this.txtPopUpComiteRecepcion_Nombre.Text = Nombre;
                    this.txtPopUpComiteRecepcion_Puesto.Text = Puesto;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpComiteRecepcion_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ComiteRecepcion(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpComiteRecepcion_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpComiteRecepcion_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpComiteRecepcion_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpComiteRecepcion_Puesto.Text.Trim();
                    oENTEvento.Separador = 0;

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoComiteRecepcion_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ComiteRecepcionPanel();

                    // Actualizar listado
                    this.gvComiteRecepcion.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvComiteRecepcion.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtComiteRecepcionNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ComiteRecepcionCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpComiteRecepcion_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpComiteRecepcion_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpComiteRecepcion_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpComiteRecepcion_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_ComiteRecepcion();

                }catch (Exception ex){
                    this.lblPopUp_ComiteRecepcionMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpComiteRecepcion_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ComiteRecepcion_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ComiteRecepcionPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - Orden Dia
            
            
            // Rutinas

            void ClearPopUp_OrdenDiaPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_OrdenDia.Visible = false;
                    this.lblPopUp_OrdenDiaTitle.Text = "";
                    this.btnPopUp_OrdenDiaCommand.Text = "";
                    this.lblPopUp_OrdenDiaMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_OrdenDiaPanel(String Orden, String Detalle){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_OrdenDia.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_OrdenDiaTitle.Text = "Edición de elemento";
                    this.btnPopUp_OrdenDiaCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpOrdenDia_OrdenAnterior.Text = Orden;
                    this.txtPopUpOrdenDia_Orden.Text = Orden;
                    this.txtPopUpOrdenDia_Detalle.Text = Detalle;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpOrdenDia_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_OrdenDia(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpOrdenDia_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpOrdenDia_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpOrdenDia_Detalle.Text.Trim();

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoOrdenDia_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_OrdenDiaPanel();

                    // Actualizar listado
                    this.gvOrdenDia.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvOrdenDia.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtOrdenDiaDetalle.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_OrdenDiaCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpOrdenDia_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpOrdenDia_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpOrdenDia_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpOrdenDia_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar el detalle de la orden del día")); }

                    // Transacción
                    UpdateConfiguracion_OrdenDia();

                }catch (Exception ex){
                    this.lblPopUp_OrdenDiaMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpOrdenDia_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_OrdenDia_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_OrdenDiaPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - Acomodo
            
            
            // Rutinas

            void ClearPopUp_AcomodoPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_Acomodo.Visible = false;
                    this.lblPopUp_AcomodoTitle.Text = "";
                    this.btnPopUp_AcomodoCommand.Text = "";
                    this.lblPopUp_AcomodoMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_AcomodoPanel(String Orden, String Nombre, String Puesto){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_Acomodo.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_AcomodoTitle.Text = "Edición de elemento";
                    this.btnPopUp_AcomodoCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpAcomodo_OrdenAnterior.Text = Orden;
                    this.txtPopUpAcomodo_Orden.Text = Orden;
                    this.txtPopUpAcomodo_Nombre.Text = Nombre;
                    this.txtPopUpAcomodo_Puesto.Text = Puesto;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpAcomodo_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_Acomodo(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpAcomodo_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpAcomodo_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpAcomodo_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpAcomodo_Puesto.Text.Trim();

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoAcomodo_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_AcomodoPanel();

                    // Actualizar listado
                    this.gvAcomodo.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvAcomodo.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtAcomodoNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_AcomodoCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpAcomodo_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpAcomodo_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpAcomodo_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpAcomodo_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_Acomodo();

                }catch (Exception ex){
                    this.lblPopUp_AcomodoMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpAcomodo_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_Acomodo_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_AcomodoPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - Listado Adicional
            
            
            // Rutinas

            void ClearPopUp_ListadoAdicionalPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_ListadoAdicional.Visible = false;
                    this.lblPopUp_ListadoAdicionalTitle.Text = "";
                    this.btnPopUp_ListadoAdicionalCommand.Text = "";
                    this.lblPopUp_ListadoAdicionalMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ListadoAdicionalPanel(String Orden, String Nombre, String Puesto, String Separador){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ListadoAdicional.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_ListadoAdicionalTitle.Text = "Edición de elemento";
                    this.btnPopUp_ListadoAdicionalCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpListadoAdicional_OrdenAnterior.Text = Orden;
                    this.txtPopUpListadoAdicional_Orden.Text = Orden;
                    this.txtPopUpListadoAdicional_Nombre.Text = Nombre;

                    if ( Separador == "1" ) {
                        
                        this.txtPopUpListadoAdicional_Puesto.Text = "";
                        this.txtPopUpListadoAdicional_Puesto.Enabled = false;
                        this.txtPopUpListadoAdicional_Puesto.CssClass = "Textbox_General_Disabled";
                    }else{

                        this.txtPopUpListadoAdicional_Puesto.Text = Puesto;
                        this.txtPopUpListadoAdicional_Puesto.Enabled = true;
                        this.txtPopUpListadoAdicional_Puesto.CssClass = "Textbox_General";
                    }

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpListadoAdicional_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ListadoAdicional(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpListadoAdicional_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpListadoAdicional_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpListadoAdicional_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpListadoAdicional_Puesto.Text.Trim();
                    oENTEvento.Separador = Int16.Parse(this.txtPopUpListadoAdicional_Puesto.Enabled ? "0" : "1");

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoListadoAdicional_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ListadoAdicionalPanel();

                    // Actualizar listado
                    this.gvListadoAdicional.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvListadoAdicional.DataBind();

                    // Modificar manualmente el orden
                    ReorderGrid_ListadoAdicional();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtListadoAdicionalNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ListadoAdicionalCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpListadoAdicional_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpListadoAdicional_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpListadoAdicional_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpListadoAdicional_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_ListadoAdicional();

                }catch (Exception ex){
                    this.lblPopUp_ListadoAdicionalMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpListadoAdicional_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ListadoAdicional_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ListadoAdicionalPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - Responsable Evento
            
            
            // Rutinas

            void ClearPopUp_ResponsableEventoPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_ResponsableEvento.Visible = false;
                    this.lblPopUp_ResponsableEventoTitle.Text = "";
                    this.btnPopUp_ResponsableEventoCommand.Text = "";
                    this.lblPopUp_ResponsableEventoMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ResponsableEventoPanel(String Orden, String Nombre, String Puesto){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ResponsableEvento.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_ResponsableEventoTitle.Text = "Edición de elemento";
                    this.btnPopUp_ResponsableEventoCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpResponsableEvento_OrdenAnterior.Text = Orden;
                    this.txtPopUpResponsableEvento_Orden.Text = Orden;
                    this.txtPopUpResponsableEvento_Nombre.Text = Nombre;
                    this.txtPopUpResponsableEvento_Puesto.Text = Puesto;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpResponsableEvento_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ResponsableEvento(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpResponsableEvento_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpResponsableEvento_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpResponsableEvento_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpResponsableEvento_Puesto.Text.Trim();

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoResponsable_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ResponsableEventoPanel();

                    // Actualizar listado
                    this.gvResponsableEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvResponsableEvento.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableEventoNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ResponsableEventoCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpResponsableEvento_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpResponsableEvento_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpResponsableEvento_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpResponsableEvento_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_ResponsableEvento();

                }catch (Exception ex){
                    this.lblPopUp_ResponsableEventoMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpResponsableEvento_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ResponsableEvento_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ResponsableEventoPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

        #region PopUp - ResponsableLogistica
            
            
            // Rutinas

            void ClearPopUp_ResponsableLogisticaPanel(){
                try
                {

                    // Estado incial de controles
                    this.pnlPopUp_ResponsableLogistica.Visible = false;
                    this.lblPopUp_ResponsableLogisticaTitle.Text = "";
                    this.btnPopUp_ResponsableLogisticaCommand.Text = "";
                    this.lblPopUp_ResponsableLogisticaMessage.Text = "";

                }catch (Exception ex){
                    throw (ex);
                }
            }

            void SetPopUp_ResponsableLogisticaPanel(String Orden, String Nombre, String Contacto){
                try
                {

                    // Acciones comunes
                    this.pnlPopUp_ResponsableLogistica.Visible = true;

                    // Detalle de acción
                    this.lblPopUp_ResponsableLogisticaTitle.Text = "Edición de elemento";
                    this.btnPopUp_ResponsableLogisticaCommand.Text = "Actualizar";

                    // Formulario
                    this.txtPopUpResponsableLogistica_OrdenAnterior.Text = Orden;
                    this.txtPopUpResponsableLogistica_Orden.Text = Orden;
                    this.txtPopUpResponsableLogistica_Nombre.Text = Nombre;
                    this.txtPopUpResponsableLogistica_Puesto.Text = Contacto;

                    // Foco
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtPopUpResponsableLogistica_Orden.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }
            
            void UpdateConfiguracion_ResponsableLogistica(){
                ENTEvento oENTEvento = new ENTEvento();
                ENTResponse oENTResponse = new ENTResponse();
                ENTSession oENTSession = new ENTSession();

                BPEvento oBPEvento = new BPEvento();

                try
                {

                    // Datos de sesión
                    oENTSession = (ENTSession)this.Session["oENTSession"];
                    oENTEvento.UsuarioId = oENTSession.UsuarioId;
                    oENTEvento.RolId = oENTSession.RolId;

                    // Formulario
                    oENTEvento.EventoId = Int32.Parse(this.hddEventoId.Value);
                    oENTEvento.OrdenAnterior = Int32.Parse(this.txtPopUpResponsableLogistica_OrdenAnterior.Text);
                    oENTEvento.NuevoOrden = Int32.Parse(this.txtPopUpResponsableLogistica_Orden.Text);
                    oENTEvento.Nombre = this.txtPopUpResponsableLogistica_Nombre.Text.Trim();
                    oENTEvento.Puesto = this.txtPopUpResponsableLogistica_Puesto.Text.Trim();

                    // Transacción
                    oENTResponse = oBPEvento.UpdateEventoResponsableLogistica_Item(oENTEvento);

                    // Validaciones
                    if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                    if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                    // Transacción exitosa
                    ClearPopUp_ResponsableLogisticaPanel();

                    // Actualizar listado
                    this.gvResponsableLogistica.DataSource = oENTResponse.DataSetResponse.Tables[1];
                    this.gvResponsableLogistica.DataBind();

                    // Mensaje de usuario
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.txtResponsableLogisticaNombre.ClientID + "'); }", true);

                }catch (Exception ex){
                    throw (ex);
                }
            }

            
            // Eventos

            protected void btnPopUp_ResponsableLogisticaCommand_Click(object sender, EventArgs e){
                try
                {

                    // Validar formulario
                    if ( this.txtPopUpResponsableLogistica_Orden.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un orden")); }
                    if ( !gcNumber.IsNumber ( this.txtPopUpResponsableLogistica_Orden.Text.Trim(), GCNumber.NumberTypes.Int32Type) ) { throw (new Exception("El campo orden debe de ser numérico")); }
                    if ( Int32.Parse( this.txtPopUpResponsableLogistica_Orden.Text.Trim() ) < 1 ) { throw (new Exception("El campo orden debe de ser mayor a 0")); }
                    if ( this.txtPopUpResponsableLogistica_Nombre.Text.Trim() == "" ) { throw (new Exception("Es necesario ingresar un nombre")); }

                    // Transacción
                    UpdateConfiguracion_ResponsableLogistica();

                }catch (Exception ex){
                    this.lblPopUp_ResponsableLogisticaMessage.Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtPopUpResponsableLogistica_Orden.ClientID + "');", true);
                }
            }

            protected void imgCloseWindow_ResponsableLogistica_Click(object sender, ImageClickEventArgs e){
                try
                {

                    // Cancelar transacción
                    ClearPopUp_ResponsableLogisticaPanel();

                }catch (Exception ex){
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
                }
            }

            
        #endregion

    }
}