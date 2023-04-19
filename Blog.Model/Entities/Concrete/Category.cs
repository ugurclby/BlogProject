using Blog.Model.Entities.Abstract;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Blog.Model.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public Category()
        {
            //Articles = new List<Article>();
            UserFollowedCategories = new List<UserFollowedCategory>();  
        }
        public string Name { get; set; }

        public string Description { get; set; } 

        // 1 kategorinn çokça makalesi olabilir.

        //public List<Article>  Articles { get; set; } HOCA

        // 1 kategoriyi çokça takip eden olabilir.

        public List<UserFollowedCategory>  UserFollowedCategories { get; set; }

        // Makale ile kategori arasında çoktan çoğa ilişki kurmak için
        public List<ArticleCategory> ArticleCategories { get; set; }

    }
}