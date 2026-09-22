using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.Models;

namespace ProdutosAPI.Controllers
{
        [ApiController]
        [Route("api/clientes/{clienteId}/enderecos")]
        [Authorize]
        public class EnderecosController : ControllerBase
        {
            private readonly AppDbContext _context;

            public EnderecosController(AppDbContext context)
            {
                _context = context;
            }

        [HttpPost]
        public async Task<ActionResult<Endereco>> Post(int clienteId, Endereco endereco)
            {
                var cliente = await _context.Clientes.FindAsync(clienteId);
                if (cliente == null) return NotFound("Cliente não encontrado.");

                endereco.ClienteId = clienteId;
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { clienteId, id = endereco.Id   }, endereco);
            }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Endereco>> GetById(int clienteId, int id)
        {
            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.ClienteId == clienteId);

            if (endereco == null) return NotFound();
            return endereco;
        }
    }
    }
