using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Dal.Repositories.Interfaces.Abstract;
using Blog.Dal.Repositories.Abstract;

namespace Blog.Dal.Repositories.Concrete
{
    public class ArticleCategoryRepository : BaseRepository<ArticleCategory>  , IArticleCategoryRepository

    {
          public ArticleCategoryRepository(ProjectContext projectContext):base(projectContext)
        {
           
        } 
      
       
    }
}
