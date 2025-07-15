using Microsoft.AspNetCore.Mvc;
using TotemPWA.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        [HttpGet("api/produto/{id}/ingredientes")]
        public IActionResult GetIngredientes(int id)
        {
            var produto = _context.Products
                .Include(p => p.Additionals)
                    .ThenInclude(a => a.Ingredient)
                .FirstOrDefault(p => p.Id == id);
            if (produto == null)
                return NotFound();

            var ingredientes = (produto.Additionals ?? new List<TotemPWA.Models.Additional>())
                .Select(a => new {
                    Id = a.IngredientId,
                    Nome = a.Ingredient != null ? a.Ingredient.Name : string.Empty,
                    Preco = a.Ingredient != null ? a.Ingredient.Price : 0
                }).ToList();

            return Json(ingredientes);
        }
    }
} 