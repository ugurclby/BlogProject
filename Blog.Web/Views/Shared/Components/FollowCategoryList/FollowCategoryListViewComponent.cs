using Blog.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

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
