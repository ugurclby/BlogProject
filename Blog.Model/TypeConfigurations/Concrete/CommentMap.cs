using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class CommentMap : BaseMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(a => a.Appuser).WithMany(a => a.Comments).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Article).WithMany(a => a.Comments).HasForeignKey(a => a.ArticleID).OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Text).IsRequired(true);

            base.Configure(builder);
        }
    }
}
