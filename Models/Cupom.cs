namespace TotemPWA.Models
{
    public class Cupom
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
} 