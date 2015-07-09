<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girDatosGenerales.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girDatosGenerales" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>
<%@ Register Src="~/Include/WebUserControls/wucTimer.ascx" TagPrefix="wuc" TagName="wucTimer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Datos generales de la Gira"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite la información de los datos generales de la Gira."></asp:Label></td>
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
				<td class="Etiqueta">Nombre de la gira</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo" colspan="2"><asp:TextBox ID="txtNombreGira" runat="server" CssClass="Textarea_General" Height="70px" TextMode="MultiLine" MaxLength="1000" Width="600px"></asp:TextBox></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha y hora de la Gira</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo" colspan="2">
                    <table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:200px;">
                                <wuc:wucCalendar ID="wucCalendar" runat="server" />
                            </td>
                            <td style="text-align:left; width:300px;">
                                <wuc:wucTimer ID="wucTimerDesde" runat="server" />&nbsp;&nbsp;a&nbsp;&nbsp;
                                <wuc:wucTimer ID="wucTimerHasta" runat="server" />&nbsp;HRS.
                            </td>
                            <td></td>
                        </tr>
                    </table>
				</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>
        <table border="0" style="width:100%">
			<tr>
				<td class="Etiqueta">Detalle</td>
                <td class="Espacio"></td>
				<td class="Campo"></td>
				<td></td>
			</tr>
			<tr>
				<td colspan="4" style="text-align:left; vertical-align:bottom;">
                    <CKEditor:CKEditorControl ID="ckeGiraDetalle" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="96px"></CKEditor:CKEditorControl>
				</td>
			</tr>
		</table>
    </asp:Panel>
        
    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="Button_General" Width="125px" OnClick="btnActualizar_Click" />&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
