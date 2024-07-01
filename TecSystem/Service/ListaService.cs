using TecSystem.Models;
using TecSystem.Service.Interface;

namespace TecSystem.Service
{
    public class ListaService : IListaService
    {
        #region Atributos
        private readonly List<Lista> _listas;
        private readonly ITarefaService _tarefaService;
        #endregion Fim Atributos

        #region Construtores
        public ListaService(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
            _listas = new();
        }
        #endregion Fim Construtores

        #region Métodos
        public RetornoPadrao AdicionarLista(Lista novaLista)
        {
            RetornoPadrao retorno = new();
            try
            {
                novaLista.Tarefas = new List<Tarefa>();
                if (_listas.Any(x => x.Nome.Equals(novaLista.Nome, StringComparison.OrdinalIgnoreCase)))
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Nome da lista já existe. Por favor, dê outro nome à lista.";
                    return retorno;
                }
                _listas.Add(novaLista);
                retorno.Sucesso = true;
                retorno.Mensagem = "Lista adicionada com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = @$"Houve um erro ao tentar adicionar a lista. 
                                      Por favor, entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ExcluirLista(string nome)
        {
            RetornoPadrao retorno = new();
            try
            {
                Lista lista = _listas.Single(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
                if (!_listas.Any(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
                {
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Essa lista não existe.";
                    return retorno;
                }

                if (_tarefaService.ObterTarefas(lista).Objeto is List<Tarefa> tarefas && tarefas.Any())
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Essa lista tem tarefas atreladas a ela, portanto, não pode ser excluída.";
                    return retorno;
                }

                _listas.RemoveAll(x => x.Nome.Equals(lista.Nome, StringComparison.OrdinalIgnoreCase));

                retorno.Sucesso = true;
                retorno.Mensagem = "Lista excluída com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = @$"Houve um erro ao tentar excluir a lista. 
                                      Por favor, entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ObterListas()
        {
            RetornoPadrao retorno = new();
            try
            {
                retorno.Sucesso = true;
                retorno.Objeto = _listas.ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = @$"Houve um erro ao tentar obter as listas. 
                                      Por favor, entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }
        #endregion Fim Métodos
    }
}
