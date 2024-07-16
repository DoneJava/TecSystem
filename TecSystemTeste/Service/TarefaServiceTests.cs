using NUnit.Framework;
using Moq;
using TecSystem.Models;
using TecSystem.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace TecSystemTestes
{
    [TestFixture]
    public class TarefaServiceTests
    {
        private ApplicationDbContext _context;
        private TarefaService _tarefaService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new ApplicationDbContext(options);
            _tarefaService = new TarefaService(_context);
        }
        #endregion

        #region AdicionarTarefa
        [Test]
        public void AdicionarTarefa_DeveAdicionarTarefa()
        {
            // Arrange
            var novaTarefa = new Tarefa(1, "Título Teste", "Descrição Teste", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));

            // Act
            RetornoPadrao resultado = _tarefaService.AdicionarTarefa(novaTarefa);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Tarefa adicionada com sucesso.", resultado.Mensagem);
        }
        #endregion

        #region EditarTarefa
        [Test]
        public void EditarTarefa_DeveEditarTarefa()
        {
            // Arrange
            var tarefaExistente = new Tarefa(1, "Título Original", "Descrição Original", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));
            _context.Tarefas.Add(tarefaExistente);
            _context.SaveChanges();

            var tarefaEditada = new Tarefa(1, "Título Editado", "Descrição Editada", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));

            // Act
            RetornoPadrao resultado = _tarefaService.EditarTarefa(tarefaEditada);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Tarefa editada com sucesso.", resultado.Mensagem);
        }
        #endregion

        #region ExcluirTarefa
        [Test]
        public void ExcluirTarefa_DeveExcluirTarefa()
        {
            // Arrange
            var tarefaExistente = new Tarefa(1, "Título Original", "Descrição Original", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));
            _context.Tarefas.Add(tarefaExistente);
            _context.SaveChanges();

            // Act
            RetornoPadrao resultado = _tarefaService.ExcluirTarefa(1);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Tarefa excluída com sucesso.", resultado.Mensagem);
        }
        #endregion

        #region MudarStatusTarefa
        [Test]
        public void MudarStatusTarefa_DeveMudarStatus()
        {
            // Arrange
            var tarefaExistente = new Tarefa(1, "Título Original", "Descrição Original", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));
            _context.Tarefas.Add(tarefaExistente);
            _context.SaveChanges();

            // Act
            RetornoPadrao resultado = _tarefaService.MudarStatusTarefa(1);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Mudança de status realizada com sucesso.", resultado.Mensagem);
        }
        #endregion

        #region ObterTarefas
        [Test]
        public void ObterTarefas_DeveRetornarTarefas()
        {
            // Arrange
            var lista = new Lista(1, "Lista Teste");
            var tarefaExistente = new Tarefa(1, "Título Original", "Descrição Original", System.DateTime.Now.AddDays(1), "Lista Teste", lista);
            _context.Tarefas.Add(tarefaExistente);
            _context.SaveChanges();

            // Act
            RetornoPadrao resultado = _tarefaService.ObterTarefas(lista);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual(1, ((List<Tarefa>)resultado.Objeto).Count);
        }
        #endregion

        #region ObterTarefa
        [Test]
        public void ObterTarefa_DeveRetornarTarefa()
        {
            // Arrange
            var tarefaExistente = new Tarefa(1, "Título Original", "Descrição Original", System.DateTime.Now.AddDays(1), "Lista Teste", new Lista(1, "Lista Teste"));
            _context.Tarefas.Add(tarefaExistente);
            _context.SaveChanges();

            // Act
            RetornoPadrao resultado = _tarefaService.ObterTarefa(1);

            // Assert
            Assert.IsTrue(resultado.Sucesso);
            Assert.IsNotNull(resultado.Objeto);
        }
        #endregion
    }
}
