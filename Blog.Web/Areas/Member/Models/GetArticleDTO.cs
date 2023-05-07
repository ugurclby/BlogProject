using Blog.Model.Entities.Concrete;

namespace Blog.Web.Areas.Member.Models
{
    public class GetArticleDTO
    {

        // ARTİCLE

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public int ArticleID { get; set; }

        // CATEGORY

        public string CategoryName { get; set; }

        public string StatuDescription { get; set; }

    }
}
