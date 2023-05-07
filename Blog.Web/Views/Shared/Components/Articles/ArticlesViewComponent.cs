using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Views.Shared.Components.Articles
{
    public class ArticlesViewComponent : ViewComponent
    {


        // en güncel 9 yada 10 makaleyei getiren component

        //Bu component 9 ya da 10 değeri takeNumber parametresine bağlanmış olup o parametre de ArticleMaxList enum üzerinden okur.  

        private readonly IArticleRepository _articleRepository;
        public ArticlesViewComponent(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IViewComponentResult Invoke(int categoryId, int takeNumber = (int)ArticleMaxList.User)
        {

            List<GetArticleWithUser> list = _articleRepository.GetByDefaults
                (
                    selector: a => new GetArticleWithUser()
                    {
                        AppUserId = a.AppUserID,
                        Title = a.Title,
                        Content = a.Content,
                        ImagePath = a.ImagePath,
                        ArticleId = a.ID,
                        CreatedDate = a.CreatedDate,
                        UserFullName = a.Appuser.FullName,
                        ArticleCategories = a.ArticleCategories
                    },
                    expression: a => a.Statu == Statu.Modified || a.Statu == Statu.Active,
                    include: a => a.Include(a => a.Appuser).Include(a => a.ArticleCategories),
                    orderBy: a => a.OrderByDescending(a => a.CreatedDate)
                ).ToList();

            // take yukarıdaki kod bloğundan çıkarılmıştır. Çünkü son yazılan 9 ya da 5 makeleyi
            // çekip onların üzerinden category id bazlı sorgulama yapıldığında sonuç dönmüyordu. 

            if (categoryId > 0)
            {
                return View(list.Where(x => x.ArticleCategories.Any(y => y.CategoryID == categoryId)).Take(takeNumber).ToList());
            }
            return View(list.Take(takeNumber).ToList());
        }
    }
}
