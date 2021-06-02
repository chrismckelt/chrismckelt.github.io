$.extend( true, $.fn.dataTable.defaults, {
    "paging":   false,
    "ordering": false,
    "info":     false,
    "searching": false
} );

$(document).ready(function() {
    $( "table" ).addClass( "stripe compact" );

    $('table').DataTable();
} );