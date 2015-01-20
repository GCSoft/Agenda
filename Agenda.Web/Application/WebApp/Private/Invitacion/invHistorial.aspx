<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invInvitacionSeguimiento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invInvitacionSeguimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconGeneral.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Invitación - Historial de cambios"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Se presentan los cambios realizados a la invitación."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre de evento</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha de evento</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
        </table>

    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvInvitacionSeguimiento" runat="server" AllowPaging="false" AllowSorting="true"  AutoGenerateColumns="False" Width="100%"
			DataKeyNames="InvitacionSeguimientoId,UsuarioId,RowNumber"
			OnRowDataBound="gvInvitacionSeguimiento_RowDataBound" 
			OnSorting="gvInvitacionSeguimiento_Sorting">
			<AlternatingRowStyle CssClass="Grid_Row_Alternating" />
			<HeaderStyle CssClass="Grid_Header" />
			<RowStyle CssClass="Grid_Row" />
			<EmptyDataTemplate>
				<table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
					<tr class="Grid_Header">
						<td style="width:120px;">Fecha</td>
                        <td style="width:90px;">Tipo</td>
                        <td style="width:170px;">Usuario</td>
                        <td>Detalle</td>
                        <td style="width:200px;">Antes</td>
                        <td style="width:200px;">Después</td>
					</tr>
					<tr class="Grid_Row">
						<td colspan="6">No se encontró el historial de la invitación</td>
					</tr>
				</table>
			</EmptyDataTemplate>
			<Columns>
                <asp:BoundField HeaderText="Fecha"          ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="120px" DataField="FechaFormato"                                SortExpression="RowNumber"></asp:BoundField>
				<asp:BoundField HeaderText="Tipo"           ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="90px" DataField="TipoTransaccionNombre"                       SortExpression="TipoTransaccionNombre"></asp:BoundField>
				<asp:BoundField HeaderText="Usuario"        ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="170px" DataField="UsuarioNombre"                               SortExpression="UsuarioNombre"></asp:BoundField>
                <asp:BoundField HeaderText="Detalle"        ItemStyle-HorizontalAlign="Left"							DataField="Comentarios"             HtmlEncode="false"  SortExpression="Comentarios"></asp:BoundField>
                <asp:BoundField HeaderText="Antes"          ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Antes"                   HtmlEncode="false"  SortExpression="Antes"></asp:BoundField>
                <asp:BoundField HeaderText="Después"        ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="200px" DataField="Despues"                 HtmlEncode="false"  SortExpression="Despues"></asp:BoundField>
			</Columns>
		</asp:GridView>
    </asp:Panel>

    <br /><br />

    <asp:HiddenField ID="hddInvitacionId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
    <asp:HiddenField ID="hddSort" runat="server" Value="RowNumber" />

</asp:Content>
