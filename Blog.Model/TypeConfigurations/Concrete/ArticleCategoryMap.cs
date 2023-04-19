using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class ArticleCategoryMap : BaseMap<ArticleCategory>
    { 
        public override void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            // ArticleCategory tablosunda aynı makale ve kategoriyi 1 defa ekleme şartı verilir.
            builder.HasIndex(a => new {a.ArticleID, a.CategoryID}).IsUnique();

            base.Configure(builder);
        }

    }
}
