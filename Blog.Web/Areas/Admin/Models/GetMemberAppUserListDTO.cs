using Blog.Model.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Web.Areas.Admin.Models
{
    public class GetMemberAppUserListDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public Statu Statu { get; set; }
        public string StatuDescription { get; set; }
        
    }
}
