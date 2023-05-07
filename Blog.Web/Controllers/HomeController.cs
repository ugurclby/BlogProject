using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Web.Models;
using Blog.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IAboutRepository _aboutRepository;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IAboutRepository aboutRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _aboutRepository = aboutRepository;
        }
        
        public IActionResult Index(int? categoryId)
        { 
            ViewBag.CategoryId = 0;
            if (categoryId.HasValue)
            {
                ViewBag.CategoryId = categoryId;
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult About(AboutDTO aboutDto)
        {
            //iletişim formu kayıt süreci
            // İletişim formları da statüsü 0 olarak atılır. Çünkü admin sayfasına düşürülür. 
            //Admin ilgili kaydı okuduktan sonra aktife çekilir ve admin sayfasından geri alınır.
            if (ModelState.IsValid)
            {
                _aboutRepository.Create(_mapper.Map<About>(aboutDto));
                ModelState.Clear();
                ViewBag.Message = "Mesajınız Başarılı Bir Şekilde Gönderildi";
                return View();
            } 

            return View(aboutDto);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
