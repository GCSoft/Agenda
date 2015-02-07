<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucTimer.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.wucTimer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:TextBox runat="server" ID="txtCanvas" CssClass="Timer_Canvas" ValidationGroup="MKE"></asp:TextBox>
<ajaxToolkit:MaskedEditExtender ID="mskeeManager" runat="server"
    AutoComplete="true"
	TargetControlID="txtCanvas" 
	Mask="99:99"
	MessageValidatorTip="true"
	MaskType="Time"
	AcceptAMPM="True"
	ErrorTooltipEnabled="True"
	OnBlurCssNegative="Timer_Error_Canvas"
	OnFocusCssClass="Timer_Focus_Canvas"
	OnFocusCssNegative="Timer_Error_Canvas"
	OnInvalidCssClass ="Timer_Error_Canvas"
    CultureName="en-US"
/>
<ajaxToolkit:MaskedEditValidator ID="mskevManager" runat="server" CssClass="Timer_Error_Messages"
	ControlExtender="mskeeManager"
	ControlToValidate="txtCanvas"
	IsValidEmpty="False"
	Display="Dynamic"
	EmptyValueBlurredText="* Hora requerida"
	InvalidValueBlurredMessage="Hora inválida"
	ValidationGroup="MKE"
/>