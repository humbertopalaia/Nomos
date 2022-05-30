$(document).ready(function () {

    formatarCampoData('dataPublicacao');

});


filtrarClick = (function () {

    var codigo = $("#codigo").val();
    var titulo = $("#titulo").val();
    var categoriaId = $("#categoria").val();
    var dataPublicacao = $("#dataPublicacao").val();
    var orgaoId = $("#orgao").val();
    var situacaoId = $("#situacao").val();


    
    var data = {
        'codigo': codigo,
        'titulo': titulo,
        'categoriaId': categoriaId,
        'dataPublicacao': dataPublicacao,
        'situacaoId': situacaoId,
        'orgaoId': orgaoId
    };

    $.ajax({
        type: "POST",
        url: urlFiltrar,
        data: JSON.stringify(data),
        cache: false,
        dataType: "json",
        contentType: "application/json",
        error: function (error) {
            $("#resultado").html('');

            $("#loader").hide();

            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Ocorreu um erro inesperado. Tente novamente - ' + error.mensagem
            });
        },
        success: function (data) {
            $("#resultado").html(data);
            $("#loader").hide();
        },
        beforeSend: function () {
            $("#loader").show();
        }
    });


    

});