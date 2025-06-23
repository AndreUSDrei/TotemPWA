using System;
using System.Collections.Generic;

namespace TotemPWA.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CupomId { get; set; }
        public Cupom? Cupom { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
} 