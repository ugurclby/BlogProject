using Blog.Model.Entities.Enums;

namespace Blog.Web.Models.DTOs
{
    public class CategoryFilterDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Statu CategoryStatu { get; set; } 
        public int ArticleCount { get; set; }

    }
}  
