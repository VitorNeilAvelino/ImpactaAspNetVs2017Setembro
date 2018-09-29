using Loja.Dominio;
using Loja.Repositorios.SqlServer.Migrations;
using Loja.Repositorios.SqlServer.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Loja.Repositorios.SqlServer
{
    //design pattern: Unit of Work
    public class LojaDbContext : IdentityDbContext<Usuario>
    {
        public LojaDbContext() : base("lojaConnectionString")
        {
            // pag: 191
            //Database.SetInitializer(new LojaDbInitializer());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<LojaDbContext, Configuration>());
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new CategoriaConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static LojaDbContext Create()
        {
            return new LojaDbContext();
        }
    }
}
