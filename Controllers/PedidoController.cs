using Microsoft.AspNetCore.Mvc;
using TotemPWA.ViewModels;

namespace TotemPWA.Controllers
{
    public class PedidoController : Controller
    {
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
    }
} 