using Microsoft.AspNetCore.Mvc;
using TotemPWA.ViewModels;
using TotemPWA.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TotemPWA.Controllers
{
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;
        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Novo(PedidoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode salvar o pedido no banco ou processar como desejar
                TempData["MensagemSucesso"] = "Pedido realizado com sucesso!";
                return RedirectToAction("Novo");
            }
            // Se houver erro de validação, retorna a view com os erros
            return View(model);
        }

        [HttpPost]
        public IActionResult Revisao([FromBody] List<PedidoViewModel> itens)
        {
            HttpContext.Session.SetString("Carrinho", JsonSerializer.Serialize(itens));
            return Ok(new { redirectUrl = Url.Action("Revisao") });
        }

        [HttpGet]
        public IActionResult Revisao()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            var itens = string.IsNullOrEmpty(carrinhoJson) ? new List<PedidoViewModel>() : JsonSerializer.Deserialize<List<PedidoViewModel>>(carrinhoJson);
            return View(itens);
        }

        // Action para tela de revisão do pedido
        public IActionResult Revisao(int id = 1) // Exemplo: pedido em andamento com Id=1
        {
            var pedido = _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new {
                    o.Id,
                    o.TotalPrice,
                    Itens = o.OrderItems.Select(oi => new {
                        oi.Product.Name,
                        oi.Product.Image,
                        oi.Product.Price,
                        oi.Quantity,
                        oi.Product.Slug,
                        oi.ProductId
                    }).ToList()
                }).FirstOrDefault();
            if (pedido == null) return NotFound();
            ViewBag.Total = pedido.TotalPrice;
            return View(pedido.Itens);
        }
    }
} 