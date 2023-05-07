using Blog.Dal.Context;
using Blog.Dal.Repositories.Abstract;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Model.Entities.Enums;
using Blog.Web.Models.DTOs;

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

        public List<CategoryFilterDto> GetCategoriesWithArticleCount()
        {
            // Çalışan Sql sorgu :
            //SELECT DISTINCT [c].[ID] AS[CategoryId],
            //                [c].[Name] AS[CategoryName],
            //                [c].[Statu] AS[CategoryStatu],
            //           ( SELECT COUNT(*)
            //               FROM [ArticleCategories] AS[a]
            //              INNER JOIN [Articles] AS[a0] ON[a].[ArticleID] = [a0].[ID]
            //              WHERE ([a].[CategoryID] = [c].[ID]) AND (([a0].[Statu] = 1) OR([a0].[Statu] = 2))) AS[ArticleCount]
            //FROM[ArticleCategories] AS[a1]
            //INNER JOIN[Categories] AS[c] ON[a1].[CategoryID] = [c].[ID]
            //INNER JOIN[Articles] AS[a2] ON[a1].[ArticleID] = [a2].[ID]
            //WHERE(([c].[Statu] = 1) OR([c].[Statu] = 2))
            //  AND(( SELECT COUNT(*)
            //         FROM[ArticleCategories] AS[a3]
            //   INNER JOIN[Articles] AS[a4] ON[a3].[ArticleID] = [a4].[ID]
            //        WHERE([a3].[CategoryID] = [c].[ID]) AND(([a4].[Statu] = 1) OR([a4].[Statu] = 2))) > 0)
             
            var categoryFilters = _projectContext.ArticleCategories
                .Join(
                    _projectContext.Categories,
                    articleCategoryTable => articleCategoryTable.CategoryID,
                    categoryTable => categoryTable.ID,
                    (articleCategoryTable, category) => new { articleCategoryTable, category }
                )
                .Join(
                    _projectContext.Articles,
                    combinedTableArticleCategory => combinedTableArticleCategory.articleCategoryTable.ArticleID,
                    articleTable => articleTable.ID,
                    (combinedTableArticleCategory, articleTable) => new CategoryFilterDto
                    {
                        CategoryId = combinedTableArticleCategory.category.ID,
                        CategoryName = combinedTableArticleCategory.category.Name,
                        CategoryStatu = combinedTableArticleCategory.category.Statu, 
                        ArticleCount = _projectContext.ArticleCategories.Count(x =>
                            x.CategoryID== combinedTableArticleCategory.category.ID && (x.Article.Statu==Statu.Active|| x.Article.Statu == Statu.Modified))
                    }
                )
                .Where(fullEntry => 
                    (fullEntry.CategoryStatu == Statu.Active || fullEntry.CategoryStatu == Statu.Modified) && fullEntry.ArticleCount>0 )
                .Distinct()
                .ToList();
            return categoryFilters;
 
        }

        public List<FollowCategoryFilterDto> GetCategoriesWithNoArticleCount(string CategoryAppUserId)
        {
            //SELECT DISTINCT[u].[AppUserID] AS[AppUserId], 
            //[c].[ID] AS[CategoryId], 
            //[c].[Name] AS[CategoryName], 
            //[c].[Statu] AS[CategoryStatu], 
            //(SELECT COUNT(*) 
            //FROM[ArticleCategories] AS[a]  
            //INNER JOIN[Articles] AS[a0] ON[a].[ArticleID] = [a0].[ID] 
            //WHERE([a].[CategoryID] = [c].[ID]) 
            //AND(([a0].[Statu] = 1) OR([a0].[Statu] = 2))) AS[ArticleCount]
            //FROM[ArticleCategories] AS[a1]
            //INNER JOIN[Categories] AS[c] ON[a1].[CategoryID] = [c].[ID]
            //INNER JOIN[UserFollowedCategories] AS[u] ON[c].[ID] = [u].[CategoryID]
            //INNER JOIN[Articles] AS[a2] ON[a1].[ArticleID] = [a2].[ID]
            //WHERE(([c].[Statu] = 1) OR([c].[Statu] = 2)) AND([u].[AppUserID] =@CategoryAppUserId )

            var categoryFilters = _projectContext.ArticleCategories
                .Join(
                    _projectContext.Categories,
                    articleCategoryTable => articleCategoryTable.CategoryID,
                    categoryTable => categoryTable.ID,
                    (articleCategoryTable, category) => new { articleCategoryTable, category }
                ).Join(
                    _projectContext.UserFollowedCategories,
                    combinedTableArticleCategory => combinedTableArticleCategory.category.ID,
                    userFollowCategory=> userFollowCategory.CategoryID,
                    (combinedTableArticleCategory, userFollowCategory) =>new{ combinedTableArticleCategory, userFollowCategory }
                    ) 
                .Join(
                    _projectContext.Articles,
                    combinedTableArticleCategory => combinedTableArticleCategory.combinedTableArticleCategory.articleCategoryTable.ArticleID,
                    articleTable => articleTable.ID,
                    (combinedTableArticleCategory, articleTable) => new FollowCategoryFilterDto
                    {
                        AppUserId = combinedTableArticleCategory.userFollowCategory.AppUserID,
                        CategoryId = combinedTableArticleCategory.combinedTableArticleCategory.category.ID,
                        CategoryName = combinedTableArticleCategory.combinedTableArticleCategory.category.Name,
                        CategoryStatu = combinedTableArticleCategory.combinedTableArticleCategory.category.Statu,
                        ArticleCount = _projectContext.ArticleCategories.Count(x =>
                            x.CategoryID == combinedTableArticleCategory.combinedTableArticleCategory.category.ID && (x.Article.Statu == Statu.Active || x.Article.Statu == Statu.Modified))
                    }
                )
                .Where(fullEntry =>
                    (fullEntry.CategoryStatu == Statu.Active || fullEntry.CategoryStatu == Statu.Modified) && fullEntry.AppUserId== CategoryAppUserId)
                .Distinct()
                .ToList();
            return categoryFilters;
        }
    }
}
