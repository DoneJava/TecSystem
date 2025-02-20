﻿using TecSystem.Models;

namespace TecSystem.Service.Interface
{
    public interface ITarefaService
    {
        RetornoPadrao ObterTarefas(Lista lista);
        RetornoPadrao ObterTarefa(int id);
        RetornoPadrao AdicionarTarefa(Tarefa tarefa);
        RetornoPadrao ExcluirTarefa(int tarefaId);
        RetornoPadrao EditarTarefa(Tarefa tarefa);
        RetornoPadrao MudarStatusTarefa(int tarefaId);
    }
}
