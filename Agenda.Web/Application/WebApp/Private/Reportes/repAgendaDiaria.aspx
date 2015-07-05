<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="repAgendaDiaria.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Reportes.repAgendaDiaria" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconAdm.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Reportes - Para Prensa"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Ajuste los filtros para poder visualizar el reporte de Agenda Diaria."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel" style="z-index:2;">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Gobernador</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtNombre" runat="server" CssClass="Textbox_General" Text="Lic. Rodrigo Medina de la Cruz" Width="210px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha</td>
				<td class="Espacio"></td>
				<td class="Campo">
                    <wuc:wucCalendar ID="wucDate" runat="server" />
				</td>
                <td></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Button_General" Width="125px" OnClick="btnBuscar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlReporte" runat="server" CssClass="ReportePanel">
        <asp:Panel ID="pnlReporteCanvas" runat="server" CssClass="ReportePanelCanvas">
            <rsweb:ReportViewer ID="rptAgendaDiaria" runat="server" SizeToReportContent="True"></rsweb:ReportViewer>
        </asp:Panel>
        <asp:Panel ID="pnlReporteWaterMark" runat="server" CssClass="ReportePanelImageWaterMark">
            <asp:Image ID="imgReporteWaterMark" runat="server" ImageUrl="~/Include/Image/Web/EscudoNL_BackGround.png" />
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddEvento" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
