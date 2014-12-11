<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="eveCalendario.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveCalendario" %>
<%@ Register src="../../../../Include/WebUserControls/FullCalendar/wucFullCalendar.ascx" tagname="wucFullCalendar" tagprefix="wuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconGeneral.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Eventos - Calendario"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="FormPanel">
        <table class="FormTable">
            <tr>
				<td class="Etiqueta"><asp:DropDownList ID="ddlEstatusInvitacion" runat="server" CssClass="DropDownList_General" Width="216px"></asp:DropDownList></td>
                <td>
                     <table style="width:100%">
                        <tr>
                            <td style="text-align:center; width:20%"><asp:Label ID="lblRegistrada" runat="server" CssClass="Label_General_Center" Text="Registrada" BackColor="#65B449" ForeColor="White" Height="20px" Width="100px"></asp:Label></td>
                            <td style="text-align:center; width:20%"><asp:Label ID="lblComentada" runat="server" CssClass="Label_General_Center" Text="Comentada" BackColor="#ff9933" ForeColor="White" Height="20px" Width="100px"></asp:Label></td>
                            <td style="text-align:center; width:20%"><asp:Label ID="lblDeclinada" runat="server" CssClass="Label_General_Center" Text="Declinada" BackColor="#348dff" ForeColor="White" Height="20px" Width="100px"></asp:Label></td>
                            <td style="text-align:center; width:20%"><asp:Label ID="lblAprobada" runat="server" CssClass="Label_General_Center" Text="Aprobada" BackColor="#675C9D" ForeColor="White" Height="20px" Width="100px"></asp:Label></td>
                            <td style="text-align:center; width:20%"><asp:Label ID="lblCancelada" runat="server" CssClass="Label_General_Center" Text="Cancelada" BackColor="red" ForeColor="White" Height="20px" Width="100px"></asp:Label></td>
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
				<td colspan="4" style="height:50px;"></td>
			</tr>
        </table>
    </asp:Panel>

        
   
    <wuc:wucFullCalendar ID="wucFullCalendar" runat="server" />

    <br /><br />
    
</asp:Content>
