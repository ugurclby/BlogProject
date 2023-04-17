using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class UserFollowedCategoryMap : IEntityTypeConfiguration<UserFollowedCategory>
    {
        public void Configure(EntityTypeBuilder<UserFollowedCategory> builder)
        {

            builder.HasOne(a => a.Appuser).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.AppUserID);
            builder.HasOne(a => a.Category).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.CategoryID);

            builder.HasKey(a => new { a.AppUserID, a.CategoryID });


        }
    }
}
