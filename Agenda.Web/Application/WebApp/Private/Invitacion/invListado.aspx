<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invListado.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invListado" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconGeneral.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Invitación - Listado de invitaciones"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Ajuste los filtros para poder visualizar el listado de invitaciones en los que esta usted vinculado."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Estatus</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlEstatusInvitacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Prioridad</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Desde</td>
				<td class="Espacio"></td>
				<td class="Campo">
                    <wuc:wucCalendar ID="wucBeginDate" runat="server" />
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Hasta</td>
				<td class="Espacio"></td>
				<td class="Campo"><wuc:wucCalendar ID="wucEndDate" runat="server" /></td>
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

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvInvitacion" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="InvitacionId, EventoNombre"
            OnRowDataBound="gvInvitacion_RowDataBound"
            OnRowCommand="gvInvitacion_RowCommand"
            OnSorting="gvInvitacion_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 120px;">Estatus</td>
                        <td style="width: 100px;">Categoría</td>
                        <td style="width: 90px;">Prioridad</td>
                        <td style="width: 200px;">Lugar del Invitacion</td>
                        <td style="width: 120px;">Fecha y Hora</td>
                        <td style="width: 200px;">Nombre de Evento</td>
                        <td>Detalle de Evento</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="7">No se encontraron Invitaciones registrados</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Estatus"            ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px" DataField="EstatusInvitacionNombre"             SortExpression="EstatusInvitacionNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Categoría"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="CategoriaNombre"                     SortExpression="CategoriaNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Prioridad"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="90px"  DataField="PrioridadNombre"                     SortExpression="PrioridadNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Lugar del Evento"   ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="LugarEvento"                         SortExpression="LugarEvento"></asp:BoundField>
                <asp:BoundField HeaderText="Fecha y Hora"       ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px" DataField="EventoFechaHora"                     SortExpression="EventoFechaHora"></asp:BoundField>
                <asp:BoundField HeaderText="Nombre de Evento"   ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="EventoNombre"                        SortExpression="EventoNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Detalle de Evento"  ItemStyle-HorizontalAlign="Left"                            DataField="EventoDetalle" HtmlEncode="false"    SortExpression="EventoDetalle"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <br /><br />

    <asp:HiddenField ID="hddInvitacion" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="EventoFechaHora" />

</asp:Content>
