<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/NonMenuTemplate.Master" AutoEventWireup="true" CodeBehind="sappNotificacion.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.SysApp.sappNotificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntNonMenuTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntNonMenuTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconAdm.png" />
    </asp:Panel>

    <asp:Panel ID="pnlPageName" runat="server" CssClass="MasterPageName">
        <asp:Label ID="lblPageName" runat="server" CssClass="PageNameText" Text="Acceso Indebido"></asp:Label>
    </asp:Panel>

    <div style="clear: both;height:50px; margin:50px auto; text-align:center;">
		<table border="0px" cellpadding="0" cellspacing ="0" style="height:100%; width:100%";>
			<tr style="font-size:12px; text-align:left;">
            <td style="width:20px;"></td>
            <td>
               <asp:Literal ID="litEncabezado" runat="server"></asp:Literal>
            </td>
         </tr>
			<tr style="height:20px;"><td colspan="2"></td></tr>
			<tr>
            <td colspan="2">
               <asp:Label ID="lblNotificacion" runat="server" CssClass="Notification" Font-Size="18px" Font-Bold="True" ForeColor="#65B449"></asp:Label>
            </td>
         </tr>
			<tr style="height:20px;"><td colspan="2"></td></tr>
			<tr>
				<td colspan="2" class="Notification" style="font-size:10px;">
					Para regresar al menú principal de la aplicación haga click
					<asp:LinkButton id="lnkRegresar" runat="server" CssClass="Notification" Font-Size="10px" OnClick="lnkRegresar_Click">aquí</asp:LinkButton>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
