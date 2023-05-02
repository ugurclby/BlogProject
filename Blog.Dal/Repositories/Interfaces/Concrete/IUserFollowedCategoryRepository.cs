using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IUserFollowedCategoryRepository
    {  
        void Delete(UserFollowedCategory like); 

    }
}
