using Blog.Model.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Model.Entities.Enums;

namespace Blog.Model.Entities.Concrete
{
    public class About : BaseEntity
    {
  
        public string AdSoyad { get; set; }
         
        public string Email { get; set; }
         
        public string Konu { get; set; }
         
        public string Aciklama { get; set; }

        
    }
}