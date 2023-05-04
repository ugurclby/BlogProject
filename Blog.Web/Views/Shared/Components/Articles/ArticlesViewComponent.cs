using System;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Web.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Model.Entities.Concrete;

namespace Blog.Web.Views.Shared.Components.Articles
{
    public class ArticlesViewComponent : ViewComponent
    {


        // en güncel 9 yada 10 makaleyei getiren component

        private readonly IArticleRepository _articleRepository;
        public ArticlesViewComponent(IArticleRepository  articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IViewComponentResult Invoke(int categoryId,string Controller)
        { 
            ViewBag.Controller=Controller;
            List<GetArticleWithUser> list = _articleRepository.GetByDefaults
                (
                    selector: a=> new GetArticleWithUser() 
                    {
                        AppUserId=a.AppUserID,
                        Title=a.Title,
                        Content=a.Content,
                        ImagePath=a.ImagePath,
                        ArticleId=a.ID,
                        CreatedDate=a.CreatedDate,
                        UserFullName=a.Appuser.FullName,
                        ArticleCategories = a.ArticleCategories 
                    },
                    expression: a=> a.Statu == Model.Entities.Enums.Statu.Modified || a.Statu == Model.Entities.Enums.Statu.Active,
                    include : a=> a.Include(a=>a.Appuser).Include(a=>a.ArticleCategories),
                    orderBy: a=>a.OrderByDescending(a=>a.CreatedDate)
                ).Take(9).ToList();

            if (categoryId>0)
            {
                return View(list.Where(x => x.ArticleCategories.Any(y => y.CategoryID == categoryId)).Take(9).ToList());
            }

            return View(list);
        }




    }
}
