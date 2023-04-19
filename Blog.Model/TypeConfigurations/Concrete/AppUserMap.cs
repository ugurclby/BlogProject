using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Model.Entities.Enums;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class AppUserMap : IEntityTypeConfiguration<Appuser>
    {
        public void Configure(EntityTypeBuilder<Appuser> builder)
        {
            builder.Property(a => a.LastName).IsRequired(true);
            builder.Property(a => a.FirstName).IsRequired(true);
 
            // Uygulamada tek bir admin kullanıcısı olacağı için db oluşturulurken admin kullanıcısı ile birlikte oluşur.
            builder.HasData(new Appuser { Id = Guid.NewGuid().ToString(),FirstName = "admin",LastName = "admin",Password = "12345",Statu = Statu.Active});
        }
    }
}
