<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucMenu.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.wucMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<table style="width:260px">
	<tr>
		<td>
			<asp:Accordion ID="acrdMenu" runat="server"
                AutoSize="None"
                ContentCssClass="AccordionContent"
                EnableViewState="true"
                FadeTransitions="false"
                FramesPerSecond="40"
                HeaderCssClass="AccordionHeader"
                HeaderSelectedCssClass="AccordionHeaderSelected"
                RequireOpenedPane="false"
                SuppressHeaderPostbacks="true"
                TransitionDuration="250">
			</asp:Accordion>					         
		</td>
	</tr>
</table>