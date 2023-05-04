using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Web.Areas.Member.Models;
using AutoMapper;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using Blog.Dal.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Dal.Repositories.Concrete;
using Blog.Web.Models.VMs;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository; 
        private readonly IMapper _mapper; 

        public ArticleController(UserManager<Appuser> userManager, IArticleRepository articleRepository, ICategoryRepository categoryRepository, IMapper mapper, IArticleCategoryRepository articleCategoryRepository,  ICommentRepository commentRepository,ILikeRepository likeRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _articleCategoryRepository = articleCategoryRepository;
            _commentRepository = commentRepository;
            _likeRepository= likeRepository;
        }
        public IActionResult ArticleDetail(int id, string sayfa)
        {
            ViewBag.ArticleId = id;
            ViewBag.Sayfa = sayfa;
            return View();
        }
        public IActionResult AddToComment(CreateCommentVM createCommentVm)
        {
            _commentRepository.Create(_mapper.Map<Comment>(createCommentVm));
            return RedirectToAction("ArticleDetail", new { id = createCommentVm.ArticleID, sayfa = "user" });
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

        public async Task<IActionResult> Create()
        {
            Appuser appuser = await _userManager.GetUserAsync(User);

            CreateArticleVM vm = new CreateArticleVM()
            {
                AppUserID = appuser.Id,
                Categories = _categoryRepository.GetByDefaults
                (
                    selector: a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() },
                    expression: a => a.Statu == Statu.Active || a.Statu == Statu.Modified
                )
            };
            return View(vm);
        }


        [HttpPost]
        public IActionResult Create(CreateArticleVM vm)
        {
            if (ModelState.IsValid)
            {
                Article article = _mapper.Map<Article>(vm);
                List<ArticleCategory> articleCategories = new List<ArticleCategory>();

                using var image = Image.Load(vm.Image.OpenReadStream());

                image.Mutate(a => a.Resize(80, 80));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                article.ImagePath = $"/images/{guid}.jpg";

                article.ReadTime = (vm.Content.Length / 100) * 2;

                if (vm.CategoryID.Length > 0)
                {
                    foreach (var categoryId in vm.CategoryID)
                    {
                        articleCategories.Add(new ArticleCategory() { CategoryID = categoryId, ArticleID = vm.ID });
                    }
                    article.ArticleCategories = articleCategories;
                }
                _articleRepository.Create(article);
                return RedirectToAction("List");
            }

            // toDo:vm nesnesi üzerinde doldurulması greken alanlar var. aksi takdirde category seçimi yapılamaz. +

            vm.Categories = _categoryRepository.GetByDefaults
            (
                selector: a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() },
                expression: a => a.Statu == Statu.Active || a.Statu == Statu.Modified
            );

            return View(vm);
        }

        public async Task<IActionResult> List()
        {
            Appuser appuser = await _userManager.GetUserAsync(User);

            var list = _articleRepository.GetByDefaults
                (
                    selector: a => new GetArticleDTO() { ArticleID = a.ID, ImagePath = a.ImagePath, Title = a.Title },
                    expression: a => a.Statu != Statu.Passive && a.AppUserID == appuser.Id,
                    include: a => a.Include(a => a.ArticleCategories)

                );
            return View(list);
        }


        public IActionResult Update(int id)
        {
            Article article = _articleRepository.GetByDefault(article1 => article1, article1 => article1.ID == id, article1 => article1.Include(a => a.ArticleCategories));
            List<int> selectedCategory = new List<int>();

            var updatedArticle = _mapper.Map<UpdateArticleVM>(article);
            article.ArticleCategories.ForEach(c => selectedCategory.Add(c.CategoryID));

            updatedArticle.Categories = _categoryRepository.GetByDefaults(
                    selector: a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() },
                    expression: a => a.Statu == Statu.Active || a.Statu == Statu.Modified);

            updatedArticle.CategoryID = selectedCategory.ToArray();

            return View(updatedArticle);

        }
        [HttpPost]
        public IActionResult Update(UpdateArticleVM vm)
        {
            if (ModelState.IsValid)
            {
                var articlefromDb = _articleRepository.GetDefault(x => x.ID == vm.ID);

                var articlefromDatabase = _articleRepository.GetByDefault(article1 => article1, article1 => article1.ID == vm.ID, article1 => article1.Include(a => a.ArticleCategories));

                var existingIds = articlefromDatabase.ArticleCategories.Select(x => x.CategoryID).ToList();

                var selectedIds = vm.CategoryID.ToList();

                var toAdd=selectedIds.Except(existingIds).ToList();

                var toRemove=existingIds.Except(selectedIds).ToList();

                articlefromDb.ArticleCategories=articlefromDatabase.ArticleCategories.Where(x=>!toRemove.Contains(x.CategoryID)).ToList();

                foreach (var item in toAdd)
                {
                    articlefromDb.ArticleCategories.Add(new ArticleCategory(){ArticleID = vm.ID,CategoryID = item});
                } 

                if (vm.Image != null)
                {
                    using var image = Image.Load(vm.Image.OpenReadStream());
                    image.Mutate(a => a.Resize(80, 80));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpeg");

                    articlefromDb.ImagePath = $"/images/{guid}.jpeg";

                    System.IO.File.Delete($"wwwroot/{vm.ImagePath}");

                }

                articlefromDb.Title = vm.Title;
                articlefromDb.Content = vm.Content;
                articlefromDb.ImagePath = vm.ImagePath;
                articlefromDb.ReadTime = (vm.Content.Length / 100) * 2;
                _articleRepository.Update(articlefromDb);
               
                return RedirectToAction("List");

            }

            // todo: categories listesi null geleceği için category nesnelerini viewa taşıyaıyoruz ?? +
            // todo : makale fotoğrafı güncellenmezse ? +
            // todo: makale fotoğrafı güncellenirse wwwroot alıntında bu makaleya ait fotoğrafı silsek ve yerine bu güncel fotoğrafı yerleştirmiş olalım ki images klasörü de gittikçe kalabalıklaşmasın. -

            vm.Categories = _categoryRepository.GetByDefaults(
                selector: a => new SelectListItem() { Text = a.Name, Value = a.ID.ToString() },
                expression: a => a.Statu != Statu.Passive);

            return View(vm);
        }
        public IActionResult Delete(int id)
        {
            Article article = _articleRepository.GetByDefault(a => a, a => a.ID == id, queryable => queryable.Include(x => x.ArticleCategories));

            if (article != null)
            { 
                _articleRepository.Delete(article);
            }
            return Json(new
            {
                message = ""
            });
        } 
    }
}
