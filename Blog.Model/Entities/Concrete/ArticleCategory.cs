using Blog.Model.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Model.Entities.Concrete
{
    public class ArticleCategory : BaseEntity
    { 
        public int CategoryID { get; set; }
        public Category Category { get; set; } 
        public int ArticleID { get; set; }
        public Article Article { get; set; }
    }
}