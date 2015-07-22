<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="repEventoDetalle.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Reportes.repEventoDetalle" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconAdm.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Reportes - Contenido para prensa"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Seleccione el evento del cual quiere otener el reporte de contenido para prensa."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel" style="z-index:2;">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Fecha</td>
				<td class="Espacio"></td>
				<td class="Campo">
                    <wuc:wucCalendar ID="wucDate" runat="server" />
				</td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Eventos" CssClass="Button_General" Width="125px" OnClick="btnBuscar_Click" />
                </td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvEvento" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="EventoId,GiraId,GiraConfiguracionId,FechaHoraInicio,FechaHoraFin,EventoNombre"
            OnRowDataBound="gvEvento_RowDataBound"
            OnRowCommand="gvEvento_RowCommand"
            OnSorting="gvEvento_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 100px;">Categoría</td>
                        <td style="width: 120px;">Fecha Inicio</td>
                        <td style="width: 120px;">Fecha Fin</td>
                        <td>Nombre de Evento</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="4">No se encontraron Eventos para la fecha seleccionada</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Categoría"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="CategoriaNombre"                     SortExpression="CategoriaNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Fecha Inicio"       ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px" DataField="FechaHoraInicioTexto"                SortExpression="FechaHoraInicio"></asp:BoundField>
                <asp:BoundField HeaderText="Fecha Fin"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px" DataField="FechaHoraFinTexto"                   SortExpression="FechaHoraFin"></asp:BoundField>
                <asp:BoundField HeaderText="Nombre de Evento"   ItemStyle-HorizontalAlign="Left"                            DataField="EventoNombre"    HtmlEncode="false"  SortExpression="EventoNombre"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgReporte" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Visualizar" ImageUrl="~/Include/Image/Buttons/Reporte.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <BR /><BR />
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlReporte" runat="server" CssClass="ReportePanel">
        <asp:Panel ID="pnlReporteCanvas" runat="server" CssClass="ReportePanelCanvas">
            <rsweb:ReportViewer ID="rptEventoDetalle" runat="server" SizeToReportContent="True"></rsweb:ReportViewer>
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
