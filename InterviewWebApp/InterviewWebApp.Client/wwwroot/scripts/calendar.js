window.initCalendar = async (dotnetHelper) => {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        dateClick: function (info) {
            dotnetHelper.invokeMethodAsync('HandleDateClick', info.dateStr);
        },
        events: [
            { title: 'Мероприятие', start: '2023-11-26' }
        ]
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
