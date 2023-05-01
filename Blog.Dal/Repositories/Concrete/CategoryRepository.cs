using Blog.Dal.Context;
using Blog.Dal.Repositories.Abstract;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Model.Entities.Enums;

namespace Blog.Dal.Repositories.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<Category> _table;

        public CategoryRepository(ProjectContext projectContext):base(projectContext)
        {
            _projectContext = projectContext;
            _table = _projectContext.Set<Category>();
        }

        public List<Category> GetCategoriesWithBlog()
        {
            //return  _projectContext.Categories.Where(x=> x.Statu == Statu.Active).OrderByDescending(I => I.ID).Include(I => I.ArticleCategories).ToList();

            return _projectContext.Categories.Join(_projectContext.ArticleCategories, c => c.ID, cb => cb.CategoryID, (category, articleCategory) => new
            {
                category,
                articleCategory
            }).Where(x => x.category.Statu == Statu.Active || x.category.Statu == Statu.Modified).
                Select(I => new Category
            {
                ID = I.category.ID,
                Name = I.category.Name,
                ArticleCategories = I.category.ArticleCategories
            }).Distinct()
                .ToList();
        }
    }
}
