using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.DTOs;
using ProdutosAPI.Models;


namespace ProdutosAPI.Controllers
{
    [ApiController]
    [Route("api/pedidos/{pedidoId}/itens")]
    [Authorize]
    public class ItensPedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItensPedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ItemPedido>> Post(int pedidoId, ItemPedidoInputDTO itemInput)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null) return NotFound("Pedido não encontrado.");

            var produto = await _context.Produtos.FindAsync(itemInput.ProdutoId);
            if (produto == null) return BadRequest("Produto não encontrado.");

            var item = new ItemPedido
            {
                PedidoId = pedidoId,
                ProdutoId = produto.Id,
                Quantidade = itemInput.Quantidade,
                PrecoUnitario = produto.Preco
            };

            _context.ItemPedidos.Add(item);
            await _context.SaveChangesAsync();

            var itemDTO = new ItemPedidoDTO
            {
                ProdutoNome = produto.Nome,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario
            };

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int pedidoId, int id, ItemPedido itemAtualizado)
        {
            var item = await _context.ItemPedidos
                .FirstOrDefaultAsync(i => i.Id == id && i.PedidoId == pedidoId);

            if (item == null) return NotFound();

            item.Quantidade = itemAtualizado.Quantidade;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int pedidoId, int id)
        {
            var item = await _context.ItemPedidos
                .FirstOrDefaultAsync(i => i.Id == id && i.PedidoId == pedidoId);

            if (item == null) return NotFound();

            _context.ItemPedidos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
