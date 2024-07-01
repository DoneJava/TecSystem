using System.ComponentModel.DataAnnotations;

namespace TecSystem.Models
{
    public class Lista
    {
        public string Nome { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
