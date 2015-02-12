<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveConfiguracionEvento.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveConfiguracionEvento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconLens.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Información complementaria del evento"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Edite la información complementaria del evento."></asp:Label></td>
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
				<td class="Etiqueta">Tipo de vestimenta</td>
				<td class="VinetaObligatorio">*</td>
				<td class="Campo"><asp:DropDownList ID="ddlTipoVestimenta" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
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
				<td class="Campo"><asp:TextBox ID="txtAccionRealizar" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Caracteristicas de invitados</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:TextBox ID="txtCaracteristicasInvitados" runat="server" CssClass="Textbox_General" MaxLength="200" Width="400px"></asp:TextBox></td>
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
				<td class="Etiqueta">Menú</td>
                <td class="Espacio"></td>
				<td class="Campo"></td>
				<td></td>
			</tr>
			<tr>
				<td colspan="4" style="text-align:left; vertical-align:bottom;">
					<CKEditor:CKEditorControl ID="ckeMenu" runat="server" BasePath="~/Include/Components/CKEditor/Core/" Height="96px"></CKEditor:CKEditorControl>
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

