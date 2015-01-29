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
    <script type="text/javascript">

        $(document).ready(function () {

            $('#calendar').fullCalendar({

                defaultDate: '2014-12-05',
                displayEventEnd: true,
                
                editable: false,
                eventLimit: false,
                eventBackgroundColor: '#65B449',
                eventBorderColor: "#675C9D",
                eventTextColor: '#FFFFFF',
                
                header: { left: '', center: 'title', right: '' },
                height: 999999999,
                lang: 'es',
                
                events: [
                    {
                        title: '',
                        start: '2014-12-10T10:30:00',
                        end: '2014-12-10T12:30:00',
                        color: '#675C9D',
                        eventBorderColor: "#C1C1C1",
                        eventTextColor: '#C1C1C1',
                        description: 'Evento especial con una descripción muy grande y extensa el objetivo sería ver como se despliega dentro del calendario para ver si se puede mandar imprimir'
                    },
                    {
                        title: '',
                        start: '2014-12-11T10:30:00',
                        end: '2014-12-11T12:30:00',
                        description: 'Comida con una descripción muy grande y extensa el objetivo sería ver como se despliega dentro del calendario para ver si se puede mandar imprimir'
                    }
                ],

                eventRender: function (event, element) {
                    
                    element.find('.fc-content').after("<div style='font-family:Arial; font-size:11px; text-align:justify;'>" + event.description + "</div>");
                }

            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="calendar" style="height:100%; margin:0 auto; max-width:1024px;"></div>

    </form>
</body>
<script type="text/javascript"> window.onload = function () { window.print(); } </script>
</html>
