using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;

namespace TotemPWA.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Exemplo: dashboard administrativa
            return View();
        }

        // Métodos futuros para gerenciar produtos, pedidos, usuários, etc.
    }
} 