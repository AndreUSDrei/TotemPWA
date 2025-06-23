using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TotemPWA.Models;
using TotemPWA.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TotemPWA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
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
        var produtos = _context.Products.ToList();
        var categorias = _context.Categories.ToList();
        ViewBag.Categorias = categorias;
        return View(produtos);
    }

    public IActionResult Sobremesas()
    {
        return View();
    }

    public IActionResult TelaPagamento()
    {
        var carrinhoJson = HttpContext.Session.GetString("Carrinho");
        var itens = string.IsNullOrEmpty(carrinhoJson) ? new List<TotemPWA.ViewModels.PedidoViewModel>() : JsonSerializer.Deserialize<List<TotemPWA.ViewModels.PedidoViewModel>>(carrinhoJson);
        return View(itens);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
