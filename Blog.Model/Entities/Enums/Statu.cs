using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model.Entities.Enums
{
    public enum Statu
    {
        //Admin Onayına sunulması için eklendi
        Confirmation=0, 
        Active,
        Modified,
        Passive,
        Rejection
    }
}
