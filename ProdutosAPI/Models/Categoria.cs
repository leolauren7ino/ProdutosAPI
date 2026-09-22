namespace ProdutosAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Uma categoria tem uma lista de produtos
        public List<Produto> Produtos { get; set; } = new();
    }
}
