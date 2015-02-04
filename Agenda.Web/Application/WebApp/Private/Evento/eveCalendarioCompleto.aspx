<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eveCalendarioCompleto.aspx.cs" Inherits="Agenda.Web.Application.WebApp.Private.Evento.eveCalendarioCompleto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agenda</title>
    <meta content="GCSoft S.A. de C.V." name="autor" />
    <link href="~/Include/WebUserControls/FullCalendar/Style/fullcalendar.css" rel="Stylesheet" />
    <link href="~/Include/WebUserControls/FullCalendar/Style/fullcalendar.print.css" rel="Stylesheet" media="print" />
    <script src="../../../../Include/WebUserControls/FullCalendar/Script/moment.min.js" type="text/javascript"></script>
    <script src="../../../../Include/WebUserControls/FullCalendar/Script/jquery.min.js" type="text/javascript"></script>
    <script src="../../../../Include/WebUserControls/FullCalendar/Script/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../../../../Include/WebUserControls/FullCalendar/Script/lang-all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Literal ID="litCalendar" runat="server"></asp:Literal>
        <div id="calendar" style="margin:0 auto; max-width:1024px;"></div>

    </form>
</body>
<script type="text/javascript"> window.onload = function () { window.print(); } </script>
</html>
