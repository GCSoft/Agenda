<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="girConfiguracionGira.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Gira.girConfiguracionGira" %>
<%@ Register Src="~/Include/WebUserControls/wucGiraAgrupacion.ascx" TagPrefix="wuc" TagName="wucGiraAgrupacion" %>
<%@ Register Src="~/Include/WebUserControls/wucTimer.ascx" TagPrefix="wuc" TagName="wucTimer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Historial de cambios"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Se presentan los cambios realizados al Gira."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Nombre de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraNombre" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta">Fecha de la Gira</td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"><asp:Label ID="lblGiraFechaHora" CssClass="Label_Detalle_Invitacion" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td class="Etiqueta"></td>
				<td class="Espacio"></td>
				<td class="Campo" colspan="2"></td>
                <td></td>
			</tr>
        </table>

    </asp:Panel>

    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:Panel ID="pnlBotones" runat="server" CssClass="ButtonPanel">
        <asp:Button ID="btnTrasladoVehiculo" runat="server" Text="Agregar traslado en vehículo" CssClass="Button_General" width="175px" onclick="btnTrasladoVehiculo_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnTrasladoHelicoptero" runat="server" Text="Agregar traslado en vehículo" CssClass="Button_General" width="175px" onclick="btnTrasladoHelicoptero_Click" /> &nbsp;&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="Button_General" width="125px" onclick="btnRegresar_Click" />
    </asp:Panel>

    <asp:Panel ID="pnlGrid" runat="server" CssClass="GridPanel">
        <asp:GridView ID="gvPrograma" runat="server" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" Width="100%"
			DataKeyNames="GiraConfiguracionId,TipoGiraConfiguracionId,ConfiguracionDetalle" 
			OnRowDataBound="gvPrograma_RowDataBound"
            OnSorting="gvPrograma_Sorting">
            <RowStyle CssClass="Grid_Row" />
            <EditRowStyle Wrap="True" />
            <HeaderStyle CssClass="Grid_Header" ForeColor="#E3EBF5" />
            <AlternatingRowStyle CssClass="Grid_Row_Alternating" />
            <EmptyDataTemplate>
                <table border="1px" cellpadding="0px" cellspacing="0px" style="text-align:center; width:100%;">
                    <tr class="Grid_Header">
                        <td style="width:150px;">Grupo</td>
						<td style="width:100px;">Fecha</td>
						<td style="width:100px;">Inicio</td>
                        <td style="width:100px;">Fin</td>
                        <td>Detalle</td>
                    </tr>
                    <tr class="Grid_Row">
                        <td colspan="5">No se ha capturado el programa de la gira</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
				<asp:BoundField HeaderText="Grupo"      ItemStyle-HorizontalAlign="Left"	ItemStyle-Width="150px"	DataField="ConfiguracionGrupo"                      SortExpression="ConfiguracionGrupo"></asp:BoundField>
				<asp:BoundField HeaderText="Fecha"      ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="GiraFechaEstandar"                       SortExpression="GiraFechaEstandar"></asp:BoundField>
				<asp:BoundField HeaderText="Inicio"     ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="ConfiguracionHoraInicioEstandar"         SortExpression="ConfiguracionHoraInicioEstandar"></asp:BoundField>
				<asp:BoundField HeaderText="Fin"        ItemStyle-HorizontalAlign="Center"	ItemStyle-Width="100px"	DataField="ConfiguracionHoraFinEstandar"            SortExpression="ConfiguracionHoraFinEstandar"></asp:BoundField>
                <asp:BoundField HeaderText="Detalle"    ItemStyle-HorizontalAlign="Left"							DataField="ConfiguracionDetalle" HtmlEncode="false" SortExpression="ConfiguracionDetalle"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_TrasladoVehiculo" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_TrasladoVehiculoContent" runat="server" CssClass="PopUpContent" style="margin-top:-145px; margin-left:-260px;" Height="290px" Width="520px">
            <asp:Panel ID="pnlPopUp_TrasladoVehiculoHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_TrasladoVehiculoTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_TrasladoVehiculo" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_TrasladoVehiculo_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_TrasladoVehiculoBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoVehiculoDetalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                        <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                        <td class="Campo"><wuc:wucGiraAgrupacion runat="server" id="wucGiraAgrupacion" /></td>
                    </tr>
                    <tr>
				        <td class="Etiqueta">Desde las</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo">
                            <wuc:wucTimer ID="wucPopUp_TrasladoVehiculoTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                            <wuc:wucTimer ID="wucPopUp_TrasladoVehiculoTimerHasta" runat="server" />
				        </td>
			        </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:DropDownList ID="ddlPopUp_TrasladoVehiculoStatus" runat="server" CssClass="DropDownList_General" Width="395px">
                                <asp:ListItem Text="Activo" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_TrasladoVehiculoCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_TrasladoVehiculoCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_TrasladoVehiculoMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlPopUp_TrasladoHelicoptero" runat="server" CssClass="PopUpBlock">
        <asp:Panel ID="pnlPopUp_TrasladoHelicopteroContent" runat="server" CssClass="PopUpContent" style="margin-top:-145px; margin-left:-260px;" Height="290px" Width="520px">
            <asp:Panel ID="pnlPopUp_TrasladoHelicopteroHeader" runat="server" CssClass="PopUpHeader">
                <table class="PopUpHeaderTable">
                    <tr>
                        <td class="Espacio"></td>
                        <td class="Etiqueta"><asp:Label ID="lblPopUp_TrasladoHelicopteroTitle" runat="server" CssClass="PopUpHeaderTitle"></asp:Label></td>
                        <td class="Cierre"><asp:ImageButton ID="imgCloseWindow_TrasladoHelicoptero" runat="server" ImageUrl="~/Include/Image/Buttons/CloseWindow.png" ToolTip="Cerrar Ventana" OnClick="imgCloseWindow_TrasladoHelicoptero_Click"></asp:ImageButton></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopUp_TrasladoHelicopteroBody" runat="server" CssClass="PopUpBody">
                <table class="PopUpBodyTable">
                    <tr>
                        <td class="Etiqueta">Nombre</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo"><asp:TextBox ID="txtPopUp_TrasladoHelicopteroDetalle" runat="server" CssClass="Textbox_General" MaxLength="1000" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="Etiqueta" style="vertical-align:top;">Agrupación</td>
                        <td class="VinetaObligatorio" style="vertical-align:top;">*</td>
                        <td class="Campo"><wuc:wucGiraAgrupacion runat="server" id="wucGiraAgrupacion1" /></td>
                    </tr>
                    <tr>
				        <td class="Etiqueta">Desde las</td>
				        <td class="VinetaObligatorio">*</td>
				        <td class="Campo">
                            <wuc:wucTimer ID="wucPopUp_TrasladoHelicopteroTimerDesde" runat="server" />&nbsp;&nbsp;&nbsp;hasta las&nbsp;&nbsp;&nbsp;
                            <wuc:wucTimer ID="wucPopUp_TrasladoHelicopteroTimerHasta" runat="server" />
				        </td>
			        </tr>
                    <tr>
                        <td class="Etiqueta">Estatus</td>
                        <td class="VinetaObligatorio">*</td>
                        <td class="Campo">
                            <asp:DropDownList ID="ddlPopUp_TrasladoHelicopteroStatus" runat="server" CssClass="DropDownList_General" Width="395px">
                                <asp:ListItem Text="Activo" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Botones" colspan="3">
                            <asp:Button ID="btnPopUp_TrasladoHelicopteroCommand" runat="server" Text="" CssClass="Button_General" Width="175px" OnClick="btnPopUp_TrasladoHelicopteroCommand_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Mensajes" colspan="3">
                            <asp:Label ID="lblPopUp_TrasladoHelicopteroMessage" runat="server" CssClass="PopUpTextMessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddGiraId" runat="server" Value="0" />
    <asp:HiddenField ID="hddGiraConfiguracionId" runat="server" Value="0"  />
    <asp:HiddenField ID="SenderId" runat="server" Value="0"  />
    <asp:HiddenField ID="hddSort" runat="server" Value="ConfiguracionGrupo" />

</asp:Content>
