<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/LoginTemplate.Master" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Public.frmLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntLoginHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntLoginBody" runat="server">

    <asp:Panel ID="pnlLogin" runat="server" Style="width:500px;">
        <table border="0" style="border-collapse: collapse; border-spacing: 0; padding: 0px; height:100%; width:100%">
            <tr><td colspan="3" style="height: 10px;"></td></tr>
            <tr style="height: 25px; vertical-align: bottom;">
                <td style="width: 10px;"></td>
                <td style="width: 80px">Correo</td>
                <td><asp:TextBox ID="txtEmail" runat="server" Width="393px"></asp:TextBox></td>
            </tr>
            <tr style="height: 25px; vertical-align: bottom;">
                <td></td>
                <td>Password</td>
                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="393px"></asp:TextBox></td>
            </tr>
            <tr><td colspan="3" style="height:10px;"></td></tr>
            <tr style="height:25px; vertical-align: bottom;">
                <td></td>
                <td colspan="2" style="text-align:right; vertical-align: middle;">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="Button_General" OnClick="btnLogin_Click" Width="100px"></asp:Button>&nbsp;&nbsp;
                </td>
            </tr>
            <tr style="height:15px; vertical-align: bottom;">
                <td></td>
                <td colspan="2" style="text-align:left; vertical-align: middle;"><span id="spanMessage" style="color: Red; font-family: Trebuchet MS, arial, verdana, calibri; font-size: 11px; display: none;"></span></td>
            </tr>
            <tr>
                <td colspan="3" style="height:1px; text-align:center;">
                    <div style="border-bottom: 1px solid #675C9D; height:1px; left:10px; position: relative; width:480px;"></div>
                </td>
            </tr>
            <tr><td colspan="3" style="height:3px;"></td></tr>
            <tr>
                <td colspan="3">
                    <table border="0" style="border-collapse: collapse; border-spacing: 0; padding: 0px; height:100%; width:100%">
                        <tr>
                            <td style="text-align:left; width:50%">&nbsp;&nbsp;<asp:Button ID="btnRecoveryPassword" runat="server" Text="Recuperar Contraseña" CssClass="Button_General" OnClick="btnRecoveryPassword_Click" Width="150px"></asp:Button></td>
                            <td style="text-align:right; width:50%"><asp:CheckBox ID="chkRememberPassword" runat="server" CssClass="CheckBox_Regular" Text="Recordar mis datos"></asp:CheckBox>&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </asp:Panel>

    <asp:HiddenField ID="hddEncryption" runat="server" Value="1"></asp:HiddenField>

</asp:Content>
