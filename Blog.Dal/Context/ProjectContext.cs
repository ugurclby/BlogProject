using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dal.Context
{
   public class ProjectContext : IdentityDbContext<Appuser>
    {
        public ProjectContext( DbContextOptions<ProjectContext> options):base(options) { }


        public DbSet<Article>  Articles { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories{ get; set; }
        public DbSet<Appuser>  Appusers { get; set; }
        public DbSet<Like>  Likes { get; set; }
        public DbSet<Comment>  Comments { get; set; }
        public DbSet<UserFollowedCategory>  UserFollowedCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new ArticleCategoryMap());
            builder.ApplyConfiguration(new  CommentMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new UserFollowedCategoryMap());
            builder.ApplyConfiguration(new CategoryMap()); 

            base.OnModelCreating(builder);
        }








    }
}
