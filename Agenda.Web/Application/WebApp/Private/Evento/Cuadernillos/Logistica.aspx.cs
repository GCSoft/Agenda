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

            try {

                oENTEvento.EventoId = idEvento;

                oENTResponse = oBPEvento.SelectEvento_Detalle(oENTEvento);

                // Validaciones
                if (oENTResponse.GeneratesException) { throw (new Exception(oENTResponse.MessageError)); }
                if (oENTResponse.MessageDB != "") { throw (new Exception(oENTResponse.MessageDB)); }

                // Nueva hoja de Word
                oSection = oDocument.AddSection();
                oSection.PageSetup.PageSize = new SizeF(612, 652);

                // Margenes
                oSection.PageSetup.Margins.Bottom = 36f;    // 1.27 cm
                oSection.PageSetup.Margins.Left = 36f;      // 1.27 cm
                oSection.PageSetup.Margins.Right = 66.3f;   // 2.34 cm
                oSection.PageSetup.Margins.Top = 42.6f;     // 1.50 cm

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

                System.Drawing.Image imgNLSGL_E = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Logo.png"));
                wPicture = wTableCell.AppendPicture(imgNLSGL_E);
                wPicture.Height = 50;
                wPicture.Width = 320;

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
                wTable = oSection.HeadersFooters.Header.AddTable();
                wTable.ResetCells(1, 1); // 1 Fila 1 columnas

                wTableRow = wTable.Rows[0];
                wTableRow.Height = 17f;

                // Celda 1 (Separador)
                wTableCell = wTableRow.Cells[0].AddParagraph();
                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                System.Drawing.Image imgSeparadorE = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Separador_NL.png"));
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaLarga"].ToString());
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PronosticoClima"].ToString());
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMinima"].ToString());
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
                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                {

                    if (oRow["MedioTrasladoId"].ToString() == "1")
                    {
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
                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                {

                    if (oRow["MedioTrasladoId"].ToString() == "2")
                    {
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
                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                {

                    if (oRow["MedioTrasladoId"].ToString() == "3")
                    {
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
                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                {

                    if (oRow["MedioTrasladoNombre"].ToString() == "Otro")
                    {
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString());
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["CaracteristicasInvitados"].ToString());
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
                wText = wTableCell.AppendText((int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 ? "SI" : "NO"));
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaSi"].ToString()) == 1)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaNo"].ToString()) == 1)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaConfirma"].ToString()) == 1)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 1)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 2)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 3)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 4)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 5)
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 6)
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
                wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                wTableRow = wTable.Rows[0];
                wTableRow.Height = 17f;

                //col 1
                wTableCell = wTableRow.Cells[0].AddParagraph();
                WTable tComiteRecepcion;
                WTableRow tRow;

                tComiteRecepcion = new WTable(oDocument, false);
                tComiteRecepcion.ResetCells(oENTResponse.DataSetResponse.Tables[8].Rows.Count, 1);
                tComiteRecepcion.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                int Fila = 0;
                string Orden;
                string Nombre;
                string Puesto;

                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[8].Rows)
                {

                    tRow = tComiteRecepcion.Rows[Fila];
                    tRow.Height = 17f;

                    Orden = oRow["Orden"].ToString() + ". ";
                    Nombre = oRow["Nombre"].ToString();
                    Puesto = ", " + oRow["puesto"].ToString() + ".";

                    //col 1
                    wTableCell = tRow.Cells[0].AddParagraph();

                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                    wText = wTableCell.AppendText(Orden + Nombre + Puesto + ENTER);
                    wText.CharacterFormat.FontName = "Arial";
                    wText.CharacterFormat.FontSize = 10f;

                    tRow.Cells[0].Width = 500;

                    Fila = Fila + 1;

                }

                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                wTableRow.Cells[0].Tables.Add(tComiteRecepcion);
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;

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
                wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                wTableRow = wTable.Rows[0];
                wTableRow.Height = 17f;

                //col 1
                wTableCell = wTableRow.Cells[0].AddParagraph();
                WTable t;

                t = new WTable(oDocument, false);
                t.ResetCells(oENTResponse.DataSetResponse.Tables[9].Rows.Count, 1);
                t.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                int FilaOrdenDia = 0;
                string OrdenDia;
                string Detalle;

                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[9].Rows)
                {

                    tRow = t.Rows[FilaOrdenDia];
                    tRow.Height = 17f;

                    OrdenDia = oRow["Orden"].ToString() + ". ";
                    Detalle = oRow["Detalle"].ToString();

                    //col 1
                    wTableCell = tRow.Cells[0].AddParagraph();

                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                    wText = wTableCell.AppendText(OrdenDia + Detalle + ENTER);
                    wText.CharacterFormat.FontName = "Arial";
                    wText.CharacterFormat.FontSize = 10f;

                    tRow.Cells[0].Width = 500;

                    FilaOrdenDia = FilaOrdenDia + 1;

                }

                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                wTableRow.Cells[0].Tables.Add(t);
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;

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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoNombre"].ToString());
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
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(oENTResponse.DataSetResponse.Tables[10].Rows.Count, 1);

                //int FilaAcomodo = 0;
                //string OrdenAcomodo;
                //string NombreAcomodo;
                //string PuestoAcomodo;

                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                //{

                //    wTableRow = wTable.Rows[FilaAcomodo];
                //    wTableRow.Height = 17f;


                //    OrdenAcomodo = oRow["Orden"].ToString() + ". ";
                //    NombreAcomodo = oRow["Nombre"].ToString();
                //    PuestoAcomodo = ", " + oRow["Puesto"].ToString();

                //    //col 1
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //    wText = wTableCell.AppendText(OrdenAcomodo + NombreAcomodo + PuestoAcomodo + ENTER);
                //    //wText.CharacterFormat.Bold = false;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[0].Width = 510;

                //    FilaAcomodo = FilaAcomodo + 1;

                //}

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();

                wTable = oSection.Body.AddTable();
                wTable.ResetCells(1, 1);
                wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                wTableRow = wTable.Rows[0];
                wTableRow.Height = 17f;

                //col 1
                wTableCell = wTableRow.Cells[0].AddParagraph();

                WTable tAcomodo;

                tAcomodo = new WTable(oDocument, false);
                tAcomodo.ResetCells(oENTResponse.DataSetResponse.Tables[10].Rows.Count, 1);
                tAcomodo.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                int FilaAcomodo = 0;
                string OrdenAcomodo;
                string NombreAcomodo;
                string PuestoAcomodo;

                foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                {

                    tRow = tAcomodo.Rows[FilaAcomodo];
                    tRow.Height = 17f;

                    OrdenAcomodo = oRow["Orden"].ToString() + ". ";
                    NombreAcomodo = oRow["Nombre"].ToString();
                    PuestoAcomodo = ", " + oRow["Puesto"].ToString();

                    //col 1
                    wTableCell = tRow.Cells[0].AddParagraph();

                    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                    wText = wTableCell.AppendText(OrdenAcomodo + NombreAcomodo + PuestoAcomodo + ENTER);
                    wText.CharacterFormat.FontName = "Arial";
                    wText.CharacterFormat.FontSize = 10f;

                    tRow.Cells[0].Width = 500;

                    FilaAcomodo = FilaAcomodo + 1;

                }

                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                wTableRow.Cells[0].Tables.Add(tAcomodo);
                wText.CharacterFormat.FontName = "Arial";
                wText.CharacterFormat.FontSize = 10f;

                wTableRow.Cells[0].Width = 510;

                // Brinco de linea (genera espacio)
                oSection.AddParagraph();

                //
                wTable = oSection.Body.AddTable();
                wTable.ResetCells(1, 1);

                wTableRow = wTable.Rows[0];
                wTableRow.Height = 17f;
                wTableCell = wTableRow.Cells[0].AddParagraph();
                wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                wText = wTableCell.AppendText("OBSERVACIONES: " + oENTResponse.DataSetResponse.Tables[6].Rows[0]["AcomodoObservaciones"].ToString());
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
                wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[11].Rows[0]["Nombre"].ToString() + ", " + oENTResponse.DataSetResponse.Tables[11].Rows[0]["Puesto"].ToString());
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

                string Responsable1;
                string Responsable2;

                Responsable1 = oENTResponse.DataSetResponse.Tables[12].Rows[0]["Nombre"].ToString() + " " + oENTResponse.DataSetResponse.Tables[12].Rows[0]["Contacto"].ToString();
                Responsable2 = oENTResponse.DataSetResponse.Tables[12].Rows[0]["Nombre"].ToString() + " " + oENTResponse.DataSetResponse.Tables[12].Rows[0]["Contacto"].ToString();


                //col 2
                wTableCell = wTableRow.Cells[1].AddParagraph();
                wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //TODO: RESPONSABLE DE LOGÍSTICA DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableLogistica y ResponsableLogistica2]
                wText = wTableCell.AppendText(Responsable1 + ENTER + Responsable2);
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
                if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PropuestaAcomodo"].ToString()) != 0)
                {
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

                    foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                    {

                        if (int.Parse(oRow["Orden"].ToString()) == 1)
                        {

                            Acomodo1 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 2)
                        {

                            Acomodo2 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 3)
                        {

                            Acomodo3 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 4)
                        {

                            Acomodo4 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 5)
                        {

                            Acomodo5 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 6)
                        {

                            Acomodo6 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 7)
                        {

                            Acomodo7 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 8)
                        {

                            Acomodo8 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 9)
                        {

                            Acomodo9 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 10)
                        {

                            Acomodo10 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                        }

                        if (int.Parse(oRow["Orden"].ToString()) == 11)
                        {

                            Acomodo11 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
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
                    wTableRow.Cells[10].Width = 46;


                    // Brinco de linea (genera espacio)
                    oSection.AddParagraph();



                    #endregion
                }

                //TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
                if (oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString() != "")
                {
                    #region Montaje
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
                    wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                    wTableRow.Cells[0].Width = 510;
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
                    //TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
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
                    System.Drawing.Image imgMontage = System.Drawing.Image.FromFile(oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString());
                    wPicture = wTableCell.AppendPicture(imgMontage);


                    wTableRow.Cells[0].Width = 510;

                    oSection.AddParagraph();
                    #endregion

                    #endregion
                }


                // Descargar el documeno en la página
                oDocument.Save("EventoLogistica.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);

            }catch (Exception ex) {
                throw (ex);
            }
        }

        void CrearCuadernillo_Nuevo(Int32 EventoId){
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

                #region Configuración de la primer página
                    
                    // Márgenes
                    oSection.PageSetup.Margins.Bottom = 36f;    // 1.27 cm
                    oSection.PageSetup.Margins.Left = 36f;      // 1.27 cm
                    oSection.PageSetup.Margins.Right = 66.3f;   // 2.34 cm
                    oSection.PageSetup.Margins.Top = 42.6f;     // 1.50 cm

                    // Establecer la sección como primer página
                    oSection.BreakCode = SectionBreakCode.NewPage;

                    #region Encabezado

                        #region Logo y Fecha

                            wTable = oSection.HeadersFooters.Header.AddTable();
                            wTable.ResetCells(4, 2); // 4 Filas 2 columnas
                            
                            // Fila 1
                            wTableRow = wTable.Rows[0];
                            wTableRow.Height = .6f;
                            
                            // Celda 2 (Fecha)
                            wTableCell = wTableRow.Cells[1].AddParagraph();
                            wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                            wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
                            wText = wTableCell.AppendText("ACTUALIZACIÓN DE " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["FechaModificacion"].ToString());
                            wText.CharacterFormat.Bold = false;
                            wText.CharacterFormat.FontName = "Arial";
                            wText.CharacterFormat.FontSize = 8f;
                            wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                            wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                            wTableRow.Cells[1].Width = 190;

                            // Agregar el párrafo recién creado
                            oSection.AddParagraph();
                            
                        #endregion

                        //#region Separador
                            
                        //    wTable = oSection.HeadersFooters.Header.AddTable();
                        //    wTable.ResetCells(1, 1); // 1 Fila 1 columnas

                        //    wTableRow = wTable.Rows[0];
                        //    wTableRow.Height = 17f;

                        //    // Celda 1 (Separador)
                        //    wTableCell = wTableRow.Cells[0].AddParagraph();
                        //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                        //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                        //    System.Drawing.Image imgSeparadorE = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Separador_NL.png"));
                        //    wPicture = wTableCell.AppendPicture(imgSeparadorE);
                        //    wPicture.Height = 5;
                        //    wPicture.Width = 510;

                        //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                        //    wTableRow.Cells[0].Width = 510;

                        //    // Agregar el párrafo recién creado
                        //    oSection.AddParagraph();
                            
                        //#endregion

                    #endregion

                #endregion

                    

                //Addnew section to the document.
                oSection = null;
                oSection = oDocument.AddSection();

                // Margenes del resto del documento
                oSection.PageSetup.Margins.Bottom = 36f;    // 1.27 cm
                oSection.PageSetup.Margins.Left = 36f;      // 1.27 cm
                oSection.PageSetup.Margins.Right = 66.3f;   // 2.34 cm
                oSection.PageSetup.Margins.Top = 42.6f;     // 1.50 cm

                //#region Encabezado

                //#region LogoYFecha


                //wTable = oSection.HeadersFooters.Header.AddTable();
                //wTable.ResetCells(1, 2); // 1 Fila 2 columnas


                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                //// Celda 1 (Logo)
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //System.Drawing.Image imgNLSGL_E = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Logo.png"));
                //wPicture = wTableCell.AppendPicture(imgNLSGL_E);
                //wPicture.Height = 50;
                //wPicture.Width = 320;

                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].Width = 350;

                //// Celda 2 (Fecha)
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
                //wText = wTableCell.AppendText("ACTUALIZACIÓN DE " + oENTResponse.DataSetResponse.Tables[1].Rows[0]["FechaModificacion"].ToString());//bd
                //wText.CharacterFormat.Bold = false;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 8f;
                //wText.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].Width = 190;

                //// Agregar el párrafo recién creado
                //oSection.AddParagraph();
                //#endregion

                //#region Separador
                //wTable = oSection.HeadersFooters.Header.AddTable();
                //wTable.ResetCells(1, 1); // 1 Fila 1 columnas

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                //// Celda 1 (Separador)
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Top;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //System.Drawing.Image imgSeparadorE = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/Separador_NL.png"));
                //wPicture = wTableCell.AppendPicture(imgSeparadorE);
                //wPicture.Height = 5;
                //wPicture.Width = 510;

                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].Width = 510;

                //// Agregar el párrafo recién creado
                ////oSection.AddParagraph();
                //#endregion

                //#endregion

                //#region NombreEvento

                //#region Label_NombreSeccion
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 21f;

                ////Encabezado
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //wText = wTableCell.AppendText("NOMBRE DEL EVENTO");
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 14f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region NombreEvento
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 25f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.Evento]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoNombre"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region FechaEvento
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 4);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("FECHA:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoFechaLarga"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["EventoHorario"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].Width = 160;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Pronostico
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 6);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("PRONÓSTICO CLIMÁTICO:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PronosticoClima"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 180;

                ////col 3
                //wTableCell = wTableRow.Cells[2].AddParagraph();
                //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("MÍNIMA:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMinima"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].Width = 39;

                ////col 5
                //wTableCell = wTableRow.Cells[4].AddParagraph();
                //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("MÁXIMA:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[4].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[4].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[4].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[4].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[4].Width = 54;

                ////Col6
                //wTableCell = wTableRow.Cells[5].AddParagraph();
                //wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: FECHA DEL EVENTO DINAMICA [RepLogistica.Maxima]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TemperaturaMaxima"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].Width = 37;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Lugar
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(3, 2);

                //#region Lugar
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("LUGAR:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoNombre"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 410;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[1].Rows[0]["LugarEventoCompleto"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 410;
                //#endregion

                //#region LugarArribo
                //wTableRow = wTable.Rows[2];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("LUGAR DE ARRIBO:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].Width = 130;

                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: LUGAR ARRIBO DINÁMICO [RepLogistica.LugarArribo]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["LugarArribo"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 380;
                //#endregion

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region MedioTraslado
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 9);

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////Col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("MEDIO DE TRASLADO:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 123;

                ////Col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.TrasladoHelicoptero]
                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                //{

                //    if (oRow["MedioTrasladoId"].ToString() == "1")
                //    {
                //        wText = wTableCell.AppendText("X");
                //        wText.CharacterFormat.Bold = true;
                //        wText.CharacterFormat.FontName = "Arial";
                //        wText.CharacterFormat.FontSize = 10f;
                //    }
                //}

                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 10;

                ////col 3
                //wTableCell = wTableRow.Cells[2].AddParagraph();
                //wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("HEICÓPTERO");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                //{

                //    if (oRow["MedioTrasladoId"].ToString() == "2")
                //    {
                //        wText = wTableCell.AppendText("X");
                //        wText.CharacterFormat.Bold = true;
                //        wText.CharacterFormat.FontName = "Arial";
                //        wText.CharacterFormat.FontSize = 10f;
                //    }
                //}

                //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].Width = 10;

                ////col 5
                //wTableCell = wTableRow.Cells[4].AddParagraph();
                //wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("VEHÍCULO");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                //{

                //    if (oRow["MedioTrasladoId"].ToString() == "3")
                //    {
                //        wText = wTableCell.AppendText("X");
                //        wText.CharacterFormat.Bold = true;
                //        wText.CharacterFormat.FontName = "Arial";
                //        wText.CharacterFormat.FontSize = 10f;
                //    }
                //}

                //wTableRow.Cells[5].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[5].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[5].Width = 10;

                ////col 7
                //wTableCell = wTableRow.Cells[6].AddParagraph();
                //wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("INFANTERÍA");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].Width = 90;

                ////Col 8
                //wTableCell = wTableRow.Cells[7].AddParagraph();
                //wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[7].Rows)
                //{

                //    if (oRow["MedioTrasladoNombre"].ToString() == "Otro")
                //    {
                //        wText = wTableCell.AppendText("X");
                //        wText.CharacterFormat.Bold = true;
                //        wText.CharacterFormat.FontName = "Arial";
                //        wText.CharacterFormat.FontSize = 10f;
                //    }
                //}

                //wTableRow.Cells[7].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[7].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[7].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[7].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[7].Width = 10;

                ////col 9
                //wTableCell = wTableRow.Cells[8].AddParagraph();
                //wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("OTRO");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].Width = 70;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region TipoMontaje
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 2);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("TIPO MONTAJE:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 110;

                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: TIPO MONTAJE DINÁMICO [RepLogistica.TipoMontaje]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 400;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Aforo
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 4);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("AFORO:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Aforo"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["CaracteristicasInvitados"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[3].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[3].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[3].Width = 260;
                //#endregion

                //#region InvitacionEsposa
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 9);

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////Col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("INVITACIÓN CON ESPOSA:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 170;

                ////Col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
                //wText = wTableCell.AppendText((int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 ? "SI" : "NO"));
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[2].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[2].Width = 75;

                ////Col 4
                //wTableCell = wTableRow.Cells[3].AddParagraph();
                //wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.AsisteEsposa]
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaSi"].ToString()) == 1)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaNo"].ToString()) == 1)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Esposa"].ToString()) == 1 && int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["EsposaConfirma"].ToString()) == 1)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[8].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[8].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[8].Width = 90;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region MediosComunicacion
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 2);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("MEDIOS DE COMUNICACIÓN:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 200;

                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: DINÁMICO SI EL CAMPO == 1 [RepLogistica.Prensa]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["MedioComunicacionNombre"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 310;

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
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 1)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 2)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 3)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].Width = 130;

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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 4)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText("CASUAL (SACO SIN CORBATA) ");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 5)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText("CASUAL (SIN SACO)");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoVestimentaId"].ToString()) == 6)
                //{
                //    wText = wTableCell.AppendText("X");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText("OTRO");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[6].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[6].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[6].Width = 130;

                //#endregion

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Menu
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 2);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("MENÚ:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 50;

                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: MENÚ DINÁMICO [RepLogistica.Menu]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["Menu"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 460;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region AcciónARealizar
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 2);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("ACCIÓN A REALIZAR:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
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
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["AccionRealizar"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 360;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#endregion

                //#region ComiteRecepcion

                //#region Label_NombreSeccion
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 21f;

                ////Encabezado
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //wText = wTableCell.AppendText("COMITÉ DE RECEPCIÓN");
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 14f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Comite
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //WTable tComiteRecepcion;
                //WTableRow tRow;

                //tComiteRecepcion = new WTable(oDocument, false);
                //tComiteRecepcion.ResetCells(oENTResponse.DataSetResponse.Tables[8].Rows.Count, 1);
                //tComiteRecepcion.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                //int Fila = 0;
                //string Orden;
                //string Nombre;
                //string Puesto;

                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[8].Rows)
                //{

                //    tRow = tComiteRecepcion.Rows[Fila];
                //    tRow.Height = 17f;

                //    Orden = oRow["Orden"].ToString() + ". ";
                //    Nombre = oRow["Nombre"].ToString();
                //    Puesto = ", " + oRow["puesto"].ToString() + ".";

                //    //col 1
                //    wTableCell = tRow.Cells[0].AddParagraph();

                //    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //    wText = wTableCell.AppendText(Orden + Nombre + Puesto + ENTER);
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;

                //    tRow.Cells[0].Width = 500;

                //    Fila = Fila + 1;

                //}

                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //wTableRow.Cells[0].Tables.Add(tComiteRecepcion);
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;

                //wTableRow.Cells[0].Width = 510;

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
                //wTableRow.Height = 21f;


                ////Encabezado
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG1]
                //wText = wTableCell.AppendText("ORDEN DEL DÍA");
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 14f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Comite

                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //WTable t;

                //t = new WTable(oDocument, false);
                //t.ResetCells(oENTResponse.DataSetResponse.Tables[9].Rows.Count, 1);
                //t.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                //int FilaOrdenDia = 0;
                //string OrdenDia;
                //string Detalle;

                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[9].Rows)
                //{

                //    tRow = t.Rows[FilaOrdenDia];
                //    tRow.Height = 17f;

                //    OrdenDia = oRow["Orden"].ToString() + ". ";
                //    Detalle = oRow["Detalle"].ToString();

                //    //col 1
                //    wTableCell = tRow.Cells[0].AddParagraph();

                //    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //    wText = wTableCell.AppendText(OrdenDia + Detalle + ENTER);
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;

                //    tRow.Cells[0].Width = 500;

                //    FilaOrdenDia = FilaOrdenDia + 1;

                //}

                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //wTableRow.Cells[0].Tables.Add(t);
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;

                //wTableRow.Cells[0].Width = 510;

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
                //wTableRow.Height = 21f;

                ////Encabezado
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                ////TODO: NOMBRE DEL EVENTO DINÁMICO [RepLogistica.ModoDeEstanciaG2]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoAcomodoNombre"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 14f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#region Presidium
                ////wTable = oSection.Body.AddTable();
                ////wTable.ResetCells(oENTResponse.DataSetResponse.Tables[10].Rows.Count, 1);

                ////int FilaAcomodo = 0;
                ////string OrdenAcomodo;
                ////string NombreAcomodo;
                ////string PuestoAcomodo;

                ////foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                ////{

                ////    wTableRow = wTable.Rows[FilaAcomodo];
                ////    wTableRow.Height = 17f;


                ////    OrdenAcomodo = oRow["Orden"].ToString() + ". ";
                ////    NombreAcomodo = oRow["Nombre"].ToString();
                ////    PuestoAcomodo = ", " + oRow["Puesto"].ToString();

                ////    //col 1
                ////    wTableCell = wTableRow.Cells[0].AddParagraph();
                ////    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                ////    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                ////    wText = wTableCell.AppendText(OrdenAcomodo + NombreAcomodo + PuestoAcomodo + ENTER);
                ////    //wText.CharacterFormat.Bold = false;
                ////    wText.CharacterFormat.FontName = "Arial";
                ////    wText.CharacterFormat.FontSize = 10f;
                ////    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                ////    wTableRow.Cells[0].Width = 510;

                ////    FilaAcomodo = FilaAcomodo + 1;

                ////}

                ////// Brinco de linea (genera espacio)
                ////oSection.AddParagraph();

                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);
                //wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;

                ////col 1
                //wTableCell = wTableRow.Cells[0].AddParagraph();

                //WTable tAcomodo;

                //tAcomodo = new WTable(oDocument, false);
                //tAcomodo.ResetCells(oENTResponse.DataSetResponse.Tables[10].Rows.Count, 1);
                //tAcomodo.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                //int FilaAcomodo = 0;
                //string OrdenAcomodo;
                //string NombreAcomodo;
                //string PuestoAcomodo;

                //foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                //{

                //    tRow = tAcomodo.Rows[FilaAcomodo];
                //    tRow.Height = 17f;

                //    OrdenAcomodo = oRow["Orden"].ToString() + ". ";
                //    NombreAcomodo = oRow["Nombre"].ToString();
                //    PuestoAcomodo = ", " + oRow["Puesto"].ToString();

                //    //col 1
                //    wTableCell = tRow.Cells[0].AddParagraph();

                //    tRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    //TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //    wText = wTableCell.AppendText(OrdenAcomodo + NombreAcomodo + PuestoAcomodo + ENTER);
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;

                //    tRow.Cells[0].Width = 500;

                //    FilaAcomodo = FilaAcomodo + 1;

                //}

                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: CICLO DINÁMICO Si Existe [RepComiteRecepcionHelipuerto.Nombre y Puesto] y [RepComiteRecepcion.Nombre y Puesto]
                //wTableRow.Cells[0].Tables.Add(tAcomodo);
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;

                //wTableRow.Cells[0].Width = 510;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();

                ////
                //wTable = oSection.Body.AddTable();
                //wTable.ResetCells(1, 1);

                //wTableRow = wTable.Rows[0];
                //wTableRow.Height = 17f;
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("OBSERVACIONES: " + oENTResponse.DataSetResponse.Tables[6].Rows[0]["AcomodoObservaciones"].ToString());
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 510;

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
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].Width = 170;

                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: RESPONSABLE DEL EVENTO DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableEvento]
                //wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[11].Rows[0]["Nombre"].ToString() + ", " + oENTResponse.DataSetResponse.Tables[11].Rows[0]["Puesto"].ToString());
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].Width = 340;

                ////Fila 2
                //wTableRow = wTable.Rows[1];
                //wTableRow.Height = 30f;
                //wTableCell = wTableRow.Cells[0].AddParagraph();
                //wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //wText = wTableCell.AppendText("RESPONSABLE DE LOGÍSTICA:");
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[0].Width = 170;

                //string Responsable1;
                //string Responsable2;

                //Responsable1 = oENTResponse.DataSetResponse.Tables[12].Rows[0]["Nombre"].ToString() + " " + oENTResponse.DataSetResponse.Tables[12].Rows[0]["Contacto"].ToString();
                //Responsable2 = oENTResponse.DataSetResponse.Tables[12].Rows[0]["Nombre"].ToString() + " " + oENTResponse.DataSetResponse.Tables[12].Rows[0]["Contacto"].ToString();


                ////col 2
                //wTableCell = wTableRow.Cells[1].AddParagraph();
                //wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                ////TODO: RESPONSABLE DE LOGÍSTICA DINÁMICO (para pasar enter usar la varibale ENTER concatenada) [RepLogistica.ResponsableLogistica y ResponsableLogistica2]
                //wText = wTableCell.AppendText(Responsable1 + ENTER + Responsable2);
                //wText.CharacterFormat.Bold = true;
                //wText.CharacterFormat.FontName = "Arial";
                //wText.CharacterFormat.FontSize = 10f;
                //wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //wTableRow.Cells[1].Width = 340;

                //// Brinco de linea (genera espacio)
                //oSection.AddParagraph();
                //#endregion

                //#endregion

                ////TODO: VALIDAR QUE EXISTA IMAGEN DE ESTRADO EN LA TABLA DE LOGÍSTICA [RepLogistica]
                //if (int.Parse(oENTResponse.DataSetResponse.Tables[6].Rows[0]["PropuestaAcomodo"].ToString()) != 0)
                //{
                //    #region Estrado
                //    oSection = oDocument.AddSection();
                //    oSection.PageSetup.PageSize = new SizeF(612, 652);
                //    oSection.PageSetup.Margins.Bottom = 28f;
                //    oSection.PageSetup.Margins.Left = 56f;
                //    oSection.PageSetup.Margins.Right = 28f;
                //    oSection.PageSetup.Margins.Top = 28f;

                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(2, 1);

                //    //Fila 1
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 20f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText("ESTRADO");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 14f;
                //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[0].Width = 510;

                //    //Fila 2
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 20f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText("(PROPUESTA DE ACOMODO)");
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[0].Width = 510;

                //    // imagen
                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(1, 1);


                //    //Fila 1
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 20f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image imgAcomodo = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/puestoAcomodo.png"));
                //    wPicture = wTableCell.AppendPicture(imgAcomodo);
                //    wPicture.Height = 50;
                //    wPicture.Width = 510;

                //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[0].Width = 510;

                //    oSection.AddParagraph();

                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(2, 11);

                //    //Fila 1 columna 1
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla10 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla10.png"));
                //    wPicture = wTableCell.AppendPicture(silla10);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[0].Width = 46;

                //    //Fila 1 columna 2
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[1].AddParagraph();
                //    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla8 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla8.png"));
                //    wPicture = wTableCell.AppendPicture(silla8);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[1].Width = 46;

                //    //Fila 1 columna 3
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[2].AddParagraph();
                //    wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla6 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla6.png"));
                //    wPicture = wTableCell.AppendPicture(silla6);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[2].Width = 46;

                //    //Fila 1 columna 4
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[3].AddParagraph();
                //    wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla4 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla4.png"));
                //    wPicture = wTableCell.AppendPicture(silla4);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[3].Width = 46;

                //    //Fila 1 columna 5
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[4].AddParagraph();
                //    wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla2 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla2.png"));
                //    wPicture = wTableCell.AppendPicture(silla2);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[4].Width = 46;

                //    //Fila 1 columna 6
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[5].AddParagraph();
                //    wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla1 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla1.png"));
                //    wPicture = wTableCell.AppendPicture(silla1);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[5].Width = 46;

                //    //Fila 1 columna 7
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[6].AddParagraph();
                //    wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla3 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla3.png"));
                //    wPicture = wTableCell.AppendPicture(silla3);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[6].Width = 46;

                //    //Fila 1 columna 8
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[7].AddParagraph();
                //    wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla5 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla5.png"));
                //    wPicture = wTableCell.AppendPicture(silla5);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[7].Width = 46;

                //    //Fila 1 columna 9
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[8].AddParagraph();
                //    wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla7 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla7.png"));
                //    wPicture = wTableCell.AppendPicture(silla7);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[8].Width = 46;

                //    //Fila 1 columna 10
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[9].AddParagraph();
                //    wTableRow.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla9 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla9.png"));
                //    wPicture = wTableCell.AppendPicture(silla9);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[9].Width = 46;

                //    //Fila 1 columna 10
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 10f;
                //    wTableCell = wTableRow.Cells[10].AddParagraph();
                //    wTableRow.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;

                //    System.Drawing.Image silla11 = System.Drawing.Image.FromFile(Server.MapPath("/Include/Image/Cuadernillo/silla11.png"));
                //    wPicture = wTableCell.AppendPicture(silla11);
                //    wPicture.Height = 64;
                //    wPicture.Width = 28;

                //    wTableRow.Cells[10].Width = 46;

                //    String Acomodo1 = "", Acomodo2 = "", Acomodo3 = "", Acomodo4 = "", Acomodo5 = "", Acomodo6 = "", Acomodo7 = "", Acomodo8 = "", Acomodo9 = "", Acomodo10 = "", Acomodo11 = "";

                //    foreach (DataRow oRow in oENTResponse.DataSetResponse.Tables[10].Rows)
                //    {

                //        if (int.Parse(oRow["Orden"].ToString()) == 1)
                //        {

                //            Acomodo1 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 2)
                //        {

                //            Acomodo2 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 3)
                //        {

                //            Acomodo3 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 4)
                //        {

                //            Acomodo4 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 5)
                //        {

                //            Acomodo5 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 6)
                //        {

                //            Acomodo6 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 7)
                //        {

                //            Acomodo7 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 8)
                //        {

                //            Acomodo8 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 9)
                //        {

                //            Acomodo9 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 10)
                //        {

                //            Acomodo10 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //        if (int.Parse(oRow["Orden"].ToString()) == 11)
                //        {

                //            Acomodo11 = oRow["Nombre"].ToString() + " " + oRow["Puesto"].ToString();
                //        }

                //    }

                //    //Fila 2 Columna 1
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[0].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo10 == "" ? " " : Acomodo10));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[0].Width = 46;

                //    //Fila 2 Columna 2
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[1].AddParagraph();
                //    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[1].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo8 == "" ? " " : Acomodo8));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[1].Width = 46;

                //    //Fila 2 Columna 3
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[2].AddParagraph();
                //    wTableRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[2].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo6 == "" ? " " : Acomodo6));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[2].Width = 46;

                //    //Fila 2 Columna 4
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[3].AddParagraph();
                //    wTableRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[3].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo4 == "" ? " " : Acomodo4));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[3].Width = 46;

                //    //Fila 2 Columna 5
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[4].AddParagraph();
                //    wTableRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[4].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo2 == "" ? " " : Acomodo2));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[4].Width = 46;

                //    //Fila 2 Columna 6
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[5].AddParagraph();
                //    wTableRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[5].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo1 == "" ? " " : Acomodo1));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[5].Width = 46;

                //    //Fila 2 Columna 77
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[6].AddParagraph();
                //    wTableRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[6].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo3 == "" ? " " : Acomodo3));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[6].Width = 46;

                //    //Fila 2 Columna 8
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[7].AddParagraph();
                //    wTableRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[7].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo5 == "" ? " " : Acomodo5));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[7].Width = 46;

                //    //Fila 2 Columna 9
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[8].AddParagraph();
                //    wTableRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[8].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo7 == "" ? " " : Acomodo7));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[8].Width = 46;

                //    //Fila 2 Columna 10
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[9].AddParagraph();
                //    wTableRow.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[9].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo9 == "" ? " " : Acomodo9));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[9].Width = 46;

                //    //Fila 2 Columna 11
                //    wTableRow = wTable.Rows[1];
                //    wTableRow.Height = 300f;
                //    wTableCell = wTableRow.Cells[10].AddParagraph();
                //    wTableRow.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableRow.Cells[10].CellFormat.TextDirection = TextDirection.VerticalBottomToTop;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText((Acomodo11 == "" ? " " : Acomodo11));
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[10].Width = 46;


                //    // Brinco de linea (genera espacio)
                //    oSection.AddParagraph();



                //    #endregion
                //}

                ////TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
                //if (oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString() != "")
                //{
                //    #region Montaje
                //    oSection = oDocument.AddSection();
                //    oSection.PageSetup.PageSize = new SizeF(612, 652);
                //    oSection.PageSetup.Margins.Bottom = 36f;
                //    oSection.PageSetup.Margins.Left = 36f;
                //    oSection.PageSetup.Margins.Right = 66f;
                //    oSection.PageSetup.Margins.Top = 42f;

                //    #region Label_NombreSeccion
                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(1, 1);
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 20f;

                //    //Encabezado
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    wText = wTableCell.AppendText("MONTAJE");
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 14f;
                //    wTableRow.Cells[0].CellFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[0].CellFormat.BackColor = Color.LightGray;
                //    wTableRow.Cells[0].Width = 510;
                //    #endregion

                //    #region Tipo
                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(1, 2);
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 24f;

                //    //col 1
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    wText = wTableCell.AppendText("TIPO:");
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[0].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[0].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[0].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[0].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[0].Width = 150;

                //    //col 2
                //    wTableCell = wTableRow.Cells[1].AddParagraph();
                //    wTableRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
                //    //TODO: VALIDAR QUE EXISTA IMAGEN DE MONTAJE EN LA TABLA DE LOGÍSTICA [RepLogistica]
                //    wText = wTableCell.AppendText(oENTResponse.DataSetResponse.Tables[6].Rows[0]["TipoMontaje"].ToString());
                //    wText.CharacterFormat.Bold = true;
                //    wText.CharacterFormat.FontName = "Arial";
                //    wText.CharacterFormat.FontSize = 10f;
                //    wTableRow.Cells[1].CellFormat.Borders.Bottom.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[1].CellFormat.Borders.Left.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
                //    wTableRow.Cells[1].CellFormat.Borders.Right.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[1].CellFormat.Borders.Top.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;
                //    wTableRow.Cells[1].Width = 360;

                //    // Brinco de linea (genera espacio)
                //    oSection.AddParagraph();

                //    wTable = oSection.Body.AddTable();
                //    wTable.ResetCells(1, 1);
                //    wTable.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;

                //    //Fila 1 columna 1
                //    wTableRow = wTable.Rows[0];
                //    wTableRow.Height = 400f;
                //    wTableCell = wTableRow.Cells[0].AddParagraph();
                //    wTableRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //    wTableCell.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //    System.Drawing.Image imgMontage = System.Drawing.Image.FromFile(oENTResponse.DataSetResponse.Tables[13].Rows[0]["RutaAbsoluta"].ToString());
                //    wPicture = wTableCell.AppendPicture(imgMontage);


                //    wTableRow.Cells[0].Width = 510;

                //    oSection.AddParagraph();
                //    #endregion

                //    #endregion
                //}


                // Descargar el documeno en la página
                oDocument.Save("EventoLogistica.doc", Syncfusion.DocIO.FormatType.Doc, Response, Syncfusion.DocIO.HttpContentDisposition.Attachment);

            }catch (Exception ex) {
                throw (ex);
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

            }catch (Exception){
                // Do Nothing
            }
        }
        


    }
}