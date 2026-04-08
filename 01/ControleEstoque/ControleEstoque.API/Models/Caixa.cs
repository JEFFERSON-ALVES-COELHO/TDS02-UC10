using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.API.Models
{
    public class Caixa : Usuario  // Propriedades específicas para caixas podem ser adicionadas aqui
    {
        [StringLength(50)]
        public string Turno { get; set; }

        public ICollection<Pedido> PedidosFechados { get; set; } = new List<Pedido>(); // Relacionamento com Pedido
    }
}