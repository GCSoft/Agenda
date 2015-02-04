<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveDatosEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveDatosEvento" %>
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
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Datos del evento"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite la información de los datos del evento."></asp:Label></td>
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
				<td class="Etiqueta">Nombre del evento</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:TextBox ID="txtNombreEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha y hora del evento</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">
                    <table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:200px;">
                                <wuc:wucCalendar ID="wucCalendar" runat="server" />
                            </td>
                            <td style="text-align:left; width:300px;">
                                <wuc:wucTimer ID="wucTimerDesde" runat="server" />&nbsp;&nbsp;a&nbsp;&nbsp;
                                <wuc:wucTimer ID="wucTimerHasta" runat="server" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Lugar del evento</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">

                    <script type = "text/javascript">

                        function LugarEventoSelected(sender, e) {

                            // Valor seleccionado
                            $get("<%=hddLugarEventoId.ClientID %>").value = e.get_value();

                            // Provocar PostBack
                            __doPostBack("<%= hddLugarEventoId.ClientID %>", "");
                        }

                    </script>
					<asp:TextBox ID="txtLugarEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
					<asp:HiddenField ID="hddLugarEventoId" runat="server" OnValueChanged="hddLugarEventoId_ValueChanged" />
					<asp:AutoCompleteExtender
						ID="autosuggestLugarEvento" 
						runat="server"
						TargetControlID="txtLugarEvento"
						ServiceMethod="WSLugarEvento"
                        ServicePath=""
						CompletionInterval="100"
                        DelimiterCharacters=""
                        Enabled="True"
						EnableCaching="False"
						MinimumPrefixLength="2"
						OnClientItemSelected="LugarEventoSelected"
						CompletionListCssClass="Autocomplete_CompletionListElement"
						CompletionListItemCssClass="Autocomplete_ListItem"
						CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                    />

				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Municipio</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtMunicipio" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Colonia</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtColonia" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Calle</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtCalle" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Numero Exterior</td>
				<td class="Espacio"></td>
				<td class="Etiqueta">
                    <asp:TextBox ID="txtNumeroExterior" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="130px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Numero Interior&nbsp;&nbsp;
                    <asp:TextBox ID="txtNumeroInterior" runat="server" CssClass="Textbox_Disabled" Enabled="false" Width="130px"></asp:TextBox>
				</td>
                <td></td>
			</tr>
        </table>
        <table border="0" style="width:100%">
			<tr>
				<td class="Etiqueta">Detalle de evento</td>
                <td class="Espacio"></td>
				<td class="Campo"></td>
				<td></td>
			</tr>
			<tr>
				<td colspan="4" style="text-align:left; vertical-align:bottom;">
					<CKEditor:CKEditorControl ID="ckeObservaciones" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl>
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

    <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>