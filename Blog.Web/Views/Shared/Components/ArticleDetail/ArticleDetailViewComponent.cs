using System;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Web.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;


namespace Blog.Web.Views.Shared.Components.ArticleDetail
{
    public class ArticleDetailViewComponent : ViewComponent
    {  
        private readonly IArticleRepository _articleRepository;  
        public ArticleDetailViewComponent(IArticleRepository  articleRepository )
        {
            _articleRepository = articleRepository; 
        } 
        public IViewComponentResult Invoke(int articleId,string SessionUserID, string sayfa)
        {  
            var article = _articleRepository.GetByDefaults(article => article, 
                article => article.ID == articleId,
                articles =>articles.Include(z=>z.Comments).ThenInclude(i=>i.Appuser).
                    Include(t=>t.Likes).ThenInclude(u=>u.Appuser).
                    Include(x=>x.ArticleCategories).ThenInclude(y=>y.Category)).FirstOrDefault();
            // Okunma Sayısı her ekran açıldığında update edilir.
            article.ReadCount += 1;
            _articleRepository.Update(article);
            ViewBag.Sayfa= sayfa;
            ViewBag.CurrentAppUserID = SessionUserID;
            return View(article);
        } 
    }
}
