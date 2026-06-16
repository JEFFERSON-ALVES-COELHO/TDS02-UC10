using ControleEstoque.API.DTOs;

namespace ControleEstoque.API.Services
{
    public interface IFormaPagamentoService
    {
        Task<IEnumerable<FormaPagamentoDto>> ObterTodosAsync();
        Task<FormaPagamentoDto?> ObterPorIdAsync(int id);
        Task<FormaPagamentoDto> CriarAsync(FormaPagamentoDto dto);
        Task<FormaPagamentoDto?> AtualizarAsync(int id, FormaPagamentoDto dto);
        Task<bool> ExcluirAsync(int id);
    }
}