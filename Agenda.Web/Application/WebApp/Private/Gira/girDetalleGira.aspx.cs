/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	girDetalleGira
' Autor:	Ruben.Cobos
' Fecha:	22-Diciembre-2014
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

namespace Agenda.Web.Application.WebApp.Private.Gira
{
    public partial class girDetalleGira : System.Web.UI.Page
    {
        

        // Utilerías
        GCCommon gcCommon = new GCCommon();
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();

        

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



        // Rutinas del programador

        void SelectGira(){
            ENTResponse oENTResponse = new ENTResponse();
            ENTGira oENTGira = new ENTGira();

            BPGira oBPGira = new BPGira();

            DataRow rowComentarioCuadernillo;

            try
            {

                // Formulario
                oENTGira.GiraId = Int32.Parse(this.hddGiraId.Value);

                // Transacción
                oENTResponse = oBPGira.SelectGira_Detalle(oENTGira);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Campos ocultos
                this.hddEstatusGiraId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusGiraId"].ToString();
                this.Expired.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Expired"].ToString();

                // Carátula
                this.lblDependenciaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["DependenciaNombre"].ToString();
                this.lblGiraNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString();
                this.lblGiraFechaHora.Text = "Del " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHoraInicioTexto"].ToString() + " al " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraFechaHoraFinTexto"].ToString();
                this.lblEstatusGiraNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusGiraNombre"].ToString();
                this.lblGiraDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["GiraDetalle"].ToString();

                // Programa
                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "1" ){

                    #region Agregar un registro al inicio del programa
                        rowComentarioCuadernillo = oENTResponse.DataSetResponse.Tables[2].NewRow();
                        rowComentarioCuadernillo["GiraConfiguracionId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraConfiguracionId"].ToString();
                        rowComentarioCuadernillo["GiraId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraId"].ToString();
                        rowComentarioCuadernillo["TipoGiraConfiguracionId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoGiraConfiguracionId"].ToString();
                        rowComentarioCuadernillo["ConfiguracionGrupo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionGrupo"].ToString();

                        rowComentarioCuadernillo["ConfiguracionFechaInicio"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFecha"].ToString();
                        rowComentarioCuadernillo["ConfiguracionFechaFin"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFecha"].ToString();

                        rowComentarioCuadernillo["ConfiguracionHoraInicio"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicio"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFin"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFin"].ToString();
                        rowComentarioCuadernillo["ConfiguracionDetalle"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionDetalle"].ToString();
                        rowComentarioCuadernillo["GiraFechaEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFechaEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraInicioEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFinEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraInicio24H"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicio24H"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFin24H"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFin24H"].ToString();
                        rowComentarioCuadernillo["NotaInicioEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioEvento"].ToString();
                        rowComentarioCuadernillo["NotaFinEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinEvento"].ToString();
                        rowComentarioCuadernillo["NotaEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaEvento"].ToString();
                        rowComentarioCuadernillo["NotaInicioComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioComite"].ToString();
                        rowComentarioCuadernillo["NotaFinComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinComite"].ToString();
                        rowComentarioCuadernillo["NotaComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaComite"].ToString();
                        rowComentarioCuadernillo["NotaInicioOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioOrden"].ToString();
                        rowComentarioCuadernillo["NotaFinOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinOrden"].ToString();
                        rowComentarioCuadernillo["NotaOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaOrden"].ToString();
                        rowComentarioCuadernillo["NotaInicioAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioAcomodo"].ToString();
                        rowComentarioCuadernillo["NotaFinAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinAcomodo"].ToString();
                        rowComentarioCuadernillo["NotaAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaAcomodo"].ToString();
                        oENTResponse.DataSetResponse.Tables[2].Rows.InsertAt(rowComentarioCuadernillo, 0);
                    #endregion

                } else if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "1" ) {

                    #region Agregar un registro al fin del programa
                        rowComentarioCuadernillo = oENTResponse.DataSetResponse.Tables[2].NewRow();
                        rowComentarioCuadernillo["GiraConfiguracionId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraConfiguracionId"].ToString();
                        rowComentarioCuadernillo["GiraId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraId"].ToString();
                        rowComentarioCuadernillo["TipoGiraConfiguracionId"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoGiraConfiguracionId"].ToString();
                        rowComentarioCuadernillo["ConfiguracionGrupo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionGrupo"].ToString();

                        rowComentarioCuadernillo["ConfiguracionFechaInicio"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFecha"].ToString();
                        rowComentarioCuadernillo["ConfiguracionFechaFin"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFecha"].ToString();
                        
                        rowComentarioCuadernillo["ConfiguracionHoraInicio"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicio"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFin"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFin"].ToString();
                        rowComentarioCuadernillo["ConfiguracionDetalle"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionDetalle"].ToString();
                        rowComentarioCuadernillo["GiraFechaEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["GiraFechaEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraInicioEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicioEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFinEstandar"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFinEstandar"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraInicio24H"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraInicio24H"].ToString();
                        rowComentarioCuadernillo["ConfiguracionHoraFin24H"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["ConfiguracionHoraFin24H"].ToString();
                        rowComentarioCuadernillo["NotaInicioEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioEvento"].ToString();
                        rowComentarioCuadernillo["NotaFinEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinEvento"].ToString();
                        rowComentarioCuadernillo["NotaEvento"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaEvento"].ToString();
                        rowComentarioCuadernillo["NotaInicioComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioComite"].ToString();
                        rowComentarioCuadernillo["NotaFinComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinComite"].ToString();
                        rowComentarioCuadernillo["NotaComite"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaComite"].ToString();
                        rowComentarioCuadernillo["NotaInicioOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioOrden"].ToString();
                        rowComentarioCuadernillo["NotaFinOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinOrden"].ToString();
                        rowComentarioCuadernillo["NotaOrden"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaOrden"].ToString();
                        rowComentarioCuadernillo["NotaInicioAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaInicioAcomodo"].ToString();
                        rowComentarioCuadernillo["NotaFinAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaFinAcomodo"].ToString();
                        rowComentarioCuadernillo["NotaAcomodo"] = oENTResponse.DataSetResponse.Tables[6].Rows[0]["NotaAcomodo"].ToString();
                        oENTResponse.DataSetResponse.Tables[2].Rows.Add(rowComentarioCuadernillo);
                    #endregion
                }
                this.gvPrograma.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvPrograma.DataBind();
                
                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[3];
                this.gvContacto.DataBind();

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetErrorPage(){
			try
            {

                this.DatosGiraPanel.Visible = false;
                this.ProgramaGiraPanel.Visible = false;
                this.ContactoPanel.Visible = false;
                this.RechazarPanel.Visible = false;
                this.CuadernilloGiraPanel.Visible = false;
                this.Historial.Visible = false;

            }catch (Exception){
				// Do Nothing
            }
		}

        void SetPermisosGenerales(Int32 RolId) {
			try
            {

                // Permisos por rol
				switch (RolId){

					case 1:	// System Administrator
                    case 2:	// Administrador
                        this.DatosGiraPanel.Visible = true;
                        this.ProgramaGiraPanel.Visible = true;
                        this.ContactoPanel.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloGiraPanel.Visible = true;
                        this.Historial.Visible = true;
						break;

                    case 4:	// Logística
                    case 5:	// Dirección de Protocolo
                        this.DatosGiraPanel.Visible = true;
                        this.ProgramaGiraPanel.Visible = true;
                        this.ContactoPanel.Visible = true;;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloGiraPanel.Visible = true;
                        this.Historial.Visible = true;
						break;

					default:
                        this.DatosGiraPanel.Visible = false;
                        this.ProgramaGiraPanel.Visible = false;
                        this.ContactoPanel.Visible = false;
                        this.RechazarPanel.Visible = false;
                        this.CuadernilloGiraPanel.Visible = false;
                        this.Historial.Visible = false;
						break;

				}
	

            }catch (Exception ex){
				throw(ex);
            }
		}

		void SetPermisosParticulares(Int32 RolId, Int32 UsuarioId) {
			try
            {

				// El Gira no se podrá operar en los siguientes Estatus:
                // 3 - Expirado
                // 4 - Cancelado
				if ( Int32.Parse(this.hddEstatusGiraId.Value) == 3 || Int32.Parse(this.hddEstatusGiraId.Value) == 4 ){

                    this.DatosGiraPanel.Visible = false;
                    this.ProgramaGiraPanel.Visible = false;
                    this.ContactoPanel.Visible = false;
                    this.RechazarPanel.Visible = false;

				}

                // Si el Gira está cancelado no se podrá generar el cuadernillo
                // 4 - Cancelado
				if ( Int32.Parse(this.hddEstatusGiraId.Value) == 4 ){

                    this.CuadernilloGiraPanel.Visible = false;
				}

                // Independientemente del estatus, si ya expiró ocultar opciones no contempladas
				if ( this.Expired.Value == "1" ){

                    this.DatosGiraPanel.Visible = false;
                    this.ProgramaGiraPanel.Visible = false;
                    this.ContactoPanel.Visible = false;
                    this.RechazarPanel.Visible = false;

				}

                 // Si el evento está expirado, pero lo está consultando un administrador, se podrán hacer ajustes a la captura
				if ( this.Expired.Value == "1" && RolId < 3 ){

                    this.DatosGiraPanel.Visible = true;
                    this.ProgramaGiraPanel.Visible = true;
                    this.ContactoPanel.Visible = true;
                    this.CuadernilloGiraPanel.Visible = true;
                    this.Historial.Visible = true;

                    // Si el Gira está cancelado no se podrá generar el cuadernillo
                    // 4 - Cancelado
                    if (Int32.Parse(this.hddEstatusGiraId.Value) == 4)
                    {

                        this.CuadernilloGiraPanel.Visible = false;
                    }

                }


            }catch (Exception ex){
				throw(ex);
            }
		}




        // Giras de la página

		protected void Page_Load(object sender, EventArgs e){
			ENTSession oENTSession = new ENTSession();
			String Key;

            try
            {

                // Validaciones de llamada
                if (Page.IsPostBack) { return; }
                if (this.Request.QueryString["key"] == null) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

				// Validaciones de parámetros
				Key = GetKey(this.Request.QueryString["key"].ToString());
				if (Key == "") { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }
				if (Key.ToString().Split(new Char[] { '|' }).Length != 2) { this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false); return; }

                // Obtener GiraId
				this.hddGiraId.Value = Key.ToString().Split(new Char[] { '|' })[0];

                // Obtener Sender
				this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

                switch (this.SenderId.Value){
					case "1":
                        this.Sender.Value = "girNuevaGira.aspx";
                        break;

					case "2":
                        this.Sender.Value = "../AppIndex.aspx";
						break;

					case "3":
                        this.Sender.Value = "../Evento/eveListadoEventos.aspx";
						break;

                    case "4":
                        this.Sender.Value = "../Evento/eveCalendario.aspx";
                        break;

                    default:
                        this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false);
                        return;
                }

                // Atributos de controles
                this.CuadernilloGiraButton.Attributes.Add("onclick", "window.open('Cuadernillos/Gira.aspx?key=" + gcEncryption.EncryptString(this.hddGiraId.Value, true) + "'); return false;");

                // Consultar detalle de El Gira
                SelectGira();

                // Obtener sesión
                oENTSession = (ENTSession)Session["oENTSession"];

                // Seguridad
                SetPermisosGenerales(oENTSession.RolId);
                SetPermisosParticulares(oENTSession.RolId, oENTSession.UsuarioId);

            }catch (Exception ex){
                SetErrorPage();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void btnRegresar_Click(object sender, EventArgs e){
            Response.Redirect(this.Sender.Value);
        }

        protected void gvContacto_RowDataBound(object sender, GridViewRowEventArgs e){
            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }                

                // Atributos Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; ");

                // Atributos Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; ");

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvContacto_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvContacto, ref this.hddSort, e.SortExpression);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

        protected void gvPrograma_RowDataBound(object sender, GridViewRowEventArgs e){
            try
            {

                // Validación de que sea fila
                if (e.Row.RowType != DataControlRowType.DataRow) { return; }                

                // Atributos Over
                e.Row.Attributes.Add("onmouseover", "this.className='Grid_Row_Over'; ");

                // Atributos Out
                e.Row.Attributes.Add("onmouseout", "this.className='" + ((e.Row.RowIndex % 2) != 0 ? "Grid_Row_Alternating" : "Grid_Row") + "'; ");

            }catch (Exception ex){
                throw (ex);
            }
        }

        protected void gvPrograma_Sorting(object sender, GridViewSortEventArgs e){
            try
            {

                gcCommon.SortGridView(ref this.gvPrograma, ref this.hddSort, e.SortExpression, true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }




        // Opciones de Menu (en orden de aparación)

        protected void DatosGiraButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girDatosGenerales.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ProgramaGiraButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girConfiguracionGira.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ContactoButton_Click(object sender, ImageClickEventArgs e){
			 String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girContacto.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void RechazarButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girCancelar.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void HistorialButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddGiraId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("girHistorial.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}



    }
}