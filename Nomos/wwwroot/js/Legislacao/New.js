$(document).ready(function () {

    inicializarCampos();

    $("input").change(function (e)
    {        
        $(e.target).removeClass('has-error');
    });


    $("select").change(function (e) {
        $(e.target).removeClass('has-error');
    });

    

});

cancelarclick = (function () {

    Swal.fire({
        title: 'Pergunta',
        text: "Deseja cancelar a operação?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            window.location = window.location.origin + '/legislacao';
        }
    });

});

salvarclick = (function () {
    if (validarCampos()) {
        incluir();
    }
    else {
        Swal.fire({
            title: 'Atenção',
            text: 'Verifique os campos destacados.',
            type: 'warning',
            confirmButtonText: 'Ok'
        });
    }
});

incluir = (function () {
    var codigo = $("#codigo").val();
    var titulo = $("#titulo").val();
    var descricao = $("#descricao").val();
    var categoriaId = parseInt($("#categoria").val());
    var tipoId = parseInt($("#tipo").val());
    var dataPublicacao = $("#dataPublicacao").val();
    var dataInicioVigencia = $("#dataInicioVigencia").val();
    var situacaoId = parseInt($("#situacao").val());
    var orgaoId = parseInt($("#orgao").val());
    var observacao = $("#observacao").val();
    var link = $("#link").val();

    var data = {
        'codigo': codigo,
        'titulo': titulo,
        'descricao': descricao,
        'categoriaId': categoriaId,
        'tipoId': tipoId,
        'dataPublicacao': dataPublicacao,
        'dataInicioVigencia': dataInicioVigencia,
        'situacaoId': situacaoId,
        'orgaoId': orgaoId,
        'observacao': observacao,
        'link': link
    };

    $.ajax({
        type: "POST",
        url: urlIncluir,
        data: JSON.stringify(data),
        cache: false,
        dataType: "json",
        contentType: "application/json",
        error: function (error) {
            $("#loader").hide();

            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Ocorreu um erro inesperado. Tente novamente - ' + error.mensagem
            });
        },
        success: function (data) {

            if (data.sucesso) {
                
                Swal.fire({
                    type: 'info',
                    title: 'Informação',
                    text: 'Registro salvo com sucesso!'
                }).then((result) => {
                    if (result.value) {
                        window.location = window.location.origin + '/legislacao/edit/' + data.id;
                    }
                });
            }
            else {
                Swal.fire({
                    type: 'warning',
                    title: 'Atenção',
                    text: data.mensagem
                });
            }

            $("#loader").hide();

        },
        beforeSend: function () {
            $("#loader").show();
        }
    });


});


limparValidacao = (function () {
    $("input").removeClass('has-error');
    $("select").removeClass('has-error');
});

validarCampos = (function () {

    var valido = true;
    this.limparValidacao();

    var codigo = $("#codigo").val();
    var titulo = $("#titulo").val();
    var descricao = $("#descricao").val();
    var categoriaId = parseInt($("#categoria").val());
    var tipoId = parseInt($("#tipo").val());
    var dataPublicacao = $("#dataPublicacao").val();
    var orgaoId = parseInt($("#orgao").val());
    var situacaoId = parseInt($("#situacao").val());
    var link = $("#link").val();


    if (codigo === null || codigo.length === 0) {
        $("#codigo").addClass('has-error');
        valido = false;
    }

    if (tipoId === null || tipoId === 0) {
        $("#tipo").addClass('has-error');
        valido = false;
    }


    if (titulo === null || titulo.length === 0) {
        $("#titulo").addClass('has-error');
        valido = false;
    }

    if (descricao === null || descricao.length === 0) {
        $("#descricao").addClass('has-error');
        valido = false;
    }

    if (categoriaId === null || categoriaId === 0) {
        $("#categoria").addClass('has-error');
        valido = false;
    }

    if (dataPublicacao === null || dataPublicacao.length === 0) {
        $("#dataPublicacao").addClass('has-error');
        valido = false;
    }

    if (orgaoId === null || orgaoId === 0) {
        $("#orgao").addClass('has-error');
        valido = false;
    }

    if (situacaoId === null || situacaoId === 0) {
        $("#situacao").addClass('has-error');
        valido = false;
    }

    if (link === null || link.length === 0) {
        $("#link").addClass('has-error');
        valido = false;
    }

    return valido;

});

inicializarCampos = (function () {
    formatarCampoData('dataPublicacao');
    formatarCampoData('dataInicioVigencia');
});