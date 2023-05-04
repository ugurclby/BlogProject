using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Member.Models
{
    public class CreateCategoryDTO
    {

        [Required(ErrorMessage ="Bu alan boş olmaz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan boş olmaz")]
        [MinLength(7,ErrorMessage ="En az 7 karakter yazmalısınız.")]
        public string Description { get; set; } 
    }
}
