using ControleEstoque.API.Data;
using ControleEstoque.API.DTOs;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.API.Models;

namespace ControleEstoque.API.Services
{
    public class FormaPagamentoService : IFormaPagamentoService


    {
        private readonly AppDbContext _context;
        public FormaPagamentoService(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<IEnumerable<FormaPagamentoDto>> ObterTodosAsync()
        {
            return await _context.FormasPagamento
                .AsNoTracking()
                .Select(f => new FormaPagamentoDto
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    Ativo = f.Ativo
                })
                .ToListAsync();
        }

        public async Task<FormaPagamentoDto?> ObterPorIdAsync(int id)
        {
                var formaPagamento = await _context.FormasPagamento
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == id);
                if(formaPagamento == null) return null;

                return new FormaPagamentoDto
                {
                    Id = formaPagamento.Id,
                    Nome = formaPagamento.Nome,
                    Ativo = formaPagamento.Ativo
                };
        }

        public async Task<FormaPagamentoDto> CriarAsync(CriarFormaPagamentoDto dto)
        {
            var formaPagamento = new FormaPagamento
            {
                    Nome = dto.Nome,
                    Ativo = true
           };
             _context.FormasPagamento.Add(formaPagamento);
            await _context.SaveChangesAsync();

            return new FormaPagamentoDto
            {
                Id = formaPagamento.Id,
                Nome = formaPagamento.Nome,
                Ativo = formaPagamento.Ativo,
                
            };
        }

        public async Task AtualizarAsync(AtualizarFormaPagamentoDto dto)
        {
            var formaExistente = await _context.FormasPagamento.FindAsync(dto.Id);
            if (formaExistente != null)
            {
                formaExistente.Nome = dto.Nome;
                formaExistente.Ativo = dto.Ativo;
                await _context.SaveChangesAsync();

            }
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var formaExistente = await _context.FormasPagamento.FindAsync(id);

            if (formaExistente != null)
            
                _context.FormasPagamento.Remove(formaExistente);
                await _context.SaveChangesAsync();
            return true;



        }
    }
}