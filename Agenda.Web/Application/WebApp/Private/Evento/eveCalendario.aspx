<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveCalendario.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveCalendario" %>
<%@ Register src="../../../../Include/WebUserControls/FullCalendar/wucFullCalendar.ascx" tagname="wucFullCalendar" tagprefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconAdm.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Eventos - Calendario"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlTitulo" runat="server" CssClass="TitlePanel">
        <table class="HeaderTable">
            <tr>
                <td class="Titulo"><asp:Label ID="lblSubTitulo" runat="server" Text="Ajuste los filtros para poder visualizar el listado de eventos en los que esta usted vinculado."></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta">Prioridad</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="DropDownList_General" Width="216px" AutoPostBack="True" OnSelectedIndexChanged="ddlPrioridad_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
				<td class="Etiqueta">Dependencia</td>
				<td class="Espacio"></td>
				<td class="Campo"><asp:DropDownList ID="ddlDependencia" runat="server" CssClass="DropDownList_General" Width="216px" AutoPostBack="True" OnSelectedIndexChanged="ddlDependencia_SelectedIndexChanged"></asp:DropDownList></td>
                <td></td>
			</tr>
            <tr>
                <td colspan="4">
                     <table style="width:100%">
                        <tr>
                            <td style="text-align:center; width:17%">
                                <asp:Panel ID="NuevosEventosPanel" CssClass="FiltroPanel" runat="server" Visible="true">
                                    <div style='background-color:#348dff; border:1px solid #675C9D; float:left; height:16px; width:16px;'></div>
                                    <div style='float:left; '><asp:CheckBox ID="chkNuevo" runat="server" CssClass="CheckBox_Regular" Checked="true" Text="Nuevo" AutoPostBack="True" OnCheckedChanged="chkNuevo_CheckedChanged" /></div>
                                </asp:Panel>
                            </td>
                            <td style="text-align:center; width:17%">
                                <asp:Panel ID="EnProcesoPanel" CssClass="FiltroPanel" runat="server" Visible="true">
                                    <div style='background-color:#65B449; border:1px solid #675C9D; float:left; height:16px; width:16px;'></div>
                                    <div style='float:left; '><asp:CheckBox ID="chkProceso" runat="server" CssClass="CheckBox_Regular" Checked="true" Text="En Proceso" AutoPostBack="True" OnCheckedChanged="chkProceso_CheckedChanged" /></div>
                                </asp:Panel>
                            </td>
                            <td style="text-align:center; width:17%">
                                <asp:Panel ID="ExpiradoPanel" CssClass="FiltroPanel" runat="server" Visible="true">
                                    <div style='background-color:#ff9933; border:1px solid #675C9D; float:left; height:16px; width:16px;'></div>
                                    <div style='float:left; '><asp:CheckBox ID="chkExpirado" runat="server" CssClass="CheckBox_Regular" Checked="true" Text="Expirado" AutoPostBack="True" OnCheckedChanged="chkExpirado_CheckedChanged" /></div>
                                </asp:Panel>
                            </td>
                            <td style="text-align:center; width:17%">
                                <asp:Panel ID="CanceladoPanel" CssClass="FiltroPanel" runat="server" Visible="true">
                                    <div style='background-color:red; border:1px solid #675C9D; float:left; height:16px; width:16px;'></div>
                                    <div style='float:left; '><asp:CheckBox ID="chkCancelar" runat="server" CssClass="CheckBox_Regular" Checked="true" Text="Cancelado" AutoPostBack="True" OnCheckedChanged="chkCancelar_CheckedChanged" /></div>
                                </asp:Panel>
                            </td>
                            <td style="text-align:center; width:17%">
                                <asp:Panel ID="RepresentadoPanel" CssClass="FiltroPanel" runat="server" Visible="true">
                                    <div style='background-color:#666666; border:1px solid #675C9D; float:left; height:16px; width:16px;'></div>
                                    <div style='float:left;'><asp:CheckBox ID="chkRepresentado" runat="server" CssClass="CheckBox_Regular" Checked="true" Text="Representado" AutoPostBack="True" OnCheckedChanged="chkRepresentado_CheckedChanged" /></div>
                                </asp:Panel>
                            </td>
                            <td style="text-align:right; width:15%">
                                <asp:ImageButton ID="imgImprimir" runat="server" OnClientClick="window.open('eveCalendarioCompleto.aspx', 'FullCalendarWindow', 'menubar=1,resizable=1,width=1024,height=800'); return false;" ImageUrl="~/Include/Image/Icon/Print.png" />
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
            <tr>
				<td colspan="4">
                    <asp:Panel ID="pnlBreak" runat="server" CssClass="BreakPanel">
                        <%--Empty Content--%>
                    </asp:Panel>
				</td>
			</tr>
            <tr>
				<td colspan="4" style="height:30px;"></td>
			</tr>
        </table>
    </asp:Panel>
   
    <wuc:wucFullCalendar ID="wucFullCalendar" runat="server" OnChangeMonth ="wucFullCalendar_ChangeMonth" />

    <asp:Panel ID="pnlFooter" runat="server" CssClass="FooterPanel">
        <%--Empty Content--%>
    </asp:Panel>

    <asp:HiddenField ID="hddCurrentMonth" runat="server" Value = "" />
    <asp:HiddenField ID="hddCurrentYear" runat="server" Value = "" />
    
</asp:Content>
