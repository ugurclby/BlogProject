using Blog.Model.Entities.Concrete;
using Blog.Model.TypeConfigurations.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Blog.Model.Entities.Enums;

namespace Blog.Dal.Context
{
    public class ProjectContext : IdentityDbContext<Appuser>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Appuser> Appusers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<UserFollowedCategory> UserFollowedCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new ArticleCategoryMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new UserFollowedCategoryMap());
            builder.ApplyConfiguration(new CategoryMap());

            string adminId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();

            //admin rol ve member rol atanır
            // IdentityRoleMap altından alınmasının sebebi rolün ardından admin kullanıcısı oluşacağı için roleId ihtiyacı vardı. 
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = roleId
            }, new IdentityRole
            {
                Name = "member",
                NormalizedName = "MEMBER",
                Id = Guid.NewGuid().ToString()
            }
                );

            //// Uygulamada tek bir admin kullanıcısı olacağı için db oluşturulurken admin kullanıcısı ile birlikte oluşur.
            var appUser = new Appuser
            {
                Id = adminId,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Statu = Statu.Active,
                NormalizedEmail = "ADMIN@GMAIL.COM"
            };

            //admin şifresi verilir
            PasswordHasher<Appuser> ph = new PasswordHasher<Appuser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "admin123");

            //admin insert edilir
            builder.Entity<Appuser>().HasData(appUser);

            //admin ve rolü insert edilir
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });


            base.OnModelCreating(builder);
        } 
    }
}
