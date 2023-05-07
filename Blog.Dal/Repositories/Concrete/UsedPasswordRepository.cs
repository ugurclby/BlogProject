using Blog.Dal.Context;
using Blog.Dal.Repositories.Abstract;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Blog.Dal.Repositories.Concrete
{
    public class UsedPasswordRepository : BaseRepository<UsedPassword>, IUsedPasswordRepository
    {
        private const int UsedPasswordLimit = 3;  
        public UsedPasswordRepository(ProjectContext context) : base(context)
        {
        } 
        public bool IsPreviousPassword(Appuser user, string newPassword)
        {
            var userPassword = GetByDefaults(
                selector: a => new UsedPassword() {PasswordHash = a.PasswordHash, AppUserID = a.AppUserID},
                expression: a => a.AppUserID== user.Id
            );
            // Kullanıcının önceki şifreleri ile yeni şifresi karşılaştırılır. Eğer eski şifrelerden biri yeni şifre ile eşleşiyorsa
            // Bu fonksiyon true döner (yani bu şifreyi daha önce kullandın)
            return userPassword.OrderByDescending(up => up.CreatedDate)
                .Select(up => up.PasswordHash).Take(UsedPasswordLimit).Any(x => new PasswordHasher<Appuser>().VerifyHashedPassword(user,x, newPassword) != PasswordVerificationResult.Failed);
             
        }


    }
}
