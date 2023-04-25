using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IAppUserRepository
    {

        Task<List<string>> Create(Appuser appuser);

        void Delete(Appuser appuser);

        int Update(Appuser appuser);



    }
}
