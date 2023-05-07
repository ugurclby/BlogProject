using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AppUserController : Controller
    { 
        private readonly UserManager<Appuser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly SignInManager<Appuser> _signInManager;

        public AppUserController(SignInManager<Appuser> signInManager, UserManager<Appuser> userManager, IMapper mapper, IAppUserRepository appUserRepository, ICategoryRepository categoryRepository, IArticleRepository articleRepository)
        { 
            _userManager = userManager;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            ViewBag.OnayBekleyenKullanici= _appUserRepository.OnayBekleyenKullaniciListesi().Count();
            ViewBag.OnayBekleyenKategori = _categoryRepository.GetDefaults(category => category.Statu == Statu.Confirmation).Count();
            ViewBag.OnayBekleyenMakale = _articleRepository.GetDefaults(article => article.Statu == Statu.Confirmation).Count();

            return View();
        }

        public IActionResult ConfirmationUserList()
        {
            var appUserList= _appUserRepository.OnayBekleyenKullaniciListesi();
            var getConfirmationList= _mapper.Map<List<GetConfirmationUserListDTO>>(appUserList);
            return View(getConfirmationList);
        }
         
        /// <summary>
        /// Kullanıcı onaylanan metod
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserApproval(string id)
        {
            // kullanıcı kontrolü yapılır. Eğer kullanıcı varsa statü kolonu aktif edilir.
            var appUser= await _userManager.FindByIdAsync(id);
            if (appUser!=null)
            {
                appUser.Statu = Statu.Active;
                _appUserRepository.UpdateApproval(appUser); 
            }
            return RedirectToAction("ConfirmationUserList");
        }
        public async Task<IActionResult> RejectionUser(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                appUser.Statu = Statu.Rejection;
                _appUserRepository.UpdateApproval(appUser);
            }
            return Json(null);
        } 
        public async Task<IActionResult> Update()
        {
            Appuser appuser = await _userManager.GetUserAsync(User); 
            var register = _mapper.Map<AdminRegisterUpdateDTO>(appuser);
            return View(register);
        }
         
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");      // redirectoaction("index","home");
        }

        public async Task<IActionResult> UserList()
        {
            var memberList = new List<GetMemberAppUserListDTO>();
            var list = await _userManager.GetUsersInRoleAsync("member"); 
            if (list !=null)
            {
                memberList = _mapper.Map<List<GetMemberAppUserListDTO>>(list);
            }
            
            return View(memberList);
        }

        public async Task<IActionResult> UserActive(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                appUser.Statu = Statu.Active;
                _appUserRepository.UpdateApproval(appUser);
            }
            return RedirectToAction("UserList");
        }
        public async Task<IActionResult> UserPassive(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                appUser.Statu = Statu.Passive;
                _appUserRepository.UpdateApproval(appUser);
            }
            return RedirectToAction("UserList");
        }
    }
}
