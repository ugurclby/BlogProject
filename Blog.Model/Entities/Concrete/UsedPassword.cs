using Blog.Model.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Blog.Model.Entities.Abstract;

namespace Blog.Model.Entities.Concrete
{
    // Kullanınıcın şifrelerinin tutulduğu tablo
    public class UsedPassword : BaseEntity
    {
        public string PasswordHash { get; set; }

        public string AppUserID { get; set; }

        public Appuser Appuser { get; set; }

    }
}
