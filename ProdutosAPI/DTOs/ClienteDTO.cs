namespace ProdutosAPI.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<EnderecoDTO> Enderecos { get; set; } = new();
    }
}
