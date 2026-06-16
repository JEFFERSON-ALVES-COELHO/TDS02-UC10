using ControleEstoque.API.DTOs;

namespace ControleEstoque.API.Services
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        public async Task<IEnumerable<FormaPagamentoDto>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FormaPagamentoDto?> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FormaPagamentoDto> CriarAsync(FormaPagamentoDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<FormaPagamentoDto?> AtualizarAsync(int id, FormaPagamentoDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}