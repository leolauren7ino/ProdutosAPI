namespace ProdutosAPI.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string ClienteNome { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }
    }
}
