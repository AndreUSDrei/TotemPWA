using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using System.Linq;

namespace TotemPWA.Controllers
{
    public class PromotionController : Controller
    {
        private readonly AppDbContext _context;
        public PromotionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var promotions = _context.Promotions
                .Include(p => p.Product)
                .ToList();
            return View(promotions);
        }

        public IActionResult Detalhe(int id)
        {
            var promotion = _context.Promotions
                .Include(p => p.Product)
                .FirstOrDefault(p => p.Id == id);
            if (promotion == null)
                return NotFound();
            return View(promotion);
        }
    }
} 