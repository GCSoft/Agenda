/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	Gira
' Autor:	Ruben.Cobos
' Fecha:	23-Abril-2015
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
using System.Drawing;
using System.Threading;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace Agenda.Web.Application.WebApp.Private.Gira.Cuadernillos
{
    public partial class Gira : System.Web.UI.Page
    {
        

        // Utilerías
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

        void CrearCuadernillo(Int32 GiraId){
            ENTGira oENTGira = new ENTGira();
            ENTResponse ENTResponseGira = new ENTResponse();
            ENTResponse ENTResponseGiraConfiguracion;

            BPGira oBPGira = new BPGira();

            WordDocument oDocument = new WordDocument();
            IWSection oSection; ;

            IWTable wTable;
            WTableRow wTableRow;
            IWParagraph wTableCell;
            IWPicture wPicture;
            IWTextRange wText;

            Char ENTER = Convert.ToChar(13);

            WParagraph footerPar;
            System.Drawing.Image imgTemporal;
            WTableRow tRow;

            String AgrupacionActual = "";
            String LevelError = "";

            WTable wtblListado;
            Int32 FilaOrden;
            String ListadoOrden;
            String ListadoNombre;
            String ListadoPuesto;

            try {

                // Formulario
                oENTGira.GiraId = GiraId;
                oENTGira.Activo = 1;

                // Consulta de información
                ENTResponseGira = oBPGira.SelectGira_Detalle(oENTGira);

                // Validaciones
                if (ENTResponseGira.GeneratesException) { throw (new Exception(ENTResponseGira.MessageError)); }
                if (ENTResponseGira.MessageDB != "") { throw (new Exception(ENTResponseGira.MessageDB)); }
                if (ENTResponseGira.DataSetResponse.Tables[1].Rows.Count == 0) { throw (new Exception("No se encontró la información de la Gira")); }

                // Nueva hoja de Word
                oSection = oDocument.AddSection();
                oSection.PageSetup.PageSize = new SizeF(612, 652);

                // Márgenes
                oSection.PageSetup.Margins.Bottom = 36f;    // 1.27 cm
                oSection.PageSetup.Margins.Left = 36f;      // 1.27 cm
                oSection.PageSetup.Margins.Right = 66.3f;   // 2.34 cm
                oSection.PageSetup.Margins.Top = 36f;       // 1.27 cm

                oSection.PageSetup.HeaderDistance = 10f;    // .35 cm (En el documento marca .25 cm -7f- pero queda muy pegado por la alineación de la tabla)
                oSection.PageSetup.FooterDistance = 22f;    // .79 cm

                #region Encabezado

                    LevelError = "Encabezado";

                    #region Logo y Título
                            
                        wTable = oSection.HeadersFooters.Header.AddTable();
                        wTable.ResetCells(1, 3); // 1 Fila 2 Columnas

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 52f;

                        // Celda 1 (Logo)
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        if (System.IO.File.Exists(Server.MapPath("~/Include/Image/Cuadernillo/LogoSolo.png")))
                        {
                            imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("~/Include/Image/Cuadernillo/LogoSolo.png"));
                            wPicture = wTableCell.AppendPicture(imgTemporal);
                            wPicture.Height = 47;
                            wPicture.Width = 89;
                        }

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 100;

                        // Celda 2 (Fecha)
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        wText = wTableCell.AppendText("GOBIERNO DEL ESTADO DE NUEVO LEÓN" + ENTER + "SEGURIDAD GUBERNAMENTAL Y LOGÍSTICA");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial Black";
                        wText.CharacterFormat.FontSize = 11f;
                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 350;

                        // Celda 3 (vacía)
                        wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 90;

                        // Agregar el párrafo recién creado
                        oSection.AddParagraph();
                            
                    #endregion
                    
                    #region Nombre de la Gira

                        wTable = oSection.HeadersFooters.Header.AddTable();
                        wTable.ResetCells(1, 3); // 1 Fila 3 Columnas

                        wTableRow = wTable.Rows[0];

                        // Celda 1 (vacía)
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 50;

                        // Celda 2 (Nombre de la Gira)
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        
                        wText = wTableCell.AppendText( System.Text.RegularExpressions.Regex.Replace( HttpUtility.HtmlDecode( ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["GiraNombre"].ToString() ), @"<(.|\n)*?>", String.Empty));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wText = wTableCell.AppendText(ENTER + ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["GiraFechaDisplay"].ToString().ToUpper() );
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 440;

                        // Celda 3 (vacía)
                        wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 50;

                        // Agregar el párrafo recién creado
                        oSection.AddParagraph();
                            
                    #endregion

                    #region Separador

                        wTable = oSection.HeadersFooters.Header.AddTable();
                        wTable.ResetCells(1, 1); // 1 Fila 1 columnas

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1 (Separador)
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        if (System.IO.File.Exists(Server.MapPath("~/Include/Image/Cuadernillo/Separador_NL.png")))
                        {
                            imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("~/Include/Image/Cuadernillo/Separador_NL.png"));
                            wPicture = wTableCell.AppendPicture(imgTemporal);
                            wPicture.Height = 5;
                            wPicture.Width = 510;
                        }

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;
                            
                    #endregion
                    
                    #region Número de página

                        wTable = oSection.HeadersFooters.Footer.AddTable();
                        wTable.ResetCells(1, 2); // 1 Fila 2 columnas

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 30f;

                        // Celda 1 (Última modificación)
                        footerPar = (WParagraph)wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        footerPar.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = footerPar.AppendText("Actualizado a las: " + ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["HoraModificacionEstandar"].ToString() + "  del " + ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["FechaModificacionEstandar"].ToString());
                        wText.CharacterFormat.Bold = false;
                        wText.CharacterFormat.FontName = "Times New Roman";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 270;

                        // Celda 2 (Número de página)
                        footerPar = (WParagraph)wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        footerPar.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = footerPar.AppendText("Hoja \t");
                        wText.CharacterFormat.Bold = false;
                        wText.CharacterFormat.FontName = "Times New Roman";
                        wText.CharacterFormat.FontSize = 10f;

                        footerPar.AppendField("Page", FieldType.FieldPage);

                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 270;

                        // Agregar el párrafo recién creado
                        oSection.AddParagraph();
                            
                    #endregion

                #endregion

                #region Nota al Inicio del Cuadernillo

                    if (ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "1") {

                        // Inicializaciones
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;
                        wTableRow.Cells[0].Width = 500;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        // Texto
                        wText = wTableCell.AppendText(ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString() + ENTER);
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();

                    }

                #endregion

                // Para cada partida del programa
                foreach( DataRow rowPrograma in ENTResponseGira.DataSetResponse.Tables[2].Rows ){

                    #region Cambio de Agrupación
                            
                        if ( AgrupacionActual != rowPrograma["ConfiguracionGrupo"].ToString().Trim() && rowPrograma["ConfiguracionGrupo"].ToString() != "[Sin agrupación]" ){

                            // Actualizar la agrupación
                            AgrupacionActual = rowPrograma["ConfiguracionGrupo"].ToString().Trim();

                            // Nueva sección
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 11f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText(AgrupacionActual);
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.Italic = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 11f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();
                        }
                        
                    #endregion

                    switch( rowPrograma["TipoGiraConfiguracionId"].ToString() ){
                        case "1":
                        case "4":

                            #region Traslado en vehículo / Actividad General
                                
                                wTable = oSection.Body.AddTable();
                                wTable.ResetCells(1, 2);

                                #region Fila 1
                                
                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText( rowPrograma["ConfiguracionHoraInicio24H"].ToString() + " A " + rowPrograma["ConfiguracionHoraFin24H"].ToString() + " HRS." );
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText(rowPrograma["ConfiguracionDetalle"].ToString());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                // Brinco de linea (genera espacio)
                                oSection.AddParagraph();
                                
                            #endregion
                            
                            break;

                        case "2":

                            // Partida a buscar
                            oENTGira.GiraConfiguracionId = Int32.Parse( rowPrograma["GiraConfiguracionId"].ToString() );
                                
                            // Consultar el detalle
                            ENTResponseGiraConfiguracion = new ENTResponse();
                            ENTResponseGiraConfiguracion = oBPGira.SelectGiraConfiguracion(oENTGira);

                            #region Traslado en helicóptero
                                
                                // Llenar encabezado
                                wTable = oSection.Body.AddTable();
                                wTable.ResetCells(4, 2);

                                #region Fila 1
                                
                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText( rowPrograma["ConfiguracionHoraInicio24H"].ToString() + " A " + rowPrograma["ConfiguracionHoraFin24H"].ToString() + " HRS." );
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText(rowPrograma["ConfiguracionDetalle"].ToString());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 2
                                
                                    wTableRow = wTable.Rows[1];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("LUGAR: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["HelipuertoLugar"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 3
                                
                                    wTableRow = wTable.Rows[2];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("DOMICILIO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["HelipuertoDomicilio"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 4
                                
                                    wTableRow = wTable.Rows[3];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("COORDENADAS: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["HelipuertoCoordenadas"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                // Brinco de linea (genera espacio)
                                oSection.AddParagraph();

                            #endregion

                            #region Comité de recepción
                                
                                if ( ENTResponseGiraConfiguracion.DataSetResponse.Tables[2].Rows.Count != 0) {
                                    
                                    // Inicializaciones
                                    wTable = oSection.Body.AddTable();
                                    wTable.ResetCells(1, 1);
                                    wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                    FilaOrden = 1;

                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    
                                    // Configuración de la tabla
                                    wtblListado = new WTable(oDocument, false);
                                    wtblListado.ResetCells(ENTResponseGiraConfiguracion.DataSetResponse.Tables[2].Rows.Count + 1, 1);
                                    wtblListado.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                                    // Encabezado
                                    tRow = wtblListado.Rows[0];
                                    tRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = tRow.Cells[0].AddParagraph();

                                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN EN EL HELIPUERTO:" + ENTER + ENTER);
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;

                                    tRow.Cells[0].Width = 500;

                                    // Cuerpo
                                    foreach (DataRow oRow in ENTResponseGiraConfiguracion.DataSetResponse.Tables[2].Rows){

                                        tRow = wtblListado.Rows[FilaOrden];
                                        tRow.Height = 17f;

                                        ListadoOrden = oRow["Orden"].ToString() + ". ";
                                        ListadoNombre = oRow["Nombre"].ToString();
                                        ListadoPuesto = ", " + oRow["Puesto"].ToString();

                                        // Celda 1
                                        wTableCell = tRow.Cells[0].AddParagraph();

                                        tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        //wText = wTableCell.AppendText(ListadoOrden);
                                        //wText.CharacterFormat.FontName = "Arial";
                                        //wText.CharacterFormat.FontSize = 10f;

                                        wText = wTableCell.AppendText(ListadoNombre);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        wText = wTableCell.AppendText(ListadoPuesto + ENTER);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;

                                        tRow.Cells[0].Width = 500;
                                        FilaOrden = FilaOrden + 1;
                                    }

                                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                    wTableRow.Cells[0].Tables.Add(wtblListado);

                                    wTableRow.Cells[0].Width = 510;

                                    // Brinco de linea (genera espacio)
                                    oSection.AddParagraph();

                                }

                            #endregion

                            break;

                        case "3":

                            // Partida a buscar
                            oENTGira.GiraConfiguracionId = Int32.Parse( rowPrograma["GiraConfiguracionId"].ToString() );
                                
                            // Consultar el detalle
                            ENTResponseGiraConfiguracion = new ENTResponse();
                            ENTResponseGiraConfiguracion = oBPGira.SelectGiraConfiguracion(oENTGira);

                            #region Evento
                                
                                #region Nota al Inicio del Evento
                                
                                    if ( rowPrograma["NotaInicioEvento"].ToString() == "1" ) {
                                    
                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText( rowPrograma["NotaEvento"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion
                                
                                // Llenar encabezado
                                wTable = oSection.Body.AddTable();
                                wTable.ResetCells(14, 2);

                                #region Fila 1
                                
                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText( rowPrograma["ConfiguracionHoraInicio24H"].ToString() + " A " + rowPrograma["ConfiguracionHoraFin24H"].ToString() + " HRS." );
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText(rowPrograma["ConfiguracionDetalle"].ToString());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 2
                                
                                    wTableRow = wTable.Rows[1];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("LUGAR: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 3
                                
                                    wTableRow = wTable.Rows[2];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("DOMICILIO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoLugarEvento"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 4
                                
                                    wTableRow = wTable.Rows[3];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("LUGAR DE ARRIBO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoLugarArribo"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion
                                
                                #region Fila 5
                                
                                    wTableRow = wTable.Rows[4];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("MEDIOS DE TRASLADO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["MediosTraslado"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 6
                                
                                    wTableRow = wTable.Rows[5];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("MONTAJE: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoTipoMontaje"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 7
                                
                                    wTableRow = wTable.Rows[6];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("AFORO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoAforo"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 8
                                
                                    wTableRow = wTable.Rows[7];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("CARACTERÍSTICAS DE INVITADOS: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoCaracteristicasInvitados"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 9
                                
                                    wTableRow = wTable.Rows[8];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("INVITACIÓN CON ESPOSA: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["InvitacionEsposa"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 10
                                
                                    wTableRow = wTable.Rows[9];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("MEDIOS DE COMUNICACIÓN: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoMedioComunicacion"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 11
                                
                                    wTableRow = wTable.Rows[10];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("VESTIMENTA: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["TipoVestimenta"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 12
                                
                                    wTableRow = wTable.Rows[11];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("MENÚ: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoMenu"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 13
                                
                                    wTableRow = wTable.Rows[12];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("PRONOSTICO CLIMÁTICO: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoPronosticoClima"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                #region Fila 14
                                
                                    wTableRow = wTable.Rows[13];
                                    wTableRow.Height = 6f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[0].Width = 120;

                                    // Celda 2
                                    wTableCell = wTableRow.Cells[1].AddParagraph();
                                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("ACCIÓN A REALIZAR: " + ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoAccionRealizar"].ToString().Trim());
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 9f;

                                    wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                    wTableRow.Cells[1].Width = 390;
                                
                                #endregion

                                // Brinco de linea (genera espacio)
                                oSection.AddParagraph();
                                
                                #region Nota al Fin del Evento
                                
                                    if ( rowPrograma["NotaFinEvento"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaEvento"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion
                                
                            #endregion

                            #region Comité de recepción
                                
                                #region Nota al Inicio del Comite
                                
                                    if ( rowPrograma["NotaInicioComite"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaComite"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion
                                
                                if ( ENTResponseGiraConfiguracion.DataSetResponse.Tables[4].Rows.Count != 0) {
                                    
                                    // Inicializaciones
                                    wTable = oSection.Body.AddTable();
                                    wTable.ResetCells(1, 1);
                                    wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                    FilaOrden = 1;

                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    
                                    // Configuración de la tabla
                                    wtblListado = new WTable(oDocument, false);
                                    wtblListado.ResetCells(ENTResponseGiraConfiguracion.DataSetResponse.Tables[4].Rows.Count + 1, 1);
                                    wtblListado.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                                    // Encabezado
                                    tRow = wtblListado.Rows[0];
                                    tRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = tRow.Cells[0].AddParagraph();

                                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN:" + ENTER + ENTER);
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;

                                    tRow.Cells[0].Width = 500;

                                    // Cuerpo
                                    foreach (DataRow oRow in ENTResponseGiraConfiguracion.DataSetResponse.Tables[4].Rows){

                                        tRow = wtblListado.Rows[FilaOrden];
                                        tRow.Height = 17f;

                                        ListadoOrden = oRow["Orden"].ToString() + ". ";
                                        ListadoNombre = oRow["Nombre"].ToString();
                                        ListadoPuesto = ", " + oRow["Puesto"].ToString();

                                        // Celda 1
                                        wTableCell = tRow.Cells[0].AddParagraph();

                                        tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        //wText = wTableCell.AppendText(ListadoOrden);
                                        //wText.CharacterFormat.FontName = "Arial";
                                        //wText.CharacterFormat.FontSize = 10f;

                                        wText = wTableCell.AppendText(ListadoNombre);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        wText = wTableCell.AppendText(ListadoPuesto + ENTER);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;

                                        tRow.Cells[0].Width = 500;
                                        FilaOrden = FilaOrden + 1;
                                    }

                                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                    wTableRow.Cells[0].Tables.Add(wtblListado);

                                    wTableRow.Cells[0].Width = 510;

                                    // Brinco de linea (genera espacio)
                                    oSection.AddParagraph();

                                }
                                
                                #region Nota al Fin del Comite
                                
                                    if ( rowPrograma["NotaFinComite"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaComite"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion

                            #endregion

                            #region Orden del día
                                
                                #region Nota al Inicio del Orden
                                
                                    if ( rowPrograma["NotaInicioOrden"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaOrden"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion

                                if ( ENTResponseGiraConfiguracion.DataSetResponse.Tables[5].Rows.Count != 0) {
                                    
                                    // Inicializaciones
                                    wTable = oSection.Body.AddTable();
                                    wTable.ResetCells(1, 1);
                                    wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                    FilaOrden = 1;

                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    
                                    // Configuración de la tabla
                                    wtblListado = new WTable(oDocument, false);
                                    wtblListado.ResetCells(ENTResponseGiraConfiguracion.DataSetResponse.Tables[5].Rows.Count + 1, 1);
                                    wtblListado.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                                    // Encabezado
                                    tRow = wtblListado.Rows[0];
                                    tRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = tRow.Cells[0].AddParagraph();

                                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText("ORDEN DEL DÍA:" + ENTER + ENTER);
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;

                                    tRow.Cells[0].Width = 500;

                                    // Cuerpo
                                    foreach (DataRow oRow in ENTResponseGiraConfiguracion.DataSetResponse.Tables[5].Rows){

                                        tRow = wtblListado.Rows[FilaOrden];
                                        tRow.Height = 17f;

                                        ListadoOrden = oRow["Orden"].ToString();
                                        ListadoPuesto = oRow["Detalle"].ToString();

                                        // Celda 1
                                        wTableCell = tRow.Cells[0].AddParagraph();

                                        tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        wText = wTableCell.AppendText(ListadoPuesto + ENTER);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        tRow.Cells[0].Width = 500;
                                        FilaOrden = FilaOrden + 1;
                                    }

                                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                    wTableRow.Cells[0].Tables.Add(wtblListado);

                                    wTableRow.Cells[0].Width = 510;

                                    // Brinco de linea (genera espacio)
                                    oSection.AddParagraph();

                                }
                                
                                #region Nota al Fin del Orden
                                
                                    if ( rowPrograma["NotaFinOrden"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaOrden"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion

                            #endregion

                            #region Acomodo
                            
                                #region Nota al Inicio del Acomodo
                                
                                    if ( rowPrograma["NotaInicioAcomodo"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaAcomodo"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion

                                if ( ENTResponseGiraConfiguracion.DataSetResponse.Tables[6].Rows.Count != 0) {
                                    
                                    // Inicializaciones
                                    wTable = oSection.Body.AddTable();
                                    wTable.ResetCells(1, 1);
                                    wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                    FilaOrden = 1;

                                    wTableRow = wTable.Rows[0];
                                    wTableRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = wTableRow.Cells[0].AddParagraph();
                                    
                                    // Configuración de la tabla
                                    wtblListado = new WTable(oDocument, false);
                                    wtblListado.ResetCells(ENTResponseGiraConfiguracion.DataSetResponse.Tables[6].Rows.Count + 1, 1);
                                    wtblListado.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                                    // Encabezado
                                    tRow = wtblListado.Rows[0];
                                    tRow.Height = 17f;

                                    // Celda 1
                                    wTableCell = tRow.Cells[0].AddParagraph();

                                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                    wText = wTableCell.AppendText(ENTResponseGiraConfiguracion.DataSetResponse.Tables[1].Rows[0]["EventoTipoAcomodo"].ToString() + ENTER + ENTER);
                                    wText.CharacterFormat.FontName = "Arial";
                                    wText.CharacterFormat.FontSize = 10f;
                                    wText.CharacterFormat.Bold = true;
                                    wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;

                                    tRow.Cells[0].Width = 500;

                                    // Cuerpo
                                    foreach (DataRow oRow in ENTResponseGiraConfiguracion.DataSetResponse.Tables[6].Rows){

                                        tRow = wtblListado.Rows[FilaOrden];
                                        tRow.Height = 17f;

                                        ListadoOrden = oRow["Orden"].ToString() + ". ";
                                        ListadoNombre = oRow["Nombre"].ToString();
                                        ListadoPuesto = ", " + oRow["Puesto"].ToString();

                                        // Celda 1
                                        wTableCell = tRow.Cells[0].AddParagraph();

                                        tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        //wText = wTableCell.AppendText(ListadoOrden);
                                        //wText.CharacterFormat.FontName = "Arial";
                                        //wText.CharacterFormat.FontSize = 10f;

                                        wText = wTableCell.AppendText(ListadoNombre);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        wText = wTableCell.AppendText(ListadoPuesto + ENTER);
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;

                                        tRow.Cells[0].Width = 500;
                                        FilaOrden = FilaOrden + 1;
                                    }

                                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                    wTableRow.Cells[0].Tables.Add(wtblListado);

                                    wTableRow.Cells[0].Width = 510;

                                    // Brinco de linea (genera espacio)
                                    oSection.AddParagraph();

                                }
                                
                                #region Nota al Fin del Acomodo
                                
                                    if ( rowPrograma["NotaFinAcomodo"].ToString() == "1" ) {

                                        // Inicializaciones
                                        wTable = oSection.Body.AddTable();
                                        wTable.ResetCells(1, 1);
                                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                        wTableRow = wTable.Rows[0];
                                        wTableRow.Height = 17f;
                                        wTableRow.Cells[0].Width = 500;

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();

                                        // Texto
                                        wText = wTableCell.AppendText(rowPrograma["NotaAcomodo"].ToString() + ENTER );
                                        wText.CharacterFormat.FontName = "Arial";
                                        wText.CharacterFormat.FontSize = 10f;
                                        wText.CharacterFormat.Bold = true;

                                        // Brinco de linea (genera espacio)
                                        oSection.AddParagraph();

                                    }

                                #endregion

                            #endregion
                            
                            break;

                    }
                }

                #region Nota al Final del Cuadernillo

                    if (ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "1") {

                        // Inicializaciones
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;
                        wTableRow.Cells[0].Width = 500;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        // Texto
                        wText = wTableCell.AppendText(ENTResponseGira.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString() + ENTER);
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();

                    }

                #endregion

                // Descargar el documeno en la página
                oDocument.Save("Gira.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);

            }catch (IOException ioEx) {
                throw ( new Exception(LevelError + "-" + ioEx.Message) );

            }catch (Exception ex) {
                throw ( new Exception(LevelError + "-" + ex.Message) );
            }
        }


        // Giras de la pagina

        protected void Page_Load(object sender, EventArgs e){
            Int32 GiraId;
            String Key = "";

            try
            {

                // Validaciones
                if ( this.Request.QueryString["key"] == null ) { return; }
                Key = GetKey( this.Request.QueryString["key"].ToString() );
                if (Key == "") { return; }
                GiraId = Int32.Parse(Key);

                // Construir cuadernillo
                CrearCuadernillo(GiraId);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

    }
}