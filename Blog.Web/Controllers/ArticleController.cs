using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class ArticleController : Controller
    { 
        public IActionResult ArticleDetail(int id, string sayfa = "guest")
        {
            ViewBag.ArticleId = id;
            ViewBag.Sayfa = sayfa;
            ViewBag.SessionUserID = null;
            return View();
        }
    }
}
