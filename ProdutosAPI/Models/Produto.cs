namespace ProdutosAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        // Chave estrangeira (Foreign Key)
        public int CategoriaId { get; set; }

        // Propriedade de navegação
        public Categoria? Categoria { get; set; }
        public List<ItemPedido> ItensPedido { get; set; } = new();
    }
}
