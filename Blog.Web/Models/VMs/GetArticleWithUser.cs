using Blog.Model.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Blog.Web.Models.VMs
{
    public class GetArticleWithUser
    {

        public string Title { get; set; }

        public string Content { get; set; }
        public string ImagePath { get; set; }

        public int ArticleId { get; set; }

        public DateTime  CreatedDate { get; set; }

        public string AppUserId { get; set; }

        public string UserFullName { get; set; }

        public List<ArticleCategory> ArticleCategories { get; set; }
    }
}
