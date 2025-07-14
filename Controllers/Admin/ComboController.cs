using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace TotemPWA.Controllers.Admin
{
    public class ComboController : AdminBaseController
    {
        private readonly AppDbContext _context;
        public ComboController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Combo
        public async Task<IActionResult> Index()
        {
            var combos = await _context.Combos
                .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Product)
                .ToListAsync();
            return View(combos);
        }

        // GET: Admin/Combo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (combo == null) return NotFound();
            return View(combo);
        }

        // GET: Admin/Combo/Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Admin/Combo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int[] selectedProducts)
        {
            if (selectedProducts != null && selectedProducts.Length > 0)
            {
                var combo = new Combo();
                _context.Combos.Add(combo);
                await _context.SaveChangesAsync();
                foreach (var productId in selectedProducts)
                {
                    _context.ComboProducts.Add(new ComboProduct { Combo = combo, ProductId = productId });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            ModelState.AddModelError("", "Selecione ao menos um produto para o combo.");
            return View();
        }

        // GET: Admin/Combo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (combo == null) return NotFound();
            ViewBag.Products = _context.Products.ToList();
            ViewBag.SelectedProducts = combo.ComboProducts?.Select(cp => cp.ProductId).ToArray() ?? new int[0];
            return View(combo);
        }

        // POST: Admin/Combo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] selectedProducts)
        {
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (combo == null) return NotFound();
            // Atualiza os produtos do combo
            var currentProducts = combo.ComboProducts?.Select(cp => cp.ProductId).ToList() ?? new List<int>();
            var toRemove = combo.ComboProducts?.Where(cp => !selectedProducts.Contains(cp.ProductId)).ToList() ?? new List<ComboProduct>();
            foreach (var item in toRemove)
                _context.ComboProducts.Remove(item);
            var toAdd = selectedProducts.Except(currentProducts);
            foreach (var productId in toAdd)
                _context.ComboProducts.Add(new ComboProduct { Combo = combo, ProductId = productId });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Combo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (combo == null) return NotFound();
            return View(combo);
        }

        // POST: Admin/Combo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var combo = await _context.Combos
                .Include(c => c.ComboProducts)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (combo != null)
            {
                if (combo.ComboProducts != null)
                    _context.ComboProducts.RemoveRange(combo.ComboProducts);
                _context.Combos.Remove(combo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 