using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.API.Models
{
    public class Cliente : Usuario // Propriedades específicas para clientes podem ser adicionadas aqui
    {
        [StringLength(14)]
        public string Cpf { get; set; }

        public ICollection<Pedido> Pedido { get; set; } = new List<Pedido>(); // Relacionamento com Pedido
    }
}
