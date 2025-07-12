using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category>? SubCategories { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
} 