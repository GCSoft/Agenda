<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="scatSubMenu.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.SysCat.scatSubMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconNew.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Catálogo de Sistema - SubMenú"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Pantalla de administración del catálogo de sistema SubMenú. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Menú</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlMenu" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Nombre</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtNombre" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Estatus</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Button_General" Width="125px" OnClick="btnBuscar_Click" />&nbsp;
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Button_General" Width="125px" OnClick="btnNuevo_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvSubMenu" runat="server" border="0" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="SubMenuId, Activo, NombreMenu, NombreSubMenu"
            OnRowDataBound="gvSubMenu_RowDataBound"
            OnRowCommand="gvSubMenu_RowCommand"
            OnSorting="gvSubMenu_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 150px;">Menú</td>
                        <td style="width: 150px;">Nombre del SubMenú</td>
                        <td style="width: 100px;">Estatus</td>
                        <td style="width: 100px;">Rank</td>
                        <td>Descripción</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="5">No se encontraron SubMenús registrados en el sistema</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Menú"               ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="NombreMenu"      SortExpression="NombreMenu"></asp:BoundField>
                <asp:BoundField HeaderText="Nombre del SubMenú" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="NombreSubMenu"   SortExpression="NombreSubMenu"></asp:BoundField>
                <asp:BoundField HeaderText="Estatus"            ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Estatus"         SortExpression="Estatus"></asp:BoundField>
                <asp:BoundField HeaderText="Rank"               ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Rank"            SortExpression="Rank"></asp:BoundField>
                <asp:BoundField HeaderText="Descripción"        ItemStyle-HorizontalAlign="Left"                            DataField="Descripcion"     SortExpression="Descripcion"></asp:BoundField>
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

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" Style="top: 200px;" Height="380px" Width="420px">
            <asp:Panel ID="pnlPopUpHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Click"></asp:ImageButton></td>
                        <td class="Espacio"></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Menú</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpMenu" runat="server" CssClass="DropDownList_General" Width="316px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre" runat="server" CssClass="Textbox_General" MaxLength="200" Width="310px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Descripción</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpDescripcion" runat="server" CssClass="Textarea_General" Height="50px" MaxLength="200" TextMode="MultiLine" Width="310px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">ASPX</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpPageName" runat="server" CssClass="Textbox_General" MaxLength="200" Width="310px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">URL</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpURL" runat="server" CssClass="Textbox_General" MaxLength="200" Width="310px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpStatus" runat="server" CssClass="DropDownList_General" Width="316px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Rank</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpRank" runat="server" CssClass="Textbox_General" MaxLength="200" Width="310px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Requerido</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpRequired" runat="server" CssClass="DropDownList_General" Width="316px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand" runat="server" Text="" CssClass="Button_General" Width="125px" OnClick="btnPopUpCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPopUpMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <asp:dragpanelextender id="dragPanelPopUp" runat="server" targetcontrolid="pnlPopUpContent" draghandleid="pnlPopUpHeader"> </asp:dragpanelextender>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddSubMenu" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="NombreSubMenu" />

</asp:Content>
