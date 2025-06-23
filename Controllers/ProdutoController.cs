using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;
using System.Linq;

namespace TotemPWA.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        
        [Route("produto/{slug}")]
        public IActionResult Detalhe(string slug)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Slug == slug);
            if (produto == null)
                return NotFound();

            return View(produto);
        }
    }
} 