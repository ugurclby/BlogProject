using System.Collections.Generic;
using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Blog.Web.Areas.Admin.Models;
using Blog.Dal.Repositories.Concrete;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository; 

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository )
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository; 
        }

        public IActionResult CategoryList()
        {
           var categoryList= _categoryRepository.GetByDefaults(category => category,x=>x.ID>0,queryable => queryable.Include(category => category.UserFollowedCategories).Include(category => category.ArticleCategories));
            return View(categoryList);
        }
        public IActionResult CategoryActive(int id)
        {
            var category = _categoryRepository.GetDefault(x => x.ID == id);
            if (category != null)
            {
                category.Statu = Statu.Active;
                _categoryRepository.UpdateApproval(category);
            }
            return RedirectToAction("CategoryList");
        }
        public IActionResult CategoryPassive(int id)
        {
            var category = _categoryRepository.GetDefault(x => x.ID == id);
            if (category != null)
            {
                category.Statu = Statu.Passive;
                _categoryRepository.UpdateApproval(category);
            }
            return RedirectToAction("CategoryList");
        }
        

        public IActionResult ConfirmationCategoryList()
        {
            var categories= _categoryRepository.GetDefaults(category => category.Statu == Statu.Confirmation); 
            var categoryListDtos =_mapper.Map<List<GetConfirmationCategoryListDTO>>(categories);

            return View(categoryListDtos);
        }
        /// <summary>
        /// Kategorilerimin onaylandığı yer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult CategoryApproval(int id)
        {
            var category =  _categoryRepository.GetDefault(category => category.ID == id);
            if (category != null)
            {
                category.Statu = Statu.Active;
                _categoryRepository.UpdateApproval(category);
            }
            return RedirectToAction("ConfirmationCategoryList");
        }
        public IActionResult RejectionCategory(int id)
        {
            var category = _categoryRepository.GetDefault(category => category.ID == id);
            if (category != null)
            {
                category.Statu = Statu.Rejection;
                _categoryRepository.UpdateApproval(category);
            }
            return Json(null);
        }
    }
}
