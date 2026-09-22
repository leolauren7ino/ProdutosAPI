using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.DTOs;
using ProdutosAPI.Models;

namespace ProdutosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PedidoDTO>>> Get()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();

            var pedidosDTO = pedidos.Select(p => new PedidoDTO
            {
                Id = p.Id,
                Data = p.Data,
                ClienteNome = p.Cliente != null ? p.Cliente.Nome : "Desconhecido",

                Itens = p.Itens.Select(i => new ItemPedidoDTO
                {
                    ProdutoNome = i.Produto != null ? i.Produto.Nome : "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList(),
                 ValorTotal = p.Itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            }).ToList();

            return pedidosDTO;

        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Post(PedidoInputDTO pedidoInput)
        {
            var pedido = new Pedido
            {
                ClienteId = pedidoInput.ClienteId,
                Data = pedidoInput.Data
            };

            foreach (var itemInput in pedidoInput.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemInput.ProdutoId);
                if (produto == null)
                {
                    return BadRequest($"Produto com Id {itemInput.ProdutoId} não existe.");
                }

                if (produto.Estoque < itemInput.Quantidade)
                {
                    return BadRequest(
                        $"Estoque insuficiente para o produto '{produto.Nome}'. " +
                        $"Disponível: {produto.Estoque}, solicitado: {itemInput.Quantidade}.");
                }

                // Atualiza o estoque do produto
                produto.Estoque -= itemInput.Quantidade;

                var item = new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = itemInput.Quantidade,
                    PrecoUnitario = produto.Preco
                };

                pedido.Itens.Add(item);
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var pedidoCompleto = await _context.Pedidos
                              .Include(p => p.Cliente)
                              .Include(p => p.Itens)
                              .ThenInclude(i => i.Produto)
                              .FirstOrDefaultAsync(p => p.Id == pedido.Id);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedidoCompleto.Id,
                Data = pedidoCompleto.Data,
                ClienteNome = pedidoCompleto.Cliente != null ? pedidoCompleto.Cliente.Nome : "Desconhecido",
                Itens = pedidoCompleto.Itens.Select(i => new ItemPedidoDTO
                {
                    ProdutoNome = i.Produto != null ? i.Produto.Nome : "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList(),
                ValorTotal = pedidoCompleto.Itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            };

            return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedidoDTO);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Pedido pedidoAtualizado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            pedido.Data = pedidoAtualizado.Data;


            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
