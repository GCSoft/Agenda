<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girContacto.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girContacto" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">
    
    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Contactos asociados a la gira"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite la información de los contactos asociados a la gira o bien, agregue uno nuevo."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"></td>
                <td></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Button_General" Width="125px" OnClick="btnNuevo_Click" />&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvContacto" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
			DataKeyNames="GiraContactoId,Nombre" 
            OnRowCommand="gvContacto_RowCommand"
			OnRowDataBound="gvContacto_RowDataBound"
            OnSorting="gvContacto_Sorting">
            <RowStyle CssClass="Grid_Row" />
            <EditRowStyle Wrap="True" />
            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width:200px;">Nombre</td>
						<td style="width:150px;">Puesto</td>
						<td style="width:150px;">Organización</td>
						<td style="width:100px;">Teléfono</td>
                        <td style="width:100px;">Correo</td>
						<td>Comentarios</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="6">No se encontraron Contactos asociados a la gira</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
				<asp:BoundField HeaderText="Nombre"         ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="200px"	DataField="Nombre"                          SortExpression="Nombre"></asp:BoundField>
				<asp:BoundField HeaderText="Puesto"         ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="Puesto"                          SortExpression="Puesto"></asp:BoundField>
				<asp:BoundField HeaderText="Organización"   ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="Organizacion"                    SortExpression="Organizacion"></asp:BoundField>
				<asp:BoundField HeaderText="Teléfono"		ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Telefono"                        SortExpression="Telefono"></asp:BoundField>
				<asp:BoundField HeaderText="Correo"         ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="Email"                           SortExpression="Email"></asp:BoundField>
				<asp:BoundField HeaderText="Comentarios"    ItemStyle-HorizontalAlign="Left"							DataField="Comentarios" HtmlEncode="false"  SortExpression="Comentarios"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" OnClientClick="return confirm('¿Seguro que desea eliminar al contacto?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent" runat="server" CssClass="PopUpContent" style="margin-top:-265px; margin-left:-310px;" Height="510px" Width="620px">
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
				        <td class="Etiqueta">Puesto</td>
				        <td class="Espacio"></td>
				        <td class="Campo"><asp:TextBox ID="txtPopUpPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
			        </tr>
                    <tr>
				        <td class="Etiqueta">Organizacion</td>
				        <td class="Espacio"></td>
				        <td class="Campo"><asp:TextBox ID="txtPopUpOrganizacion" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
			        </tr>
                    <tr>
				        <td class="Etiqueta">Teléfono</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo"><asp:TextBox ID="txtPopUpTelefono" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
			        </tr>
                    <tr>
				        <td class="Etiqueta">Correo</td>
				        <td class="Espacio"></td>
				        <td class="Campo"><asp:TextBox ID="txtPopUpEmail" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
			        </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpComentarios" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="130px"></CKEditor:CKEditorControl>
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

    <asp:HiddenField ID="hddGiraContactoId" runat="server" Value="0" />
    <asp:HiddenField ID="hddGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre"  />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
