$(document).ready(function () {

    inicializarCampos();

    $("input").change(function (e) {
        $(e.target).removeClass('has-error');
    });
});

cancelarClick = (function () {

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
            window.location = window.location.origin + '/empresa';
        }
    });

});

salvarClick = (function () {
    if (validarCampos()) {
        alterar();
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

excluirClick = (function () {

    Swal.fire({
        title: 'Pergunta',
        text: "Deseja excluir o registro?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            excluir();
        }
    });

});


excluir = (function () {

    var id = $("#id").val();

    var data = {
        'id': id
    };



    $.ajax({
        type: "POST",
        url: urlExcluir,
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
                    text: 'Registro excluído com sucesso!'
                }).then((result) => {
                    if (result.value) {
                        window.location = window.location.origin + '/empresa';
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

alterar = (function () {
    var id = $("#id").val();
    var cnpj = getCnpj();
    var razaoSocial = $("#razao-social").val();
    var nomeFantasia = $("#nome-fantasia").val();
    var emailResponsavel = $("#email-responsavel").val();
    var cliente = $("#cliente").val();
    var ativo = $("#ativo").val();

    var data = {
        'id': id,
        'cnpj': cnpj,
        'razaoSocial': razaoSocial,
        'nomeFantasia': nomeFantasia,
        'emailResponsavel': emailResponsavel,
        'cliente': cliente,
        'ativo': ativo
    };

    $.ajax({
        type: "POST",
        url: urlAtualizar,
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

    var cnpj = getCnpj();
    var razaoSocial = $("#razao-social").val();
    var nomeFantasia = $("#nome-fantasia").val();
    var emailResponsavel = $("#email-responsavel").val();

    var cnpjValido = validarCnpj(cnpj);

    if (cnpj === null || cnpj.length === 0 || !cnpjValido) {
        $("#cnpj").addClass('has-error');
        valido = false;
    }

    if (razaoSocial === null || razaoSocial.length === 0) {
        $("#razao-social").addClass('has-error');
        valido = false;
    }

    if (nomeFantasia === null || nomeFantasia.length === 0) {
        $("#nome-fantasia").addClass('has-error');
        valido = false;
    }

    if (emailResponsavel === null || emailResponsavel.length === 0) {
        $("#email-responsavel").addClass('has-error');
        valido = false;
    }

    return valido;
});

inicializarCampos = (function () {
    $("#cnpj").mask("99.999.999/9999-99");
});


abrirUrl = (function () {
    var url = $("#link").val();

    if (url.indexOf('http://') === -1) {
        url = 'http://' + url;
    }

    window.open(url, '_blank');
});


getCnpj = (function () {

    var cnpj = $("#cnpj").unmask().val();
    $("#cnpj").mask("99.999.999/9999-99");

    return cnpj;
});