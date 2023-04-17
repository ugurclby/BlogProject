using AutoMapper;
using Blog.Model.Entities.Concrete;
using Blog.Web.Areas.Member.Models;
using Blog.Web.Areas.Member.Models.VMs;
using Blog.Web.Models.DTOs;

namespace Blog.Web.Models.AutoMappers
{
    public class Mappers  : Profile
    {
        public Mappers()
        {
            CreateMap<RegisterDTO, Appuser>();
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<CreateArticleVM, Article>();
            CreateMap<UpdateArticleVM,Article>().ReverseMap();
        }
    }
}
