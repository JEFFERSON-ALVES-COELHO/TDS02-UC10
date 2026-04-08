using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.API.Models
{
    public class Fornecedor

    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NomeFantansia { get; set; }

        [Required, StringLength(14)]
        public string CNPJ { get; set; }

        public ICollection<Produto> Pedidos { get; set; } = new List<Produto>(); // Relacionamento com Produto
    }
}
