using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class AppUserController : Controller
    {
        private readonly SignInManager<Appuser> _signInManager;

        public AppUserController(SignInManager<Appuser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
           await _signInManager.SignOutAsync();
            return Redirect("~/");      // redirectoaction("index","home");
        }




    }
}
