using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Model.TypeConfigurations.Concrete
{
    public class ArticleMap : BaseMap<Article>
    {

        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired(true);

            //builder.HasOne(a => a.Category).WithMany(a => a.Articles).HasForeignKey(a => a.CategoryID).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ArticleCategories).WithOne(b => b.Article).HasForeignKey(c => c.ArticleID).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Appuser).WithMany(a => a.Articles).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);

            /*
             DeleteBehavior => Silinme davranışı.  Mic.Efcoredan gelen bir enum yapısıdır. İlişkili entitylerde silinme durumunda nasıl davranılacağını tanımlamaya çalışır.

            * RESTRICT => ebeveyn- çocuk ilişkisi (parent - child) yani ebeveynsiz çocuk olmaması gibi categoryı silmeye kalktığınızda makale kategorisiz kalacağını için müsade etmez.
            * 
            * NOACTION => ilişkilerde silinmeyi serbest bırakır hata vermez, bir şey yapmaz.
            * 
            * CASCADE => Ebeveyn - çocuk ilişkisinde ebeveyn silindiğinde ona bağlı tüm çocuklarda silinir.
            * 
            * SETNULL => Defaulttaki silinme davranışıdır. FK(foreign keyi) boş geeçebilmemizi sağlar. (nortwinddedi catrgory-product gibi)
             
             */  

            base.Configure(builder);
        }

    }
}
