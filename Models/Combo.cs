using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Combo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ComboProduct>? ComboProducts { get; set; }
    }
} 