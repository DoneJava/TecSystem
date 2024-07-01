$(document).ready(function () {
    $(document).on('change', '.changeStatusCheckbox', function () {
        var checkbox = $(this);
        var taskId = checkbox.data("id");

        if (checkbox.prop('disabled')) {
            return; 
        }
        checkbox.prop('disabled', true);

        $.post("/Home/MudarStatusTarefa", { id: taskId })
            .done(function (data) {
                $("#tarefasContainer").html(data);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao mudar status da tarefa: " + xhr.responseText);
            })
            .always(function () {
                checkbox.prop('disabled', false);
            });
    });

    $(document).on('submit', '.excluirTarefaForm', function (event) {
        event.preventDefault();
        var form = $(this);
        var button = form.find('button');

        if (button.prop('disabled')) {
            return;
        }
        button.prop('disabled', true);

        $.post("/Home/ExcluirTarefa", form.serialize())
            .done(function (data) {
                $("#tarefasContainer").html(data);
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao excluir tarefa: " + xhr.responseText);
            })
            .always(function () {
                button.prop('disabled', false); 
            });
    });

    $(document).on('click', '.btn-editar', function () {
        var button = $(this);
        var tarefaId = button.data('id');
        var titulo = button.data('titulo');
        var descricao = button.data('descricao');

        $('#editarTarefaId').val(tarefaId);
        $('#editarTitulo').val(titulo);
        $('#editarDescricao').val(descricao);
    });

    $('#editarTarefaForm').submit(function (event) {
        event.preventDefault();
        var form = $(this);
        var button = form.find('button[type="submit"]');

        if (button.prop('disabled')) {
            return; 
        }
        button.prop('disabled', true); 

        $.post("/Home/EditarTarefa", form.serialize())
            .done(function (data) {
                $("#tarefasContainer").html(data);
                $('#editarTarefaModal').modal('hide');
            })
            .fail(function (xhr, status, error) {
                alert("Erro ao editar tarefa: " + xhr.responseText);
            })
            .always(function () {
                button.prop('disabled', false); 
            });
    });
});
