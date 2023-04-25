using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Web.Areas.Member.Models
{
    public class RegisterUpdateDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Bu alan boş olamaz")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan boş olamaz")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bu alan boş olamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş olamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan boş olamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped] 
        public IFormFile  Image { get; set; }

        [DisplayName("Geçerli Fotoğraf")]
        public string ImagePath { get; set; }
        

        [NotMapped]
        public string DmlType { get; set; }
    }
}
