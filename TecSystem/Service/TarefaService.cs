using TecSystem.Models;
using TecSystem.Service.Interface;

namespace TecSystem.Service
{
    public class TarefaService : ITarefaService
    {
        #region Atributos
        private readonly ApplicationDbContext _context;
        #endregion Fim Atributos

        #region Construtores
        public TarefaService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Fim Construtores

        #region Métodos
        public RetornoPadrao AdicionarTarefa(Tarefa novaTarefa)
        {
            RetornoPadrao retorno = new RetornoPadrao();
            try
            {
                if (_context.Tarefas.Any(x => x.Titulo == novaTarefa.Titulo))
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Tarefa com o mesmo título já existe.";
                    return retorno;
                }

                _context.Tarefas.Add(novaTarefa);
                _context.SaveChanges();
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa adicionada com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Erro ao adicionar tarefa: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao EditarTarefa(Tarefa tarefa)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaEditada = _context.Tarefas.Single(x => x.Id == tarefa.Id);
                tarefaEditada.AlterarTitulo(tarefa.Titulo);
                tarefaEditada.AlterarDescricao(tarefa.Descricao);
                _context.SaveChanges();
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa editada com sucesso.";
                retorno.Objeto = _context.Tarefas.Where(x => x.ListaNome == tarefaEditada.ListaNome).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar editar a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ExcluirTarefa(int tarefaId)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaExcluida = _context.Tarefas.Single(x => x.Id == tarefaId);
                _context.Tarefas.Remove(tarefaExcluida);
                _context.SaveChanges();
                retorno.Sucesso = true;
                retorno.Mensagem = "Tarefa excluída com sucesso.";
                retorno.Objeto = _context.Tarefas.Where(x => x.ListaNome == tarefaExcluida.ListaNome).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar excluir a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao MudarStatusTarefa(int tarefaId)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefaParaConclusao = _context.Tarefas.Single(x => x.Id == tarefaId);
                tarefaParaConclusao.MarcarComoConcluida();
                _context.SaveChanges();
                retorno.Objeto = _context.Tarefas.Where(x => x.ListaNome == tarefaParaConclusao.ListaNome).ToList();
                retorno.Sucesso = true;
                retorno.Mensagem = "Mudança de status realizada com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar realizar a mudança de status. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ObterTarefas(Lista lista)
        {
            RetornoPadrao retorno = new();
            try
            {
                List<Tarefa> tarefas = _context.Tarefas.Where(x => x.ListaNome == lista.Nome).ToList();
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

        public RetornoPadrao ObterTarefa(int id)
        {
            RetornoPadrao retorno = new();
            try
            {
                Tarefa tarefa = _context.Tarefas.Single(x => x.Id == id);
                retorno.Sucesso = true;
                retorno.Objeto = tarefa;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar obter a tarefa. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }
        #endregion Fim Métodos
    }
}
