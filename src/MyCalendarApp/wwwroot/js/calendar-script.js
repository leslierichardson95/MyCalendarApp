$(document).ready(function () {
    var selectedEvent = null;
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        firstDay: 1, //The day that each week begins (Monday=1)
        plugins: ['bootstrap'],
        themeSystem: 'bootstrap',
        slotMinutes: 60,
        eventLimit: true,
        eventClick:
            function (calEvent, jsEvent, view) {
                selectedEvent = calEvent;
                $('#eventInfoModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("MMM-DD-YYYY HH:mm a")));
                if (calEvent.end !== null) {
                    $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("MMM-DD-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
                $('#eventInfoModal #pDetails').empty().html($description);

                $('#eventInfoModal').modal();
            },
        selectable: true,
        select: function (start, end) {
            selectedEvent = {
                eventID: 0,
                title: '',
                description: '',
                start: start,
                end: end,
                allDay: false,
                color: ''
            };
            openAddEditForm();
            $('#calendar').fullCalendar('unselect');
        },
        editable: true,
        eventDrop: function (event) {
            var data = {
                EventID: event.eventID,
                Subject: event.title,
                Start: event.start.format('MM/DD/YYYY HH:mm A'),
                End: event.end !== null ? event.end.format('MM/DD/YYYY HH:mm A') : null,
                Description: event.description,
                ThemeColor: event.color,
                IsFullDay: event.allDay
            };
            //SaveEvent(data);
        }
    });

    // TODO: Figure out why clock doesn't display
    $('#txtStart,#dtp2').datetimepicker({
        format: 'MM/DD/YYYY HH:mm A'
    });

    $('#chkIsFullDay').change(function () {
        if ($(this).is(':checked')) {
            $('#divEndDate').hide();
        }
        else {
            $('#divEndDate').show();
        }
    });

    function openAddEditForm() {
        if (selectedEvent !== null) {
            $('#hdEventID').val(selectedEvent.eventID);
            $('#txtSubject').val(selectedEvent.title);

            //TODO: Format to display in date input box
            $('#txtStart').val(selectedEvent.start.format('MM/DD/YYYY HH:mm A'));

            $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
            $('#chkIsFullDay').change();
            //$('#txtEnd').val(selectedEvent.end !== null ? selectedEvent.end.format('MM/DD/YYYY HH:mm A') : '');
            $('#txtEnd').val(selectedEvent.start.format('MM/DD/YYYY HH:mm A'));
            $('#txtDescription').val(selectedEvent.description);
            $('#ddThemeColor').val(selectedEvent.color);
        }
        $('#eventInfoModal').modal('hide');
        $('#addEditEventModal').modal();
    }
}); 