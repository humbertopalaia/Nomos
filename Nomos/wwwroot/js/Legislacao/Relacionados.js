$(document).ready(function () {

    var tableDisponiveis = configurarTabela('table-disponiveis');
    var tableRelacionados = configurarTabela('table-relacionados');

    tableDisponiveis.on('click', 'tbody tr', function () {
        var $row = $(this);
        var addRow = tableDisponiveis.row($row);
        tableRelacionados.row.add(addRow.data()).draw(false);
        addRow.remove().draw(false);

        configurarExibicaoTabelas();
    });


    tableRelacionados.on('click', 'tbody tr', function () {
        var $row = $(this);
        var addRow = tableRelacionados.row($row);
        tableDisponiveis.row.add(addRow.data()).draw(false);
        addRow.remove().draw(false);

        configurarExibicaoTabelas();
    });


    configurarExibicaoTabelas();

});


configurarTabela= function(tabelaId)
{
    return $('#' + tabelaId).DataTable({
        "order": [[0, 'asc']],
        "rowGroup": {
            dataSrc: 0
        },
        "bPaginate": true,
        "bInfo": false,
        "pagingType": "full_numbers",
        "lengthMenu": [[10], [10]],
        "searching": true,
        "bLengthChange": false, //thought this line could hide the LengthMenu
        "bSort": false,
        "bJQueryUI": true,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "",
            "sEmptyTable": "",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando de 0 até 0 de 0 registros",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sPrevious": "Anterior",
                "sNext": "Seguinte",
                "sLast": "Último"
            }
        },
        'columnDefs': [
            {
                "targets": [0],
                "visible": false,
                "className": "id-legislacao"
            },
            {
                "targets": [1],
                "className": 'text-info'
            }]
    });
};


configurarExibicaoTabelas = function () {

    var totalDisponiveis = $('#table-disponiveis').DataTable().rows().data().length;
    var totalRelacionados = $('#table-relacionados').DataTable().rows().data().length;

       
    if (totalDisponiveis <= 0) {
        $('#table-disponiveis_wrapper').hide();
    }
    else {
        $('#table-disponiveis_wrapper').show();

    }


    if (totalRelacionados <= 0) {
        $('#table-relacionados_wrapper').hide();
    }
    else {
        $('#table-relacionados_wrapper').show();

    }

};
