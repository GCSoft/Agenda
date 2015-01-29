<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="invDatosGenerales.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Invitacion.invDatosGenerales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Datos generales de Invitación"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite la información de los datos generales de la invitación."></asp:Label></td>
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
				<td class="Etiqueta">Tipo de cita</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:DropDownList ID="ddlCategoria" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Conducto</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:DropDownList ID="ddlConducto" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Prioridad</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Secretario ramo</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">
                    <script type = "text/javascript"> function SecretarioRamoSelected(sender, e) { $get("<%=hddSecretarioRamoId.ClientID %>").value = e.get_value(); } </script>
					<asp:TextBox ID="txtSecretarioRamo" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="501px"></asp:TextBox>
					<asp:HiddenField ID="hddSecretarioRamoId" runat="server" />
					<asp:AutoCompleteExtender
						ID="autosuggestSecretarioRamo" 
						runat="server"
						TargetControlID="txtSecretarioRamo"
						ServiceMethod="WSSecretario"
                        ServicePath=""
						CompletionInterval="100"
                        DelimiterCharacters=""
                        Enabled="True"
						EnableCaching="False"
						MinimumPrefixLength="2"
						OnClientItemSelected="SecretarioRamoSelected"
						CompletionListCssClass="Autocomplete_CompletionListElement"
						CompletionListItemCssClass="Autocomplete_ListItem"
						CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                    />
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Responsable</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">
                    <script type = "text/javascript"> function ResponsableSelected(sender, e) { $get("<%=hddResponsableId.ClientID %>").value = e.get_value(); } </script>
					<asp:TextBox ID="txtResponsable" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="501px"></asp:TextBox>
					<asp:HiddenField ID="hddResponsableId" runat="server" />
					<asp:AutoCompleteExtender
						ID="autosuggestResponsable" 
						runat="server"
						TargetControlID="txtResponsable"
						ServiceMethod="WSSecretario"
                        ServicePath=""
						CompletionInterval="100"
                        DelimiterCharacters=""
                        Enabled="True"
						EnableCaching="False"
						MinimumPrefixLength="2"
						OnClientItemSelected="ResponsableSelected"
						CompletionListCssClass="Autocomplete_CompletionListElement"
						CompletionListItemCssClass="Autocomplete_ListItem"
						CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                    />
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Representante</td>
				<td class="Espacio"></td>
				<td class="Campo">
                    <script type = "text/javascript"> function RepresentanteSelected(sender, e) { $get("<%=hddRepresentanteId.ClientID %>").value = e.get_value(); } </script>
                    <table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:520px;">
                                <asp:TextBox ID="txtRepresentante" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="501px"></asp:TextBox>
                            </td>
                            <td style="text-align:left; width:30px;">
                                <asp:Button ID="btnDescartarRepresentante" runat="server" Text="X" CssClass="Button_Special" ToolTip="Descartar representante" Visible="false" Width="25px" OnClick="btnDescartarRepresentante_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
					<asp:HiddenField ID="hddRepresentanteId" runat="server" />
					<asp:AutoCompleteExtender
						ID="autosuggestRepresentante" 
						runat="server"
						TargetControlID="txtRepresentante"
						ServiceMethod="WSSecretario"
                        ServicePath=""
						CompletionInterval="100"
                        DelimiterCharacters=""
                        Enabled="True"
						EnableCaching="False"
						MinimumPrefixLength="2"
						OnClientItemSelected="RepresentanteSelected"
						CompletionListCssClass="Autocomplete_CompletionListElement"
						CompletionListItemCssClass="Autocomplete_ListItem"
						CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                    />
				</td>
                <td></td>
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
				<td class="Etiqueta">Observaciones</td>
                <td class="Espacio"></td>
				<td class="Campo"></td>
				<td></td>
			</tr>
			<tr>
				<td colspan="4" style="text-align:left; vertical-align:bottom;">
					<CKEditor:CKEditorControl ID="ckeObservaciones" BasePath="~/Include/Components/CKEditor/Core" runat="server" Height="96px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
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

    <asp:HiddenField ID="hddInvitacionId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>
