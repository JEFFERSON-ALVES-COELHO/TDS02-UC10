using ControleEstoque.API.DTOs;
using ControleEstoque.API.Models;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ControleEstoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuario = await _usuarioService.ListarTodosUsuariosAsync();
            return Ok(usuario);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            return Ok(usuario);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorEmail(email);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado para email informado");
            }
            return Ok(usuario);
        }
        [HttpPost("registrar-cliente")]

        public async Task<IActionResult> RegistrarCliente ([FromBody] CriarClienteDto dto)
        {
            var novoCliente = await _usuarioService.RegistrarCliente(dto);
            return Ok();
        }
        [HttpPost("registrar-caixa")]

        public async Task<IActionResult> RegistrarCaixa ([FromBody] CriarCaixaDto dto)
        {
            var novoCaixa = await _usuarioService.RegistrarCaixa(dto);
            return Ok();
        }
        [HttpPost("registrar-gerente")]

        public async Task<IActionResult> RegistrarGerente ([FromBody] CriarGerenteDto dto)
        {
            var novoGerente = await _usuarioService.RegistrarGerente(dto);
            return Ok();
        }
    }
}