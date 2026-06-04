using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using static ControleEstoque.API.DTOs.CriarContaReceberDto;

namespace ControleEstoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaReceberController : ControllerBase
    {
        private readonly IContaReceberService _ContaReceberService;

        public ContaReceberController(IContaReceberService contaReceberService)
        {
            _ContaReceberService = contaReceberService;
        }
         
           [HttpGet]
           public async Task<IActionResult> GetAll()
           {
               var contasReceber = await _ContaReceberService.ObterTodosAsync();
               return Ok(contasReceber);
           }
           [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contaReceber = await _ContaReceberService.ObterPorIdAsync(id);
            if (contaReceber == null)
                return NotFound();
            return Ok(contaReceber);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CriarContaReceberDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ContaReceberService.CriarAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]AtualizarContaReceberDto dto)
        {
            if (id != dto.Id) return BadRequest("Id informado diferente da conta informada");
            var existe = await _ContaReceberService.ObterPorIdAsync(id);
            if (existe == null) 
                return NotFound();

            await _ContaReceberService.AtualizarAsync(dto);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _ContaReceberService.RemoverAsync(id);
            return NoContent();

        }

    }
}
