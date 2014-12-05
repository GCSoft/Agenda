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
   
    <wuc:wucFullCalendar ID="wucFullCalendar" runat="server" />

    <br /><br />
    
</asp:Content>
