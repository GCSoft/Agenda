<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invFuncionarios.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invFuncionarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Funcionarios asociados a la invitación"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Asocie nuevos funcionarios a la invitación, o bien, descarte funcionarios asociados. Recuerde que cada nueva asociación generá un correo de invitación."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre de evento</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha de evento</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Nuevo Funcionario</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">

                    <script type = "text/javascript"> function FuncionarioSelected(sender, e) { $get("<%=hddFuncionarioId.ClientID %>").value = e.get_value(); } </script>
					<asp:TextBox ID="txtFuncionario" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
					<asp:HiddenField ID="hddFuncionarioId" runat="server" />
					<asp:AutoCompleteExtender
						ID="autosuggestFuncionario" 
						runat="server"
						TargetControlID="txtFuncionario"
						ServiceMethod="WSFuncionario"
                        ServicePath=""
						CompletionInterval="100"
                        DelimiterCharacters=""
                        Enabled="True"
						EnableCaching="False"
						MinimumPrefixLength="2"
						OnClientItemSelected="FuncionarioSelected"
						CompletionListCssClass="Autocomplete_CompletionListElement"
						CompletionListItemCssClass="Autocomplete_ListItem"
						CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                    />

				</td>
                <td></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnNuevo" runat="server" Text="Asociar Funcionario" CssClass="Button_General" Width="175px" OnClick="btnNuevo_Click" />&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvFuncionario" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
			DataKeyNames="UsuarioId, Nombre" 
            OnRowCommand="gvFuncionario_RowCommand"
			OnRowDataBound="gvFuncionario_RowDataBound"
            OnSorting="gvFuncionario_Sorting">
            <RowStyle CssClass="Grid_Row" />
            <EditRowStyle Wrap="True" />
            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width:300px;">Puesto</td>
                        <td>Funcionario</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="2">No se encontraron Funcionarios asociados a la invitación</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
				<asp:BoundField HeaderText="Puesto"         ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="300px" DataField="Puesto"                  SortExpression="Puesto"></asp:BoundField>
                <asp:BoundField HeaderText="Funcionario"    ItemStyle-HorizontalAlign="Left"                            DataField="NombreCompletoTitulo"    SortExpression="NombreCompletoTitulo"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" OnClientClick="return confirm('¿Seguro que desea eliminar al funcionario de la invitación?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddInvitacionId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre"  />

</asp:Content>
