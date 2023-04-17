using Blog.Dal.Repositories.Interfaces.Abstract;
using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
    }
}
