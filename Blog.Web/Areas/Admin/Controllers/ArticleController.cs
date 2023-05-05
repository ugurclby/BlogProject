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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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
        private readonly UserManager<Appuser> _userManager;

        public ArticleController(IMapper mapper, IArticleRepository articleRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, UserManager<Appuser> userManager)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _userManager = userManager;
        }

        public IActionResult ArticleList()
        {
            var articleList = _articleRepository.GetByDefaults(article => article, x => x.ID > 0, queryable => queryable.Include(article => article.Appuser).Include(article => article.Likes).Include(article => article.ArticleCategories).Include(article => article.Comments));
            return View(articleList);
        }
        public IActionResult ArticleActive(int id)
        {
            var article = _articleRepository.GetDefault(x => x.ID == id);
            if (article != null)
            {
                article.Statu = Statu.Active;
                _articleRepository.UpdateApproval(article);
            }
            return RedirectToAction("ArticleList");
        }
        public  IActionResult ArticlePassive(int id)
        {
            var article = _articleRepository.GetDefault(x => x.ID == id);
            if (article != null)
            {
                article.Statu = Statu.Passive;
                _articleRepository.UpdateApproval(article);
            }
            return RedirectToAction("ArticleList");
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
        public async Task<IActionResult> ArticleDetail(int id,string sayfa= "user")
        {
            Appuser appuser = await _userManager.GetUserAsync(User);
            ViewBag.ArticleId = id;
            ViewBag.Sayfa = sayfa;
            ViewBag.SessionUserID = appuser.Id;
            return View();
        }
        public IActionResult AddToComment(CreateCommentVM createCommentVm)
        {
            _commentRepository.Create(_mapper.Map<Comment>(createCommentVm));
            return RedirectToAction("ArticleDetail",new{id=createCommentVm.ArticleID,sayfa="user"});
        }
        public IActionResult AddToLike(CreateLikeVM createLikeVm)
        {
            if (createLikeVm.Type.Equals("cancel"))
            {
                _likeRepository.Delete(_mapper.Map<Like>(createLikeVm));
                return RedirectToAction("ArticleDetail", new { id = createLikeVm.ArticleID, sayfa = "user" });
            }
            
            _likeRepository.Create(_mapper.Map<Like>(createLikeVm));
 
            return RedirectToAction("ArticleDetail", new { id = createLikeVm.ArticleID, sayfa = "user" });
        }
        

    }
}
