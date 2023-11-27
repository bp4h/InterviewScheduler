window.initCalendar = async (dotnetHelper, interviews) => {
    console.log(interviews);
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        dateClick: function (info) {
            dotnetHelper.invokeMethodAsync('HandleDateClick', info.dateStr);
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
    var modalElement = $('#modal');
    modalElement.removeClass('show');
    modalElement.modal('hide');
};
