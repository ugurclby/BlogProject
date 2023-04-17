using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Concrete
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly ProjectContext _projectContext;
        private readonly DbSet<Appuser> _table;

        public AppUserRepository(UserManager<Appuser> userManager,ProjectContext projectContext)
        {
            _userManager = userManager;
           _projectContext = projectContext;
            _table = _projectContext.Set<Appuser>();
        }

        public async Task Create(Appuser appuser)
        {
          await  _userManager.CreateAsync(appuser,appuser.Password);
           
           await _userManager.AddToRoleAsync(appuser, "Member");
            _projectContext.SaveChanges();
        }

        public void Delete(Appuser appuser)
        {
            appuser.Statu = Model.Entities.Enums.Statu.Passive;
            _projectContext.SaveChanges();
        }

        public void Update(Appuser appuser)
        {
            appuser.Statu = Model.Entities.Enums.Statu.Modified;
            // todo: usermanager.update ??
            _table.Update(appuser);
            _projectContext.SaveChanges();
        }
    }
}
