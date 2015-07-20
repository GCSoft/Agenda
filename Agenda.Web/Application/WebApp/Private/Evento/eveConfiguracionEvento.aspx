<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveConfiguracionEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveConfiguracionEvento" %>
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
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>

        <%-- Sección: Nombre del evento --%>
        <asp:Accordion ID="acrdNombreEvento" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanNombreEvento" runat="server">
					<Header>
                        <table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label1" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Evento</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Pronóstico</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtPronostico" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Temp. mínima</td>
				                <td class="Espacio"></td>
				                <td class="Campo">
                                    <asp:TextBox ID="txtTemperaturaMinima" runat="server" CssClass="Textbox_General" MaxLength="10" Width="130px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Máxima&nbsp;&nbsp;
                                    <asp:TextBox ID="txtTemperaturaMaxima" runat="server" CssClass="Textbox_General" MaxLength="10" Width="130px"></asp:TextBox>
				                </td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Lugar de arribo</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtLugarArribo" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Medio de traslado</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:CheckBoxList ID="chklMedioTraslado" runat="server" CssClass="CheckBox_Regular" RepeatDirection="Horizontal" ></asp:CheckBoxList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Tipo de montaje</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtTipoMontaje" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Aforo</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtAforo" runat="server" CssClass="Textbox_General" MaxLength="5" Text="0" Width="130px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Caracteristicas de invitados</td>
				                <td class="Espacio"></td>
				                <td class="Campo" colspan="2"><asp:TextBox ID="txtCaracteristicasInvitados" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Esposa</td>
				                <td class="Espacio"></td>
				                <td class="Campo" colspan="2">
                                    <table style="border:0px; border-spacing:0px; width:100%;">
                                        <tr>
                                            <td style="text-align:left; width:150px;">
                                                <asp:CheckBox ID="chkEsposaInvitada" runat="server" CssClass="CheckBox_Regular" Text="Invitada" AutoPostBack="True" OnCheckedChanged="chkEsposaInvitada_CheckedChanged" />
                                            </td>
                                            <td style="text-align:left; width:70px;">
                                                Asiste:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:RadioButtonList ID="rblConfirmacionEsposa" runat="server" RepeatDirection="Horizontal" Enabled ="false">
						                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
						                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Pendiente" Value="3"></asp:ListItem>
					                            </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
				                </td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Medios de comunicación</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlMedioComunicacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
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
				                <td class="Etiqueta">Menú</td>
				                <td class="Espacio"></td>
				                <td class="Campo" colspan="2"><asp:TextBox ID="txtMenu" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Acción a realizar</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo" colspan="2"><asp:TextBox ID="txtAccionRealizar" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                             <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Sección: Comité de Recepción en el Helipuerto Provisional --%>
        <asp:Accordion ID="acrdComiteHelipuerto" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanComiteHelipuerto" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label7" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Comité de Recepción en el Helipuerto Provisional</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Incluir comité de Helipuerto</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlComiteHelipuerto" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Lugar</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteHelipuertoLugar" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Domicilio</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteHelipuertoDomicilio" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Coordenadas</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteHelipuertoCoordenadas" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtComiteHelipuertoNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Puesto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtComiteHelipuertoPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarComiteHelipuerto" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarComiteHelipuerto_Click" /></td>
				                <td class="Espacio"></td>
                                <td colspan="2"><asp:Label ID="lblComiteHelipuerto" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvComiteHelipuerto" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Puesto"
                                        OnRowCommand="gvComiteHelipuerto_RowCommand"
                                        OnRowDataBound="gvComiteHelipuerto_RowDataBound"
                                        OnSorting="gvComiteHelipuerto_Sorting">
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
                                                    <td colspan="3">No se ha capturado el comité de recepción en el helipuerto provisional</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="400px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                            <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Comité de recepción --%>
        <asp:Accordion ID="acrdComiteRecepcion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanComiteRecepcion" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label2" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Comité de recepción</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
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
				                <td colspan="2"><asp:Label ID="lblComiteRecepcion" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Orden del día --%>
        <asp:Accordion ID="acrdOrdenDia" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanOrdenDia" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label3" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Orden del día</asp:Label>
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
				                <td class="Campo" colspan="4"><asp:TextBox ID="txtOrdenDiaDetalle" runat="server" CssClass="Textarea_General" Height="70px" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarOrdenDia" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarOrdenDia_Click" /></td>
				                <td class="Espacio"></td>
				                <td colspan="2"><asp:Label ID="lblOrdenDia" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Acomodo --%>
        <asp:Accordion ID="acrdAcomodo" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanAcomodo" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label4" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Acomodo</asp:Label>
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
				                <td class="Etiqueta">Incluir propuesta de Acomodo</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlPropuestaAcomodo" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
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
				                <td colspan="2"><asp:Label ID="lblAcomodo" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Listado adicional --%>
        <asp:Accordion ID="acrdListadoAdicional" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanListadoAdicional" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label9" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Listado adicional</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Incluir listado adicional</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:DropDownList ID="ddlListadoAdicional" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Título de sección</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtListadoAdicionalTitulo" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtListadoAdicionalNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Puesto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtListadoAdicionalPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarListadoAdicional" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarListadoAdicional_Click" /></td>
				                <td class="Espacio"></td>
				                <td colspan="2"><asp:Label ID="lblListadoAdicional" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvListadoAdicional" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Puesto"
                                        OnRowCommand="gvListadoAdicional_RowCommand"
                                        OnRowDataBound="gvListadoAdicional_RowDataBound"
                                        OnSorting="gvListadoAdicional_Sorting">
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Observaciones --%>
        <asp:Accordion ID="acrdObservaciones" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanObservaciones" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label8" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Observaciones</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Observaciones</td>
				                <td class="Espacio"></td>
				                <td class="Campo"></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Campo" colspan="4"><asp:TextBox ID="txtAcomodoObservaciones" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
			                </tr>
                            <tr style="height:10px;"><td colspan="4"></td></tr>
                        </table>
					</Content>
				</asp:AccordionPane>
			</Panes>
		</asp:Accordion>
        <br /><br />

        <%-- Sección: Responsable del evento --%>
        <asp:Accordion ID="acrdResponsable" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanResponsable" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label5" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Responsable del evento</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtResponsableEventoNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Puesto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtResponsableEventoPuesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarResponsableEvento" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarResponsableEvento_Click" /></td>
				                <td class="Espacio"></td>
				                <td colspan="2"><asp:Label ID="lblResponsableEvento" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvResponsableEvento" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Puesto"
                                        OnRowCommand="gvResponsableEvento_RowCommand"
                                        OnRowDataBound="gvResponsableEvento_RowDataBound"
                                        OnSorting="gvResponsableEvento_Sorting">
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
                                                    <td colspan="3">No se han capturado responsables del evento</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="400px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
                                            <asp:BoundField HeaderText="Puesto" ItemStyle-HorizontalAlign="Left"                            DataField="Puesto"  SortExpression="Puesto"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Responsable de logística --%>
        <asp:Accordion ID="acrdResponsableLogistica" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanResponsableLogistica" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label6" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Responsable de logística</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Nombre</td>
				                <td class="VinetaObligatorio">*</td>
				                <td class="Campo"><asp:TextBox ID="txtResponsableLogisticaNombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta">Contacto</td>
				                <td class="Espacio"></td>
				                <td class="Campo"><asp:TextBox ID="txtResponsableLogisticaContacto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                                <td></td>
			                </tr>
                            <tr>
				                <td class="Etiqueta"><asp:Button ID="btnAgregarResponsableLogistica" runat="server" Text="Agregar" CssClass="Button_General" Width="125px" OnClick="btnAgregarResponsableLogistica_Click" /></td>
				                <td class="Espacio"></td>
				                <td colspan="2"><asp:Label ID="lblResponsableLogistica" runat="server" CssClass="PopUpTextMessage" Text="Es necesario confirmar la transacción para poder editar" Visible="false"></asp:Label></td>
			                </tr>
                            <tr><td colspan="4" style="height:10px;"></td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvResponsableLogistica" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="Orden,Nombre,Contacto"
                                        OnRowCommand="gvResponsableLogistica_RowCommand"
                                        OnRowDataBound="gvResponsableLogistica_RowDataBound"
                                        OnSorting="gvResponsableLogistica_Sorting">
                                        <HeaderStyle CssClass="Grid_Header_PopUp" />
                                        <RowStyle CssClass="Grid_Row_PopUp" />
                                        <EmptyDataTemplate>
                                            <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                                                <tr class="Grid_Header_PopUp">
                                                    <td style="width:100px;">Orden</td>
                                                    <td style="width:400px;">Nombre</td>
                                                    <td>Contacto</td>
                                                </tr>
                                                <tr class="Grid_Row">
                                                    <td colspan="3">No se han capturado responsables de logística</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px" DataField="Orden"       SortExpression="Orden"></asp:BoundField>
                                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="400px" DataField="Nombre"      SortExpression="Nombre"></asp:BoundField>
                                            <asp:BoundField HeaderText="Contacto" ItemStyle-HorizontalAlign="Left"                          DataField="Contacto"    SortExpression="Contacto"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" CommandArgument="<%#Container.DataItemIndex%>" CommandName="Editar" ImageUrl="~/Include/Image/Buttons/Edit.png" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

        <%-- Sección: Nota en Documento --%>
        <asp:Accordion ID="acrdNotaDocumento" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="False">
			<Panes>
				<asp:AccordionPane ID="apanNotaDocumento" runat="server">
					<Header>
						<table style="width:100%">
							<tr>
								<td>
									<div style="background: #fff url('../../../../Include/Image/Web/TituloAcordeon.png') no-repeat; bottom:-3px; cursor:pointer; height:25px; left:-3px; position:relative; text-align:left; width:100%;">
                                        <asp:Label ID="Label10" style="height:23px;" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White">&nbsp;Nota en Documento</asp:Label>
                                    </div>
								</td>
							</tr>
						</table>
					</Header>
					<Content>
						<table class="FormTable" style="border:solid 1px #336600;">
                            <tr>
				                <td class="Etiqueta">Nota en Documento</td>
				                <td class="Espacio"></td>
				                <td class="Campo" colspan="2">
                                    <asp:RadioButtonList ID="rblNotaDocumento" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Text="No Incluir" Value="1" Selected="True"></asp:ListItem>
										<asp:ListItem Text="Al Inicio del cuadernillo" Value="2"></asp:ListItem>
										<asp:ListItem Text="Al Final del cuadernillo" Value="3"></asp:ListItem>
									</asp:RadioButtonList>
				                </td>
			                </tr>
                            <tr>
				                <td class="Campo" colspan="4"><asp:TextBox ID="txtNotaDocumento" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
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

    <asp:Panel ID="pnlPopUp_ComiteHelipuerto" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ComiteHelipuertoContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_ComiteHelipuertoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ComiteHelipuertoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ComiteHelipuerto" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ComiteHelipuerto_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ComiteHelipuertoBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteHelipuerto_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteHelipuerto_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteHelipuerto_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteHelipuerto_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ComiteHelipuertoCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ComiteHelipuertoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ComiteHelipuertoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_ComiteRecepcion" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ComiteRecepcionContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_ComiteRecepcionHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ComiteRecepcionTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ComiteRecepcion" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ComiteRecepcion_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ComiteRecepcionBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteRecepcion_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteRecepcion_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteRecepcion_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpComiteRecepcion_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ComiteRecepcionCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ComiteRecepcionCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ComiteRecepcionMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_OrdenDia" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_OrdenDiaContent" runat="server" CssClass="PopUpContent" style="margin-top:-135px; margin-left:-245px;" Height="270px" Width="490px">
            <asp:Panel ID="pnlPopUp_OrdenDiaHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_OrdenDiaTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_OrdenDia" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_OrdenDia_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_OrdenDiaBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpOrdenDia_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpOrdenDia_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Detalle</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpOrdenDia_Detalle" runat="server" CssClass="Textarea_General" Height="70px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_OrdenDiaCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_OrdenDiaCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_OrdenDiaMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_Acomodo" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_AcomodoContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_AcomodoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_AcomodoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_Acomodo" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_Acomodo_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_AcomodoBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpAcomodo_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpAcomodo_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpAcomodo_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpAcomodo_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_AcomodoCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_AcomodoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_AcomodoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_ListadoAdicional" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ListadoAdicionalContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_ListadoAdicionalHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ListadoAdicionalTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ListadoAdicional" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ListadoAdicional_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ListadoAdicionalBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpListadoAdicional_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpListadoAdicional_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpListadoAdicional_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpListadoAdicional_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ListadoAdicionalCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ListadoAdicionalCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ListadoAdicionalMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_ResponsableEvento" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ResponsableEventoContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_ResponsableEventoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ResponsableEventoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ResponsableEvento" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ResponsableEvento_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ResponsableEventoBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableEvento_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableEvento_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableEvento_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Puesto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableEvento_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ResponsableEventoCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ResponsableEventoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ResponsableEventoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_ResponsableLogistica" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_ResponsableLogisticaContent" runat="server" CssClass="PopUpContent" style="margin-top:-125px; margin-left:-245px;" Height="250px" Width="490px">
            <asp:Panel ID="pnlPopUp_ResponsableLogisticaHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_ResponsableLogisticaTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_ResponsableLogistica" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_ResponsableLogistica_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_ResponsableLogisticaBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Orden Anterior</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableLogistica_OrdenAnterior" runat="server" CssClass="Textbox_General_Disabled" Enabled="false" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nuevo Orden</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableLogistica_Orden" runat="server" CssClass="Textbox_General" MaxLength="3" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableLogistica_Nombre" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Etiqueta">Contacto</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:TextBox ID="txtPopUpResponsableLogistica_Puesto" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_ResponsableLogisticaCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_ResponsableLogisticaCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_ResponsableLogisticaMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
    <asp:HiddenField ID="hddSort" runat="server" Value="Nombre" />

</asp:Content>

