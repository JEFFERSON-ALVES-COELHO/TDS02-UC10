namespace ControleEstoque.API.Models
{
    public class FormaPagamento
    {
        public int Id { get; set; }
        // Exemplo: "Dinheiro", "Cartão de Crédito", "Pix", etc.
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true; 
        // Indica se a forma de pagamento está associada a um pedido
     
    }
}
