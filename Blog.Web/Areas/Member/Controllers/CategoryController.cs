using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Areas.Member.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "member")] 
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<Appuser> _userManager;
        private readonly IUserFollowedCategoryRepository _followedCategoryRepository;


        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository, UserManager<Appuser> userManager, IUserFollowedCategoryRepository followedCategoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _followedCategoryRepository = followedCategoryRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                Appuser appuser = await _userManager.GetUserAsync(User);
                Category category = _mapper.Map<Category>(dto);
                category.AppUserID = appuser.Id;
                _categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }

        public async Task<IActionResult> List()
        {
            Appuser appuser = await _userManager.GetUserAsync(User);
            ViewBag.UserId = appuser.Id;
            var list = _categoryRepository.GetByDefaults<Category>
            (
                selector: a => new Category()
                {
                    Description = a.Description,
                    Name = a.Name,
                    ID = a.ID,
                    Statu = a.Statu,
                    UserFollowedCategories = a.UserFollowedCategories,
                    AppUserID = a.AppUserID
                },
                expression: a => a.Statu != Statu.Passive,
                include: a => a.Include(a => a.UserFollowedCategories).Include(a => a.Appuser)
            );


            return View(list);
        }


        public IActionResult Update(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            var updatedCategory = _mapper.Map<UpdateCategoryDTO>(category);
            return View(updatedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                Appuser appuser = await _userManager.GetUserAsync(User);
                var category = _mapper.Map<Category>(dto);
                category.AppUserID = appuser.Id;
                _categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }

        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.GetByDefault(a => a, a => a.ID == id, queryable => queryable.Include(x => x.ArticleCategories));

            if (category != null)
            {
                if (category.ArticleCategories.Count > 0)
                {
                    return Json(new
                    {
                        message = "İlgili Kategorinin Bağlı Olduğu Makaleler Vardır..!"
                    });
                } 
                _categoryRepository.Delete(category);
            }
            return Json(new
            {
                message = ""
            });
        }


        public async Task<IActionResult> Follow(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);

            Appuser appuser = await _userManager.GetUserAsync(User);

            category.UserFollowedCategories.Add
            (
                new UserFollowedCategory()
                {
                    Appuser = appuser,
                    AppUserID = appuser.Id,
                    Category = category,
                    CategoryID = category.ID
                }
            );

            _categoryRepository.Update(category);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> UnFollow(int id)
        {
            var category = _categoryRepository.GetByDefaults<Category>
            (
                selector: a => new Category()
                {
                    Description = a.Description,
                    Name = a.Name,
                    ID = a.ID,
                    Statu = a.Statu,
                    UserFollowedCategories = a.UserFollowedCategories
                },
                expression: a => a.ID == id,
                include: a => a.Include(a => a.UserFollowedCategories)

            ).FirstOrDefault();

            Appuser appuser = await _userManager.GetUserAsync(User);

            if (category != null && appuser != null)
            {
                var followedCategory = category.UserFollowedCategories.FirstOrDefault(x => x.AppUserID == appuser.Id && x.CategoryID == category.ID);

                _followedCategoryRepository.Delete(followedCategory);
            }

            return RedirectToAction("List");
        }


        // todo : unfollow. kişi listeleme ekranındayken eğer ilgili satırdaki kategoriyi takip ediyorsa takibi bırak butonu, eğer takip etmiyosa takip et butonu olmalı ki. Duplicate key hatası engellenmeiş olsun ve kullanıcı deneyimi açıısndan başarılı olunsun.





    }
}
