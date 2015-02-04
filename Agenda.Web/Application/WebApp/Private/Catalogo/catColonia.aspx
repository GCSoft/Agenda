<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="catColonia.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Catalogo.catColonia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconDetail.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Catálogo - Colonias"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Pantalla de administración del catálogo de Colonias. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Estado</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlEstado" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Municipio</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
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
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Button_General" Width="125px" OnClick="btnBuscar_Click" />&nbsp;
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Button_General" Width="125px" OnClick="btnNuevo_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvColonia" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="ColoniaId,Activo,Nombre"
            OnRowDataBound="gvColonia_RowDataBound"
            OnRowCommand="gvColonia_RowCommand"
            OnSorting="gvColonia_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 150px;">Estado</td>
                        <td style="width: 150px;">Municipio</td>
                        <td style="width: 200px;">Colonia</td>
                        <td style="width: 100px;">Estatus</td>
                        <td>Descripción</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="5">No se encontraron Colonias registrados en el sistema</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Estado"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="EstadoNombre"                            SortExpression="EstadoNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Municipio"      ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="150px" DataField="MunicipioNombre"                         SortExpression="MunicipioNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Colonia"        ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Nombre"                                  SortExpression="Nombre"></asp:BoundField>
                <asp:BoundField HeaderText="Estatus"        ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Estatus"                                 SortExpression="Estatus"></asp:BoundField>
                <asp:BoundField HeaderText="Descripción"    ItemStyle-HorizontalAlign="Left"                            DataField="Descripcion"         HtmlEncode="false"  SortExpression="Descripcion"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="PopUp" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" style="margin-top:-270px; margin-left:-310px;" Height="540px" Width="620px">
            <asp:Panel ID="pnlPopUpHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Estado</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpEstado" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpEstado_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Municipio</td>
                        <td class="Espacio"></td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpMunicipio" runat="server" CssClass="DropDownList_General" Width="405px" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpStatus" runat="server" CssClass="DropDownList_General" Width="405px"></asp:DropDownList></td>
                    </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpDescripcion" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="130px"></CKEditor:CKEditorControl>
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUpCommand_Click" />
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
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddColonia" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
