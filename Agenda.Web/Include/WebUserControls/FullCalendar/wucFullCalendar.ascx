<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFullCalendar.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.FullCalendar.wucFullCalendar" %>

<script src="../../../../Include/WebUserControls/FullCalendar/Script/moment.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/jquery.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/fullcalendar.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/lang-all.js" type="text/javascript"></script>
<script type="text/javascript">
    
    function CreateLineBreaks(LineToCheck) {
        
        var Response = LineToCheck;

        while ( Response.indexOf("<br />") > -1 ) {

            Response = Response.replace("<br />", "\n");
        }

        return Response;
    }

</script>

<asp:Literal ID="litCalendar" runat="server"></asp:Literal>
<div id="calendar" style="margin:0 auto; max-width:100%;"></div>

<asp:HiddenField ID="hddCurrentMonth" runat="server" Value = "" />
<asp:HiddenField ID="hddCurrentYear" runat="server" Value = "" />

<asp:HiddenField ID="hddEventoNuevos" runat="server" Value = "1" />
<asp:HiddenField ID="hddEventoProceso" runat="server" Value = "1" />
<asp:HiddenField ID="hddEventoExpirado" runat="server" Value = "1" />
<asp:HiddenField ID="hddEventoCancelado" runat="server" Value = "1" />
<asp:HiddenField ID="hddEventoRepresentado" runat="server" Value = "1" />

<asp:HiddenField ID="hddPrioridadId" runat="server" Value = "0" />
<asp:HiddenField ID="hddDependencia" runat="server" Value = "0" />

<asp:HiddenField ID="MonthChangeEvent" runat="server" Value = "0" OnValueChanged="MonthChangeEvent_ValueChanged" />
