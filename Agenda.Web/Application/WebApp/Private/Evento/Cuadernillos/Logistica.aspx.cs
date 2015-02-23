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

        void CrearCuadernillo(Int32 idEvento){

            WordDocument oDocument = new WordDocument();
            IWSection oSection; ;

            IWTable wTable;
            WTableRow wTableRow;
            IWParagraph wTableCell;
            IWPicture wPicture;
            IWTextRange wText;
            

            Char ENTER = Convert.ToChar(13);

            #region Seccion-Evento

            #region Seccion

            // Nueva hoja
            oSection = oDocument.AddSection();
            oSection.PageSetup.PageSize = new SizeF(612, 652);

            // Margenes
            //oSection.PageSetup.Margins.Bottom = 36f;
            //oSection.PageSetup.Margins.Left = 36f;
            //oSection.PageSetup.Margins.Right = 66f;
            //oSection.PageSetup.Margins.Top = 100f;


            #region Encabezado

            #region LogoYFecha
   
            wTable = oSection.HeadersFooters.Header.AddTable();
            wTable.ResetCells(1, 2); // 1 Fila 2 columnas
             
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            // Celda 1 (Logo)
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

            System.Drawing.Image imgNLSGL_E = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Icon/Logo.png"));
            wPicture = wTableCell.AppendPicture(imgNLSGL_E);
            wPicture.Height = 50;
            wPicture.Width = 320;

            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].Width = 350;

            // Celda 2 (Fecha)
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            wText = wTableCell.AppendText("ACTUALIZACIÓN...");//bd
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
            wTable = oSection.HeadersFooters.Header.AddTable();
            wTable.ResetCells(1, 1); // 1 Fila 1 columnas

            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            // Celda 1 (Separador)
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

            System.Drawing.Image imgSeparadorE = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Icon/Separador_NL.png"));
            wPicture = wTableCell.AppendPicture(imgSeparadorE);
            wPicture.Height = 5;
            wPicture.Width = 510;

            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].Width = 510;

            // Agregar el párrafo recién creado
            //oSection.AddParagraph();
            #endregion

            #endregion

            #region NombreEvento

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
            wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #region NombreEvento
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 25f;

            //col 1
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.Evento]
            wText = wTableCell.AppendText("Pruebas GCSOFT");
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

            //col 1
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

            //Col2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.FechaEvento]
            wText = wTableCell.AppendText("MARTES 3 DE JULIO DE 2013");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 200;

            //col 3
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

            //Col4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.FechaEvento]
            wText = wTableCell.AppendText("09:00 A 10:30 HRS ");
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

            //col 1
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

            //Col2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.PronosticoClimatico]
            wText = wTableCell.AppendText("SOLEADO/DESPEJADO");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 180;

            //col 3
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

            //Col4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.Minima]
            wText = wTableCell.AppendText("22 C");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].Width = 39;

            //col 5
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

            //Col6
            wTableCell = wTableRow.Cells[5].AddParagraph();
            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.Maxima]
            wText = wTableCell.AppendText("35 C");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: LUGAR DINÁMICO [RepLogistica.Lugar]
            wText = wTableCell.AppendText("PATIO DE HONOR DE PALACIO DE GOBIERNO");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DOMICILIO DINÁMICO [RepLogistica.Domicilio + Colonia + Municipio]
            wText = wTableCell.AppendText("AV. MADERO Y ANOTONIO I. VILLARREAL, COL, OBRERA, MONTERREY");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: LUGAR ARRIBO DINÁMICO [RepLogistica.LugarArribo]
            wText = wTableCell.AppendText("PUERTA PRINCIPAL");
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

            //Col 1
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("MEDIO DE TRASLADO:");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 123;

            //Col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoHelicoptero]
            if (1 == 1)
            {
                wText = wTableCell.AppendText("X");
                wText.CharacterFormat.Bold = true;
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;
            }
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 10;

            //col 3
            wTableCell = wTableRow.Cells[2].AddParagraph();
            wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("HEICÓPTERO");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[2].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[2].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[2].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[2].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[2].Width = 90;

            //Col 4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoVehiculo]
            if (1 == 1)
            {
                wText = wTableCell.AppendText("X");
                wText.CharacterFormat.Bold = true;
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;
            }
            wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].Width = 10;

            //col 5
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

            //Col 6
            wTableCell = wTableRow.Cells[5].AddParagraph();
            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoInfantería]
            if (1 == 1)
            {
                wText = wTableCell.AppendText("X");
                wText.CharacterFormat.Bold = true;
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;
            }
            wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[5].Width = 10;

            //col 7
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

            //Col 8
            wTableCell = wTableRow.Cells[7].AddParagraph();
            wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoOtro]
            if (1 == 1)
            {
                wText = wTableCell.AppendText("X");
                wText.CharacterFormat.Bold = true;
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: TIPO MONTAJE DINÁMICO [RepLogistica.TipoMontaje]
            wText = wTableCell.AppendText("AUDITORIO");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: AFORO DINÁMICO [RepLogistica.Aforo]
            wText = wTableCell.AppendText("7000");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 50;

            //col 3
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
            wTableRow.Cells[2].Width = 150;

            //col 4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: CARACTERÍSTICAS DE INVITADOS DINÁMICO [RepLogistica.CaracteristicasInvitados]
            wText = wTableCell.AppendText("FUNCIONARIOS ESTATALES, DEPORTISTAS, MAESTROS, MAESTROS DE EDUCACIÓN FISICA, PADRES...");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[3].Width = 260;
            #endregion

            #region InvitacionEsposa
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 9);

            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            //Col 1
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

            //Col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
            wText = wTableCell.AppendText((1 == 1 ? "SI" : "NO"));
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 30;

            //col 3
            wTableCell = wTableRow.Cells[2].AddParagraph();
            wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("ASISTE:");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[2].Width = 75;

            //Col 4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
            if (1 == 1)
            {
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

            //col 5
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

            //Col 6
            wTableCell = wTableRow.Cells[5].AddParagraph();
            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
            if (0 != 1)
            {
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

            //col 7
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

            //Col 8
            wTableCell = wTableRow.Cells[7].AddParagraph();
            wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
            if (1 == 1)
            {
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

            #region InvitadoEspecial
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 2);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            //col 1
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("INVITADO ESPECIAL:");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 150;

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("NO APLICA");
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

            #region MediosComunicacion
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 2);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Prensa]
            wText = wTableCell.AppendText((1 == 1 ? "SI" : "NO"));
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

            //Col 1
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

            //Col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 3
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

            //Col 4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 5
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

            //Col 6
            wTableCell = wTableRow.Cells[5].AddParagraph();
            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 7
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

            //Col 1 (solo formato)
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 80;

            //Col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 3
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

            //Col 4
            wTableCell = wTableRow.Cells[3].AddParagraph();
            wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 5
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

            //Col 6
            wTableCell = wTableRow.Cells[5].AddParagraph();
            wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Vestimenta]
            if (1 == 1)
            {
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

            //col 7
            wTableCell = wTableRow.Cells[6].AddParagraph();
            wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("OTRO");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: MENÚ DINÁMICO [RepLogistica.Menu]
            wText = wTableCell.AppendText("AGUA");
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

            //col 1
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: ACCIÓN A REALIZAR DINÁMICA  [RepLogistica.AccionRealizar]
            wText = wTableCell.AppendText("DECLARATORIA INAUGURAL");
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

            #region ComiteRecepcion

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
            wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #region Comite
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 17f;

            //col 1
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
            wText = wTableCell.AppendText("1.  ARRIBO AL PARTIDO DE HONOR Y TRASLADO DE INFANTERIA A LA MESA PRINCIPAL");
            wText.CharacterFormat.Bold = false;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #endregion

            //TODO: VALIDAR QUE EXISTA ESTANCIA G1 [RepLogistica.ModoDeEstanciaG1]
            #region EstanciaG1

            #region Label_NombreSeccion
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 21f;

            //Encabezado
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG1]
            wText = wTableCell.AppendText("ORDEN DEL DÍA");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 14f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #region Comite
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);

            // Fila1
            wTableRow = wTable.Rows[0];
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: CICLO DINÁMICO [RepGrupo1.Nombre y Puesto]
            wText = wTableCell.AppendText("1.  LIC RODRIGO MEDINA DE LA CRUZ, GOBERNADOR CONSTITUCIONAL DEL ESTADO DE NUEVO LEON" + ENTER + ENTER);
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #endregion

            //TODO: VALIDAR QUE EXISTA ESTANCIA G2 [RepLogistica.ModoDeEstanciaG2]
            #region EstanciaG2

            #region Label_NombreSeccion
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 21f;

            //Encabezado
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            //TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG2]
            wText = wTableCell.AppendText("PRESÍDIUM ");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 14f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #region Presidium
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);

            // Fila1
            wTableRow = wTable.Rows[0];
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: CICLO DINÁMICO [RepGrupo2.Nombre y Puesto]
            wText = wTableCell.AppendText("1.  LIC RODRIGO MEDINA DE LA CRUZ, GOBERNADOR CONSTITUCIONAL DEL ESTADO DE NUEVO LEON" + ENTER + ENTER + ENTER + ENTER);
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();

            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 1);

            wTableRow = wTable.Rows[0];
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("OBSERVACIONES: ");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 510;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();

            #endregion

            #region Responsable
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: RESPONSABLE DEL EVENTO DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableEvento]
            wText = wTableCell.AppendText("ING. JOSÉ ANTONIO GONZALEZ TREVIÑO");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
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

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: RESPONSABLE DE LOGÍSTICA DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableLogistica y ResponsableLogistica2]
            wText = wTableCell.AppendText("RESPONSABLE 1" + ENTER + "RESPONSABLE 2");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].Width = 340;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #endregion

            //TODO: VALIDAR QUE EXISTA IMAGEN DE ESTRADO EN LA TABLA DE LOGÍSTICA [RepLogistica]
            #region Estrado
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

            System.Drawing.Image imgAcomodo = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Icon/puestoAcomodo.png"));
            wPicture = wTableCell.AppendPicture(imgAcomodo);
            wPicture.Height = 50;
            wPicture.Width = 510;

            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].Width = 510;

            wTable = oSection.Body.AddTable();
            wTable.ResetCells(2, 11);

            //tabla
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 10f;
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

            System.Drawing.Image silla10 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Icon/silla10.png"));
            wPicture = wTableCell.AppendPicture(silla10);
            wPicture.Height = 64;
            wPicture.Width = 28;

            wTableRow.Cells[0].Width = 36;

            //Fila 2
            wTableRow = wTable.Rows[1];
            wTableRow.Height = 300f;
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableRow.Cells[0].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            wText = wTableCell.AppendText("(PROPUESTA DE ACOMODO)");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 10f;
            wTableRow.Cells[0].Width = 36;
            

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();



            #endregion

            //TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
            #region Montaje
            oSection = oDocument.AddSection();
            oSection.PageSetup.PageSize = new SizeF(612, 652);
            oSection.PageSetup.Margins.Bottom = 28f;
            oSection.PageSetup.Margins.Left = 56f;
            oSection.PageSetup.Margins.Right = 28f;
            oSection.PageSetup.Margins.Top = 28f;

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
            wText.CharacterFormat.FontSize = 12f;
            wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
            wTableRow.Cells[0].Width = 500;
            #endregion

            #region Tipo
            wTable = oSection.Body.AddTable();
            wTable.ResetCells(1, 2);
            wTableRow = wTable.Rows[0];
            wTableRow.Height = 24f;

            //col 1
            wTableCell = wTableRow.Cells[0].AddParagraph();
            wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            wText = wTableCell.AppendText("TIPO:");
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 9f;
            wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[0].Width = 150;

            //col 2
            wTableCell = wTableRow.Cells[1].AddParagraph();
            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            //TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
            wText = wTableCell.AppendText("TRADICIONAL");
            wText.CharacterFormat.Bold = true;
            wText.CharacterFormat.FontName = "Arial";
            wText.CharacterFormat.FontSize = 9f;
            wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
            wTableRow.Cells[1].Width = 350;

            // Brinco de linea (genera espacio)
            oSection.AddParagraph();
            #endregion

            #endregion

            #endregion

            #endregion

            oDocument.Save("EventoOrdinario.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);


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

            }catch (Exception){
                // Do Nothing
            }
        }
        


    }
}