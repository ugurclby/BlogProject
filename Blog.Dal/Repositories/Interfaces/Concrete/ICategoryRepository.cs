using Blog.Dal.Repositories.Interfaces.Abstract;
using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Web.Models.DTOs;


namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public List<CategoryFilterDto> GetCategoriesWithArticleCount();
        public List<FollowCategoryFilterDto> GetCategoriesWithNoArticleCount(string CategoryAppUserId); 
    }
}
