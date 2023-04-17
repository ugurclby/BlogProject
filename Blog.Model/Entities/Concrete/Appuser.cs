using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Model.Entities.Concrete
{
    public class Appuser : IdentityUser
    {
        public Appuser()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            UserFollowedCategories = new List<UserFollowedCategory>();
        }

      //  public string ID { get; set; }

        private DateTime _createdDate = DateTime.Now;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }


        private Statu _statu = Statu.Active;

        public Statu Statu
        {
            get { return _statu; }
            set { _statu = value; }
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName => $"{FirstName}  {LastName}";
        public string Password { get; set; }

        public string ImagePath { get; set; }  // foto kaynağını tutar

        [NotMapped]    // buu alanı kolon olarak işaretleme
        public IFormFile Image { get; set; }  // fotoyu çekip almaya yarayacak.

        // navigation prop

        // 1 kullıcının çokça makalesi olabilir.

        public List<Article> Articles { get; set; }

        // çkça beğeni

        public List<Like> Likes { get; set; }

        // çokça yorumu

        public List<Comment> Comments { get; set; }

        public List<UserFollowedCategory> UserFollowedCategories { get; set; }


    }
}
