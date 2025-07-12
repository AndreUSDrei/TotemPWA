using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public ICollection<Order>? Orders { get; set; }
    }
} 