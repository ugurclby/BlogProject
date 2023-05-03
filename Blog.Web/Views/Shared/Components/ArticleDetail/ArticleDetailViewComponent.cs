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
    public class ArticleDetailViewComponent : ViewComponent
    {  
        private readonly IArticleRepository _articleRepository;
        public ArticleDetailViewComponent(IArticleRepository  articleRepository)
        {
            _articleRepository = articleRepository;
        } 
        public IViewComponentResult Invoke(int articleId,string sayfa)
        {
            var article = _articleRepository.GetByDefaults(article => article, 
                article => article.ID == articleId,
                articles =>articles.Include(z=>z.Comments).ThenInclude(i=>i.Appuser).
                    Include(t=>t.Likes).ThenInclude(u=>u.Appuser).
                    Include(x=>x.ArticleCategories).ThenInclude(y=>y.Category)).FirstOrDefault();
            ViewBag.Sayfa= sayfa;
            
            return View(article);
        } 
    }
}
