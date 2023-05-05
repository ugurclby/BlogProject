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
using Blog.Model.Entities.Enums;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Appuser> _userManager;

        public ArticleController(IMapper mapper, UserManager<Appuser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> ArticleDetail(int id, string sayfa="guest")
        {
            Appuser appuser = await _userManager.GetUserAsync(User);
            ViewBag.ArticleId = id;
            ViewBag.Sayfa = sayfa;
            ViewBag.SessionUserID = null;
            return View();
        }
    }
}
