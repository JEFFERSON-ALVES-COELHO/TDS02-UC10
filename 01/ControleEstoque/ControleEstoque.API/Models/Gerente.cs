using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.API.Models
{
    public class Gerente : Usuario // Propriedades específicas para gerentes podem ser adicionadas aqui
    {
        [StringLength(50)]
        public string Setor { get; set; }
    }
}