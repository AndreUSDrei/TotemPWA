using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TotemPWA.Controllers.Admin
{
    public class IngredientController : AdminBaseController
    {
        private readonly AppDbContext _context;
        public IngredientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Ingredient
        public async Task<IActionResult> Index()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return View(ingredients);
        }

        // GET: Admin/Ingredient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        // GET: Admin/Ingredient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Limit")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Admin/Ingredient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        // POST: Admin/Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Limit")] Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ingredients.Any(e => e.Id == ingredient.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Admin/Ingredient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        // POST: Admin/Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 