using AutoMapper;
using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Web.Models.DTOs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Dal.Repositories.Concrete;
using Blog.Web.Areas.Member.Models;
using System.Linq;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class AppUserController : Controller
    {
        private readonly SignInManager<Appuser> _signInManager;
        private readonly UserManager<Appuser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        public AppUserController(SignInManager<Appuser> signInManager, UserManager<Appuser> userManager, IMapper mapper, IAppUserRepository appUserRepository )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
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

        public async Task<IActionResult> Update()
        {
            Appuser appuser = await _userManager.GetUserAsync(User);
            var register= _mapper.Map<RegisterUpdateDTO>(appuser); 
            return View(register);
        }
        [HttpPost]
        public IActionResult Update(RegisterUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {

                var updateUser = _userManager.Users.FirstOrDefault(I => I.Id == dto.Id);
                if ( dto.Image!=null)
                {
                    using var image = Image.Load(dto.Image.OpenReadStream());
                    image.Mutate(a => a.Resize(80, 80));
                    image.Save($"wwwroot/images/{dto.UserName}.jpg");
                    updateUser.ImagePath = $"/images/{dto.UserName}.jpg";
                }

                updateUser.FirstName = dto.FirstName;
                updateUser.LastName = dto.LastName;
                updateUser.UserName = dto.UserName;
                updateUser.Password = dto.Password;
                updateUser.Email = dto.Email; 

                _appUserRepository.Update(updateUser);

                ViewBag.Message = "Kayıt Başarılı Bir Şekilde Güncellendi";
            }
            return View(dto);
        }

    }
}
