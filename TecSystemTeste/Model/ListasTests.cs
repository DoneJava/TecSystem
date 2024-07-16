using NUnit.Framework;
using TecSystem.Models;
using System;

namespace TecSystemTestes
{
    [TestFixture]
    public class ListaTests
    {
        #region AlterarNome
        [Test]
        public void AlterarNome_DeveAlterarNome()
        {
            Lista lista = new Lista(1, "Nome Original");

            lista.AlterarNome("Nome Alterado");

            Assert.AreEqual("Nome Alterado", lista.Nome);
        }
        #endregion

        #region AdicionarTarefa
        [Test]
        public void AdicionarTarefa_DeveAdicionarTarefa()
        {
            Lista lista = new Lista(1, "Lista Teste");
            Tarefa tarefa = new Tarefa(1, "Título Teste", "Descrição Teste", DateTime.Now.AddDays(1), "Lista Teste", lista);

            lista.AdicionarTarefa(tarefa);

            Assert.Contains(tarefa, (System.Collections.ICollection)lista.Tarefas);
        }

        [Test]
        public void AdicionarTarefa_TarefaNula_DeveLancarExcecao()
        {
            Lista lista = new Lista(1, "Lista Teste");

            Assert.Throws<ArgumentNullException>(() => lista.AdicionarTarefa(null));
        }
        #endregion

        #region RemoverTarefa
        [Test]
        public void RemoverTarefa_DeveRemoverTarefa()
        {
            Lista lista = new Lista(1, "Lista Teste");
            Tarefa tarefa = new Tarefa(1, "Título Teste", "Descrição Teste", DateTime.Now.AddDays(1), "Lista Teste", lista);
            lista.AdicionarTarefa(tarefa);

            lista.RemoverTarefa(tarefa);

            Assert.IsFalse(lista.Tarefas.Contains(tarefa));
        }

        [Test]
        public void RemoverTarefa_TarefaNula_DeveLancarExcecao()
        {
            Lista lista = new Lista(1, "Lista Teste");

            Assert.Throws<ArgumentNullException>(() => lista.RemoverTarefa(null));
        }
        #endregion
    }
}
