using ControleEstoque.API.Data;
using ControleEstoque.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CriarPedidoAsync(
            int clienteId,
            int formaPagamentoId,
            List<ItemPedido> itens)
        {
            // Valida se a forma de pagamento existe
            var formaPagamento = await _context.FormasPagamento
                .FirstOrDefaultAsync(f => f.Id == formaPagamentoId && f.Ativo);

            if (formaPagamento == null)
                throw new Exception("Forma de pagamento inválida.");

            foreach (var item in itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);

                if (produto == null)
                    throw new Exception($"Produto {item.ProdutoId} não encontrado.");

                if (produto.QuantidadeEstoque < item.Quantidade)
                    throw new Exception($"Estoque insuficiente para o produto {produto.Nome}.");

                item.PrecoUnitario = produto.Preco;

                produto.QuantidadeEstoque -= item.Quantidade;
            }

            var pedido = new Pedido
            {
                ClienteId = clienteId,
                FormaPagamentoId = formaPagamentoId,
                DataPedido = DateTime.Now,
                Status = "Aberto",
                Itens = itens
            };

            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();

            return pedido;
        }

        public Task<Pedido> CriarPedidoAsync(int clienteId, List<ItemPedido> itens)
        {
            var formaPagamentoPadrao = _context.FormasPagamento.FirstOrDefault(f => f.Ativo);
            if (formaPagamentoPadrao == null)
                throw new Exception("Forma de pagamento padrão não encontrada.");

            return CriarPedidoAsync(clienteId, formaPagamentoPadrao.Id, itens);
        }

        public async Task<IEnumerable<Pedido>> ListarPedidosDoClienteAsync(int clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.FormaPagamento)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.ClienteId == clienteId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pedido?> ObterPedidoComDetalhesAsync(int pedidoId)
        {
            return await _context.Pedidos
                .Include(p => p.FormaPagamento)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }
    }
}