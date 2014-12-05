<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFullCalendar.ascx.cs" Inherits="Agenda.Web.Include.WebUserControls.FullCalendar.wucFullCalendar" %>

<script src="../../../../Include/WebUserControls/FullCalendar/Script/moment.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/jquery.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/fullcalendar.min.js" type="text/javascript"></script>
<script src="../../../../Include/WebUserControls/FullCalendar/Script/lang-all.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {

        $('#calendar').fullCalendar({
            businessHours: {    // Sombra en gris que apoya a identificar el horario laboral, de L-V entre  6 am y 7 pm, el parámetro dow (Days Of Week) es un arreglo basado en cero en donde el domingo es el 0
                start: '06:00',
                end: '19:00',
                dow: [1, 2, 3, 4, 5]
            },
            defaultDate: '2014-12-05',
            editable: true,
            eventLimit: true,    // Permite la opción "más" cuando hay muchos elementos
            header: {
                left: 'month,agendaWeek,agendaDay',
                center: 'title'
            },
            lang: 'es',
            eventBackgroundColor: '#65B449',
            eventBorderColor: "#675C9D",
            eventTextColor: '#FFFFFF',

            events: [
				{
				    title: 'Evento de todo el día',
				    start: '2014-12-01'
				},
				{
				    title: 'Ocupado 3 días',
				    start: '2014-12-07',
				    end: '2014-12-10'
				},
				{
				    id: 999,
				    title: 'Evento repetido',
				    start: '2014-12-09T16:00:00'
				},
				{
				    id: 999,
				    title: 'Evento repetido',
				    start: '2014-12-16T16:00:00'
				},
				{
				    title: 'Conferencia',
				    start: '2014-12-11',
				    end: '2014-12-13'
				},
				{
				    title: 'Evento especial',
				    start: '2014-12-12T10:30:00',
				    end: '2014-12-12T12:30:00',
				    color: '#675C9D',
				    eventBorderColor: "#C1C1C1",
				    eventTextColor: '#C1C1C1'
				},
				{
				    title: 'Comida',
				    start: '2014-12-12T12:00:00'
				},
				{
				    title: 'Reunión',
				    start: '2014-12-12T14:30:00'
				},
				{
				    title: 'Hora feliz',
				    start: '2014-12-12T17:30:00'
				},
				{
				    title: 'Cena',
				    start: '2014-12-12T20:00:00'
				},
				{
				    title: 'Fiesta de cumpleaños',
				    start: '2014-12-13T07:00:00'
				},
				{
				    title: 'Click para ir al Sitio de Nuevo León',
				    url: 'http://www.nl.gob.mx/',
				    start: '2014-12-28'
				}
            ]
        });

    });

</script>



<div id="calendar" style="margin: 0 auto; max-width:100%;"></div>