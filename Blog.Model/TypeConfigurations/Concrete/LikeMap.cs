using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(a => new { a.AppUserID, a.ArticleID });

            // todo:tekrar düşünülebilir 
            builder.HasOne(a => a.Article).WithMany(a => a.Likes).HasForeignKey(a => a.ArticleID);
            builder.HasOne(a => a.Appuser).WithMany(a => a.Likes).HasForeignKey(a => a.AppUserID);

        }
    }
}
