using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ArticleController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository; 

        public ArticleController(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;  
        }
        public IActionResult ConfirmationArticleList()
        {
            List<GetConfirmationArticleListDTO> articleListDtos= new List<GetConfirmationArticleListDTO>();

            var articles= _articleRepository.GetByDefaults(article => article, article => article.Statu == Statu.Confirmation,queryable => queryable.Include(x=>x.Appuser));

            foreach (var article in articles)
            {
                articleListDtos.Add(new GetConfirmationArticleListDTO()
                {
                    Appuser = article.Appuser,
                    AppUserID = article.AppUserID,
                    Id = article.ID,
                    Title = article.Title
                });
            }

            return View(articleListDtos);
        }
        public IActionResult ArticleApproval(int id)
        {
            var article= _articleRepository.GetDefault(article => article.ID == id);
            if (article != null)
            {
                article.Statu = Statu.Active;
                _articleRepository.UpdateApproval(article);
            }
            return RedirectToAction("ConfirmationArticleList");
        }
        public IActionResult RejectionArticle(int id)
        {
            var article = _articleRepository.GetDefault(article => article.ID == id);
            if (article != null)
            {
                article.Statu = Statu.Rejection;
                _articleRepository.UpdateApproval(article);
            }
            return Json(null);
        }
        public IActionResult ArticleDetail(int id,string sayfa)
        {
            ViewBag.ArticleId = id;
            ViewBag.Sayfa=sayfa;
            return View();
        }
        
    }
}
