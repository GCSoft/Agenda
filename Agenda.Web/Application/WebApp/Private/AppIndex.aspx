<%@ Page Title="" Language="C#" MasterPageFile="~/Include/MasterPage/PrivateTemplate.Master" AutoEventWireup="true" CodeBehind="AppIndex.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.AppIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntPrivateTemplateHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntPrivateTemplateBody" runat="server">

    <asp:Panel ID="pnlIconPage" runat="server" CssClass="MasterIconPage">
        <img id="imgIconPage" alt="Nuevo León" runat="server" src="~/Include/Image/Icon/IconUser.png" />
    </asp:Panel>

</asp:Content>
