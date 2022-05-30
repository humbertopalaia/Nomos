formatarCampoData = (function (campoId, formatoData, placeHolder, mascara) {

    formatoData = typeof formatoData !== 'undefined' ? formatoData : 'dd/mm/yy';
    placeHolder = typeof placeHolder !== 'undefined' ? placeHolder : 'dd/mm/aaaa';
    mascara = typeof mascara !== 'undefined' ? mascara : '00/00/0000';

    $('#' + campoId).datepicker(
        {
            dateFormat: formatoData,
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
        }
    );

    $('#' + campoId).mask(mascara);

});


configurarTabela = (function (tabelaId, tamanhoPagina, semBusca, semPaginacao, semOrdenacao, semMsgZeroRows) {

    tamanhoPagina = typeof tamanhoPagina !== 'undefined' ? tamanhoPagina : 50;

    return $('#' + tabelaId).DataTable({
        "order": [[0, 'asc']],
        "rowGroup": {
            dataSrc: 0
        },
        "bPaginate": !semPaginacao,
        "bInfo": false,
        "pagingType": "full_numbers",
        //"lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],        
        "lengthMenu": [[tamanhoPagina], [tamanhoPagina]],
        "searching": !semBusca,
        "bLengthChange": false, //thought this line could hide the LengthMenu
        "bSort": !semOrdenacao,
        "bJQueryUI": true,
        "oLanguage": {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": (semMsgZeroRows ? "" : "Não foram encontrados resultados"),
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
        }
    });

    //if (semBusca)
    //    $('#' + tabelaId).dataTable({ searching: false });
});


toLower = (function (strInput) {
    strInput.value = strInput.value.toLowerCase();
});

toUpper = (function (strInput) {
    strInput.value = strInput.value.toUpperCase();
});


isNumber = (function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode > 31 && charCode < 48) || charCode > 57) {
        return false;
    }
    return true;
});


validarCnpj = (function (cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

});