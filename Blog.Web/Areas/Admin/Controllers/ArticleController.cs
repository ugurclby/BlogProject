using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Blog.Model.Entities.Concrete;
using Blog.Web.Models.VMs;
using Blog.Dal.Repositories.Concrete;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ArticleController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;


        public ArticleController(IMapper mapper, IArticleRepository articleRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
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
        public IActionResult AddToComment(CreateCommentVM createCommentVm)
        {
            _commentRepository.Create(_mapper.Map<Comment>(createCommentVm));
            return RedirectToAction("ArticleDetail",new{id=createCommentVm.ArticleID,sayfa="admin"});
        }
        public IActionResult AddToLike(CreateLikeVM createLikeVm)
        {
            if (createLikeVm.Type.Equals("Vazgeç"))
            {
                _likeRepository.Delete(_mapper.Map<Like>(createLikeVm));
                return RedirectToAction("ArticleDetail", new { id = createLikeVm.ArticleID, sayfa = "admin" });
            }
            
            _likeRepository.Create(_mapper.Map<Like>(createLikeVm));
 
            return RedirectToAction("ArticleDetail", new { id = createLikeVm.ArticleID, sayfa = "admin" });
        }
        

    }
}
