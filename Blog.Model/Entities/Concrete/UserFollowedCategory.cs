using Blog.Model.Entities.Abstract;
using Blog.Model.Entities.Enums;

namespace Blog.Model.Entities.Concrete
{
    public class UserFollowedCategory
    { 
        public string AppUserID { get; set; }
        public Appuser Appuser { get; set; }


        public int CategoryID { get; set; }
        public Category  Category { get; set; }

    }
}