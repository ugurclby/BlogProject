using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AppUserController : Controller
    {  
        public IActionResult UserDetail(string appUserId)
        {
            ViewBag.AppUserId = appUserId;
            return View();
        }
    }
}
