using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Repositories.Concrete
{
    public class UserFollowedCategoryRepository : IUserFollowedCategoryRepository
    {
        private readonly ProjectContext _projectContext;
        private readonly DbSet<UserFollowedCategory> _table;

        public UserFollowedCategoryRepository(ProjectContext projectContext)
        {
            _projectContext = projectContext;
            _table = _projectContext.Set<UserFollowedCategory>();
        } 
      
        public void Delete(UserFollowedCategory followedCategory)
        {
            _table.Remove(followedCategory);
            _projectContext.SaveChanges();
        }
    }
}
