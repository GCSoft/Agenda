<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCalendar.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.wucCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:TextBox runat="server" ID="txtCanvas" CssClass="Calendar_Canvas"></asp:TextBox>
<asp:MaskedEditExtender ID="maskCalendar" runat="server"
    CultureName="es-MX"
    TargetControlID="txtCanvas"
    MaskType="Date"
    Mask="99/99/9999"
    MessageValidatorTip="true"
    UserDateFormat="DayMonthYear"
/>
<asp:MaskedEditValidator ID="validatorMask" runat="server" CssClass="Calendar_Error_Messages"
    ControlExtender="maskCalendar"
    ControlToValidate="txtCanvas"
    IsValidEmpty="false"
    EmptyValueBlurredText="* Fecha requerida"
    InvalidValueMessage="Formato de fecha inválido" 
/>
<asp:CalendarExtender ID="ceManager" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCanvas" CssClass="Calendar_General" />