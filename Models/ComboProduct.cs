namespace TotemPWA.Models
{
    public class ComboProduct
    {
        public int Id { get; set; }
        public int ProductComboId { get; set; }
        public Combo? Combo { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
} 