using Blog.Dal.Context;
using Blog.Dal.Repositories.Concrete;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Blog.Web.Models.AutoMappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ProjectContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default"),o=> o.CommandTimeout(9999999)));

            services.AddIdentity<Appuser, IdentityRole>
                (
                    x=> 
                    {  
                        x.User.RequireUniqueEmail = true;   // kişiye ait eşsiz mail olsun mu
                        x.Password.RequiredLength = 4;      // şifre kaç karakter
                        x.Password.RequireUppercase = false;    // büyük harf şart mı
                        x.Password.RequireDigit= false;         // şifrede rakam şart mı
                        x.Password.RequireLowercase = false;    // şifrede küçük harf şart mı
                        x.Password.RequireNonAlphanumeric = false;     // şifrede özel karakter şart mı 
                        x.Password.RequiredUniqueChars = 0;         // eşşsiz karakter gerekli mi
                    }
                ).AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();



            services.AddAutoMapper(typeof(Mappers));

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IUsedPasswordRepository, UsedPasswordRepository>();
            services.AddScoped<IUserFollowedCategoryRepository, UserFollowedCategoryRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();

            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // localHost/areaName/controller/action/parametre
                endpoints.MapControllerRoute(name:"area",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // localHost/controller/action/parametre
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
