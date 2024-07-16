using System.ComponentModel.DataAnnotations;

namespace TecSystem.Models
{
    public class Lista
    {
        [Key]
        public int Id { get; private set; }

        private string _nome;
        private ICollection<Tarefa> _tarefas;
        public Lista(int id, string nome)
        {
            Id = id;
            Nome = nome;
            _tarefas = new List<Tarefa>();
        }

        [Required]
        [StringLength(200)]
        public string Nome
        {
            get { return _nome; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome não pode ser vazio.");
                }
                _nome = value;
            }
        }

        public ICollection<Tarefa> Tarefas
        {
            get { return _tarefas; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Tarefas), "A coleção de tarefas não pode ser nula.");
                }
                _tarefas = value;
            }
        }


        public void AlterarNome(string novoNome)
        {
            Nome = novoNome;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                throw new ArgumentNullException(nameof(tarefa), "A tarefa não pode ser nula.");
            }
            _tarefas.Add(tarefa);
        }

        public void RemoverTarefa(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                throw new ArgumentNullException(nameof(tarefa), "A tarefa não pode ser nula.");
            }
            _tarefas.Remove(tarefa);
        }
    }
}
