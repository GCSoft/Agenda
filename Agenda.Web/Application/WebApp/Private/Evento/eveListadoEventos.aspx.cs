/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	scatEvento
' Autor:	Ruben.Cobos
' Fecha:	27-Octubre-2013
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveListadoEventos : BPPage
    {
       
        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();


        // Funciones del programador

        String GetUTCBeginDate(){
            DateTime tempDate = DateTime.Now;
            String UTCCurrentDate = "";

            try
            {

                // Inicio de mes
                tempDate = tempDate.AddDays( (DateTime.Now.Day - 1) * -1);

                // Año
                UTCCurrentDate = tempDate.Year.ToString() + "-";

                // Mes
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Month > 9 ? tempDate.Month.ToString() : "0" + tempDate.Month.ToString() ) + "-";

                // Día
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Day > 9 ? tempDate.Day.ToString() : "0" + tempDate.Day.ToString() );

            }catch(Exception ex){
                throw(ex);
            }

            return UTCCurrentDate;
        }

        String GetUTCEndDate(){
            DateTime tempDate = DateTime.Now;
            String UTCCurrentDate = "";

            try
            {

                // Inicio de mes
                tempDate = tempDate.AddDays( (DateTime.Now.Day - 1) * -1);
                tempDate = tempDate.AddMonths(1);

                // Año
                UTCCurrentDate = tempDate.Year.ToString() + "-";

                // Mes
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Month > 9 ? tempDate.Month.ToString() : "0" + tempDate.Month.ToString() ) + "-";

                // Día
                UTCCurrentDate = UTCCurrentDate + ( tempDate.Day > 9 ? tempDate.Day.ToString() : "0" + tempDate.Day.ToString() );

            }catch(Exception ex){
                throw(ex);
            }

            return UTCCurrentDate;
        }



        // Rutinas del programador

        void SelectDependencia(){
            ENTSession oENTSession = new ENTSession();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Opciones por Rol
                switch( oENTSession.RolId ){
                    case 4: // Logística

                        this.ddlDependencia.Items.Insert(0, new ListItem("Logística", "1"));
                        break;

                    case 5: // Dirección de Protocolo

                        this.ddlDependencia.Items.Insert(0, new ListItem("Dirección de Protocolo", "2"));
                        break;

                    default:

                        this.ddlDependencia.Items.Insert(0, new ListItem("Dirección de Protocolo", "2"));
                        this.ddlDependencia.Items.Insert(0, new ListItem("Logística", "1"));
                        this.ddlDependencia.Items.Insert(0, new ListItem("[Todas]", "0"));
                        break;
                }

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEstatusEvento(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEstatusEvento oENTEstatusEvento = new ENTEstatusEvento();
            ENTSession oENTSession = new ENTSession();

            BPEstatusEvento oBPEstatusEvento = new BPEstatusEvento();

            try
            {

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];
                oENTEstatusEvento.RolId = oENTSession.RolId;

                // Formulario
                oENTEstatusEvento.EstatusEventoId = 0;
                oENTEstatusEvento.Nombre = "";

                // Transacción
                oENTResponse = oBPEstatusEvento.SelectEstatusEvento(oENTEstatusEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Llenado de combo
                this.ddlEstatusEvento.DataTextField = "Nombre";
                this.ddlEstatusEvento.DataValueField = "EstatusEventoId";
                this.ddlEstatusEvento.DataSource = oENTResponse.DataSetResponse.Tables[1];
                this.ddlEstatusEvento.DataBind();

                // Agregar Item de selección
                this.ddlEstatusEvento.Items.Insert(0, new ListItem("[Todos]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SelectEvento(Boolean CheckDate){
            ENTResponse oENTResponse = new ENTResponse();
            ENTEvento oENTEvento = new ENTEvento();
            ENTSession oENTSession = new ENTSession();

            BPEvento oBPEvento = new BPEvento();

            try
            {

                // Validaciones
                if (CheckDate){
                    if (!this.wucBeginDate.IsValidDate()) { throw new Exception("El campo [Fecha Inicial] es requerido"); }
                    if (!this.wucEndDate.IsValidDate()) { throw new Exception("El campo [Fecha Inicial] es requerido"); }
                }

                if( this.rblBusqueda.SelectedIndex > 0 ){ // Por palabra clave

                    if (this.txtNombre.Text.Trim() == "") { throw new Exception("Las consultas históricas consideran mucha información. Introduzca por lo menos una palabra clave como filtro."); }
                }

                // Datos de sesión
                oENTSession = (ENTSession)this.Session["oENTSession"];

                // Formulario
                oENTEvento.EventoId = 0;
                oENTEvento.UsuarioId = oENTSession.UsuarioId;
                oENTEvento.EstatusEventoId = Int32.Parse( this.ddlEstatusEvento.SelectedItem.Value );
                oENTEvento.PrioridadId = Int32.Parse(this.ddlPrioridad.SelectedItem.Value);
                oENTEvento.Nivel = 1;
                oENTEvento.Dependencia = Int16.Parse(this.ddlDependencia.SelectedItem.Value);

                if( this.rblBusqueda.SelectedIndex == 0 ){ // Por fecha

                    oENTEvento.FechaInicio = (CheckDate ? this.wucBeginDate.DisplayUTCDate : GetUTCBeginDate());
                    oENTEvento.FechaFin = (CheckDate ? this.wucEndDate.DisplayUTCDate : GetUTCEndDate());
                    oENTEvento.PalabraClave = "";
                }else{ // Por Palabra Clave

                    oENTEvento.FechaInicio = "1900-01-01";
                    oENTEvento.FechaFin = "2900-01-01";
                    oENTEvento.PalabraClave = this.txtNombre.Text.Trim();
                }

                // Transacción
                oENTResponse = oBPEvento.SelectEvento(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }

                // Listado de Eventoes
                this.gvEvento.DataSource = oENTResponse.DataSetResponse.Tables[0];
                this.gvEvento.DataBind();

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
                this.ddlPrioridad.Items.Insert(0, new ListItem("[Todas]", "0"));

            }catch (Exception ex){
                throw (ex);
            }
        }

        
        // Eventos de la página

        protected void Page_Load(object sender, EventArgs e){
            DateTime tempDate = DateTime.Now;

            try
            {

                // Validaciones
                if (this.IsPostBack) { return; }

                // Llenado de controles
                SelectEstatusEvento();
                SelectPrioridad();
                SelectDependencia();

                // Por default preseleccionado todo el mes
                tempDate = tempDate.AddDays((DateTime.Now.Day - 1) * -1);
                this.wucBeginDate.SetDate(tempDate);

                tempDate = tempDate.AddMonths(1);
                tempDate = tempDate.AddDays(-1);
                this.wucEndDate.SetDate(tempDate);

                // Consulta
                SelectEvento(false);

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e){
            try
            {

                SelectEvento(true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

        protected void gvEvento_RowCommand(object sender, GridViewCommandEventArgs e){
            Int32 EventoId = 0;
            Int32 intRow = 0;

            String strCommand = "";
            String Gira = "";
            String Key = "";

            try
            {

                // Opción seleccionada
                strCommand = e.CommandName.ToString();

                // Se dispara el Evento RowCommand en el ordenamiento
                if (strCommand == "Sort") { return; }

                // Fila
                intRow = Int32.Parse(e.CommandArgument.ToString());

                // Datakeys
                EventoId = Int32.Parse(this.gvEvento.DataKeys[intRow]["EventoId"].ToString());
                Gira = this.gvEvento.DataKeys[intRow]["Gira"].ToString();

                // Acción
                switch (strCommand){
                    case "Editar":

                        // Llave encriptada
                        Key = EventoId.ToString() + "|3";
                        Key = gcEncryption.EncryptString(Key, true);
                        this.Response.Redirect( ( Gira == "1" ? "../Gira/girDetalleGira.aspx" : "eveDetalleEvento.aspx") + "?key=" + Key, false);
                        break;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

        protected void gvEvento_RowDataBound(object sender, GridViewRowEventArgs e){
            ImageButton imgEdit = null;

            String EventoId = "";
            String EventoNombre = "";

            String sImagesAttributes = "";
            String sTootlTip = "";

            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }

                // Obtener imagenes
                imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                // Datakeys
                EventoId = this.gvEvento.DataKeys[e.Row.RowIndex]["EventoId"].ToString();
                EventoNombre = this.gvEvento.DataKeys[e.Row.RowIndex]["EventoNombre"].ToString();

                // Tooltip Edición
                sTootlTip = "Detalle de invitación [" + EventoNombre + "]";
                imgEdit.Attributes.Add("title", sTootlTip);

                // Atributos Over
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit_Over.png';";
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; " + sImagesAttributes);

                // Atributos Out
                sImagesAttributes = " document.getElementById('" + imgEdit.ClientID + "').src='../../../../Include/Image/Buttons/Edit.png';";
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; " + sImagesAttributes);

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvEvento_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvEvento, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){ alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }

        }

        protected void rblBusqueda_SelectedIndexChanged(object sender, EventArgs e){
            DateTime tempDate = DateTime.Now;

            try
            {

                // Por default preseleccionado todo el mes
                tempDate = tempDate.AddDays((DateTime.Now.Day - 1) * -1);
                this.wucBeginDate.SetDate(tempDate);

                tempDate = tempDate.AddMonths(1);
                tempDate = tempDate.AddDays(-1);
                this.wucEndDate.SetDate(tempDate);

                this.txtNombre.Text = "";

                // ocultar/mostrar panel
                if( this.rblBusqueda.SelectedIndex == 0 ){

                    this.pnlBusquedaFecha.Visible = true;
                    this.pnlBusquedaPalabra.Visible = false;
                }else{

                    this.pnlBusquedaFecha.Visible = false;
                    this.pnlBusquedaPalabra.Visible = true;
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "function pageLoad(){  alert('" + gcJavascript.ClearText(ex.Message) + "'); focusControl('" + this.ddlEstatusEvento.ClientID + "'); }", true);
            }
        }

    }
}