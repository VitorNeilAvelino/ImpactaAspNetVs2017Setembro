namespace Loja.Dominio
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Unidade { get; set; }
        public bool Ativo { get; set; }

        // virtual - habilita o lazy load do EF.
        public virtual Categoria Categoria { get; set; }
    }
}