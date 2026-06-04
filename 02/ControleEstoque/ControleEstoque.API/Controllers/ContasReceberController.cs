using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContasReceberController : ControllerBase
    {
        private readonly IContaReceberService _contaReceberService;

        public ContasReceberController(IContaReceberService contaReceberService)
        {
            _contaReceberService = contaReceberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clienteIdClaim = User.FindFirst("ClienteId")?.Value;

            if (!int.TryParse(clienteIdClaim, out var clienteIdToken))
                return Unauthorized("ClienteId não encontrado no token.");

            var contas = await _contaReceberService.ObterPorClienteIdAsync(clienteIdToken);

            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var conta = await _contaReceberService.ObterPorIdAsync(id);
            if (conta == null) return NotFound();

            var clienteIdClaim = User.FindFirst("ClienteId")?.Value;

            // Verifica se a conta pertence ao cliente autenticado
            if (!int.TryParse(clienteIdClaim, out var clienteIdToken))
                return Unauthorized("ClienteId não encontrado no token.");

            if (conta.ClienteId != clienteIdToken)
                return Forbid();


            return Ok(conta);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Caixa")]
        public async Task<IActionResult> Create([FromBody] CriarContaReceberDto dto)
        {
            var novaConta = await _contaReceberService.CriarAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novaConta.Id },
                novaConta);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente,Caixa")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarContaReceberDto dto)
        {
            if (id != dto.Id)
                return BadRequest("O ID da rota difere do ID da conta a receber.");

            await _contaReceberService.AtualizarAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente,Caixa")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contaReceberService.RemoverAsync(id);

            return NoContent();
        }
    }
}