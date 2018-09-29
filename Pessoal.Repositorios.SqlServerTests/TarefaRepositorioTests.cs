using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pessoal.Dominio;
using Pessoal.Repositorios.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoal.Repositorios.SqlServer.Tests
{
    [TestClass()]
    public class TarefaRepositorioTests
    {
        private TarefaRepositorio _tarefaRepositorio = new TarefaRepositorio();

        [TestMethod()]
        public void InserirTest()
        {
            var tarefa = new Tarefa();

            tarefa.Concluida = false;
            tarefa.Nome = "Passar roupa";
            tarefa.Observacoes = "Rápido";
            tarefa.Prioridade = Prioridade.Alta;

            tarefa.Id = _tarefaRepositorio.Inserir(tarefa);

            Assert.AreNotEqual(tarefa.Id, 0);
        }

        [TestMethod()]
        public void AtualizarTest()
        {
            var tarefa = _tarefaRepositorio.Selecionar()[0];

            tarefa.Nome = "Passar muita roupa";
            tarefa.Observacoes = "Observações";
            tarefa.Concluida = true;
            tarefa.Prioridade = Prioridade.Baixa;

            _tarefaRepositorio.Atualizar(tarefa);
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            _tarefaRepositorio.Excluir(1);

            var tarefa = _tarefaRepositorio.Selecionar(1);

            Assert.IsNull(tarefa);
        }
    }
}