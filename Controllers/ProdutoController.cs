using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TotemPWA.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        
        [Route("produto/{id:int}")]
        public IActionResult Detalhe(int id)
        {
            var produto = _context.Products
                .Include(p => p.Additionals)
                    .ThenInclude(a => a.Ingredient)
                .Include(p => p.Category)
                .Include(p => p.Promotions)
                .FirstOrDefault(p => p.Id == id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }
    }
} 