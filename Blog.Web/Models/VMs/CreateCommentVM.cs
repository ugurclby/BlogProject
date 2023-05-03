using Blog.Model.Entities.Concrete;

namespace Blog.Web.Models.VMs
{
    public class CreateCommentVM
    {
        public string Text { get; set; } 

        public string AppUserID { get; set; } 

        public int ArticleID { get; set; } 
    }
}
