namespace ProdutosAPI.DTOs
{
    public class PedidoInputDTO
    {
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public List<ItemPedidoInputDTO> Itens { get; set; } = new();
    }
}
