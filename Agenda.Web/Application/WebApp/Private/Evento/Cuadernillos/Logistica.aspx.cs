/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	Logistica
' Autor:	Daniel.Chavez
' Fecha:	21-Febrero-2015
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


namespace Agenda.Web.Application.WebApp.Private.Evento.Cuadernillos
{
    public partial class Logistica : System.Web.UI.Page
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

        void CrearCuadernillo(Int32 EventoId){
            ENTEvento oENTEvento = new ENTEvento();
            ENTResponse oENTResponse = new ENTResponse();

            BPEvento oBPEvento = new BPEvento();

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
            String LevelError = "";

            try {

                // Formulario
                oENTEvento.EventoId = EventoId;

                // Consulta de información
                oENTResponse = oBPEvento.SelectEvento_Detalle(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }
                if (oENTResponse.DataSetResponse.Tables[1].Rows.Count == 0) { throw (new Exception("No se encontró la información del evento")); }

                // Nueva hoja de Word
                oSection = oDocument.AddSection();
                oSection.PageSetup.PageSize = new SizeF(612, 652);
                oSection.PageSetup.DifferentFirstPage = true;

                // Márgenes
                oSection.PageSetup.Margins.Bottom = 36f;    // 1.27 cm
                oSection.PageSetup.Margins.Left = 36f;      // 1.27 cm
                oSection.PageSetup.Margins.Right = 66.3f;   // 2.34 cm
                oSection.PageSetup.Margins.Top = 42.6f;     // 1.50 cm

                #region Encabezado de la primer página

                    LevelError = "Encabezado 1";

                    #region Logo y Fecha
                            
                        wTable = oSection.HeadersFooters.FirstPageHeader.AddTable();
                        wTable.ResetCells(1, 2); // 1 Fila 2 columnas

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1 (Logo)
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Logo.png"));
                        wPicture = wTableCell.AppendPicture(imgTemporal);
                        wPicture.Height = 50;
                        wPicture.Width = 249;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 350;

                        // Celda 2 (Fecha)
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
                        wText = wTableCell.AppendText("ACTUALIZACIÓN DE " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["FechaModificacion"].ToString());//bd
                        wText.CharacterFormat.Bold = false;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 8f;
                        wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 190;

                        // Agregar el párrafo recién creado
                        oSection.AddParagraph();
                            
                    #endregion

                    #region Separador

                        wTable = oSection.HeadersFooters.FirstPageHeader.AddTable();
                        wTable.ResetCells(1, 1); // 1 Fila 1 columnas

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1 (Separador)
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Separador_NL.png"));
                        wPicture = wTableCell.AppendPicture(imgTemporal);
                        wPicture.Height = 5;
                        wPicture.Width = 510;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;
                            
                    #endregion
                    
                    #region Número de página

                        footerPar = new WParagraph(oDocument);
                        footerPar.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
                        footerPar.AppendText("\t");
                        footerPar.AppendField("Page", FieldType.FieldPage);
                        oSection.HeadersFooters.FirstPageFooter.Paragraphs.Add(footerPar);
                            
                    #endregion

                #endregion

                #region Encabezado del resto del documento
                    
                    LevelError = "Encabezado 2";

                    #region Logo y Fecha
                        
                        wTable = oSection.HeadersFooters.Header.AddTable();
                        wTable.ResetCells(1, 2); // 1 Fila 2 columnas


                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1 (Logo)
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Logo.png"));
                        wPicture = wTableCell.AppendPicture(imgTemporal);
                        wPicture.Height = 50;
                        wPicture.Width = 249;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 350;

                        // Celda 2 (vacía)
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 190;

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

                        imgTemporal = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Separador_NL.png"));
                        wPicture = wTableCell.AppendPicture(imgTemporal);
                        wPicture.Height = 5;
                        wPicture.Width = 510;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;

                    #endregion
                    
                    #region Número de página

                        footerPar = new WParagraph(oDocument);
                        footerPar.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
                        footerPar.AppendText("\t");
                        footerPar.AppendField("Page", FieldType.FieldPage);
                        oSection.HeadersFooters.Footer.Paragraphs.Add(footerPar);
                            
                    #endregion

                #endregion

                #region Sección: NombreEvento
                    
                    LevelError = "Evento";

                    #region Label_NombreSeccion
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 21f;

                        //Encabezado
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        wText = wTableCell.AppendText("NOMBRE DEL EVENTO");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 14f;
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 510;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                        
                    #endregion

                    #region NombreEvento
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 510;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                        
                    #endregion

                    #region FechaEvento
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 4);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("FECHA:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 50;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaLarga"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 200;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("HORARIO:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 100;

                        // Celda 4
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHorario"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].Width = 160;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    #endregion

                    #region Pronostico
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 6);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("PRONÓSTICO CLIMÁTICO:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 150;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PronosticoClima"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 180;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("MÍNIMA:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 50;

                        // Celda 4
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMinima"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].Width = 39;

                        // Celda 5
                        wTableCell = wTableRow.Cells[4].AddParagraph();
                        wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("MÁXIMA:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].Width = 54;

                        // Celda 6
                        wTableCell = wTableRow.Cells[5].AddParagraph();
                        wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMaxima"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].Width = 37;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    #endregion

                    #region Lugar
                    
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(3, 2);

                        #region Lugar
                            
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("LUGAR:");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].Width = 100;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString());
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 410;
                            
                        #endregion

                        #region Domicilio
                        
                            wTableRow = wTable.Rows[1];
                            wTableRow.Height = 20f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("DOMICILIO:");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].Width = 100;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoCompleto"].ToString());
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 410;
                        
                        #endregion

                        #region LugarArribo
                        
                            wTableRow = wTable.Rows[2];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("LUGAR DE ARRIBO:");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].Width = 130;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["LugarArribo"].ToString());
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 380;
                        
