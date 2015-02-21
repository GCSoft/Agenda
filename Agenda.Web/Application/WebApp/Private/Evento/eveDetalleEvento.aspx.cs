/*---------------------------------------------------------------------------------------------------------------------------------
' Nombre:	eveDetalleEvento
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

namespace Agenda.Web.Application.WebApp.Private.Evento
{
    public partial class eveDetalleEvento : System.Web.UI.Page
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

      void createEventoOrdinario(int idEvento)
      {

          //WordDocument oDocument = new WordDocument();
          //IWSection oSection; ;

          //IWTable wTable;
          //WTableRow wTableRow;
          //IWParagraph wTableCell;
          //IWPicture wPicture;
          //IWTextRange wText;

          //Char ENTER = Convert.ToChar(13);

          //#region Seccion-Evento

          //#region Separador
          //oSection = oDocument.AddSection();
          //oSection.PageSetup.PageSize = new SizeF(567, 567);
          //oSection.PageSetup.Margins.All = 28f; // 1 cm
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1); // 1 Fila 1 columna
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 270f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("EVENTO");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 40f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //#endregion

          //#region Seccion

          //// Nueva hoja
          //oSection = oDocument.AddSection();
          //oSection.PageSetup.PageSize = new SizeF(567, 567);

          //// Margenes a 1 cm a excepción del izquierdo
          //oSection.PageSetup.Margins.Bottom = 28f;
          //oSection.PageSetup.Margins.Left = 56f;
          //oSection.PageSetup.Margins.Right = 28f;
          //oSection.PageSetup.Margins.Top = 28f;


          //#region Encabezado

          //#region LogoYFecha
          //wTable = oSection.HeadersFooters.Header.AddTable();
          //wTable.ResetCells(1, 2); // 1 Fila 2 columnas

          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 25f;

          //// Celda 1 (Logo)
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

          //System.Drawing.Image imgNLSGL_E = System.Drawing.Image.FromFile(Server.MapPath("../Include/Image/NL_SGL.png"));
          //wPicture = wTableCell.AppendPicture(imgNLSGL_E);
          //wPicture.Height = 50;
          //wPicture.Width = 200;

          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 350;

          //// Celda 2 (Fecha)
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
          //wText = wTableCell.AppendText("ACTUALIZACIÓN...");
          //wText.CharacterFormat.Bold = false;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 8f;
          //wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].Width = 150;

          //// Agregar el párrafo recién creado
          //oSection.AddParagraph();
          //#endregion

          //#region Separador
          //wTable = oSection.HeadersFooters.Header.AddTable();
          //wTable.ResetCells(1, 1); // 1 Fila 1 columnas

          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          //// Celda 1 (Separador)
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

          //System.Drawing.Image imgSeparadorE = System.Drawing.Image.FromFile(Server.MapPath("../Include/Image/Separador.png"));
          //wPicture = wTableCell.AppendPicture(imgSeparadorE);
          //wPicture.Height = 5;
          //wPicture.Width = 500;

          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 550;

          //// Agregar el párrafo recién creado
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          //#region NombreEvento

          //#region Label_NombreSeccion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////Encabezado
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("NOMBRE DEL EVENTO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 12f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region NombreEvento
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.Evento]
          //wText = wTableCell.AppendText("DESAYUNO CON DEPORTISTAS LÍMPICOS SELECCIONADOS PARA LONDRES 2012 Y ENTREGA DE APOYOS ECONOMICOS");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region FechaEvento
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 4);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("FECHA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 50;

          ////Col2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.FechaEvento]
          //wText = wTableCell.AppendText("MARTES 3 DE JULIO DE 2013");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 200;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("HORARIO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 100;

          ////Col4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.FechaEvento]
          //wText = wTableCell.AppendText("HORARIO: 09:00 A 10:30 HRS ");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 150;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Pronostico
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 6);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("PRONÓSTICO CLIMÁTICO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////Col2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.PronosticoClimatico]
          //wText = wTableCell.AppendText("SOLEADO/DESPEJADO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 150;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("MÍNIMA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 50;

          ////Col4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.Minima]
          //wText = wTableCell.AppendText("22 C");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 50;

          ////col 5
          //wTableCell = wTableRow.Cells[4].AddParagraph();
          //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("MÁXIMA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].Width = 50;

          ////Col6
          //wTableCell = wTableRow.Cells[5].AddParagraph();
          //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.Maxima]
          //wText = wTableCell.AppendText("35 C");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].Width = 50;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Lugar
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(3, 2);

          //#region Lugar
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("LUGAR:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 100;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: LUGAR DINÁMICO [RepLogistica.Lugar]
          //wText = wTableCell.AppendText("PATIO DE HONOR DE PALACIO DE GOBIERNO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 400;
          //#endregion

          //#region Domicilio
          //wTableRow = wTable.Rows[1];
          //wTableRow.Height = 20f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("DOMICILIO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 100;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DOMICILIO DINÁMICO [RepLogistica.Domicilio + Colonia + Municipio]
          //wText = wTableCell.AppendText("AV. MADERO Y ANOTONIO I. VILLARREAL, COL, OBRERA, MONTERREY");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 400;
          //#endregion

          //#region LugarArribo
          //wTableRow = wTable.Rows[2];
          //wTableRow.Height = 20f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("LUGAR DE ARRIBO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 100;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: LUGAR ARRIBO DINÁMICO [RepLogistica.LugarArribo]
          //wText = wTableCell.AppendText("PUERTA PRINCIPAL");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 400;
          //#endregion

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region MedioTraslado
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 9);

          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////Col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("MEDIO DE TRASLADO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 80;

          ////Col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoHelicoptero]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 15;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("HEICÓPTERO");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 90;

          ////Col 4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoVehiculo]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 15;

          ////col 5
          //wTableCell = wTableRow.Cells[4].AddParagraph();
          //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("VEHÍCULO");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].Width = 90;

          ////Col 6
          //wTableCell = wTableRow.Cells[5].AddParagraph();
          //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoInfantería]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].Width = 15;

          ////col 7
          //wTableCell = wTableRow.Cells[6].AddParagraph();
          //wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("INFANTERÍA");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].Width = 90;

          ////Col 8
          //wTableCell = wTableRow.Cells[7].AddParagraph();
          //wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoOtro]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[7].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[7].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].Width = 15;

          ////col 9
          //wTableCell = wTableRow.Cells[8].AddParagraph();
          //wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("OTRO");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].Width = 90;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region TipoMontaje
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("TIPO MONTAJE:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: TIPO MONTAJE DINÁMICO [RepLogistica.TipoMontaje]
          //wText = wTableCell.AppendText("AUDITORIO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Aforo
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 4);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 30f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("AFORO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 50;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: AFORO DINÁMICO [RepLogistica.Aforo]
          //wText = wTableCell.AppendText("7000");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 50;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("CARACTERÍSTICAS DE INVITADOS");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 150;

          ////col 4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: CARACTERÍSTICAS DE INVITADOS DINÁMICO [RepLogistica.CaracteristicasInvitados]
          //wText = wTableCell.AppendText("FUNCIONARIOS ESTATALES, DEPORTISTAS, MAESTROS, MAESTROS DE EDUCACIÓN FISICA, PADRES...");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 250;
          //#endregion

          //#region InvitacionEsposa
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 9);

          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////Col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("INVITACIÓN CON ESPOSA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 160;

          ////Col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
          //wText = wTableCell.AppendText((1 == 1 ? "SI" : "NO"));
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 30;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("ASISTE:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 75;

          ////Col 4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 15;

          ////col 5
          //wTableCell = wTableRow.Cells[4].AddParagraph();
          //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("SI");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].Width = 50;

          ////Col 6
          //wTableCell = wTableRow.Cells[5].AddParagraph();
          //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
          //if (0 != 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].Width = 15;

          ////col 7
          //wTableCell = wTableRow.Cells[6].AddParagraph();
          //wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("NO");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].Width = 50;

          ////Col 8
          //wTableCell = wTableRow.Cells[7].AddParagraph();
          //wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[7].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[7].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[7].Width = 15;

          ////col 9
          //wTableCell = wTableRow.Cells[8].AddParagraph();
          //wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("PENDIENTE");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[8].Width = 90;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region InvitadoEspecial
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("INVITADO ESPECIAL:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("NO APLICA");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region MediosComunicacion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("MEDIOS DE COMUNICACIÓN:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Prensa]
          //wText = wTableCell.AppendText((1 == 1 ? "SI" : "NO"));
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Vestimenta
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(2, 7);

          //#region Fila1
          //wTableRow = wTable.Rows[0];

          //// Merge con la celda de abajo
          //wTable.Rows[0].Cells[0].CellFormat.VerticalMerge = CellMerge.Start;
          //wTable.Rows[1].Cells[0].CellFormat.VerticalMerge = CellMerge.Continue;
          //wTable.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;

          ////Col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("VESTIMENTA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 80;

          ////Col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 20;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("GALA");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 120;

          ////Col 4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 20;

          ////col 5
          //wTableCell = wTableRow.Cells[4].AddParagraph();
          //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("FORMAL");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].Width = 120;

          ////Col 6
          //wTableCell = wTableRow.Cells[5].AddParagraph();
          //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].Width = 20;

          ////col 7
          //wTableCell = wTableRow.Cells[6].AddParagraph();
          //wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("FORMAL (TRAJE OSCURO)");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].Width = 120;

          //#endregion

          //#region Fila2
          //wTableRow = wTable.Rows[1];

          ////Col 1 (solo formato)
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 80;

          ////Col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 20;

          ////col 3
          //wTableCell = wTableRow.Cells[2].AddParagraph();
          //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("GALA");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[2].Width = 120;

          ////Col 4
          //wTableCell = wTableRow.Cells[3].AddParagraph();
          //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[3].Width = 20;

          ////col 5
          //wTableCell = wTableRow.Cells[4].AddParagraph();
          //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("FORMAL");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[4].Width = 120;

          ////Col 6
          //wTableCell = wTableRow.Cells[5].AddParagraph();
          //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
          //if (1 == 1)
          //{
          //    wText = wTableCell.AppendText("X");
          //    wText.CharacterFormat.Bold = true;
          //    wText.CharacterFormat.FontName = "Arial";
          //    wText.CharacterFormat.FontSize = 9f;
          //}
          //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[5].Width = 20;

          ////col 7
          //wTableCell = wTableRow.Cells[6].AddParagraph();
          //wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("FORMAL (TRAJE OSCURO)");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[6].Width = 120;

          //#endregion

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Menu
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("MENÚ:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: MENÚ DINÁMICO [RepLogistica.Menu]
          //wText = wTableCell.AppendText("AGUA");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region AcciónARealizar
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("ACCIÓN A REALIZAR:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: ACCIÓN A REALIZAR DINÁMICA  [RepLogistica.AccionRealizar]
          //wText = wTableCell.AppendText("DECLARATORIA INAUGURAL");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          //#region ComiteRecepcion

          //#region Label_NombreSeccion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////Encabezado
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 12f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Comite
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
          //wText = wTableCell.AppendText("1.  ARRIBO AL PARTIDO DE HONOR Y TRASLADO DE INFANTERIA A LA MESA PRINCIPAL");
          //wText.CharacterFormat.Bold = false;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          ////TODO: VALIDAR QUE EXISTA ESTANCIA G1 [RepLogistica.ModoDeEstanciaG1]
          //#region EstanciaG1

          //#region Label_NombreSeccion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////Encabezado
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG1]
          //wText = wTableCell.AppendText("PRIMERA FILA");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 12f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Comite
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(3, 1);

          //// Fila1
          //wTableRow = wTable.Rows[0];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: CICLO DINÁMICO [RepGrupo1.Nombre y Puesto]
          //wText = wTableCell.AppendText("1.  LIC RODRIGO MEDINA DE LA CRUZ, GOBERNADOR CONSTITUCIONAL DEL ESTADO DE NUEVO LEON" + ENTER + ENTER);
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Fila2
          //wTableRow = wTable.Rows[1];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("OBSERVACIONES");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Fila3
          //wTableRow = wTable.Rows[2];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: OBSERVACIONES DINÁMICO [RepLogistica.Observaciones]
          //wText = wTableCell.AppendText("** EL ENCENDIDO DEL PEBETERO SE REALIZARÁ POR MEDIO DE UN VIDEO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Responsable
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(2, 2);

          ////Fila 1
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("RESPONSABLE DEL EVENTO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: RESPONSABLE DEL EVENTO DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableEvento]
          //wText = wTableCell.AppendText("ING. JOSÉ ANTONIO GONZALEZ TREVIÑO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          ////Fila 2
          //wTableRow = wTable.Rows[1];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("RESPONSABLE DE LOGÍSTICA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: RESPONSABLE DE LOGÍSTICA DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableLogistica y ResponsableLogistica2]
          //wText = wTableCell.AppendText("RESPONSABLE 1" + ENTER + "RESPONSABLE 2");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          ////TODO: VALIDAR QUE EXISTA ESTANCIA G2 [RepLogistica.ModoDeEstanciaG2]
          //#region EstanciaG2

          //#region Label_NombreSeccion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////Encabezado
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG2]
          //wText = wTableCell.AppendText("SEGUNDA FILA");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 12f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Comite
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(3, 1);

          //// Fila1
          //wTableRow = wTable.Rows[0];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: CICLO DINÁMICO [RepGrupo2.Nombre y Puesto]
          //wText = wTableCell.AppendText("1.  LIC RODRIGO MEDINA DE LA CRUZ, GOBERNADOR CONSTITUCIONAL DEL ESTADO DE NUEVO LEON" + ENTER + ENTER + ENTER + ENTER);
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Fila2
          //wTableRow = wTable.Rows[1];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("OBSERVACIONES");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Fila3
          //wTableRow = wTable.Rows[2];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: OBSERVACIONES DINÁMICO [RepLogistica.Observaciones]
          //wText = wTableCell.AppendText("** EL ENCENDIDO DEL PEBETERO SE REALIZARÁ POR MEDIO DE UN VIDEO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Responsable
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(2, 2);

          ////Fila 1
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("RESPONSABLE DEL EVENTO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: RESPONSABLE DEL EVENTO DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableEvento]
          //wText = wTableCell.AppendText("ING. JOSÉ ANTONIO GONZALEZ TREVIÑO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          ////Fila 2
          //wTableRow = wTable.Rows[1];
          //wTableRow.Height = 30f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("RESPONSABLE DE LOGÍSTICA:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: RESPONSABLE DE LOGÍSTICA DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableLogistica y ResponsableLogistica2]
          //wText = wTableCell.AppendText("RESPONSABLE 1" + ENTER + "RESPONSABLE 2");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          ////TODO: VALIDAR QUE EXISTA IMAGEN DE ESTRADO EN LA TABLA DE LOGÍSTICA [RepLogistica]
          //#region Estrado
          //oSection = oDocument.AddSection();
          //oSection.PageSetup.PageSize = new SizeF(567, 567);
          //oSection.PageSetup.Margins.Bottom = 28f;
          //oSection.PageSetup.Margins.Left = 56f;
          //oSection.PageSetup.Margins.Right = 28f;
          //oSection.PageSetup.Margins.Top = 28f;

          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(3, 1);

          ////Fila 1
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("ESTRADO");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 500;

          ////Fila 2
          //wTableRow = wTable.Rows[1];
          //wTableRow.Height = 20f;
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("(PROPUESTA DE ACOMODO)");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 500;

          ////Fila 3
          //wTableRow = wTable.Rows[2];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          ////TODO: VALIDAR QUE EXISTA IMAGEN DE ESTRADO EN LA TABLA DE LOGÍSTICA [RepLogistica]
          //System.Drawing.Image imgEstrado = System.Drawing.Image.FromFile(Server.MapPath("../Include/Image/NL_Large.gif"));
          //wPicture = wTableCell.AppendPicture(imgEstrado);
          //wPicture.Height = 350;
          //wPicture.Width = 450;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();

          //#endregion

          ////TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
          //#region Montaje
          //oSection = oDocument.AddSection();
          //oSection.PageSetup.PageSize = new SizeF(567, 567);
          //oSection.PageSetup.Margins.Bottom = 28f;
          //oSection.PageSetup.Margins.Left = 56f;
          //oSection.PageSetup.Margins.Right = 28f;
          //oSection.PageSetup.Margins.Top = 28f;

          //#region Label_NombreSeccion
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 20f;

          ////Encabezado
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          //wText = wTableCell.AppendText("MONTAJE");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 12f;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
          //wTableRow.Cells[0].Width = 500;
          //#endregion

          //#region Tipo
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 2);
          //wTableRow = wTable.Rows[0];
          //wTableRow.Height = 24f;

          ////col 1
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          //wText = wTableCell.AppendText("TIPO:");
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[0].Width = 150;

          ////col 2
          //wTableCell = wTableRow.Cells[1].AddParagraph();
          //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
          ////TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
          //wText = wTableCell.AppendText("TRADICIONAL");
          //wText.CharacterFormat.Bold = true;
          //wText.CharacterFormat.FontName = "Arial";
          //wText.CharacterFormat.FontSize = 9f;
          //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
          //wTableRow.Cells[1].Width = 350;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#region Imagen
          //wTable = oSection.Body.AddTable();
          //wTable.ResetCells(1, 1);

          //wTableRow = wTable.Rows[0];
          //wTableCell = wTableRow.Cells[0].AddParagraph();
          //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
          ////TODO: VALIDAR QUE EXISTA IMAGEN DE ESTRADO EN LA TABLA DE LOGÍSTICA [RepLogistica]
          //System.Drawing.Image imgMontaje = System.Drawing.Image.FromFile(Server.MapPath("../Include/Image/NL_Large.gif"));
          //wPicture = wTableCell.AppendPicture(imgMontaje);
          //wPicture.Height = 350;
          //wPicture.Width = 450;
          //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
          //wTableRow.Cells[0].Width = 500;

          //// Brinco de linea (genera espacio)
          //oSection.AddParagraph();
          //#endregion

          //#endregion

          //#endregion

          //#endregion
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

                // Campos ocultos
                this.hddEstatusEventoId.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoId"].ToString();
                this.Expired.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Expired"].ToString();
                this.Logistica.Value = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Logistica"].ToString();

                // Formulario
                this.lblDependenciaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["Dependencia"].ToString();
                this.lblEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString();
                this.lblEventoFechaHora.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaHora"].ToString();
                this.lblEstatusEventoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoNombre"].ToString();

                this.lblCategoriaNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["CategoriaNombre"].ToString();
                this.lblConductoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["ConductoNombre"].ToString();
                this.lblPrioridadNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["PrioridadNombre"].ToString();

                this.lblSecretarioRamoNombre.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRamoNombre"].ToString();
                this.lblSecretarioResponsable.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioResponsable"].ToString();
                this.lblSecretarioRepresentante.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["SecretarioRepresentante"].ToString();

                this.lblLugarEventoCompleto.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoCompleto"].ToString();
                this.lblEventoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoDetalle"].ToString();

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoObservaciones"].ToString().Trim() == "" ){

                    this.lblObservacionesGenerales.Visible = false;
                    this.lblObservacionesGeneralesDetalle.Visible = false;
                }else{

                    this.lblObservacionesGenerales.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Visible = true;
                    this.lblObservacionesGeneralesDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoObservaciones"].ToString();
                }

                if ( oENTResponse.DataSetResponse.Tables[1].Rows[0]["EstatusEventoId"].ToString().Trim() != "2" ){ // Declinada

                    this.lblMotivoRechazo.Visible = false;
                    this.lblMotivoRechazoDetalle.Visible = false;
                }else{

                    this.lblMotivoRechazo.Visible = true;
                    this.lblMotivoRechazoDetalle.Visible = true;
                    this.lblMotivoRechazoDetalle.Text = oENTResponse.DataSetResponse.Tables[1].Rows[0]["MotivoRechazo"].ToString();
                }

                // Contactos
                this.gvContacto.DataSource = oENTResponse.DataSetResponse.Tables[2];
                this.gvContacto.DataBind();

                // Documentos
                if (oENTResponse.DataSetResponse.Tables[3].Rows.Count == 0){

					this.SinDocumentoLabel.Text = "<br /><br />No hay documentos anexados";
				}else{

					this.SinDocumentoLabel.Text = "";
                    this.dlstDocumentoList.DataSource = oENTResponse.DataSetResponse.Tables[3];
					this.dlstDocumentoList.DataBind();
				}

            }catch (Exception ex){
                throw (ex);
            }
        }

        void SetErrorPage(){
			try
            {

                this.DatosGeneralesPanel.Visible = false;
                this.DatosEventoPanel.Visible = false;
                this.ProgramaLogisticaPanel.Visible = false;
                this.ContactoPanel.Visible = false;
                this.AdjuntarPanel.Visible = false;
                this.Historial.Visible = false;
                this.RechazarPanel.Visible = false;
                this.CuadernilloPanel.Visible = false;

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
                        this.EliminarRepresentantePanel.Visible = true;
                        this.DatosGeneralesPanel.Visible = true;
                        this.DatosEventoPanel.Visible = true;
                        this.ProgramaLogisticaPanel.Visible = ( this.Logistica.Value == "1" ? true : false );
                        this.ProgramaProtocoloPanel.Visible = ( this.Logistica.Value == "1" ? false : true );
                        this.ContactoPanel.Visible = true;
                        this.AdjuntarPanel.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloPanel.Visible = true;
                        this.Historial.Visible = true;
						break;

                    case 4:	// Logística
                    case 5:	// Dirección de Protocolo
                        this.EliminarRepresentantePanel.Visible = true;
						this.DatosGeneralesPanel.Visible = true;
                        this.DatosEventoPanel.Visible = true;
                        this.ProgramaLogisticaPanel.Visible = ( this.Logistica.Value == "1" ? true : false );
                        this.ProgramaProtocoloPanel.Visible = ( this.Logistica.Value == "1" ? false : true );
                        this.ContactoPanel.Visible = true;
                        this.AdjuntarPanel.Visible = true;
                        this.RechazarPanel.Visible = true;
                        this.CuadernilloPanel.Visible = false;
                        this.Historial.Visible = true;
						break;

					default:
                        this.EliminarRepresentantePanel.Visible = false;
                        this.DatosGeneralesPanel.Visible = false;
                        this.DatosEventoPanel.Visible = false;
                        this.ProgramaLogisticaPanel.Visible = false;
                        this.ProgramaProtocoloPanel.Visible = false;
                        this.ContactoPanel.Visible = false;
                        this.AdjuntarPanel.Visible = false;
                        this.RechazarPanel.Visible = false;
                        this.CuadernilloPanel.Visible = false;
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

				// El Evento no se podrá operar en los siguientes Estatus:
                // 3 - Expirado
                // 4 - Cancelado
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) == 3 || Int32.Parse(this.hddEstatusEventoId.Value) == 4 || Int32.Parse(this.hddEstatusEventoId.Value) == 5 ){

                    this.DatosGeneralesPanel.Visible = false;
                    this.DatosEventoPanel.Visible = false;
                    this.ProgramaLogisticaPanel.Visible = false;
                    this.ProgramaProtocoloPanel.Visible = false;
                    this.ContactoPanel.Visible = false;
                    this.AdjuntarPanel.Visible = false;
                    this.RechazarPanel.Visible = false;

				}

                // Si el evento está cancelado o representado no se podrá generar el cuadernillo
                // 4 - Cancelado
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) == 4 || Int32.Parse(this.hddEstatusEventoId.Value) == 5 ){

                    this.CuadernilloPanel.Visible = false;

				}

                // Si el evento no está representado no se podrá eliminar al respresentante
                // 5 - Representado
				if ( Int32.Parse(this.hddEstatusEventoId.Value) != 5 ){

                    this.EliminarRepresentantePanel.Visible = false;

				}

                // Independientemente del estatus, si ya expiró ocultar opciones no contempladas
				if ( this.Expired.Value == "1" ){

                    this.EliminarRepresentantePanel.Visible = false;

				}

            }catch (Exception ex){
				throw(ex);
            }
		}



        // Eventos de la página

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

                // Obtener EventoId
				this.hddEventoId.Value = Key.ToString().Split(new Char[] { '|' })[0];

                // Obtener Sender
				this.SenderId.Value = Key.ToString().Split(new Char[] { '|' })[1];

                switch (this.SenderId.Value){
					case "1":
                        this.Sender.Value = "eveNuevoEvento.aspx";
                        break;

					case "2":
                        this.Sender.Value = "../AppIndex.aspx";
						break;

					case "3":
                        this.Sender.Value = "eveListadoEventos.aspx";
						break;

                    case "4":
                        this.Sender.Value = "eveCalendario.aspx";
                        break;

                    default:
                        this.Response.Redirect("~/Application/WebApp/Private/SysApp/sappNotificacion.aspx", false);
                        return;
                }

                // Consultar detalle de El Evento
                SelectEvento();

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

        protected void dlstDocumentoList_ItemDataBound(Object sender, DataListItemEventArgs e){
            Label DocumentoLabel;
            Image DocumentoImage;
            DataRowView DataRow;

			String DocumentoId = "";
			String Key = "";

            try
            {

                // Validación de que sea Item 
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) { return; }

                // Obtener controles
                DocumentoImage = (Image)e.Item.FindControl("DocumentoImage");
                DocumentoLabel = (Label)e.Item.FindControl("DocumentoLabel");
                DataRow = (DataRowView)e.Item.DataItem;

				// Id del documento
				DocumentoId = DataRow["DocumentoId"].ToString();
				Key = gcEncryption.EncryptString(DocumentoId, true);

                // Configurar imagen
				DocumentoLabel.Text = DataRow["NombreDocumentoCorto"].ToString();

                DocumentoImage.ImageUrl = "~/Include/Image/File/" + DataRow["Icono"].ToString();
				DocumentoImage.ToolTip = DataRow["NombreDocumento"].ToString();
                DocumentoImage.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
                DocumentoImage.Attributes.Add("onmouseout", "this.style.cursor='auto'");
                DocumentoImage.Attributes.Add("onclick", "window.open('" + ResolveUrl("~/Include/Handler/Documento.ashx") + "?key=" + Key + "');");

            }catch (Exception ex){
                throw (ex);
            }
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



        // Opciones de Menu (en orden de aparación)

        protected void EliminarRepresentanteButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveEliminarRepresentante.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

		protected void InformacionGeneralButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDatosGenerales.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void DatosEventoButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDatosEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ProgramaLogisticaButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveConfiguracionEvento.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ProgramaProtocoloButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveConfiguracionEventoProtocolo.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void ContactoButton_Click(object sender, ImageClickEventArgs e){
			 String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveContacto.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void AdjuntarButton_Click(object sender, ImageClickEventArgs e){
            String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveDocumentos.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void RechazarButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveCancelar.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void CuadernilloButton_Click(object sender, ImageClickEventArgs e){
            try
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('TODO: Se manda llamar componente de generación de cuadernillos');", true);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

        protected void HistorialButton_Click(object sender, ImageClickEventArgs e){
			String sKey = "";

            try
            {

                // Llave encriptada
                sKey = this.hddEventoId.Value + "|" + this.SenderId.Value;
                sKey = gcEncryption.EncryptString(sKey, true);
                this.Response.Redirect("eveHistorial.aspx?key=" + sKey, false);

            }catch (Exception ex){
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Convert.ToString(Guid.NewGuid()), "alert('" + gcJavascript.ClearText(ex.Message) + "');", true);
            }
		}

    }
}