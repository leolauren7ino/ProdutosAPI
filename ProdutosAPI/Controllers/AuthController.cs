using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.DTOs;
using ProdutosAPI.Models;
using ProdutosAPI.Services;

namespace ProdutosAPI.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly AppDbContext _context;
            private readonly TokenService _tokenService;

            public AuthController(AppDbContext context, TokenService tokenService)
            {
                _context = context;
                _tokenService = tokenService;
            }

            [HttpPost("registrar")]
            public async Task<ActionResult> Registrar(RegistroInputDTO registroInput)
            {
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Login == registroInput.Login);

                if (usuarioExistente != null)
                {
                    return BadRequest("Esse login já está em uso.");
                }

                var usuario = new Usuario
                {
                    Login = registroInput.Login,
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword(registroInput.Senha)
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuário registrado com sucesso.");
            }

            [HttpPost("login")]
            public async Task<ActionResult> Login(LoginInputDTO loginInput)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Login == loginInput.Login);

                if (usuario == null)
                {
                    return Unauthorized("Login ou senha inválidos.");
                }

                bool senhaValida = BCrypt.Net.BCrypt.Verify(loginInput.Senha, usuario.SenhaHash);

                if (!senhaValida)
                {
                    return Unauthorized("Login ou senha inválidos.");
                }

                var token = _tokenService.GerarToken(usuario);

                return Ok(new { token });
            }
        }
    }
