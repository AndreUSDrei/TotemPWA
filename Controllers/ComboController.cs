using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using System.Linq;

namespace TotemPWA.Controllers
{
    public class ComboController : Controller
    {
        private readonly AppDbContext _context;
        public ComboController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var combos = _context.Products
                .Include(p => p.ComboProducts)
                .Where(p => p.Category.Name == "Combos")
                .ToList();
            return View(combos);
        }

        public IActionResult Detalhe(int id)
        {
            var combo = _context.Products
                .Include(p => p.ComboProducts)
                .FirstOrDefault(p => p.Id == id && p.Category.Name == "Combos");
            if (combo == null)
                return NotFound();
            return View(combo);
        }
    }
} 