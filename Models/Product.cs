using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Additional>? Additionals { get; set; }
        public ICollection<ComboProduct>? ComboProducts { get; set; }
        public ICollection<Promotion>? Promotions { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Customize>? Customizes { get; set; }
    }
} 