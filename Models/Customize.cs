namespace TotemPWA.Models
{
    public class Customize
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
} 