namespace ProdutosAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data {  get; set; }
        //Aponta pro cliente dono do pedido
        public int ClienteId {  get; set; }
        public Cliente? Cliente { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }
}
