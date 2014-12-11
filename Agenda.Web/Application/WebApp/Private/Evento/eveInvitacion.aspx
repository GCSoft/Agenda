<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveInvitacion.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveInvitacion" %>
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
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Eventos - Invitación"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Registre una nueva invitación a un evento. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <asp:TabContainer ID="tabInvitacion" runat="server" ActiveTabIndex="0" CssClass="Tabcontainer_General" Height="435px" >
            <asp:TabPanel ID="tpnlDetalle" runat="server">
                <HeaderTemplate>Datos generales</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
                        <tr>
				            <td class="Etiqueta">Categoría</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:DropDownList ID="ddlCategoria" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Prioridad</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Tipo de Evento</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:DropDownList ID="ddlTipoEvento" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Lugar del evento</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo">

                                <script type = "text/javascript"> function LugarEventoSelected(sender, e) { $get("<%=hddLugarEventoId.ClientID %>").value = e.get_value(); } </script>
								<asp:TextBox ID="txtLugarEvento" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
								<asp:HiddenField ID="hddLugarEventoId" runat="server" />
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
				            <td class="Etiqueta">Fecha y hora del evento</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo">
                                <table style="border:0px; padding:0px; width:100%;">
                                    <tr>
                                        <td style="text-align:left; width:200px;">
                                            <wuc:wucCalendar ID="wucCalendar" runat="server" />
                                        </td>
                                        <td style="text-align:left; width:300px;">
                                            <wuc:wucTimer ID="wucTimer" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
				            </td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Adjuntar invitación</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:FileUpload ID="fupInvitacion" runat="server" Width="405px" /></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta"></td>
				            <td class="Espacio"></td>
				            <td class="Campo">
                                <asp:CheckBox ID="chkEsposa" runat="server" CssClass="CheckBox_Regular" Text="Acompañado por la esposa" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkEventoCivico" runat="server" CssClass="CheckBox_Regular" Text="Evento cívico" />
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
								<CKEditor:CKEditorControl ID="ckeDetalleEvento" BasePath="~/Include/Components/CKEditor/Core" runat="server" Height="85px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
							</td>
						</tr>
					</table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tpnlUbicacion" runat="server" Height="400px">
                <HeaderTemplate>Ubicación del evento</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
                        <tr>
				            <td class="Etiqueta">Municipio</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="DropDownList_General" Width="216px" AutoPostBack="true" OnSelectedIndexChanged="ddlMunicipio_SelectedIndexChanged" ></asp:DropDownList></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Colonia</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo">

                                <script type = "text/javascript"> function ColoniaSelected(sender, e) { $get("<%=hddColoniaId.ClientID %>").value = e.get_value(); } </script>
								<asp:TextBox ID="txtColonia" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
								<asp:HiddenField ID="hddColoniaId" runat="server" />
								<asp:AutoCompleteExtender
									ID="autosuggestColonia" 
									runat="server"
									TargetControlID="txtColonia"
									ServiceMethod="WSColonia"
                                    ServicePath=""
									CompletionInterval="100"
                                    DelimiterCharacters=""
                                    Enabled="True"
									EnableCaching="False"
									MinimumPrefixLength="2"
									OnClientItemSelected="ColoniaSelected"
									CompletionListCssClass="Autocomplete_CompletionListElement"
									CompletionListItemCssClass="Autocomplete_ListItem"
									CompletionListHighlightedItemCssClass="Autocomplete_HighLightedListItem"
                                    UseContextKey="true"
                                />

				            </td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Calle</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtCalle" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Numero Exterior</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtNumeroExterior" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Numero Interior</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtNumeroInterior" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
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
								<CKEditor:CKEditorControl ID="ckeUbicacionComentario" BasePath="~/Include/Components/CKEditor/Core" runat="server" Height="124px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
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
				            <td class="Campo"><asp:TextBox ID="txtFuncionarioNombre" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Puesto</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtFuncionarioPuesto" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Organizacion</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtFuncionarioOrganizacion" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Teléfono</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtFuncionarioTelefono" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Correo</td>
				            <td class="Espacio"></td>
				            <td class="Campo"><asp:TextBox ID="txtFuncionarioEmail" runat="server" CssClass="Textbox_General" Width="210px"></asp:TextBox></td>
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
								<CKEditor:CKEditorControl ID="ckeContactoComentarios" BasePath="~/Include/Components/CKEditor/Core" runat="server" Height="125px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
							</td>
						</tr>
					</table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tpnlFuncionarios" runat="server" Height="400px">
                <HeaderTemplate>Asociar funcionarios</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
                        <tr>
				            <td class="Etiqueta">Funcionarios</td>
				            <td class="Espacio"></td>
				            <td style="text-align:left;">
                                <table style="border:0px; padding:0px; width:100%;">
                                    <tr>
                                        <td style="text-align:left; width:230px;">
                                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList>
                                        </td>
                                        <td style="text-align:left; width:300px;">
                                            <asp:Button ID="btnAsociarFuncionario" runat="server" Text="Asociar Funcionario" CssClass="Button_General" Width="150px" OnClick="btnAsociarFuncionario_Click" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
				            </td>
			            </tr>
                        <tr>
                            <td colspan="3">
                                <div style="border:1px solid #808080; background-color:#f5f3f3; height:350px; overflow-x:hidden; overflow-y:scroll; text-align:left; width:100%;">
								    <asp:GridView ID="gvFuncionario" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="UsuarioId, FuncionarioNombre"
                                        OnRowCommand="gvFuncionario_RowCommand"
                                        OnRowDataBound="gvFuncionario_RowDataBound"
                                        OnSorting="gvFuncionario_Sorting">
                                        <HeaderStyle CssClass="Grid_Header_PopUp" />
                                        <RowStyle CssClass="Grid_Row_PopUp" />
                                        <EmptyDataTemplate>
                                            <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                                <tr class="Grid_Header_PopUp">
                                                    <td>Funcionario</td>
                                                </tr>
                                                <tr class="Grid_Row">
                                                    <td style="text-align:center;">No se han asociado funcionarios al evento</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Funcionario"    ItemStyle-HorizontalAlign="Left"    SortExpression="FuncionarioNombre"    DataField="FuncionarioNombre"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="Button_General" Width="125px" OnClick="btnRegistrar_Click" />&nbsp;
        <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="Button_General" Width="125px" OnClick="btnAprobar_Click" OnClientClick="return confirm('¿Seguro que desea generar un evento sin ser comentado por los funcionarios?');" />&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Button_General" Width="125px" OnClick="btnCancelar_Click" />
    </asp:Panel> 

    <br /><br />
    
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
