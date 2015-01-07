<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveListadoEventos.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveListadoEventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconAdm.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Eventos - Listado de eventos"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Ajuste los filtros para poder visualizar el listado de eventos en los que esta usted vinculado."></asp:Label></td>
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
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvEvento" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="EventoId, Activo, Nombre"
            OnRowDataBound="gvEvento_RowDataBound"
            OnRowCommand="gvEvento_RowCommand"
            OnSorting="gvEvento_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 150px;">Estatus</td>
                        <td style="width: 100px;">Categoría</td>
                        <td style="width: 100px;">Prioridad</td>
                        <td style="width: 100px;">Tipo de Evento</td>
                        <td style="width: 200px;">Lugar del evento</td>
                        <td style="width: 120px;">fecha y Hora</td>
                        <td>Detalle</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="7">No se encontraron Eventos registrados en el sistema</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Categoría"      ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="Nombre"      SortExpression="Nombre"></asp:BoundField>
                <asp:BoundField HeaderText="Estatus"        ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Estatus"     SortExpression="Estatus"></asp:BoundField>
                <asp:BoundField HeaderText="Rank"           ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Rank"        SortExpression="Rank"></asp:BoundField>
                <asp:BoundField HeaderText="Descripción"    ItemStyle-HorizontalAlign="Left"                            DataField="Descripcion" SortExpression="Descripcion"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgPopUp" CommandArgument="<%#Container.DataItemIndex%>" CommandName="PopUp" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <br /><br />

    <asp:HiddenField ID="hddEvento" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />
    
</asp:Content>