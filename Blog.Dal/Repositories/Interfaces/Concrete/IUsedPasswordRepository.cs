using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Dal.Repositories.Interfaces.Abstract;
using Blog.Dal.Repositories.Concrete;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IUsedPasswordRepository:IBaseRepository<UsedPassword>
    {
        bool IsPreviousPassword(Appuser user, string newPassword); 
    }
}
