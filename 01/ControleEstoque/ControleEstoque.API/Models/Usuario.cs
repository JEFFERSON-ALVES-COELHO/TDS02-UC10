using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.API.Models
{
    public enum PerfilUsuario
    {
        Cliente,
        Caixa,
        Gerente,
    }
    public abstract class Usuario //abstract impede instancia da classe
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; } // tamanho minimo

        [Required]
        public PerfilUsuario Perfil { get; set; }
    }

    public class Cliente : Usuario // Propriedades específicas para clientes podem ser adicionadas aqui
    {
        [StringLength(14)]
        public string Cpf { get; set; }
    }

    public class Caixa : Usuario  // Propriedades específicas para caixas podem ser adicionadas aqui
    {
        [StringLength(50)]
        public string Turno { get; set; }
    }

    public class Gerente : Usuario // Propriedades específicas para gerentes podem ser adicionadas aqui
    {
        [StringLength(50)]
        public string Setor { get; set; }
    }
}