<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girNuevaGira.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girNuevaGira" %>
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
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Nueva Gira"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Registre una nueva gira. "></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <asp:TabContainer ID="tabGira" runat="server" ActiveTabIndex="0" CssClass="Tabcontainer_General" Height="435px" >
            <asp:TabPanel ID="tpnlDatosGira" runat="server" Height="400px">
                <HeaderTemplate>Datos de la Gira</HeaderTemplate>
                <ContentTemplate>
                    <br />
                    <table class="FormTable">
                        <tr>
				            <td class="Etiqueta">Nombre de la gira</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo"><asp:TextBox ID="txtNombreGira" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="1000" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                            <td></td>
			            </tr>
                        <tr>
				            <td class="Etiqueta">Fecha y hora de la gira</td>
				            <td class="VinetaObligatorio">*</td>
				            <td class="Campo">
                                <table style="border:0px; padding:0px; width:100%;">
                                    <tr>
                                        <td style="text-align:left; width:200px;">
                                            <wuc:wucCalendar ID="wucCalendar" runat="server" />
                                        </td>
                                        <td style="text-align:left; width:300px;">
                                            <wuc:wucTimer ID="wucTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;a&nbsp;&nbsp;&nbsp;
                                            <wuc:wucTimer ID="wucTimerHasta" runat="server" />&nbsp;HRS.
                                        </td>
                                    </tr>
                                </table>
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
				            <td class="Etiqueta" style="width:180px">Detalle</td>
                            <td class="Espacio"></td>
				            <td class="Campo"></td>
				            <td></td>
			            </tr>
			            <tr>
				            <td colspan="4" style="text-align:left; vertical-align:bottom;">
					            <CKEditor:CKEditorControl ID="ckeGiraDetalle" runat="server" BasePath="~/Include/Components/CKEditor/Core" Height="165px" ContentsCss="~/Include/Components/CKEditor/Core/contents.css" TemplatesFiles="~/Include/Components/CKEditor/Core/plugins/templates/templates/default.js" Width=""></CKEditor:CKEditorControl>
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

</asp:Content>
