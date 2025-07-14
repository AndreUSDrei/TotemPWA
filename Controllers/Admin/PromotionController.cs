using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TotemPWA.Controllers.Admin
{
    public class PromotionController : AdminBaseController
    {
        private readonly AppDbContext _context;
        public PromotionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Promotion
        public async Task<IActionResult> Index()
        {
            var promotions = await _context.Promotions.Include(p => p.Product).ToListAsync();
            return View(promotions);
        }

        // GET: Admin/Promotion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var promotion = await _context.Promotions.Include(p => p.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (promotion == null) return NotFound();
            return View(promotion);
        }

        // GET: Admin/Promotion/Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Admin/Promotion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Percent,ValidUntil")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            return View(promotion);
        }

        // GET: Admin/Promotion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion == null) return NotFound();
            ViewBag.Products = _context.Products.ToList();
            return View(promotion);
        }

        // POST: Admin/Promotion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Percent,ValidUntil")] Promotion promotion)
        {
            if (id != promotion.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Promotions.Any(e => e.Id == promotion.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            return View(promotion);
        }

        // GET: Admin/Promotion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var promotion = await _context.Promotions.Include(p => p.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (promotion == null) return NotFound();
            return View(promotion);
        }

        // POST: Admin/Promotion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 