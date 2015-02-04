<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveNuevoEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveNuevoEvento" %>
<%@ Register Src="~/Include/WebUserControls/wucCalendar.ascx" TagPrefix="wuc" TagName="wucCalendar" %>
<%@ Register Src="~/Include/WebUserControls/wucTimer.ascx" TagPrefix="wuc" TagName="wucTimer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconGeneral.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Nuevo Evento"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Registre un nuevo evento. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <asp:TabContainer ID="tabEvento" runat="server" ActiveTabIndex="0" CssClass="Tabcontainer_General" Height="435px" >
            <asp:TabPanel ID="tpnlDatosGenerales" runat="server">
                <HeaderTemplate>Datos generales</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
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
								<CKEditor:CKEditorControl ID="ckeObservaciones" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="125px"></CKEditor:CKEditorControl>
							</td>
						</tr>
					</table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tpnlDatosEvento" runat="server" Height="400px">
                <HeaderTemplate>Datos del evento</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
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
                                            <wuc:wucTimer ID="wucTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;a&nbsp;&nbsp;&nbsp;
                                            <wuc:wucTimer ID="wucTimerHasta" runat="server" />
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
							<td class="Etiqueta" style="width:180px">Detalle de evento</td>
                            <td class="Espacio"></td>
							<td class="Campo"></td>
							<td></td>
						</tr>
						<tr>
							<td colspan="4" style="text-align:left; vertical-align:bottom;">
								<CKEditor:CKEditorControl ID="ckeDetalleEvento" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="90px"></CKEditor:CKEditorControl>
							</td>
						</tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tpnlContacto" runat="server" Height="400px">
                <HeaderTemplate>Contacto</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
                        <tr>
				            <td class="Etiqueta">Nombre</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtContactoNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Puesto</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtContactoPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Organizacion</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtContactoOrganizacion" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Teléfono</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtContactoTelefono" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Correo</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtContactoEmail" runat="server" CssClass="Textbox_General" MaxLength="100" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                    </table>
                    <br /><br />
                    <table border="0" style="width:100%">
						<tr>
							<td class="Etiqueta" style="width:180px">Comentarios</td>
                            <td class="Espacio"></td>
							<td class="Campo"></td>
							<td></td>
						</tr>
						<tr>
							<td colspan="4" style="text-align:left; vertical-align:bottom;">
								<CKEditor:CKEditorControl ID="ckeContactoComentarios" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="130px"></CKEditor:CKEditorControl>
							</td>
						</tr>
					</table>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnRegistrar" runat="server" Text="Agendar" CssClass="Button_General" Width="125px" OnClick="btnRegistrar_Click" />&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Button_General" Width="125px" OnClick="btnCancelar_Click" OnClientClick="return confirm('Esta opción borrará todos los campos capturados, ¿Seguro que desea continuar?');" />
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>
    
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
