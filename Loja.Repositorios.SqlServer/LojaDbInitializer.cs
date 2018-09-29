using System;
using System.Collections.Generic;
using System.Data.Entity;
using Loja.Dominio;

namespace Loja.Repositorios.SqlServer
{
    internal class LojaDbInitializer : DropCreateDatabaseIfModelChanges<LojaDbContext>
    {
        protected override void Seed(LojaDbContext context)
        {
            context.Produtos.AddRange(ObterProdutos());

            context.SaveChanges();
        }

        private IEnumerable<Produto> ObterProdutos()
        {
            var barbeador = new Produto();
            barbeador.Estoque = 45;
            barbeador.Nome = "Barbeador";
            barbeador.Preco = 17.40m;
            barbeador.Categoria = new Categoria { Nome = "Perfumaria" };
            barbeador.Unidade = "Caixa";

            var caneta = new Produto();
            caneta.Estoque = 52;
            caneta.Nome = "Caneta";
            caneta.Preco = 11.52m;
            caneta.Categoria = new Categoria { Nome = "Papelaria" };
            caneta.Unidade = "Caixa";

            return new List<Produto> { barbeador, caneta };
        }
    }
}