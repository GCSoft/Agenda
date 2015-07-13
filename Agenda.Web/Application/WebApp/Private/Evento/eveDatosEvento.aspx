<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveDatosEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveDatosEvento" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>
<%@ Register Src="~/Include/WebUserControls/wucTimer.ascx" TagPrefix="wuc" TagName="wucTimer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
    <script type = "text/javascript">

        function ColoniaSelected_LugarEvento(sender, e) {
            $get("<%=hddPopUpColoniaId_LugarEvento.ClientID %>").value = e.get_value();
        }

    </script>
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
				<td class="Campo"><asp:TextBox ID="txtNombreEvento" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="1000" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha y hora del evento</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo">
                    <table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:250px;">
                                Desde&nbsp;<wuc:wucCalendar ID="wucCalendarInicio" runat="server" />
                            </td>
                            <td style="text-align:left; width:300px;">
                                <wuc:wucTimer ID="wucTimerDesde" runat="server" />&nbsp;HRS.
                            </td>
                        </tr>
                    </table>
				</td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="VinetaObligatorio"></td>
				<td class="Campo">
                    <table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:250px;">
                                Hasta&nbsp;&nbsp;<wuc:wucCalendar ID="wucCalendarFin" runat="server" />
                            </td>
                            <td style="text-align:left; width:300px;">
                                <wuc:wucTimer ID="wucTimerHasta" runat="server" />&nbsp;HRS.
                            </td>
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
					<table style="border:0px; padding:0px; width:100%;">
                        <tr>
                            <td style="text-align:left; width:520px;">
                                <asp:TextBox ID="txtLugarEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                            </td>
                            <td style="text-align:left; width:30px;">
                                <asp:Button ID="btnNuevoLugarEvento" runat="server" Text="+" CssClass="Button_Special_Blue" ToolTip="Nuevo Lugar de evento" Width="25px" OnClick="btnNuevoLugarEvento_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
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

    <asp:Panel ID="pnlPopUp_LugarEvento" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUpContent_LugarEvento" runat="server" CssClass="PopUpContent" style="margin-top:-285px; margin-left:-310px;" Height="570px" Width="620px">
            <asp:Panel ID="pnlPopUpHeader_LugarEvento" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUpTitle_LugarEvento" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_LugarEvento" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_LugarEvento_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUpBody_LugarEvento" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpNombre_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Estado</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpEstado_LugarEvento" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpEstado_LugarEvento_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Municipio</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:DropDownList ID="ddlPopUpMunicipio_LugarEvento" runat="server" CssClass="DropDownList_General" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlPopUpMunicipio_LugarEvento_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Colonia</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
							<asp:TextBox ID="txtPopUpColonia_LugarEvento" runat="server" CssClass="Textbox_General_Disabled" MaxLength="1000" Width="400px" Enabled="false"></asp:TextBox>
							<asp:HiddenField ID="hddPopUpColoniaId_LugarEvento" runat="server" />
							<asp:AutoCompleteExtender
								ID="autosuggestColonia_LugarEvento" 
								runat="server"
								TargetControlID="txtPopUpColonia_LugarEvento"
								ServiceMethod="WSColonia"
                                ServicePath=""
								CompletionInterval="100"
                                DelimiterCharacters=""
                                Enabled="True"
								EnableCaching="False"
								MinimumPrefixLength="2"
								OnClientItemSelected="ColoniaSelected_LugarEvento"
								CompletionListCssClass="Autocomplete_CompletionListElement"
								CompletionListItemCssClass="Autocomplete_ListItem"
								CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                                UseContextKey="true"
                            />
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Calle</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUpCalle_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta"># Ext</td>
				        <td class="VinetaObligatorio"></td>
				        <td class="Etiqueta">
                            <asp:TextBox ID="txtPopUpNumeroExterior_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            # Int&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPopUpNumeroInterior_LugarEvento" runat="server" CssClass="Textbox_General" MaxLength="50" Width="130px"></asp:TextBox>
				        </td>
                    </tr>
                    <tr>
				        <td colspan="3">
                            <CKEditor:CKEditorControl ID="ckePopUpDescripcion_LugarEvento" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="140px"></CKEditor:CKEditorControl>
				        </td>
			        </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUpCommand_LugarEvento" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUpCommand_LugarEvento_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblPopUpMessage_LugarEvento" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />

</asp:Content>