using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Web.Areas.Member.Models.VMs
{
    public class CreateArticleVM
    {
        // ARTİCLE
        public int ID { get; set; }
        [Required(ErrorMessage ="Bu alan boş olmaz.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu alan boş olmaz.")]
        [MinLength(100,ErrorMessage ="En az 100 karakter kullanmalısınız.")]
        public string Content { get; set; }

        [NotMapped]    // buu alanı kolon olarak işaretleme
        [Required(ErrorMessage = "Fotoğraf seçmelisiniz !")]
        public IFormFile Image { get; set; }


        // CATEGORY
        [Required(ErrorMessage = "Kategori seçmelisiniz.")]
        [Display(Name = "Kategoriler")]
        public int[] CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; } // Ekran üzerindeki kategoriler alannı selectlist item üzerinden okunması için yapıldı.


        //[Required(ErrorMessage = "Kategori seçmelisiniz.")]
        //public int CategoryID { get; set; }

        //// amaç sadece kategorileir taşımak bu yüzden required diyemem
        //public List<GetCategoryDTO>  Categories { get; set; }  // sahip olduğun her kategorinin yalnızca id ve name bilgisini taşaımak için 

        // appuser
        [Required(ErrorMessage = "Bu alan boş olmaz.")]
        public string AppUserID { get; set; }


    }
}
