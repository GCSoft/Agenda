<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invDocumentos.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invDocumentos" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:UpdatePanel ID="DocumentUpdate" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
                <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
            </asp:Panel>

            <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Documentos adjuntos a la invitación"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
                <table class="HeaderTable">
                    <tr>
                        <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Adjunte nuevos documentos a la invitación."></asp:Label></td>
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
				        <td class="Campo"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
                        <td></td>
			        </tr>
                     <tr>
				        <td class="Etiqueta">Tipo de documento</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo"><asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                        <td></td>
			        </tr>
                    <tr>
						<td class="Etiqueta">Adjuntar invitación</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo"><asp:FileUpload ID="fupInvitacion" runat="server" Width="600px" /></td>
                        <td></td>
					</tr>
					<tr><td class="Etiqueta" colspan="4" style="text-align:left;">Descripción</td></tr>
					<tr>
						<td colspan="4"><CKEditor:CKEditorControl ID="ckeDescripcion" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl></td>
					</tr>
                </table>

            </asp:Panel>
	
			<asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
                <%--Empty Content--%>
            </asp:Panel>

            <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="Button_General" width="125px" onclick="btnAgregar_Click" /> &nbsp;&nbsp;
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
            </asp:Panel>

            <asp:HiddenField ID="hddInvitacionId" runat="server" Value="0" />
            <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
	
		</ContentTemplate                                       >
		<Triggers>
			<asp:PostBackTrigger ControlID="btnAgregar" />
		</Triggers>
	</asp:UpdatePanel>

</asp:Content>
