<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="catSecretario.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Catalogo.catSecretario" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconDetail.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Catálogo - Secretarios"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Pantalla de administración del catálogo de Secretarios. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Palabra clave</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="210px"></asp:TextBox></td>
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
        <asp:GridView ID="gvSecretario" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="SecretarioId,Activo,TituloNombre"
            OnRowDataBound="gvSecretario_RowDataBound"
            OnRowCommand="gvSecretario_RowCommand"
            OnSorting="gvSecretario_Sorting">
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <HeaderStyle CssClass="Grid_Header" />
            <RowStyle CssClass="Grid_Row" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width: 250px;">Nombre</td>
                        <td style="width: 200px;">Puesto</td>
                        <td style="width: 200px;">Correo</td>
                        <td style="width: 100px;">Teléfono</td>
                        <td style="width: 80px;">Estatus</td>
                        <td>Observaciones</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="6">No se encontraron Secretarios registrados en el sistema</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Nombre"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="250px" DataField="Nombre"                          SortExpression="Nombre"></asp:BoundField>
                <asp:BoundField HeaderText="Puesto"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Puesto"                          SortExpression="Puesto"></asp:BoundField>
                <asp:BoundField HeaderText="Correo"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Correo"                          SortExpression="Correo"></asp:BoundField>
                <asp:BoundField HeaderText="Teléfono"       ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Telefono"                        SortExpression="Telefono"></asp:BoundField>
                <asp:BoundField HeaderText="Estatus"        ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px"  DataField="Estatus"                         SortExpression="Estatus"></asp:BoundField>
                <asp:BoundField HeaderText="Observaciones"  ItemStyle-HorizontalAlign="Left"                            DataField="Descripcion" HtmlEncode="false"  SortExpression="Descripcion"></asp:BoundField>
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

    <asp:Panel ID="pnlPaginado" runat="server" CssClass="PaginadoPanel" >
        <table class="PaginadoTable" >
            <tr>
                <td style="text-align:left; vertical-align:bottom;">
                    Registros por Página
                    <asp:Label ID="lblPageSize" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td style="text-align:right; vertical-align:bottom;">
                    Página
                    <asp:Label ID="lblPage" runat="server" Font-Bold="true" Text="1"></asp:Label>
                    de
                    <asp:Label ID="lblPages" runat="server" Font-Bold="true" Text="1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height:20px; text-align:right; vertical-align:bottom;">
                    <asp:LinkButton ID="lnkFirstPage"       CssClass="PaginadoText"   CommandName="FirstPage"     OnCommand="GridView_SelectPage" runat="server" Font-Bold="true" Text="Primera"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="lnkPreviousPage"    CssClass="PaginadoText"   CommandName="PreviousPage"  OnCommand="GridView_SelectPage" runat="server" Font-Bold="true" Text="Anterior"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="lnkNextPage"        CssClass="PaginadoText"   CommandName="NextPage"      OnCommand="GridView_SelectPage" runat="server" Font-Bold="true" Text="Siguiente"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="lnkLastPage"        CssClass="PaginadoText"   CommandName="LastPage"      OnCommand="GridView_SelectPage" runat="server" Font-Bold="true" Text="Última"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" style="margin-top:-250px; margin-left:-310px;" Height="500px" Width="620px">
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
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Título</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpTitulo" runat="server" CssClass="DropDownList_General" Width="405px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Correo</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpCorreo" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Teléfono</td>
                        <td class="VinetaObligatorio"></td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpTelefono" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpStatus" runat="server" CssClass="DropDownList_General" Width="405px"></asp:DropDownList></td>
                    </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpDescripcion" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl>
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand" runat="server" Text="" CssClass="Button_General" Width="125px" OnClick="btnPopUpCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
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

    <asp:HiddenField ID="hddSecretario" runat="server" Value="" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
