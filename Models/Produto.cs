namespace TotemPWA.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Slug { get; set; } = string.Empty;
    }
} 