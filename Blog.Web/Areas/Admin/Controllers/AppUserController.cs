using System.Collections.Generic;
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
using Blog.Web.Areas.Admin.Models;
using System.Linq;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.IO;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AppUserController : Controller
    {
  
        private readonly UserManager<Appuser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository; 
        public AppUserController( UserManager<Appuser> userManager, IMapper mapper, IAppUserRepository appUserRepository )
        { 
            _userManager = userManager;
            _mapper = mapper;
            _appUserRepository = appUserRepository; 
        }
        public IActionResult Index()
        {
            ViewBag.Onay_Bekleyen_Kullanici= _appUserRepository.OnayBekleyenKullaniciListesi().Count();

            return View();
        }

        public IActionResult ConfirmationUserList()
        {
            var appUserList= _appUserRepository.OnayBekleyenKullaniciListesi();
            var getConfirmationList= _mapper.Map<List<GetConfirmationUserListDTO>>(appUserList);
            return View(getConfirmationList);
        }
         
        public async Task<IActionResult> UserApproval(string id)
        {
            var appUser= await _userManager.FindByIdAsync(id);
            if (appUser!=null)
            {
                appUser.Statu = Statu.Active;
                _appUserRepository.Update(appUser); 
            }
            return RedirectToAction("ConfirmationUserList");
        }
        public async Task<IActionResult> RejectionUser(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser != null)
            {
                appUser.Statu = Statu.Rejection;
                _appUserRepository.Update(appUser);
            }
            return Json(null);
        }
    }
}
