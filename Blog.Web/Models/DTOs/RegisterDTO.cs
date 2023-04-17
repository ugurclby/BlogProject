using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Web.Models.DTOs
{
    public class RegisterDTO
    {
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
        [Required(ErrorMessage ="Fotoğraf seçimi yapınız")]
        public IFormFile  Image { get; set; }
    }
}
