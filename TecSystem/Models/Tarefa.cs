using System;
using System.ComponentModel.DataAnnotations;

namespace TecSystem.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; private set; }

        private string _titulo;
        private string _descricao;
        private DateTime _dataConclusao;
        private bool _concluida;
        private string _listaNome;

        [Required]
        [StringLength(100)]
        public string Titulo
        {
            get { return _titulo; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O título não pode ser vazio.");
                }
                _titulo = value;
            }
        }

        [Required]
        [StringLength(500)]
        public string Descricao
        {
            get { return _descricao; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A descrição não pode ser vazia.");
                }
                _descricao = value;
            }
        }

        [Required]
        public DateTime DataConclusao
        {
            get { return _dataConclusao; }
            private set
            {
                if (value <= DateTime.Now)
                {
                    throw new ArgumentException("A data de conclusão deve ser futura.");
                }
                _dataConclusao = value;
            }
        }

        public bool Concluida
        {
            get { return _concluida; }
            private set
            {
                if (value && DateTime.Now < _dataConclusao)
                {
                    throw new InvalidOperationException("Não pode marcar como concluída antes da data de conclusão.");
                }
                _concluida = value;
            }
        }

        [Required]
        [StringLength(100)]
        public string ListaNome
        {
            get { return _listaNome; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome da lista não pode ser vazio.");
                }
                _listaNome = value;
            }
        }

        [Required]
        public Lista Lista { get; private set; }

        private Tarefa() { }

        public Tarefa(int id, string titulo, string descricao, DateTime dataConclusao, string listaNome, Lista lista)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            DataConclusao = dataConclusao;
            ListaNome = listaNome;
            Lista = lista;
            _concluida = false;
        }

        public void MarcarComoConcluida()
        {
            if (!_concluida)
            {
                _dataConclusao = DateTime.Now;
                _concluida = true;
            }
            else
            {
                _concluida = false;
            }
        }

        public void AlterarTitulo(string novoTitulo)
        {
            Titulo = novoTitulo;
        }

        public void AlterarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
        }

        public void AlterarDataConclusao(DateTime novaDataConclusao)
        {
            DataConclusao = novaDataConclusao;
        }

        public void AlterarListaNome(string novoListaNome)
        {
            ListaNome = novoListaNome;
        }
    }
}
