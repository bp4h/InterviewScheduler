window.initCalendar = async (dotnetHelper, interviews) => {
    console.log(interviews);
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        dateClick: function (info) {
            dotnetHelper.invokeMethodAsync('HandleDateClick', info.dateStr);
        },
        displayEventTime: true,
        displayEventEnd: true,
        eventTimeFormat: {
            hour: '2-digit',
            minute: '2-digit',
            meridiem: false
        },
        events: interviews.map(interview => ({
            title: interview.title,
            start: interview.start,
            end: interview.end
        }))
    });
    calendar.render();

    await new Promise(resolve => setTimeout(resolve, 100));
    window.openEventModal = () => {
        var modal = $('#eventModal');
        if (modal) {
            modal.addClass('show');
            modal.modal('show');
        }
    };
    window.openGeneratedLinkModal = () => {
        var modal = $('#generatedLinkModal');
        if (modal) {
            modal.addClass('show');
            modal.modal('show');
        }
    };
    window.openBookEventErrorModal = () => {
        var modal = $('#bookEventErrorModal');
        if (modal) {
            modal.addClass('show');
            modal.modal('show');
        }
    };
    window.closeBookEventErrorModal = () => {
        var modal = $('#bookEventErrorModal');
        if (modal) {
            modal.removeClass('show');
            modal.modal('hide');
        }
    };
    /*var modalElement = $('#modal');
    modalElement.removeClass('show');
    modalElement.modal('hide');*/
};
