$(document).ready(function () {

    configurarTabela('gridEmpresa', 50, false, false, true);

});


editarClick = (function (id) {
    window.location = window.location.origin + '/empresa/edit/' + id;
});


excluirClick = (function (id) {

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
            excluir(id);
        }
    });

});


excluir = (function (id) {

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
                        $("#rowId_" + id).remove();
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

