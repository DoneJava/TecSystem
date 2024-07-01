$(document).ready(function () {
    let listaSelecionada = '';

    $("#addListaForm").submit(function (event) {
        event.preventDefault();
        $("#submitListaButton").prop("disabled", true);
        $.post("/Home/AdicionarLista", $(this).serialize())
            .done(function (data) {
                $("#listaContainer").html(data);
                $("#submitListaButton").prop("disabled", false);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao adicionar lista: " + xhr.responseText);
                $("#submitListaButton").prop("disabled", false);
            });
    });

    $(document).on('click', '.nav-link', function () {
        $('.nav-link').removeClass('selected-list');
        $(this).addClass('selected-list');
        listaSelecionada = $(this).text();
        $('#listaNomeSelecionada').val(listaSelecionada);
        $('#botaoAdicionarTarefa').prop('disabled', false); 
        carregarTarefas(listaSelecionada);
    });

    function carregarTarefas(listaNome) {
        $.get("/Home/ObterTarefas", { listaNome: listaNome })
            .done(function (data) {
                $("#tarefasContainer").html(data);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao carregar tarefas: " + xhr.responseText);
            });
    }

    $(document).on('submit', '.excluirListaForm', function (event) {
        event.preventDefault();
        var form = $(this);
        var button = form.find('button');

        if (button.prop('disabled')) {
            return; // Prevent multiple submissions
        }
        button.prop('disabled', true); // Disable button during request

        $.post("/Home/ExcluirLista", form.serialize())
            .done(function (data) {
                $("#listaContainer").html(data);
                $('#botaoAdicionarTarefa').prop('disabled', true); // Desabilitar botão adicionar tarefa na index
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao excluir lista: " + xhr.responseText);
            })
            .always(function () {
                button.prop('disabled', false); // Re-enable button after request
            });
    });
});
