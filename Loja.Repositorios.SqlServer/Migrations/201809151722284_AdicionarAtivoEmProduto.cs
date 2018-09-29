namespace Loja.Repositorios.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarAtivoEmProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produto", "Ativo", 
                c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produto", "Ativo");
        }
    }
}
