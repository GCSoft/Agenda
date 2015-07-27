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
    public partial class Protocolo : System.Web.UI.Page
    {
       

        // Utilerías
        GCEncryption gcEncryption = new GCEncryption();
        GCJavascript gcJavascript = new GCJavascript();
        

        // Variables públicas
        public String ENTER_KEY = Convert.ToChar(13).ToString();

        
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

            WordDocument WDocument = new WordDocument();
            IWSection WSection;

            IWTable wTable;
            WTableRow wTableRow;
            IWParagraph wTableCell;
            IWTextRange wText;

            String LevelError = "";
            Int32 CurrentRow;

            Char ENTER = Convert.ToChar(13);

            try {

                // Formulario
                oENTEvento.EventoId = EventoId;

                // Consulta de información
                oENTResponse = oBPEvento.SelectEvento_Detalle(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }
                if (oENTResponse.DataSetResponse.Tables[1].Rows.Count == 0) { throw (new Exception("No se encontró la información del evento")); }

                // Nuevo documento de Word
                WSection = WDocument.AddSection();
                WSection.PageSetup.PageSize = new SizeF(612, 790);  // 21.59 cm X 27.94 cm

                // Márgenes
                WSection.PageSetup.Margins.Bottom = 14.2f;  // 0.5 cm
                WSection.PageSetup.Margins.Left = 17.8f;    // 0.63 cm
                WSection.PageSetup.Margins.Right = 85.1f;   // 3 cm
                WSection.PageSetup.Margins.Top = 21.2f;     // 0.75 cm

                #region Nota al Inicio del Cuadernillo

                    if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaInicioDocumento"].ToString() == "1") {

                        // Inicializaciones
                        wTable = WSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTable.IndentFromLeft = 28.3f;      // 1 cm de sangría
                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 21f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;
                        wTableRow.Cells[0].Width = 530;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        // Texto
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString() + ENTER);
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        // Brinco de linea (genera espacio)
                        WSection.AddParagraph();

                    }

                #endregion

                #region Evento
                    
                    // Configuración general de la tabla
                    wTable = WSection.Body.AddTable();
                    wTable.ResetCells(44, 3);           // 44 filas X 3 Columnas
                    wTable.IndentFromLeft = 28.3f;      // 1 cm de sangría

                    #region Encabezado
                    
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 21f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                    
                        wText = wTableCell.AppendText("DIRECCION DE PROTOCOLO");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 20f;
                        wText.CharacterFormat.Bold = true;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio
                    
                        wTableRow = wTable.Rows[1];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[2];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Fecha y Hora del Evento

                        LevelError = "Fecha y Hora del Evento";
                        wTableRow = wTable.Rows[3];
                        wTableRow.Height = 30f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Fecha del" + ENTER_KEY + "Evento:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaLarga"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText("Hora: " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHorarioInicio"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Nombre del Evento

                        LevelError = "Nombre del Evento";
                        wTableRow = wTable.Rows[4];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Nombre del" + ENTER_KEY + "Evento:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 14f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText("DURACIÓN" + ENTER_KEY + oENTResponse.DataSetResponse.Tables[1].Rows[0]["Duracion"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 9f;
                    
                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio
                    
                        wTableRow = wTable.Rows[5];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Lugar

                        LevelError = "Lugar";
                        wTableRow = wTable.Rows[6];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Lugar:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[7];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Domicilio

                        LevelError = "Domicilio";
                        wTableRow = wTable.Rows[8];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Domicilio:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["DireccionEventoCompleto"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio
                    
                        wTableRow = wTable.Rows[9];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Invitación A

                        LevelError = "Invitación A";
                        wTableRow = wTable.Rows[10];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Invitación a:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloInvitacionA"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[11];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Aforo

                        LevelError = "Aforo";
                        wTableRow = wTable.Rows[12];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Aforo:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString() + " PERSONAS");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Vestimenta

                        LevelError = "Vestimenta";
                        wTableRow = wTable.Rows[13];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText("Vestimenta:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText( ( oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString() == "6" ? oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaOtro"].ToString() : oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaNombre"].ToString() ) );
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText("  X");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;

                        wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[14];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Responsable del Evento

                        LevelError = "Responsable del Evento";
                        wTableRow = wTable.Rows[15];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Responsable de" + ENTER_KEY + "evento:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsableEvento"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 9f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio
                    
                        wTableRow = wTable.Rows[16];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Prensa

                        LevelError = "Prensa";
                        wTableRow = wTable.Rows[17];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Prensa:");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionNombre"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[18];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Acomodo

                        LevelError = "Acomodo";
                        wTableRow = wTable.Rows[19];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoNombre"].ToString() + ENTER_KEY + ENTER_KEY + ENTER_KEY);
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows){

                            wText = wTableCell.AppendText(ENTER_KEY + oRow["Orden"].ToString() + ".- " + oRow["Nombre"].ToString() + ENTER_KEY);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 9f;
                            wText.CharacterFormat.Bold = true;

                            wText = wTableCell.AppendText(oRow["Puesto"].ToString() + ENTER_KEY);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 9f;
                        }
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Orden del Día

                        LevelError = "Orden del Día";
                        wTableRow = wTable.Rows[20];
                        wTableRow.Height = 25f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Orden del Día");
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[9].Rows){

                            wText = wTableCell.AppendText(oRow["Orden"].ToString() + ". " + oRow["Detalle"].ToString() + ENTER_KEY + ENTER_KEY);
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 12f;
                        }

                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[21];
                        wTableRow.Height = 30f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Titulo Requerimientos
                    
                        wTableRow = wTable.Rows[22];
                        wTableRow.Height = 23f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                        wText = wTableCell.AppendText("REQUERIMIENTOS TÉCNICOS Y MONTAJE");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 13f;
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.Italic = true;

                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[23];
                        wTableRow.Height = 35f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Banderas

                        LevelError = "Banderas";
                        wTableRow = wTable.Rows[24];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Banderas:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloBandera"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[25];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Leyenda

                        LevelError = "Leyenda";
                        wTableRow = wTable.Rows[26];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Leyenda:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloLeyenda"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[27];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[28];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Responsable

                        LevelError = "Responsable";
                        wTableRow = wTable.Rows[29];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Responsable:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsable"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[30];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio
                    
                        wTableRow = wTable.Rows[31];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Sonido

                        LevelError = "Sonido";
                        wTableRow = wTable.Rows[32];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Sonido:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloSonido"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[33];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Responsable Sonido

                        LevelError = "Responsable Sonido";
                        wTableRow = wTable.Rows[34];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Responsable:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloResponsableSonido"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[35];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Desayuno

                        LevelError = "Desayuno";
                        wTableRow = wTable.Rows[36];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Desayuno:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Desayuno Respuesta

                        LevelError = "Desayuno Respuesta";
                        wTableRow = wTable.Rows[37];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloDesayuno"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[38];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Sillas

                        LevelError = "Sillas";
                        wTableRow = wTable.Rows[39];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Sillas:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloSillas"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[40];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Mesas

                        LevelError = "Mesas";
                        wTableRow = wTable.Rows[41];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Mesa (s)");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloMesas"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Espacio Sombreado
                    
                        wTableRow = wTable.Rows[42];
                        wTableRow.Height = 10.5f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#999999");
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                    #region Presentación

                        LevelError = "Presentación";
                        wTableRow = wTable.Rows[43];
                        wTableRow.Height = 13f;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

                        wText = wTableCell.AppendText("Presentación:");
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[0].Width = 92f;

                        // Celda 2
                        wTableCell = wTableRow.Cells[1].AddParagraph();
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["ProtocoloPresentacion"].ToString());
                        wText.CharacterFormat.Bold = true;
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                    
                        wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[1].Width = 315f;

                        // Celda 3
                        wTableCell = wTableRow.Cells[2].AddParagraph();
                        wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                        wTableRow.Cells[2].Width = 1100f;
                    
                    #endregion

                #endregion

                #region Asistentes

                        LevelError = "Asistentes";
                    
                    if( oENTResponse.DataSetResponse.Tables[8].Rows.Count > 0 ){

                        #region Label_NombreSeccion
                            
                            // Nueva sección (empieza en una nueva página)
                            WSection = WDocument.AddSection();
                            
                            // Configuración de la tabla de encabzado
                            wTable = WSection.Body.AddTable();
                            wTable.ResetCells(1, 1);
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = 21f;

                            //Encabezado
                            wTableCell = wTableRow.Cells[0].AddParagraph();
                            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                            wText = wTableCell.AppendText("ASISTENTES:");
                            wText.CharacterFormat.Bold = true;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 12f;
                            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[0].Width = 510;

                            // Brinco de linea (genera espacio)
                            WSection.AddParagraph();
                    
                        #endregion

                        #region Asistentes

                            wTable = WSection.Body.AddTable();
                            wTable.ResetCells((oENTResponse.DataSetResponse.Tables[8].Rows.Count + 1), 2);
                            wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                            #region Encabezado

                                wTableRow = wTable.Rows[0];
                                wTableRow.Height = 30f;

                                // Celda 1
                                wTableCell = wTableRow.Cells[0].AddParagraph();
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                                wText = wTableCell.AppendText("Nombre");
                                wText.CharacterFormat.FontName = "Cambria";
                                wText.CharacterFormat.FontSize = 13f;
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.Italic = true;

                                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFCC");
                                wTableRow.Cells[0].Width = 325;

                                // Celda 2
                                wTableCell = wTableRow.Cells[1].AddParagraph();
                                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                                wText = wTableCell.AppendText("Cargo");
                                wText.CharacterFormat.FontName = "Cambria";
                                wText.CharacterFormat.FontSize = 13f;
                                wText.CharacterFormat.Bold = true;
                                wText.CharacterFormat.Italic = true;

                                wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                wTableRow.Cells[1].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFCC");
                                wTableRow.Cells[1].Width = 185;
                                
                            #endregion
                            
                            #region Detalle

                                CurrentRow = 1;

                                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[8].Rows){

                                    wTableRow = wTable.Rows[CurrentRow];
                                    wTableRow.Height = 15f;

                                    // Separador
                                    if ( oRow["Separador"].ToString() == "1" ){

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;

                                        wText = wTableCell.AppendText(oRow["Nombre"].ToString());
                                        wText.CharacterFormat.FontName = "Cambria";
                                        wText.CharacterFormat.FontSize = 13f;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].CellFormat.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCFFCC");
                                        wTableRow.Cells[0].Width = 500;

                                        wTableRow.Cells[0].CellFormat.HorizontalMerge = CellMerge.Start;
                                        wTableRow.Cells[1].CellFormat.HorizontalMerge = CellMerge.Continue;

                                        // Celda 2
                                        wTableRow.Cells[1].Width = 10;

                                    }else{

                                        // Celda 1
                                        wTableCell = wTableRow.Cells[0].AddParagraph();
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        wText = wTableCell.AppendText(oRow["Orden"].ToString() + ". " + oRow["Nombre"].ToString());
                                        wText.CharacterFormat.FontName = "Cambria";
                                        wText.CharacterFormat.FontSize = 13f;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                        wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[0].Width = 325;

                                        // Celda 2
                                        wTableCell = wTableRow.Cells[1].AddParagraph();
                                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                                        wText = wTableCell.AppendText(oRow["puesto"].ToString());
                                        wText.CharacterFormat.FontName = "Cambria";
                                        wText.CharacterFormat.FontSize = 13f;

                                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                        wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                        wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                                        wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                                        wTableRow.Cells[1].Width = 185;
                                    }

                                    // Siguiente fila
                                    CurrentRow = CurrentRow + 1;
                                }
                                
                            #endregion

                        #endregion

                    }
                    
                #endregion

                // Brinco de linea (genera espacio)
                WSection.AddParagraph();

                #region Nota al Final del Cuadernillo

                    if (oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaFinDocumento"].ToString() == "1") {

                        // Inicializaciones
                        wTable = WSection.Body.AddTable();
                        wTable.ResetCells(1, 1);
                        wTable.IndentFromLeft = 28.3f;      // 1 cm de sangría
                        wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 21f;
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                        wTableRow = wTable.Rows[0];
                        wTableRow.Height = 17f;
                        wTableRow.Cells[0].Width = 530;

                        // Celda 1
                        wTableCell = wTableRow.Cells[0].AddParagraph();

                        // Texto
                        wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["NotaDocumento"].ToString() + ENTER);
                        wText.CharacterFormat.FontName = "Arial";
                        wText.CharacterFormat.FontSize = 10f;
                        wText.CharacterFormat.Bold = true;

                        // Brinco de linea (genera espacio)
                        WSection.AddParagraph();

                    }

                #endregion

                // Descargar el documeno en la página
                try
                {

                    Char[] invalidPathChars = Path.GetInvalidPathChars();
                    String FileName = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString().Trim();

                    foreach (char currentChar in invalidPathChars){
                        FileName = FileName.Replace(currentChar.ToString(), "");
                    }

                    FileName = gcJavascript.ClearText(FileName);
                    FileName = FileName.Replace("Ñ", "N");
                    FileName = FileName.Replace("ñ", "n");
                    FileName = FileName.Replace(".", "").Trim();
                    FileName = FileName.Replace(",", "").Trim();
                    FileName = (FileName.Length > 60 ? FileName.Substring(0, 60) : FileName).Trim();
                    FileName = FileName + ".doc";

                    WDocument.Save( FileName, Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment );
                }
                catch (Exception)
                {
                    WDocument.Save("EventoProtocolo.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);
                }

            }catch (IOException ioEx) {
                throw ( new Exception(LevelError + "-" + ioEx.Message) );

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