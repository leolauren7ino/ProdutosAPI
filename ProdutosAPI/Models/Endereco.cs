namespace ProdutosAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }

        // Chave estrangeira
        public int ClienteId { get; set; }

        // Propriedade de navegação
        public Cliente? Cliente { get; set; }
    }
}
