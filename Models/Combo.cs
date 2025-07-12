using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Combo
    {
        public int Id { get; set; }
        public ICollection<ComboProduct>? ComboProducts { get; set; }
    }
} 