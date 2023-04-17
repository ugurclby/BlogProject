using Blog.Model.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Model.Entities.Concrete
{
    public class Article : BaseEntity
    {

        public Article()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }  // foto kaynağını tutar

        [NotMapped]    // buu alanı kolon olarak işaretleme
        public IFormFile Image { get; set; }


        // navigationProp

        // 1 kategorisi vardır.

        public int CategoryID { get; set; }

        public Category  Category { get; set; }


        // 1 makalenin 1 yazarı 

        public string AppUserID { get; set; }

        public Appuser  Appuser { get; set; }

        // 1 MAKALENİN ÇOKÇA YORUMU VE LİKE NESNESİ OLABİLİR.
        public List<Like>  Likes { get; set; }

        public List<Comment>  Comments { get; set; }

    }
}