using TecSystem.Models;

namespace TecSystem.Service.Interface
{
    public interface IListaService
    {
        RetornoPadrao ObterListas();
        RetornoPadrao AdicionarLista(Lista lista);
        RetornoPadrao ExcluirLista(string nome);
    }
}
