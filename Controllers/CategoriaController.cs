using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TotemPWA.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // Exemplo: /categoria/lanches
        [Route("categoria/{slug}")]
        public IActionResult Detalhe(string slug)
        {
            var categoria = _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Slug == slug);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }
    }
} 