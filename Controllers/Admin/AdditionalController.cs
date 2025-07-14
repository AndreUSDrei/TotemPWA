using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TotemPWA.Controllers.Admin
{
    public class AdditionalController : AdminBaseController
    {
        private readonly AppDbContext _context;
        public AdditionalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Additional
        public async Task<IActionResult> Index()
        {
            var additionals = await _context.Additionals
                .Include(a => a.Product)
                .Include(a => a.Ingredient)
                .ToListAsync();
            return View(additionals);
        }

        // GET: Admin/Additional/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var additional = await _context.Additionals
                .Include(a => a.Product)
                .Include(a => a.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additional == null) return NotFound();
            return View(additional);
        }

        // GET: Admin/Additional/Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View();
        }

        // POST: Admin/Additional/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,IngredientId")] Additional additional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View(additional);
        }

        // GET: Admin/Additional/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var additional = await _context.Additionals.FindAsync(id);
            if (additional == null) return NotFound();
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View(additional);
        }

        // POST: Admin/Additional/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,IngredientId")] Additional additional)
        {
            if (id != additional.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Additionals.Any(e => e.Id == additional.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View(additional);
        }

        // GET: Admin/Additional/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var additional = await _context.Additionals
                .Include(a => a.Product)
                .Include(a => a.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additional == null) return NotFound();
            return View(additional);
        }

        // POST: Admin/Additional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additional = await _context.Additionals.FindAsync(id);
            if (additional != null)
            {
                _context.Additionals.Remove(additional);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 