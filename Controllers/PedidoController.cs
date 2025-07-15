using Microsoft.AspNetCore.Mvc;
using TotemPWA.ViewModels;
using TotemPWA.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
                // Buscar ou criar cliente
                var cliente = _context.Clients.FirstOrDefault(c => c.Name == model.NomeCliente);
                if (cliente == null)
                {
                    cliente = new Models.Client { Name = model.NomeCliente, CPF = "" };
                    _context.Clients.Add(cliente);
                    _context.SaveChanges();
                }

                // Criar pedido
                var pedido = new Models.Order
                {
                    ClientId = cliente.Id,
                    Date = DateTime.Now,
                    Status = "Em andamento",
                    TotalPrice = 0,
                    OrderItems = new List<Models.OrderItem>()
                };

                // Adicionar itens ao pedido
                foreach (var produtoId in model.ItensSelecionados)
                {
                    var produto = _context.Products.FirstOrDefault(p => p.Id == produtoId);
                    if (produto != null)
                    {
                        pedido.OrderItems.Add(new Models.OrderItem
                        {
                            ProductId = produto.Id,
                            Quantity = model.Quantidade > 0 ? model.Quantidade : 1,
                            UnitPrice = produto.Price
                        });
                        pedido.TotalPrice += produto.Price * (model.Quantidade > 0 ? model.Quantidade : 1);
                    }
                }
                _context.Orders.Add(pedido);
                _context.SaveChanges();

                // Criar pagamento
                var pagamento = new Models.Payment
                {
                    OrderId = pedido.Id,
                    Type = model.MetodoPagamento,
                    Value = pedido.TotalPrice,
                    Status = "Pendente"
                };
                _context.Payments.Add(pagamento);
                _context.SaveChanges();

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

        [HttpGet]
        public IActionResult Pagamento()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Pix()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dinheiro()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CartaoTipo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Concluido()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Negado()
        {
            return View();
        }

        // Action para tela de revisão do pedido
        public IActionResult Revisao(int id)
        {
            var pedido = _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Client)
                .Include(o => o.Payments)
                .Select(o => new {
                    o.Id,
                    o.TotalPrice,
                    o.Status,
                    Cliente = o.Client.Name,
                    Itens = o.OrderItems.Select(oi => new {
                        oi.Product.Name,
                        oi.Product.Image,
                        oi.Product.Price,
                        oi.Quantity,
                        oi.ProductId
                    }).ToList(),
                    Pagamentos = o.Payments.Select(p => new { p.Type, p.Value, p.Status }).ToList()
                }).FirstOrDefault();
            if (pedido == null) return NotFound();
            ViewBag.Total = pedido.TotalPrice;
            ViewBag.Status = pedido.Status;
            ViewBag.Cliente = pedido.Cliente;
            ViewBag.Pagamentos = pedido.Pagamentos;
            return View(pedido.Itens);
        }
    }
} 