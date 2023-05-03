using Blog.Model.Entities.Concrete;

namespace Blog.Web.Areas.Admin.Models
{
    public class GetConfirmationArticleListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AppUserID { get; set; } 
        public Appuser Appuser { get; set; }
    }
}
