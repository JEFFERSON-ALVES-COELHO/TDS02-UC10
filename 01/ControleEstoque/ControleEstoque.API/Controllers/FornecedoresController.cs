using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {

        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _fornecedorService.ObterTodosAsync();
            return Ok(fornecedores);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fornecedor = await _fornecedorService.ObterPorIdAsync(id);
            if (fornecedor == null)
                return NotFound();
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CriarFornecedorDto dto)
        {
            var fornecedor = await _fornecedorService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, fornecedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]AtualizarFornecedorDto dto)
        {
            var existe = await _fornecedorService.ObterPorIdAsync(id);
            if (existe == null) 
                return NotFound();

            await _fornecedorService.AtualizarAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fornecedorService.RemoverAsync(id);
            return NoContent();
        }
    }
}
