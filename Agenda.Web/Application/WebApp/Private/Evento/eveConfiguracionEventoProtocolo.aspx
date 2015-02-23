<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveConfiguracionEventoProtocolo.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveConfiguracionEventoProtocolo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">
        
    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Programa"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite el programa del evento."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        
        <%-- Carátula --%>
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Evento</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblEventoNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblEventoFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>

        <%-- Sección: Datos Generales --%>
        <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="AccordionPane1" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label1" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Datos Generales</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Tipo de vestimenta</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlTipoVestimenta" runat="server" CssClass="DropDownList_General" Width="216px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoVestimenta_SelectedIndexChanged"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"></td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtTipoVestimentaOtro" runat="server" CssClass="Textbox_Disabled" MaxLength="1000" Width="400px" Enabled="false"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Prensa</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlMedioComunicacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Aforo</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtAforo" runat="server" CssClass="Textbox_General" MaxLength="5" Text="0" Width="130px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Invitación a</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloInvitacionA" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Responsable</td>
				                <td class="Espacio"></td>
				                <td class="Campo" colspan="2"><asp:TextBox ID="txtProtocoloResponsableEvento" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Sección: Acomodo --%>
        <asp:Accordion ID="Accordion2" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="AccordionPane2" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label2" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Acomodo</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Tipo de Acomodo</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlTipoAcomodo" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtAcomodoNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Puesto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtAcomodoPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarAcomodo" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarAcomodo_Click" /></td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvAcomodo" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Puesto"
                                        OnRowCommand="gvAcomodo_RowCommand"
                                        OnRowDataBound="gvAcomodo_RowDataBound"
                                        OnSorting="gvAcomodo_Sorting">
                                        <HeaderStyle CssClass="Grid_Header_PopUp" />
                                        <RowStyle CssClass="Grid_Row_PopUp" />
                                        <EmptyDataTemplate>
                                            <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                                <tr class="Grid_Header_PopUp">
                                                    <td style="width:100px;">Orden</td>
                                                    <td style="width:400px;">Nombre</td>
                                                    <td>Puesto</td>
                                                </tr>
                                                <tr class="Grid_Row">
                                                    <td colspan="3">No se ha capturado el Acomodo</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="400px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                            <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
			                </tr>
                            <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Orden del Día --%>
        <asp:Accordion ID="Accordion3" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="AccordionPane3" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label3" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Orden del Día</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Detalle</td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Campo" colspan="4"><asp:TextBox ID="txtOrdenDiaDetalle" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarOrdenDia" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarOrdenDia_Click" /></td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvOrdenDia" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Detalle"
                                        OnRowCommand="gvOrdenDia_RowCommand"
                                        OnRowDataBound="gvOrdenDia_RowDataBound"
                                        OnSorting="gvOrdenDia_Sorting">
                                        <HeaderStyle CssClass="Grid_Header_PopUp" />
                                        <RowStyle CssClass="Grid_Row_PopUp" />
                                        <EmptyDataTemplate>
                                            <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                                <tr class="Grid_Header_PopUp">
                                                    <td style="width:100px;">Orden</td>
                                                    <td>Detalle</td>
                                                </tr>
                                                <tr class="Grid_Row">
                                                    <td colspan="3">No se ha capturado la orden del día</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"      ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Detalle"    ItemStyle-HorizontalAlign="Left"                            DataField="Detalle" SortExpression="Detalle"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
			                </tr>
                            <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Requerimentos Técnicos y Montaje --%>
        <asp:Accordion ID="Accordion4" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="AccordionPane4" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label4" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Requerimentos Técnicos y Montaje</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Banderas</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloBandera" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Leyenda</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloLeyenda" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Responsable</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloResponsable" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Sonido</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloSonido" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Responsable Sonido</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloResponsableSonido" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Desayuno</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloDesayuno" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Sillas</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloSillas" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Mesas</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloMesas" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Presentación</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtProtocoloPresentacion" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"></td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Asistentes --%>
        <asp:Accordion ID="Accordion5" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="AccordionPane5" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label5" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Asistentes</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
                        <table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteRecepcionNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Puesto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteRecepcionPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarComiteRecepcion" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarComiteRecepcion_Click" /></td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvComiteRecepcion" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Puesto"
                                        OnRowCommand="gvComiteRecepcion_RowCommand"
                                        OnRowDataBound="gvComiteRecepcion_RowDataBound"
                                        OnSorting="gvComiteRecepcion_Sorting">
                                        <HeaderStyle CssClass="Grid_Header_PopUp" />
                                        <RowStyle CssClass="Grid_Row_PopUp" />
                                        <EmptyDataTemplate>
                                            <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                                <tr class="Grid_Header_PopUp">
                                                    <td style="width:100px;">Orden</td>
                                                    <td style="width:400px;">Nombre</td>
                                                    <td>Puesto</td>
                                                </tr>
                                                <tr class="Grid_Row">
                                                    <td colspan="3">No se ha capturado el comité de recepción</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="400px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                            <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDelete" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Eliminar" ImageUrl="~/Include/Image/Buttons/Delete.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
			                </tr>
                            <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

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
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>
