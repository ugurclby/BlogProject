using System.Collections.Generic;
using System.Linq;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;
using Blog.Model;
using Blog.Model.Entities.Enums;
using Blog.Web.Models.DTOs;

namespace Blog.Web.Views.Shared.Components.CategoryList
{
    public class CategoryListViewComponent:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryListViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var list = _categoryRepository.GetCategoriesWithBlog(); 
            return View(list);  
        }
    }
}
