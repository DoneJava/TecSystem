$(document).ready(function () {
    let listaSelecionada = '';

    $("#addTarefaForm").on('submit', function (event) {
        event.preventDefault();
        const botaoAdicionar = $('#adicionarTarefaBotao');
        if (botaoAdicionar.prop('disabled')) {
            return; 
        }
        botaoAdicionar.prop('disabled', true); 
        const formData = $(this).serialize() + '&listaNome=' + encodeURIComponent(listaSelecionada);

        $.post("/Home/AdicionarTarefa", formData)
            .done(function (data) {
                $('#adicionarTarefaModal').modal('hide');
                $("#addTarefaForm")[0].reset();
                carregarTarefas(listaSelecionada);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao adicionar tarefa: " + xhr.responseText);
            })
            .always(function () {
                botaoAdicionar.prop('disabled', false); 
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
    $(document).on('submit', '.excluirTarefaForm', function (event) {
        event.preventDefault();
        var form = $(this);
        form.find('button').prop('disabled', true); 

        $.post("/Home/ExcluirTarefa", form.serialize())
            .done(function (data) {
                $("#tarefasContainer").html(data);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao excluir tarefa: " + xhr.responseText);
            })
            .always(function () {
                form.find('button').prop('disabled', false);
            });
    });
});
