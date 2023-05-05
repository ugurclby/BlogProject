using System.Collections.Generic;
using System.Linq;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;
using Blog.Model;
using Blog.Model.Entities.Enums;
using Blog.Web.Models.DTOs;

namespace Blog.Web.Views.Shared.Components.FollowCategoryList
{
    public class FollowCategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public FollowCategoryListViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke(string CategoryAppUserId)
        {
            var list = _categoryRepository.GetCategoriesWithNoArticleCount(CategoryAppUserId); 
            return View(list);  
        }
    }
}
