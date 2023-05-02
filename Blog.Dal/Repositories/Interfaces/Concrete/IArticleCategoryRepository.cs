using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Dal.Repositories.Interfaces.Abstract;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IArticleCategoryRepository:IBaseRepository<ArticleCategory>
    {  
    }
}
