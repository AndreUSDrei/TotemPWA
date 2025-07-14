using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TotemPWA.Controllers.Admin
{
    public class AdminBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAdmin = context.HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
            base.OnActionExecuting(context);
        }
    }
} 