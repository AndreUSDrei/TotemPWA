using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TotemPWA.Models;
using Microsoft.EntityFrameworkCore;

namespace TotemPWA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Data.AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, Data.AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ComerOuLevar()
    {
        return View();
    }

    public IActionResult Tela2()
    {
        var categorias = _context.Categories.ToList();
        var produtos = _context.Products
            .Include(p => p.Additionals)
                .ThenInclude(a => a.Ingredient)
            .Include(p => p.Category)
            .Include(p => p.Promotions)
            .Include(p => p.ComboProducts)
            .ToList();
        var ingredientes = _context.Ingredients.ToList();
        
        // Debug: verificar se os dados estão sendo carregados
        Console.WriteLine($"Categorias carregadas: {categorias.Count}");
        Console.WriteLine($"Produtos carregados: {produtos.Count}");
        Console.WriteLine($"Produtos com categoria: {produtos.Count(p => p.Category != null)}");
        
        ViewBag.Categorias = categorias;
        ViewBag.Produtos = produtos;
        ViewBag.Ingredientes = ingredientes;
        return View();
    }

    public IActionResult Combos()
    {
        var categorias = _context.Categories.ToList();
        var produtos = _context.Products
            .Include(p => p.Additionals)
                .ThenInclude(a => a.Ingredient)
            .Include(p => p.Category)
            .Include(p => p.Promotions)
            .Include(p => p.ComboProducts)
            .ToList();
        var ingredientes = _context.Ingredients.ToList();
        ViewBag.Categorias = categorias;
        ViewBag.Produtos = produtos;
        ViewBag.Ingredientes = ingredientes;
        return View();
    }

    public IActionResult Sobremesas()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
