using Microsoft.AspNetCore.Mvc;
using TecSystem.Models;
using TecSystem.Service.Interface;

namespace TecSystem.Controllers
{
    public class HomeController : Controller
    {
        #region Atributos
        private readonly IListaService _listaService;
        private readonly ITarefaService _tarefaService;
        #endregion Fim Atributos

        #region Construtores
        public HomeController(IListaService listaService, ITarefaService tarefaService)
        {
            _listaService = listaService;
            _tarefaService = tarefaService;
        }
        #endregion Fim Construtores

        #region Métodos
        public IActionResult Index()
        {
            try
            {
                RetornoPadrao resultado = _listaService.ObterListas();
                if (!resultado.Sucesso)
                {
                    ModelState.AddModelError(string.Empty, resultado.Mensagem ?? string.Empty);
                    return RedirectToAction(nameof(Index));
                }
                List<Lista>? listas = resultado.Objeto as List<Lista>;
                return View(listas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterListas()
        {
            try
            {
                RetornoPadrao resultado = _listaService.ObterListas();
                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao obter listas.");
                }
                List<Lista>? listas = resultado.Objeto as List<Lista>;
                return Json(listas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterTarefas(string listaNome)
        {
            try
            {
                RetornoPadrao resultado = _tarefaService.ObterTarefas(new Lista { Nome = listaNome });
                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao obter tarefas.");
                }
                List<Tarefa>? tarefas = resultado.Objeto as List<Tarefa>;
                return PartialView("_TarefasPartial", tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarLista(string nome)
        {
            try
            {
                Lista novaLista = new Lista { Nome = nome };
                RetornoPadrao resultado = _listaService.AdicionarLista(novaLista);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao adicionar lista.");
                }

                List<Lista>? listas = _listaService.ObterListas().Objeto as List<Lista>;
                return PartialView("_ListaPartial", listas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AdicionarTarefa(string titulo, string descricao, string listaNome)
        {
            try
            {
                Tarefa novaTarefa = new Tarefa { Titulo = titulo, Descricao = descricao, listaNome = listaNome, DataConclusao = DateTime.Now, Concluida = false };
                RetornoPadrao resultado = _tarefaService.AdicionarTarefa(novaTarefa);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao adicionar tarefa.");
                }

                RetornoPadrao resultadoListas = _tarefaService.ObterTarefas(new Lista { Nome = listaNome });
                List<Tarefa>? tarefas = resultadoListas.Objeto as List<Tarefa>;
                return PartialView("_TarefasPartial", tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ExcluirTarefa(int id)
        {
            try
            {
                RetornoPadrao resultado = _tarefaService.ExcluirTarefa(id);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao excluir tarefa.");
                }
                List<Tarefa>? tarefas = resultado.Objeto as List<Tarefa>;
                return PartialView("_TarefasPartial", tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult MudarStatusTarefa(int id)
        {
            try
            {
                RetornoPadrao resultado = _tarefaService.MudarStatusTarefa(id);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao mudar status da tarefa.");
                }
                List<Tarefa>? tarefas = resultado.Objeto as List<Tarefa>;
                return PartialView("_TarefasPartial", tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult ExcluirLista(string nome)
        {
            try
            {
                RetornoPadrao resultado = _listaService.ExcluirLista(nome);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao excluir lista.");
                }

                List<Lista>? listas = _listaService.ObterListas().Objeto as List<Lista>;
                return PartialView("_ListaPartial", listas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditarTarefa(int id, string titulo, string descricao)
        {
            try
            {
                Tarefa tarefaEditada = new Tarefa { Id = id, Titulo = titulo, Descricao = descricao };
                RetornoPadrao resultado = _tarefaService.EditarTarefa(tarefaEditada);

                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado.Mensagem ?? "Erro ao editar tarefa.");
                }

                List<Tarefa>? tarefas = resultado.Objeto as List<Tarefa>;
                return PartialView("_TarefasPartial", tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        #endregion Fim Métodos
    }
}
