using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Bu alan boş olmaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan boş olmaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // bu alanı postta iişlemler başarılı olduysa kullanıcıyı gitmek istediği yere yönlendirmek için kullanacağız. Kullanıcıdan veri alacağımız bir alan olmayacak yani viewda gözükmeyecek.
        public string ReturnUrl { get; set; }
    }
}