                        #endregion

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();

                    #endregion

                    #region MedioTraslado
                    
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 9);

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("MEDIO DE TRASLADO:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 123;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows){
                            if (oRow["MedioTrasladoId"].ToString() == "1"){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                        }
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 10;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("HELICÓPTERO");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 90;

                        // Celda 4
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows){
                            if (oRow["MedioTrasladoId"].ToString() == "2"){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                        }
                        wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].Width = 10;

                        // Celda 5
                        wTableCell = wTableRow.Cells[4].AddParagraph();
                        wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("VEHÍCULO");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].Width = 90;

                        // Celda 6
                        wTableCell = wTableRow.Cells[5].AddParagraph();
                        wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows){
                            if (oRow["MedioTrasladoId"].ToString() == "3"){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                        }
                        wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].Width = 10;

                        // Celda 7
                        wTableCell = wTableRow.Cells[6].AddParagraph();
                        wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("INFANTERÍA");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].Width = 90;

                        // Celda 8
                        wTableCell = wTableRow.Cells[7].AddParagraph();
                        wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows){
                            if (oRow["MedioTrasladoNombre"].ToString() == "Otro"){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                        }
                        wTableRow.Cells[7].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[7].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].Width = 10;

                        //col 9
                        wTableCell = wTableRow.Cells[8].AddParagraph();
                        wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("OTRO");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].Width = 70;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    #endregion

                    #region TipoMontaje
                    
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 2);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("TIPO MONTAJE:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 110;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 400;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    #endregion

                    #region Aforo
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 4);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("AFORO:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 50;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 50;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("CARACTERÍSTICAS DE INVITADOS");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 200;

                        // Celda 4
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["CaracteristicasInvitados"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].Width = 210;
                    
                    #endregion

                    #region InvitacionEsposa
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 9);

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("INVITACIÓN CON ESPOSA:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 170;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText((int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 ? "SI" : "NO"));
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 30;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("ASISTE:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 75;

                        // Celda 4
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaSi"].ToString()) == 1){
                            wText = wTableCell.AppendText("X");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                        }
                        wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[3].Width = 15;

                        // Celda 5
                        wTableCell = wTableRow.Cells[4].AddParagraph();
                        wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("SI");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[4].Width = 50;

                        // Celda 6
                        wTableCell = wTableRow.Cells[5].AddParagraph();
                        wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaNo"].ToString()) == 1){
                            wText = wTableCell.AppendText("X");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                        }
                        wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[5].Width = 15;

                        // Celda 7
                        wTableCell = wTableRow.Cells[6].AddParagraph();
                        wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("NO");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[6].Width = 50;

                        // Celda 8
                        wTableCell = wTableRow.Cells[7].AddParagraph();
                        wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaConfirma"].ToString()) == 1){
                            wText = wTableCell.AppendText("X");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                        }
                        wTableRow.Cells[7].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[7].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[7].Width = 15;

                        //col 9
                        wTableCell = wTableRow.Cells[8].AddParagraph();
                        wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("PENDIENTE");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[8].Width = 90;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    #endregion

                    #region MediosComunicacion
                    
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 2);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("MEDIOS DE COMUNICACIÓN:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 200;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionNombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 310;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();

                    #endregion

                    #region Vestimenta
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(2, 7);

                        #region Fila1
                            
                            wTableRow = wTable.Rows[0];

                            // Merge con la celda de abajo
                            wTable.Rows[0].Cells[0].CellFormat.VerticalMerge = CellMerge.Start;
                            wTable.Rows[1].Cells[0].CellFormat.VerticalMerge = CellMerge.Continue;
                            wTable.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("VESTIMENTA:");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].Width = 80;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 1){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 20;

                            // Celda 3
                            wTableCell = wTableRow.Cells[2].AddParagraph();
                            wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("GALA");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].Width = 120;

                            // Celda 4
                            wTableCell = wTableRow.Cells[3].AddParagraph();
                            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 2){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].Width = 20;

                            // Celda 5
                            wTableCell = wTableRow.Cells[4].AddParagraph();
                            wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("FORMAL");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].Width = 120;

                            // Celda 6
                            wTableCell = wTableRow.Cells[5].AddParagraph();
                            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 3){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].Width = 20;

                            // Celda 7
                            wTableCell = wTableRow.Cells[6].AddParagraph();
                            wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("FORMAL (TRAJE OSCURO)");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].Width = 130;

                        #endregion

                        #region Fila2
                            
                            wTableRow = wTable.Rows[1];

                            // Celda 1 (solo formato)
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].Width = 80;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 4){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 20;

                            // Celda 3
                            wTableCell = wTableRow.Cells[2].AddParagraph();
                            wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("CASUAL (SACO SIN CORBATA) ");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[2].Width = 120;

                            // Celda 4
                            wTableCell = wTableRow.Cells[3].AddParagraph();
                            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 5){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[3].Width = 20;

                            // Celda 5
                            wTableCell = wTableRow.Cells[4].AddParagraph();
                            wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("CASUAL (SIN SACO)");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[4].Width = 120;

                            // Celda 6
                            wTableCell = wTableRow.Cells[5].AddParagraph();
                            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 6){
                                wText = wTableCell.AppendText("X");
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            }
                            wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[5].Width = 20;

                            // Celda 7
                            wTableCell = wTableRow.Cells[6].AddParagraph();
                            wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText((oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString() == "6" ? oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaOtro"].ToString() : "OTRO"));
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[6].Width = 130;

                        #endregion

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                        
                    #endregion

                    #region Menu
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 2);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("MENÚ:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 50;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Menu"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 460;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                        
                    #endregion

                    #region AcciónARealizar
                        
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 2);
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText("ACCIÓN A REALIZAR:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 150;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["AccionRealizar"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 360;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                        
                    #endregion

                #endregion

                #region Sección: Comite de Recepcion en el Helipuerto Provisional
                    
                    LevelError = "Comité Helipuerto";
                    
                    if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ComiteHelipuerto"].ToString()) != 0){

                        #region Label_NombreSeccion
                    
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 21f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN EN EL HELIPUERTO PROVISIONAL");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 14f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();
                    
                        #endregion

                        #region Comité
                        
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            WTable tComiteRecepcion;

                            tComiteRecepcion = new WTable(oDocument, false);
                            tComiteRecepcion.ResetCells(oENTResponse.DataSetResponse.Tables[8].Rows.Count, 1);
                            tComiteRecepcion.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                            int FilaHelipuerto = 0;
                            string OrdenHelipuerto;
                            string NombreHelipuerto;
                            string PuestoHelipuerto;

                            foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[8].Rows){
                                tRow = tComiteRecepcion.Rows[FilaHelipuerto];
                                tRow.Height = 17f;

                                OrdenHelipuerto = oRow["Orden"].ToString() + ". ";
                                NombreHelipuerto = oRow["Nombre"].ToString();
                                PuestoHelipuerto = ", " + oRow["Puesto"].ToString() + ".";

                                // Celda 1
                                wTableCell = tRow.Cells[0].AddParagraph();
                                tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText(OrdenHelipuerto);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wText = wTableCell.AppendText(NombreHelipuerto);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                                wText.CharacterFormat.Bold = true;

                                wText = wTableCell.AppendText(PuestoHelipuerto + ENTER);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            
                                tRow.Cells[0].Width = 500;

                                FilaHelipuerto = FilaHelipuerto + 1;
                            }

                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wTableRow.Cells[0].Tables.Add(tComiteRecepcion);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                        
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion

                        #region Ubicación
                            
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(3, 2);

                            #region Fila 1
                                
                                wTableRow = wTable.Rows[0];
                                wTableRow.Height = 6f;

                                // Celda 1
                                wTableCell = wTableRow.Cells[0].AddParagraph();
                                wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[0].Width = 170;

                                // Celda 2
                                wTableCell = wTableRow.Cells[1].AddParagraph();
                                wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText("LUGAR: " + oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoLugar"].ToString());
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[1].Width = 340;
                                
                            #endregion

                            #region Fila 2
                                
                                wTableRow = wTable.Rows[1];
                                wTableRow.Height = 6f;

                                // Celda 1
                                wTableCell = wTableRow.Cells[0].AddParagraph();
                                wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[0].Width = 170;

                                // Celda 2
                                wTableCell = wTableRow.Cells[1].AddParagraph();
                                wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText("DOMICILIO: " + oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoDomicilio"].ToString());
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[1].Width = 340;
                                
                            #endregion
                            
                            #region Fila 2
                                
                                wTableRow = wTable.Rows[2];
                                wTableRow.Height = 6f;

                                // Celda 1
                                wTableCell = wTableRow.Cells[0].AddParagraph();
                                wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[0].Width = 170;

                                // Celda 2
                                wTableCell = wTableRow.Cells[1].AddParagraph();
                                wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText("COORDENADAS: " + oENTResponse.DataSetResponse.Tables[6].Rows[0]["HelipuertoCoordenadas"].ToString());
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[1].Width = 340;
                                
                            #endregion

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion

                    }

                #endregion
                
                #region Sección: Comite de Recepcion
                    
                    LevelError = "Comité Recepción";
                    
                    if( oENTResponse.DataSetResponse.Tables[8].Rows.Count > 0 ){

                        #region Label_NombreSeccion
                    
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 21f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 14f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();
                    
                        #endregion

                        #region Comité
                        
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            WTable tComiteRecepcion;

                            tComiteRecepcion = new WTable(oDocument, false);
                            tComiteRecepcion.ResetCells(oENTResponse.DataSetResponse.Tables[8].Rows.Count, 1);
                            tComiteRecepcion.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                            int Fila = 0;
                            string Orden;
                            string Nombre;
                            string Puesto;

                            foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[8].Rows){
                                tRow = tComiteRecepcion.Rows[Fila];
                                tRow.Height = 17f;

                                Orden = oRow["Orden"].ToString() + ". ";
                                Nombre = oRow["Nombre"].ToString();
                                Puesto = ", " + oRow["puesto"].ToString() + ".";

                                // Celda 1
                                wTableCell = tRow.Cells[0].AddParagraph();
                                tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText(Orden);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wText = wTableCell.AppendText(Nombre);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                                wText.CharacterFormat.Bold = true;

                                wText = wTableCell.AppendText(Puesto + ENTER);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                            
                                tRow.Cells[0].Width = 500;

                                Fila = Fila + 1;
                            }

                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wTableRow.Cells[0].Tables.Add(tComiteRecepcion);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                        
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion

                    }

                #endregion
                
                #region Orden del Día

                    LevelError = "Orden del Día";
                    
                    if( oENTResponse.DataSetResponse.Tables[9].Rows.Count > 0 ){
                        
                        #region Label_NombreSeccion

                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 21f;


                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText("ORDEN DEL DÍA");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 14f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();
                            
                        #endregion

                        #region Comite

                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            WTable t;

                            t = new WTable(oDocument, false);
                            t.ResetCells(oENTResponse.DataSetResponse.Tables[9].Rows.Count, 1);
                            t.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                            int FilaOrdenDia = 0;
                            string OrdenDia;
                            string Detalle;

                            foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[9].Rows){

                                tRow = t.Rows[FilaOrdenDia];
                                tRow.Height = 17f;

                                OrdenDia = oRow["Orden"].ToString() + ". ";
                                Detalle = oRow["Detalle"].ToString();

                                // Celda 1
                                wTableCell = tRow.Cells[0].AddParagraph();
                                tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                                wText = wTableCell.AppendText(OrdenDia + Detalle + ENTER);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                                wText.CharacterFormat.Bold = true;

                                tRow.Cells[0].Width = 500;
                                FilaOrdenDia = FilaOrdenDia + 1;

                            }

                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wTableRow.Cells[0].Tables.Add(t);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;

                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion

                    }
                    
                #endregion
                
                #region Acomodo

                    LevelError = "Acomodo";
                    
                    if( oENTResponse.DataSetResponse.Tables[10].Rows.Count > 0 ){

                        #region Label_NombreSeccion
                    
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 21f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoNombre"].ToString());
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 14f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();
                    
                        #endregion

                        #region Acomodo
                        
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();

                            WTable tAcomodo;

                            tAcomodo = new WTable(oDocument, false);
                            tAcomodo.ResetCells(oENTResponse.DataSetResponse.Tables[10].Rows.Count, 1);
                            tAcomodo.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                            int FilaAcomodo = 0;
                            string OrdenAcomodo;
                            string NombreAcomodo;
                            string PuestoAcomodo;

                            foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows){

                                tRow = tAcomodo.Rows[FilaAcomodo];
                                tRow.Height = 17f;

                                OrdenAcomodo = oRow["Orden"].ToString() + ". ";
                                NombreAcomodo = oRow["Nombre"].ToString();
                                PuestoAcomodo = ", " + oRow["Puesto"].ToString();

                                // Celda 1
                                wTableCell = tRow.Cells[0].AddParagraph();

                                tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                wText = wTableCell.AppendText(OrdenAcomodo);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;

                                wText = wTableCell.AppendText(NombreAcomodo);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;
                                wText.CharacterFormat.Bold = true;

                                wText = wTableCell.AppendText(PuestoAcomodo + ENTER);
                                wText.CharacterFormat.FontName = "Arial";
                                wText.CharacterFormat.FontSize = 10f;



                                tRow.Cells[0].Width = 500;
                                FilaAcomodo = FilaAcomodo + 1;
                            }

                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wTableRow.Cells[0].Tables.Add(tAcomodo);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;

                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion

                        #region Observaciones
                            
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);

                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 17f;
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            
                            wText = wTableCell.AppendText("OBSERVACIONES: ");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wText.CharacterFormat.Bold = true;

                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["AcomodoObservaciones"].ToString());
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;

                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                        #endregion
                    
                    }

                #endregion
                
                #region Responsables

                    LevelError = "Responsables";

                    Int32 Responsables;
                    Int32 CurrentResponsable;
                    
                    wTable = oSection.Body.AddTable();
                    wTable.ResetCells(2, 2);

                    //Fila 1
                    wTableRow = wTable.Rows[0];
                    wTableRow.Height = 30f;
                    wTableCell = wTableRow.Cells[0].AddParagraph();
                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                    wText = wTableCell.AppendText("RESPONSABLE DEL EVENTO:");
                    wText.CharacterFormat.FontName = "Arial";
                    wText.CharacterFormat.FontSize = 10f;
                    wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[0].Width = 170;

                    // Celda 2
                    wTableCell = wTableRow.Cells[1].AddParagraph();
                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                    Responsables = oENTResponse.DataSetResponse.Tables[11].Rows.Count;
                    CurrentResponsable = 1;
                    foreach( DataRow drResponsable in oENTResponse.DataSetResponse.Tables[11].Rows ){
                        
                        wText = wTableCell.AppendText(drResponsable["Nombre"].ToString() + ", ");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wText = wTableCell.AppendText( drResponsable["Puesto"].ToString() + ( CurrentResponsable  < Responsables ? ENTER.ToString() : "" ) );
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        CurrentResponsable = CurrentResponsable + 1;
                    }
                    
                    wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[1].Width = 340;

                    //Fila 2
                    wTableRow = wTable.Rows[1];
                    wTableRow.Height = 30f;
                    wTableCell = wTableRow.Cells[0].AddParagraph();
                    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                    wText = wTableCell.AppendText("RESPONSABLE DE LOGÍSTICA:");
                    wText.CharacterFormat.FontName = "Arial";
                    wText.CharacterFormat.FontSize = 10f;
                    wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[0].Width = 170;

                    // Celda 2
                    wTableCell = wTableRow.Cells[1].AddParagraph();
                    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                    Responsables = oENTResponse.DataSetResponse.Tables[12].Rows.Count;
                    CurrentResponsable = 1;
                    foreach( DataRow drResponsableLogistica in oENTResponse.DataSetResponse.Tables[12].Rows ){
                        
                        wText = wTableCell.AppendText(drResponsableLogistica["Nombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wText = wTableCell.AppendText( "(" + drResponsableLogistica["Contacto"].ToString() + ")" + ( CurrentResponsable  < Responsables ? ENTER.ToString() : "" ) );
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        CurrentResponsable = CurrentResponsable + 1;
                    }
                    
                    wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                    wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                    wTableRow.Cells[1].Width = 340;

                    // Brinco de linea (genera espacio)
                    oSection.AddParagraph();
                    
                #endregion

                #region Propuesta de Acomodo

                    LevelError = "Propuesta Acomodo";
                    
                    if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PropuestaAcomodo"].ToString()) != 0){

                        oSection = oDocument.AddSection();
                        oSection.PageSetup.PageSize = new SizeF(612, 652);
                        oSection.PageSetup.Margins.Bottom = 28f;
                        oSection.PageSetup.Margins.Left = 56f;
                        oSection.PageSetup.Margins.Right = 28f;
                        oSection.PageSetup.Margins.Top = 28f;

                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(2, 1);

                        //Fila 1
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 20f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        wText = wTableCell.AppendText("ESTRADO");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 14f;
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;

                        //Fila 2
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 20f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                        wText = wTableCell.AppendText("(PROPUESTA DE ACOMODO)");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;

                        // imagen
                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        
                        //Fila 1
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 20f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image imgAcomodo = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/puestoAcomodo.png"));
                        wPicture = wTableCell.AppendPicture(imgAcomodo);
                        wPicture.Height = 50;
                        wPicture.Width = 510;

                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 510;

                        oSection.AddParagraph();

                        wTable = oSection.Body.AddTable();
                        wTable.ResetCells(2, 11);

                        //Fila 1 columna 1
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla10 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla10.png"));
                        wPicture = wTableCell.AppendPicture(silla10);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[0].Width = 46;

                        //Fila 1 columna 2
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla8 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla8.png"));
                        wPicture = wTableCell.AppendPicture(silla8);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[1].Width = 46;

                        //Fila 1 columna 3
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla6 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla6.png"));
                        wPicture = wTableCell.AppendPicture(silla6);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[2].Width = 46;

                        //Fila 1 columna 4
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla4 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla4.png"));
                        wPicture = wTableCell.AppendPicture(silla4);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[3].Width = 46;

                        //Fila 1 columna 5
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[4].AddParagraph();
                        wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla2 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla2.png"));
                        wPicture = wTableCell.AppendPicture(silla2);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[4].Width = 46;

                        //Fila 1 columna 6
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[5].AddParagraph();
                        wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla1 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla1.png"));
                        wPicture = wTableCell.AppendPicture(silla1);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[5].Width = 46;

                        //Fila 1 columna 7
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[6].AddParagraph();
                        wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla3 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla3.png"));
                        wPicture = wTableCell.AppendPicture(silla3);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[6].Width = 46;

                        //Fila 1 columna 8
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[7].AddParagraph();
                        wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla5 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla5.png"));
                        wPicture = wTableCell.AppendPicture(silla5);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[7].Width = 46;

                        //Fila 1 columna 9
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[8].AddParagraph();
                        wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla7 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla7.png"));
                        wPicture = wTableCell.AppendPicture(silla7);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[8].Width = 46;

                        //Fila 1 columna 10
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[9].AddParagraph();
                        wTableRow.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla9 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla9.png"));
                        wPicture = wTableCell.AppendPicture(silla9);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[9].Width = 46;

                        //Fila 1 columna 10
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 10f;
                        wTableCell = wTableRow.Cells[10].AddParagraph();
                        wTableRow.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        System.Drawing.Image silla11 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla11.png"));
                        wPicture = wTableCell.AppendPicture(silla11);
                        wPicture.Height = 64;
                        wPicture.Width = 28;

                        wTableRow.Cells[10].Width = 46;

                        String Acomodo1 = "", Acomodo2 = "", Acomodo3 = "", Acomodo4 = "", Acomodo5 = "", Acomodo6 = "", Acomodo7 = "", Acomodo8 = "", Acomodo9 = "", Acomodo10 = "", Acomodo11 = "";
                        String Puesto1 = "", Puesto2 = "", Puesto3 = "", Puesto4 = "", Puesto5 = "", Puesto6 = "", Puesto7 = "", Puesto8 = "", Puesto9 = "", Puesto10 = "", Puesto11 = "";

                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows){

                            if (int.Parse(oRow["Orden"].ToString()) == 1){

                                Acomodo1 = oRow["Nombre"].ToString() + ", ";
                                Puesto1 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 2){

                                Acomodo2 = oRow["Nombre"].ToString() + ", ";
                                Puesto2 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 3){

                                Acomodo3 = oRow["Nombre"].ToString() + ", ";
                                Puesto3 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 4){

                                Acomodo4 = oRow["Nombre"].ToString() + ", ";
                                Puesto4 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 5){

                                Acomodo5 = oRow["Nombre"].ToString() + ", ";
                                Puesto5 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 6){

                                Acomodo6 = oRow["Nombre"].ToString() + ", ";
                                Puesto6 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 7){

                                Acomodo7 = oRow["Nombre"].ToString() + ", ";
                                Puesto7 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 8){

                                Acomodo8 = oRow["Nombre"].ToString() + ", ";
                                Puesto8 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 9){

                                Acomodo9 = oRow["Nombre"].ToString() + ", ";
                                Puesto9 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 10){

                                Acomodo10 = oRow["Nombre"].ToString() + ", ";
                                Puesto10 = oRow["Puesto"].ToString();
                            }

                            if (int.Parse(oRow["Orden"].ToString()) == 11){

                                Acomodo11 = oRow["Nombre"].ToString() + ", ";
                                Puesto11 = oRow["Puesto"].ToString();
                            }

                        }

                        //Fila 2 Columna 1
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[0].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo10 == "" ? " " : Acomodo10));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto10 == "" ? " " : Puesto10));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[0].Width = 46;

                        //Fila 2 Columna 2
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[1].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo8 == "" ? " " : Acomodo8));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto8 == "" ? " " : Puesto8));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[1].Width = 46;

                        //Fila 2 Columna 3
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[2].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo6 == "" ? " " : Acomodo6));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto6 == "" ? " " : Puesto6));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[2].Width = 46;

                        //Fila 2 Columna 4
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[3].AddParagraph();
                        wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[3].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo4 == "" ? " " : Acomodo4));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto4 == "" ? " " : Puesto4));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[3].Width = 46;

                        //Fila 2 Columna 5
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[4].AddParagraph();
                        wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[4].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo2 == "" ? " " : Acomodo2));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto2 == "" ? " " : Puesto2));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[4].Width = 46;

                        //Fila 2 Columna 6
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[5].AddParagraph();
                        wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[5].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo1 == "" ? " " : Acomodo1));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto1 == "" ? " " : Puesto1));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[5].Width = 46;

                        //Fila 2 Columna 77
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[6].AddParagraph();
                        wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[6].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo3 == "" ? " " : Acomodo3));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto3 == "" ? " " : Puesto3));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[6].Width = 46;

                        //Fila 2 Columna 8
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[7].AddParagraph();
                        wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[7].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo5 == "" ? " " : Acomodo5));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto5 == "" ? " " : Puesto5));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[7].Width = 46;

                        //Fila 2 Columna 9
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[8].AddParagraph();
                        wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[8].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo7 == "" ? " " : Acomodo7));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto7 == "" ? " " : Puesto7));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[8].Width = 46;

                        //Fila 2 Columna 10
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[9].AddParagraph();
                        wTableRow.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[9].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo9 == "" ? " " : Acomodo9));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto9 == "" ? " " : Puesto9));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[9].Width = 46;

                        //Fila 2 Columna 11
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 300f;
                        wTableCell = wTableRow.Cells[10].AddParagraph();
                        wTableRow.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[10].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText((Acomodo11 == "" ? " " : Acomodo11));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        wText = wTableCell.AppendText((Puesto11 == "" ? " " : Puesto11));
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[10].Width = 46;

                        // Brinco de linea (genera espacio)
                        oSection.AddParagraph();
                    
                    }

                #endregion
                
                #region Imágen de Montaje

                    LevelError = "Imagen Montaje";
                
                    if (oENTResponse.DataSetResponse.Tables[13].Rows.Count > 0){
                    
                        oSection = oDocument.AddSection();
                        oSection.PageSetup.PageSize = new SizeF(612, 652);
                        oSection.PageSetup.Margins.Bottom = 36f;
                        oSection.PageSetup.Margins.Left = 36f;
                        oSection.PageSetup.Margins.Right = 66f;
                        oSection.PageSetup.Margins.Top = 42f;

                        #region Label_NombreSeccion
                            
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 20f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            wText = wTableCell.AppendText("MONTAJE");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 14f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                            wTableRow.Cells[0].Width = 510;
                            
                        #endregion

                        #region Tipo
                            
                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 2);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 24f;

                            // Celda 1
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("TIPO:");
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[0].Width = 150;

                            // Celda 2
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString());
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 10f;
                            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                            wTableRow.Cells[1].Width = 360;

                            // Brinco de linea (genera espacio)
                            oSection.AddParagraph();

                            wTable = oSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                            //Fila 1 columna 1
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 400f;
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                            if (System.IO.File.Exists(oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString())){
                                System.Drawing.Image imgMontage = System.Drawing.Image.FromFile(oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString());
                                wPicture = wTableCell.AppendPicture(imgMontage);
                            }
                            wTableRow.Cells[0].Width = 510;

                            oSection.AddParagraph();
                            
                        #endregion
                    
                    }

                #endregion

                // Descargar el documeno en la página
                oDocument.Save("EventoLogistica.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);

            }catch (Exception ex) {
                throw ( new Exception(LevelError + "-" + ex.Message) );
            }
        }


        // Eventos de la pagina

        protected void Page_Load(object sender, EventArgs e){
            Int32 EventoId;
            String Key = "";

            try
            {

                // Validaciones
                if ( this.Request.QueryString["key"] == null ) { return; }
                Key = GetKey( this.Request.QueryString["key"].ToString() );
                if (Key == "") { return; }
                EventoId = Int32.Parse(Key);

                // Construir cuadernillo
                CrearCuadernillo(EventoId);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
        }

    }
}