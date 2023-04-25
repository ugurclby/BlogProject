using System;
using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Web.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly IUsedPasswordRepository _usedPasswordRepository;

        public AccountController(IMapper mapper, IAppUserRepository appUserRepository, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, IUsedPasswordRepository usedPasswordRepository)
        {
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _usedPasswordRepository = usedPasswordRepository;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (ModelState.IsValid)
            {
                Appuser appuser = _mapper.Map<Appuser>(dto);

                using var image = Image.Load(dto.Image.OpenReadStream());
                image.Mutate(a => a.Resize(80, 80));
                image.Save($"wwwroot/images/{appuser.UserName}.jpg");

                appuser.ImagePath = $"/images/{appuser.UserName}.jpg";

                var errors = await _appUserRepository.Create(appuser);
                
                if (errors.Count > 0)
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(dto);
                }
                _usedPasswordRepository.Create(new UsedPassword()
                {
                    AppUserID = appuser.Id,
                    PasswordHash = appuser.PasswordHash
                });
                 
                return RedirectToAction("Login");

            }
            return View(dto);
        }

        public IActionResult LogIn(string returnUrl)   // returnUrl : giriş yamadan önce gitmek istediği adres
        {
            return View(new LoginDTO() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Appuser appuser = await _userManager.FindByEmailAsync(dto.Email);
                if (appuser != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(appuser.UserName, dto.Password, false, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(appuser);

                        if (roles.Contains("admin"))
                        {
                            return Redirect(dto.ReturnUrl ?? "/admin/appuser/index");
                        }
                        else
                        {
                            return Redirect(dto.ReturnUrl ?? "/member/appuser/index");
                        }
                    } 
                    ModelState.AddModelError("", "Kullanıcı Adı Ve Şifre Hatalı.");
                }
                else
                {
                    ModelState.AddModelError("", "Girmiş olduğunuz mail adresi bulunamadı");
                }
            }
            return View(dto);
        }
    }
}
