namespace TotemPWA.Models
{
    public class Employee
    {
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public string Type { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 