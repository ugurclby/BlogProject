using Blog.Model.Entities.Abstract;

namespace Blog.Model.Entities.Concrete
{
    public class Like 
    {
        public string AppUserID { get; set; }
        public Appuser  Appuser { get; set; }

        public  int ArticleID { get; set; }
        public Article  Article { get; set; }
    }
}