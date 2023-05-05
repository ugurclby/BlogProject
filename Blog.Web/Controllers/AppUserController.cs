using AutoMapper;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Model.Entities.Enums;
using Blog.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{ 
    public class AppUserController : Controller
    {  
        public IActionResult UserDetail(string appUserId)
        {
            ViewBag.AppUserId = appUserId;
            return View();
        }
    }
}
