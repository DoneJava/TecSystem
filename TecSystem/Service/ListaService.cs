using TecSystem.Models;
using TecSystem.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TecSystem.Service
{
    public class ListaService : IListaService
    {
        #region Atributos
        private readonly ApplicationDbContext _context;
        #endregion Fim Atributos

        #region Construtores
        public ListaService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion Fim Construtores

        #region Métodos
        public RetornoPadrao AdicionarLista(Lista novaLista)
        {
            RetornoPadrao retorno = new();
            try
            {
                if (_context.Listas.Any(x => x.Nome == novaLista.Nome))
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Nome da lista já existe. Por favor, dê outro nome à lista.";
                    return retorno;
                }
                _context.Listas.Add(novaLista);
                _context.SaveChanges();
                retorno.Sucesso = true;
                retorno.Mensagem = "Lista adicionada com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar adicionar a lista. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ExcluirLista(string nome)
        {
            RetornoPadrao retorno = new();
            try
            {
                Lista lista = _context.Listas.SingleOrDefault(x => x.Nome == nome);
                if (lista == null)
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Essa lista não existe.";
                    return retorno;
                }

                if (_context.Tarefas.Any(x => x.ListaNome == nome))
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Essa lista tem tarefas atreladas a ela, portanto, não pode ser excluída.";
                    return retorno;
                }

                _context.Listas.Remove(lista);
                _context.SaveChanges();
                retorno.Sucesso = true;
                retorno.Mensagem = "Lista excluída com sucesso.";
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar excluir a lista. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ObterListas()
        {
            RetornoPadrao retorno = new();
            try
            {
                List<Lista> listas = _context.Listas.ToList();
                retorno.Sucesso = true;
                retorno.Objeto = listas;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar obter as listas. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }

        public RetornoPadrao ObterLista(string nomeLista)
        {
            RetornoPadrao retorno = new();
            try
            {
                Lista listas = _context.Listas.Single(x => x.Nome == nomeLista);
                retorno.Sucesso = true;
                retorno.Objeto = listas;
                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = $"Houve um erro ao tentar obter as listas. Por favor entre em contato com o setor do CPD e informe este erro: {ex.Message}";
                return retorno;
            }
        }
        #endregion Fim Métodos
    }
}
