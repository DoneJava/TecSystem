using TecSystem.Models;

namespace TecSystemTestes
{
    [TestFixture]
    public class TarefaTests
    {
        #region MarcarComoConcluida
        [Test]
        public void MarcarComoConcluida_DeveMarcarComoConcluida()
        {
            Lista lista = new (1, "Lista Teste");
            Tarefa tarefa = new (1, "Título Teste", "Descrição Teste", DateTime.Now.AddDays(1), "Lista Teste", lista);

            tarefa.MarcarComoConcluida();

            Assert.IsTrue(tarefa.Concluida);
        }
        #endregion

        #region AlterarTitulo
        [Test]
        public void AlterarTitulo_DeveAlterarTitulo()
        {
            Lista lista = new (1, "Lista Teste");
            Tarefa tarefa = new (1, "Título Original", "Descrição Original", DateTime.Now.AddDays(1), "Lista Teste", lista);

            tarefa.AlterarTitulo("Título Alterado");

            Assert.AreEqual("Título Alterado", tarefa.Titulo);
        }
        #endregion

        #region AlterarDescricao
        [Test]
        public void AlterarDescricao_DeveAlterarDescricao()
        {
            Lista lista = new (1, "Lista Teste");
            Tarefa tarefa = new (1, "Título Original", "Descrição Original", DateTime.Now.AddDays(1), "Lista Teste", lista);

            tarefa.AlterarDescricao("Descrição Alterada");

            Assert.AreEqual("Descrição Alterada", tarefa.Descricao);
        }
        #endregion

        #region AlterarDataConclusao
        [Test]
        public void AlterarDataConclusao_DeveAlterarDataConclusao()
        {
            Lista lista = new (1, "Lista Teste");
            Tarefa tarefa = new (1, "Título Original", "Descrição Original", DateTime.Now.AddDays(1), "Lista Teste", lista);

            tarefa.AlterarDataConclusao(DateTime.Now.AddDays(2));

            Assert.AreEqual(DateTime.Now.AddDays(2).Date, tarefa.DataConclusao.Date);
        }
        #endregion

        #region AlterarListaNome
        [Test]
        public void AlterarListaNome_DeveAlterarListaNome()
        {
            Lista lista = new (1, "Lista Teste");
            Tarefa tarefa = new (1, "Título Original", "Descrição Original", DateTime.Now.AddDays(1), "Lista Teste", lista);

            tarefa.AlterarListaNome("Nova Lista");

            Assert.AreEqual("Nova Lista", tarefa.ListaNome);
        }
        #endregion
    }
}
