using Microsoft.AspNetCore.Mvc;

namespace TotemPWA.Controllers.Admin
{
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 