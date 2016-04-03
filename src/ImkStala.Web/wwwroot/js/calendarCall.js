var picker = new Pikaday({
    field: document.getElementById('datepicker'),
    bound: false,
    format: 'YYYY-MM-DD',
    container: document.getElementById('container')
});

var $input = $('#timepicker').pickatime({
})
