using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoService _service;

        public FormaPagamentoController(IFormaPagamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObterTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var formaPagamento = await _service.ObterPorIdAsync(id);

            if (formaPagamento == null)
                return NotFound();

            return Ok(formaPagamento);
        }

        [HttpPost]
        public async Task<IActionResult> Post(FormaPagamentoDto dto)
        {
            var resultado = await _service.CriarAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FormaPagamentoDto dto)
        {
            var resultado = await _service.AtualizarAsync(id, dto);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _service.ExcluirAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}