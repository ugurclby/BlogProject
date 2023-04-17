using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Web.Areas.Member.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<Appuser> _userManager;

        public CategoryController(IMapper mapper,ICategoryRepository categoryRepository,UserManager<Appuser> userManager)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
           _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            if(ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(dto);
                _categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }

        public IActionResult List()
        {
            var list = _categoryRepository.GetDefaults(a => a.Statu !=Statu.Passive);
            return View(list);
        }


        public IActionResult Update(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);
            var updatedCategory=_mapper.Map<UpdateCategoryDTO>(category);
            return View(updatedCategory);

        }


        [HttpPost]
        public IActionResult Update(UpdateCategoryDTO dto)
        {
            if(ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(dto);
                _categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(dto);
        }

        public async Task<IActionResult> Follow(int id)
        {
            Category category = _categoryRepository.GetDefault(a => a.ID == id);

            Appuser appuser = await _userManager.GetUserAsync(User);

            category.UserFollowedCategories.Add
            (
                new UserFollowedCategory() 
              {
                Appuser=appuser, AppUserID=appuser.Id, Category=category,CategoryID=category.ID
              }
            );

            _categoryRepository.Update(category);
            return RedirectToAction("List");
        }

        // todo : unfollow. kişi listeleme ekranındayken eğer ilgili satırdaki kategoriyi takip ediyorsa takibi bırak butonu, eğer takip etmiyosa takip et butonu olmalı ki. Duplicate key hatası engellenmeiş olsun ve kullanıcı deneyimi açıısndan başarılı olunsun.





    }
}
