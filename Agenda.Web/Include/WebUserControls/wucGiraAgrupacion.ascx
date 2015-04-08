<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucGiraAgrupacion.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.wucGiraAgrupacion" %>

<table style="width:405px">
	<tr>
		<td colspan="2" style="text-align:left;">
            <asp:DropDownList ID="ddlAgrupacion" runat="server" CssClass="DropDownList_General" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupacion_SelectedIndexChanged"></asp:DropDownList>
		</td>
    </tr>
    <tr>
        <td style="text-align:left;">
            <asp:TextBox ID="txtOtraAgrupacion" runat="server" CssClass="Textbox_Disabled" Enabled="false" MaxLength="1000" Width="360px"></asp:TextBox>
		</td>
        <td style="text-align:right;">
            <asp:Button ID="btnNuevaAgrupacion" runat="server" Text="+" CssClass="Button_Special_Gray" Enabled="false" ToolTip="Nueva Agrupacion" Width="25px" OnClick="btnNuevaAgrupacion_Click" />
		</td>
	</tr>
</table>

<asp:HiddenField ID="hddAgrupacion" runat="server" Value ="" />
