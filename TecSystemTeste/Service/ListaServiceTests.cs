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
    public class ListaServiceTests
    {
        private ApplicationDbContext _context;
        private ListaService _listaService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _listaService = new ListaService(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
        #endregion

        #region AdicionarLista
        [Test]
        public void AdicionarLista_DeveAdicionarLista()
        {
            Lista novaLista = new Lista(1, "Lista Teste");

            RetornoPadrao resultado = _listaService.AdicionarLista(novaLista);

            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Lista adicionada com sucesso.", resultado.Mensagem);
        }
        #endregion

        #region TearDown
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        #endregion

        #region ExcluirLista
        [Test]
        public void ExcluirLista_DeveExcluirLista()
        {
            Lista listaExistente = new Lista(1, "Lista Teste");
            _context.Listas.Add(listaExistente);
            _context.SaveChanges();

            RetornoPadrao resultado = _listaService.ExcluirLista("Lista Teste");

            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual("Lista excluída com sucesso.", resultado.Mensagem);
        }

        [Test]
        public void ExcluirLista_ListaNaoExistente_DeveRetornarMensagem()
        {
            RetornoPadrao resultado = _listaService.ExcluirLista("Lista Não Existente");

            Assert.IsFalse(resultado.Sucesso);
            Assert.AreEqual("Essa lista não existe.", resultado.Mensagem);
        }
        #endregion

        #region ObterListas
        [Test]
        public void ObterListas_DeveRetornarListas()
        {
            Lista listaExistente = new Lista(1, "Lista Teste");
            _context.Listas.Add(listaExistente);
            _context.SaveChanges();

            RetornoPadrao resultado = _listaService.ObterListas();

            Assert.IsTrue(resultado.Sucesso);
            Assert.AreEqual(1, ((List<Lista>)resultado.Objeto).Count);
        }
        #endregion

        #region ObterLista
        [Test]
        public void ObterLista_DeveRetornarLista()
        {
            Lista listaExistente = new Lista(1, "Lista Teste");
            _context.Listas.Add(listaExistente);
            _context.SaveChanges();

            RetornoPadrao resultado = _listaService.ObterLista("Lista Teste");

            Assert.IsTrue(resultado.Sucesso);
            Assert.IsNotNull(resultado.Objeto);
        }
        #endregion
    }
}
