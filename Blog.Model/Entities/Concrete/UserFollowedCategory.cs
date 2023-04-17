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