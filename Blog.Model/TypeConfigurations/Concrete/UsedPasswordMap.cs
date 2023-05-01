using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class UsedPasswordMap : BaseMap<UsedPassword>
    {

        public override void Configure(EntityTypeBuilder<UsedPassword> builder)
        { 
            builder.HasOne(a => a.Appuser).WithMany(a => a.UsedPasswords).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Cascade); ;
             
            base.Configure(builder);
        }

    }
}
