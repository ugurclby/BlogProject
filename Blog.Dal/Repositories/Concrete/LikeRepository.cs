using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Repositories.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<Like> _table;

        public LikeRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
            _table = _projectContext.Set<Like>();
        }

        public void Create(Like like)
        {
            _table.Add(like);
            _projectContext.SaveChanges();
        }

        public void Delete(Like like)
        {
            _table.Remove(like);
            _projectContext.SaveChanges();
        }
    }
}
