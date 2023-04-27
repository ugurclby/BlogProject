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
using Microsoft.EntityFrameworkCore;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ArticleController(UserManager<Appuser> userManager,IArticleRepository articleRepository,ICategoryRepository categoryRepository,IMapper mapper)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
           _mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            Appuser appuser = await _userManager.GetUserAsync(User);

            CreateArticleVM vm = new CreateArticleVM()
            {
                AppUserID = appuser.Id,
                Categories = _categoryRepository.GetByDefaults
                (
                    selector: a=> new GetCategoryDTO() { ID=a.ID,Name=a.Name},
                    expression: a=>a.Statu==Statu.Active || a.Statu == Statu.Modified
                )
            };
            return View(vm);
        }


        [HttpPost]
        public IActionResult Create(CreateArticleVM vm)
        {
            if(ModelState.IsValid)
            {
                 Article article=_mapper.Map<Article>(vm);

                using var image = Image.Load(vm.Image.OpenReadStream()); 

                image.Mutate(a => a.Resize(80, 80));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                article.ImagePath = $"/images/{guid}.jpg";

                _articleRepository.Create(article);
                return RedirectToAction("List");
            }

            // toDo:vm nesnesi üzerinde doldurulması greken alanlar var. aksi takdirde category seçimi yapılamaz.
            
            vm.Categories = _categoryRepository.GetByDefaults
            (
                selector: a => new GetCategoryDTO() {ID = a.ID, Name = a.Name},
                expression: a => a.Statu != Statu.Passive
            );

            return View(vm);
        }

        public async Task<IActionResult> List()
        {
            Appuser appuser =await  _userManager.GetUserAsync(User);

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
            Article article = _articleRepository.GetDefault(a => a.ID == id);

            var updatedArticle = _mapper.Map<UpdateArticleVM>(article);
            
            updatedArticle.Categories = _categoryRepository.GetByDefaults
                (selector: a=> new GetCategoryDTO() { ID=a.ID, Name=a.Name},
                expression: a=>a.Statu!=Statu.Passive);
            return View(updatedArticle);
        }


        [HttpPost]
        public IActionResult Update(UpdateArticleVM vm)
        {
            if(ModelState.IsValid)
            {
                var article = _mapper.Map<Article>(vm);

                if (vm.Image != null)
                {
                    using var image = Image.Load(vm.Image.OpenReadStream());
                    image.Mutate(a => a.Resize(80, 80));

                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpeg");

                    article.ImagePath = $"/images/{guid}.jpeg";

                    System.IO.File.Delete($"wwwroot/{vm.ImagePath}");

                }  

                _articleRepository.Update(article);
                return RedirectToAction("List");

            }

            // todo: categories listesi null geleceği için category nesnelerini viewa taşıyaıyoruz ??
            // todo : makale fotoğrafı güncellenmezse ?
            // todo: makale fotoğrafı güncellenirse wwwroot alıntında bu makaleya ait fotoğrafı silsek ve yerine bu güncel fotoğrafı yerleştirmiş olalım ki images klasörü de gittikçe kalabalıklaşmasın.
            
            vm.Categories = _categoryRepository.GetByDefaults
            (selector: a => new GetCategoryDTO() { ID = a.ID, Name = a.Name },
                expression: a => a.Statu != Statu.Passive);

            return View(vm);
        }










    }
}
