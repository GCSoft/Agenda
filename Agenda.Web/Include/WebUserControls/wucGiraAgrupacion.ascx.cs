/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre: wucGiraAgrupacion
' Autor:  Ruben.Cobos
' Fecha:  06-Abril-2015
'----------------------------------------------------------------------------------------------------------------------------------*/

// Referencias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Referencias manuales
using System.Data;

namespace Agenda.Web.Include.WebUserControls
{
    public partial class wucGiraAgrupacion : System.Web.UI.UserControl
    {


        // Propiedades

        ///<remarks>
        ///   <name>wucGiraAgrupacion.ItemDisplayed</name>
        ///   <create>20-Agosto-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Obtiene la el Item desplegado en el control</summary>
        public String ItemDisplayed
        {
            get { return this.ddlAgrupacion.SelectedItem.Text; }
        }



        // Métodos públicos

        ///<remarks>
        ///   <name>wucGiraAgrupacion.IsValidItemSelected</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Determina si el control tiene seleccionado un elemento válido</summary>
        ///<param name="Item">Elemento a seleccionar</param>
        public Boolean IsValidItemSelected(){
            Boolean Response = false;

            try
            {
                // Validaciones
                if (this.txtOtraAgrupacion.Enabled) { throw(new Exception("Estado inválido")); }
                if (this.ddlAgrupacion.SelectedItem.Value == "-1") { throw (new Exception("Estado inválido")); }

                // Estado válido
                Response = true;

            }catch(Exception){
                Response = false;
            }

            return Response;
        }

        ///<remarks>
        ///   <name>wucGiraAgrupacion.CargarAgrupaciones</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Cargar un DataTable al listado de agrupaciones</summary>
        ///<param name="dtAgrupacion">DataTable con el listado a cargar</param>
        public void CargarAgrupaciones(DataTable dtAgrupacion){
            try
            {

                // Limpiar combo
                this.ddlAgrupacion.Items.Clear();

                // Llenado de combo
                this.ddlAgrupacion.DataTextField = "Agrupacion";
                this.ddlAgrupacion.DataValueField = "Row";
                this.ddlAgrupacion.DataSource = dtAgrupacion;
                this.ddlAgrupacion.DataBind();

                // Agregar Item de selección
                this.ddlAgrupacion.Items.Insert(this.ddlAgrupacion.Items.Count, new ListItem("[Sin agrupación]", "-2"));
                this.ddlAgrupacion.Items.Insert(this.ddlAgrupacion.Items.Count, new ListItem("[Otro]", "-1"));

                // Guardar ViewState
                this.ViewState["dtAgrupacion"] = dtAgrupacion;
                
            }catch (Exception ex){
                throw(ex);
            }
        }

        ///<remarks>
        ///   <name>wucGiraAgrupacion.SelectItem</name>
        ///   <create>19-Diciembre-2014</create>
        ///   <author>Ruben.Cobos</author>
        ///</remarks>
        ///<summary>Selecciona un elemento especifico en el control</summary>
        ///<param name="Item">Elemento a seleccionar</param>
        public void SelectItem( String Item ){
            DataTable dtAgrupacion;

            try
            {
                // Recuperar agrupación
                dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                // Validación
                if (dtAgrupacion.Select("Agrupacion='" + Item + "'").Length == 0) { return; }

                // Seleccionar elemento
                this.ddlAgrupacion.SelectedValue = dtAgrupacion.Select("Agrupacion='" + Item + "'")[0]["Row"].ToString();

            }catch(Exception ex){
                throw(ex);
            }
        }



        // Eventos del Control

        protected void Page_Load(object sender, EventArgs e){
            DataTable dtAgrupacion;

            try
            {

                // Validaciones de llamada
                if (Page.IsPostBack) { return; }

                // DataSet en ViewState
                dtAgrupacion = new DataTable("dtAgrupacion");
                dtAgrupacion.Columns.Add("Row", typeof(Int32));
                dtAgrupacion.Columns.Add("Agrupacion", typeof(String));

                // Llenado de combo
                CargarAgrupaciones(dtAgrupacion);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnNuevaAgrupacion_Click(object sender, EventArgs e){
            DataTable dtAgrupacion;
            DataRow rowAgrupacion;

            try
            {

                // Recuperar agrupación
                dtAgrupacion = (DataTable)this.ViewState["dtAgrupacion"];

                // Validación
                if ( dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion.Text.Trim() + "'").Length == 0 ){

                    rowAgrupacion = dtAgrupacion.NewRow();
                    rowAgrupacion["Row"] = dtAgrupacion.Rows.Count + 1;
                    rowAgrupacion["Agrupacion"] = this.txtOtraAgrupacion.Text.Trim();
                    dtAgrupacion.Rows.Add(rowAgrupacion);

                    CargarAgrupaciones(dtAgrupacion);
                }

                // Seleccionar el item deseado
                this.ddlAgrupacion.SelectedValue = dtAgrupacion.Select("Agrupacion='" + this.txtOtraAgrupacion.Text.Trim() + "'")[0]["Row"].ToString();

                // Estado inicial
                this.txtOtraAgrupacion.Text = "";
                this.txtOtraAgrupacion.Enabled = false;
                this.btnNuevaAgrupacion.Enabled = false;

                this.txtOtraAgrupacion.CssClass = "Textbox_Disabled";
                this.btnNuevaAgrupacion.CssClass = "Button_Special_Gray";

                // Foco
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.ddlAgrupacion.ClientID + "');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
            }
		}

        protected void ddlAgrupacion_SelectedIndexChanged(object sender, EventArgs e){
            try
            {

                if( this.ddlAgrupacion.SelectedItem.Value == "-1" ){

                    this.txtOtraAgrupacion.Text = "";
                    this.txtOtraAgrupacion.Enabled = true;
                    this.btnNuevaAgrupacion.Enabled = true;

                    this.txtOtraAgrupacion.CssClass = "Textbox_General";
                    this.btnNuevaAgrupacion.CssClass = "Button_Special_Blue";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "focusControl('" + this.txtOtraAgrupacion.ClientID + "');", true);
                }else{

                    this.txtOtraAgrupacion.Text = "";
                    this.txtOtraAgrupacion.Enabled = false;
                    this.btnNuevaAgrupacion.Enabled = false;

                    this.txtOtraAgrupacion.CssClass = "Textbox_Disabled";
                    this.btnNuevaAgrupacion.CssClass = "Button_Special_Gray";
                }

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + ex.Message + "');", true);
            }
        }


    }
}