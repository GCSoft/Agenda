<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveEnviarCuadernillo.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveEnviarCuadernillo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">
    
    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Enviar evento por correo"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Envía el formato del evento en Word por correo electrónico a las personas seleccionadas"></asp:Label></td>
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
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"></td>
                <td></td>
			</tr>
			<tr><td class="Etiqueta" colspan="4" style="text-align:left;">Detalle (se incluye en la prosa del correo electrónico)</td></tr>
			<tr>
				<td colspan="4"><CKEditor:CKEditorControl ID="ckeMotivoRechazo" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl></td>
			</tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="Button_General" width="125px" onclick="btnEnviar_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
    <asp:HiddenField ID="Logistica" runat="server" Value="0" />

</asp:Content>
