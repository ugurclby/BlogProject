using System.ComponentModel.DataAnnotations.Schema;
using Blog.Model.Entities.Concrete;
using Blog.Model.Entities.Enums;

namespace Blog.Web.Models.VMs
{
    public class CreateLikeVM
    {
        public string AppUserID { get; set; } 

        public int ArticleID { get; set; }
        
        [NotMapped]
        public string Type { get; set; }
    }
}
