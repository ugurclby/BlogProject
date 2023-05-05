using Blog.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Blog.Web.Views.Shared.Components.UserDetail
{
    public class UserDetailViewComponent : ViewComponent
    {  
        private readonly IAppUserRepository _appUserRepository;  
        public UserDetailViewComponent(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        } 
        public IViewComponentResult Invoke(string appUserId)
        { 
            return View(_appUserRepository.GeAppUserById(appUserId));
        } 
    }
}
