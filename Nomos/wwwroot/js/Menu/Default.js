$(document).ready(function () {


    $("#empresa").data('selecionado', $("#empresa").val());

    $("#empresa").change(function () {
        Swal.fire({
            title: 'Pergunta',
            text: "Deseja alterar a empresa atual?",
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não'
        }).then((result) => {
            if (result.value) {
                var id = $("#empresa").val();
                alterarEmpresa(id);
                $("#empresa").data('selecionado', id);                
            }
            else {
                var currentId = $("#empresa").data("selecionado");
                $("#empresa").val(currentId);
                return false;
            }
        });
    });
});


logoffClick = (function () {

    Swal.fire({
        title: 'Pergunta',
        text: "Deseja efetuar logoff do sistema?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            efetuarLogoff();
        }
        });
});



alterarEmpresa = (function (id) {


    var data = { 'empresaId': id };

    $.ajax({
        type: "POST",
        url: urlAlterarEmpresa,
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

                window.location.reload(false); 
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

efetuarLogoff = (function () {

    $.ajax({
        type: "POST",
        url: urlLogoff,        
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

                window.location = window.location.origin + '/login';
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
