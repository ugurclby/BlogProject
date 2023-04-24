using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.DTOs
{
    public class AboutDTO
    {
        [Required(ErrorMessage = "Bu alan boş olmaz")] 
        public string AdSoyad { get; set; }

        [Required(ErrorMessage ="Bu alan boş olmaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan boş olmaz")]
        public string Konu { get; set; }

        [Required(ErrorMessage = "Bu alan boş olmaz")]
        public string Aciklama { get; set; }
    }
}
