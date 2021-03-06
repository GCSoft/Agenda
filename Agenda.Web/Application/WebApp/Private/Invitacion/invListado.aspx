﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invListado.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invListado" %>
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

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel" style="z-index:2;">
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
				<td class="Etiqueta">Búsqueda</td>
				<td class="Espacio"></td>
				<td class="Campo">
                    <asp:RadioButtonList ID="rblBusqueda" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblBusqueda_SelectedIndexChanged">
						<asp:ListItem Text="Por fecha" Value="1" Selected="True"></asp:ListItem>
						<asp:ListItem Text="Por palabra clave" Value="2"></asp:ListItem>
					</asp:RadioButtonList>
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td colspan="2">
                    <asp:Panel ID="pnlBusquedaFecha" runat="server" Visible="true" >
                        <table style="width:100%;"">
                            <tr>
                                <td style="text-align:left; width:100%;">
                                    Desde&nbsp;<wuc:wucCalendar ID="wucBeginDate" runat="server" />&nbsp;&nbsp;&nbsp;Hasta&nbsp;<wuc:wucCalendar ID="wucEndDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlBusquedaPalabra" runat="server" Visible="false">
                        <table style="width:100%;"">
                            <tr>
                                <td style="text-align:left; width:100%;">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
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
            DataKeyNames="InvitacionId,EventoNombre,PrioridadNombre,EventoHoraInicioEstandar,EventoHoraFinEstandar,InvitacionObservaciones,BeginDateSort"
            OnRowDataBound="gvInvitacion_RowDataBound"
            OnRowCommand="gvInvitacion_RowCommand"
            OnSorting="gvInvitacion_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 100px;">Estatus</td>
                        <td style="width: 100px;">Categoría</td>
                        <td style="width: 120px;">Lugar del Evento</td>
                        <td style="width: 200px;">Representante</td>
                        <td style="width: 80px;">Fecha</td>
                        <td style="width: 200px;">Nombre de Evento</td>
                        <td>Detalle de Evento</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="7">No se encontraron Invitaciones registradas</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Estatus"            ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="EstatusInvitacionNombre"             SortExpression="EstatusInvitacionNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Categoría"          ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="CategoriaNombre"                     SortExpression="CategoriaNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Lugar del Evento"   ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="120px" DataField="LugarEvento"                         SortExpression="LugarEvento"></asp:BoundField>
                <asp:BoundField HeaderText="Representante"      ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="RepresentanteEvento"                 SortExpression="RepresentanteEvento"></asp:BoundField>
                <asp:BoundField HeaderText="Fecha"              ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px"  DataField="EventoFechaEstandar"                 SortExpression="BeginDateSort"></asp:BoundField>
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

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddInvitacion" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="EventoFechaHora" />

</asp:Content>
