using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;
using System.Linq;

namespace TotemPWA.Controllers
{
    public class CupomController : Controller
    {
        private readonly AppDbContext _context;
        public CupomController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cupons = _context.Cupoms.ToList();
            return View(cupons);
        }

        public IActionResult Detalhe(int id)
        {
            var cupom = _context.Cupoms.FirstOrDefault(c => c.Id == id);
            if (cupom == null)
                return NotFound();
            return View(cupom);
        }
    }
} 