namespace Blog.Model.Entities.Enums
{
    public enum Statu
    { 
        Confirmation=0, //Admin Onayına sunulması için eklendi
        Active,
        Modified,
        Passive,
        Rejection // Admin tarafından reddedilen işlemler için eklendi
    }
}
