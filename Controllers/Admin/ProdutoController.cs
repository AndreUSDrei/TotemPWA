using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;
using TotemPWA.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TotemPWA.Controllers.Admin
{
    public class ProdutoController : AdminBaseController
    {
        private readonly AppDbContext _context;
        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Produto
        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(produtos);
        }

        // GET: Admin/Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // GET: Admin/Produto/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categories.ToList();
            return View();
        }

        // POST: Admin/Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,CategoryId")] Product produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _context.Categories.ToList();
            return View(produto);
        }

        // GET: Admin/Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.Products.FindAsync(id);
            if (produto == null) return NotFound();
            ViewBag.Categorias = _context.Categories.ToList();
            return View(produto);
        }

        // POST: Admin/Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] Product produto)
        {
            if (id != produto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == produto.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _context.Categories.ToList();
            return View(produto);
        }

        // GET: Admin/Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // POST: Admin/Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Products.FindAsync(id);
            if (produto != null)
            {
                _context.Products.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 