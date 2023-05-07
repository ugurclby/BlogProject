using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class CategoryMap : BaseMap<Category>
    {

        public override void Configure(EntityTypeBuilder<Category> builder)
        { 
            builder.HasMany(a => a.ArticleCategories).WithOne(b => b.Category).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.Restrict);
             
            base.Configure(builder);
        }

    }
}
