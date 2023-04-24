using Blog.Dal.Context;
using Blog.Dal.Repositories.Abstract;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Repositories.Concrete
{
    public class AboutRepository : BaseRepository<About>, IAboutRepository
    {
        public AboutRepository(ProjectContext context) : base(context)
        {
        }
    }
}
