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

        <%-- Sección: Nombre del evento --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Nombre del evento</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
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
				<td class="Campo"><asp:TextBox ID="txtTipoVestimentaOtro" runat="server" CssClass="Textbox_Disabled" MaxLength="200" Width="400px" Enabled="false"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Medios de comunicación</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:DropDownList ID="ddlMedioComunicacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Medio de traslado</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:CheckBoxList ID="chklMedioTraslado" runat="server" CssClass="CheckBox_Regular" RepeatDirection="Horizontal" ></asp:CheckBoxList></td>
                <td></td>
			</tr>
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
				<td class="Etiqueta">Aforo</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtAforo" runat="server" CssClass="Textbox_General" MaxLength="5" Text="0" Width="130px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Tipo de montaje</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtTipoMontaje" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Lugar de arribo</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtLugarArribo" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                <td></td>
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
				<td class="Etiqueta">Acción a realizar</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtAccionRealizar" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Caracteristicas de invitados</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtCaracteristicasInvitados" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Menú</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtMenu" runat="server" CssClass="Textarea_General" Height="70px" MaxLength="200" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
        </table>
        <br /><br />

        <%-- Sección: Comité de recepción --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Comité de recepción</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Nombre</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtComiteRecepcionNombre" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Puesto</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtComiteRecepcionPuesto" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta"><asp:Button ID="btnAgregarComiteRecepcion" runat="server" Text="Actualizar" CssClass="Button_General" Width="125px" OnClick="btnAgregarComiteRecepcion_Click" /></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
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
                                    <td style="width:80px;">Orden</td>
                                    <td style="width:300px;">Nombre</td>
                                    <td>Puesto</td>
                                </tr>
                                <tr class="Grid_Row">
                                    <td colspan="3">No se ha capturado el comité de recepción</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="Orden"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px"  DataField="Orden"   SortExpression="Orden"></asp:BoundField>
                            <asp:BoundField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"    ItemStyle-Width="300px" DataField="Nombre"  SortExpression="Nombre"></asp:BoundField>
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
        </table>
        <br /><br />

        <%-- Sección: Orden del día --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Orden del día</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>
        <br /><br />

        <%-- Sección: Presídium --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Presídium</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>
        <br /><br />

        <%-- Sección: Responsable del evento --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Responsable del evento</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>
        <br /><br />

        <%-- Sección: Responsable de logística --%>
        <table class="FormTable">
            <tr>
                <td class="Encabezado" colspan="4">Responsable de logística</td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo"></td>
                <td></td>
			</tr>
        </table>
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

