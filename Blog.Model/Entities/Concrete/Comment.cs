using Blog.Model.Entities.Abstract;
using System.ComponentModel;

namespace Blog.Model.Entities.Concrete
{
    public class Comment : BaseEntity
    {

        public string Text { get; set; }

        // yorum kime ait?

        public string AppUserID { get; set; }

        public Appuser  Appuser { get; set; }

        // yorum hangi makale  ait?

        public int ArticleID { get; set; }
        public Article  Article { get; set; } 

    }
}