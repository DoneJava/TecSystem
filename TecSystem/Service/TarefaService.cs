using TecSystem.Models;
using TecSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TecSystem.Service
{
    public class TarefaService : ITarefaService
    {
        #region Atributos
        private readonly List<Tarefa> _tarefas;
        private int _proximoId;
        #endregion Fim Atributos

        #region Construtores
        public TarefaService()
        {
            _tarefas = new();
            _proximoId = 1;
        }
        #endregion Fim Construtores

        #region Métodos
        public RetornoPadrao AdicionarTarefa(Tarefa novaTarefa)
        {
            RetornoPadrao retorno = new();
            try
            {
                novaTarefa.Id = _proximoId++;
                _tarefas.Add(novaTarefa);
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa adicionada com sucesso.";
                retorno.Objeto = _tarefas.Where(x => x.listaNome.Equals(novaTarefa.listaNome, StringComparison.OrdinalIgnoreCase)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar adicionar a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao EditarTarefa(Tarefa tarefa)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaEditada = _tarefas.Single(x => x.Id == tarefa.Id);
                tarefaEditada.Titulo = tarefa.Titulo;
                tarefaEditada.Descricao = tarefa.Descricao;
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa editada com sucesso.";
                retorno.Objeto = _tarefas.Where(x => x.listaNome.Equals(tarefaEditada.listaNome, StringComparison.OrdinalIgnoreCase)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar editar a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }


        public RetornoPadrao ExcluirTarefa(int tarefaid)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaExcluida = _tarefas.Single(x => x.Id == tarefaid);
                _tarefas.Remove(tarefaExcluida);
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa excluida com sucesso.";
                retorno.Objeto = _tarefas.Where(x => x.listaNome.Equals(tarefaExcluida.listaNome, StringComparison.OrdinalIgnoreCase)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar Excluir a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao MudarStatusTarefa(int tarefaId)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaParaConclusao = _tarefas.Single(x => x.Id == tarefaId);
                tarefaParaConclusao.Concluida = !tarefaParaConclusao.Concluida;
                tarefaParaConclusao.DataConclusao = DateTime.Now;
                retorno.Objeto = _tarefas.Where(x => x.listaNome.Equals(tarefaParaConclusao.listaNome, StringComparison.OrdinalIgnoreCase)).ToList();
                retorno.Sucesso = true;
                retorno.Mensagem = "Mudança realizada com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar realizar mudança. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ObterTarefas(Lista lista)
        {
            RetornoPadrao retorno = new();
            try
            {
                List<Tarefa> tarefas = _tarefas.Where(x => x.listaNome.Equals(lista.Nome, StringComparison.OrdinalIgnoreCase)).ToList();
                retorno.Sucesso = true;
                retorno.Objeto = tarefas;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar obter as tarefas. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }
        #endregion Fim Métodos
    }
}
