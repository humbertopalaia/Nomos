$(document).ready(function () {

    //formatarCampoData('dataPublicacao');

});


filtrarClick = (function () {

    var cnpj = $("#cnpj").val();
    var nomeFantasia = $("#nome-fantasia").val();
    var razaoSocial = $("#razao-social").val();


    var data = {
        'cnpj': cnpj,
        'nomeFantasia': nomeFantasia,
        'razaoSocial': razaoSocial
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