using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAboutRepository _aboutRepository; 

        public AboutController(IMapper mapper,IAboutRepository aboutRepository)
        { 
            _mapper = mapper;
            _aboutRepository = aboutRepository; 
        } 
        public IActionResult AboutList()
        {
            var aboutList = _aboutRepository.GetDefaults(x => x.Statu == Statu.Confirmation);
            return View(aboutList);
        }
        public IActionResult AboutDetail(int id)
        {
            var about = _aboutRepository.GetDefault(x => x.ID == id);
            about.Statu=Statu.Active;
            _aboutRepository.UpdateApproval(about); 
            return View(about);
        }


        
    }
}
