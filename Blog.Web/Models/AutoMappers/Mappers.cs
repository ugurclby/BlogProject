using AutoMapper;
using Blog.Model.Entities.Concrete;
using Blog.Web.Areas.Admin.Models;
using Blog.Web.Areas.Member.Models;
using Blog.Web.Areas.Member.Models.VMs;
using Blog.Web.Models.DTOs;
using Blog.Web.Models.VMs;

namespace Blog.Web.Models.AutoMappers
{
    public class Mappers  : Profile
    {
        public Mappers()
        {
            CreateMap<RegisterDTO, Appuser>().ReverseMap();
            CreateMap<UserApprovalUpdateDTO, Appuser>();
            CreateMap<RegisterUpdateDTO, Appuser>().ReverseMap();
            CreateMap<AdminRegisterUpdateDTO, Appuser>().ReverseMap();
            CreateMap<GetConfirmationUserListDTO, Appuser>().ReverseMap();
            CreateMap<Appuser,GetMemberAppUserListDTO>();

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<GetConfirmationCategoryListDTO, Category>().ReverseMap(); 

            CreateMap<CreateArticleVM, Article>();
            CreateMap<UpdateArticleVM,Article>().ReverseMap();
            
            CreateMap<AboutDTO, About>();

            CreateMap<CreateCommentVM, Comment>();

            CreateMap<CreateLikeVM, Like>();
            
        }
    }
}
