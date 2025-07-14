using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TotemPWA.Controllers.Admin
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "Usuário ou senha inválidos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }
    }
} 