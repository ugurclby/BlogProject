using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Concrete;
using Blog.Model.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Model.Entities.Enums;

namespace Blog.Dal.Repositories.Concrete
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly ProjectContext _projectContext;
        private readonly DbSet<Appuser> _table;

        public AppUserRepository(UserManager<Appuser> userManager, ProjectContext projectContext)
        {
            _userManager = userManager;
            _projectContext = projectContext;
            _table = _projectContext.Set<Appuser>(); 
        }

        public async Task<List<string>> Create(Appuser appuser)
        {
            //Insert sırasında IdentityUser üzerinde mail ve kullanıcı adı benzersiz kontrolü olduğu için hataları burada listeye atıp controller tarafında yakalama ve göster işlemi yapılmıştır.
            // O yüzden metod List dönecek şekilde düzenlenmiştir.

            List<string> errors = new List<string>();

            var appUserResult = await _userManager.CreateAsync(appuser, appuser.Password);

            if (!appUserResult.Succeeded)
            {
                foreach (var error in appUserResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return errors;
            }
            await _userManager.AddToRoleAsync(appuser, "member"); 
            await _projectContext.SaveChangesAsync();
            return errors;
        }

        public void Delete(Appuser appuser)
        {
            appuser.Statu = Model.Entities.Enums.Statu.Passive;
            _projectContext.SaveChanges();
        }

        //Dönüş tipi int verilmesinin sebebi başarılı update olup olmadığının kontrolü yapılması gerekmektedir. Çünkü şifre değişikliğinde usedpass tablosuna insert atılmalıdır. 
        public int Update(Appuser appuser)
        {

            //await _userManager.UpdateAsync(appuser); 
            //// todo: usermanager.update ??
            ///  _userManager.UpdateAsync(appuser);  metodu ile de update işlemi yapılabiliyor. Fakat içinde bulunduğumuz update metodu async metod olmak zorunda ve db context üzerinden işlem yapamdığımız için savechanges kendi yapar.
            /// O kod buradan kalkar. Sadece bu kod olur : _userManager.UpdateAsync(appuser);
            /// Ayrıca EF üzerinden yapılan update işlemlerinde EF devamlı _table nesnesi üzerinden tabloları üzerinde tuttuğu için bir dml metodu çağırmadan bu kod ile de _table.Update(appuser); işlem yapılabilir. 

            appuser.Statu = Model.Entities.Enums.Statu.Modified;
            _table.Update(appuser);
            return _projectContext.SaveChanges();
        }
        public int UpdateApproval(Appuser appuser)
        {  
            _table.Update(appuser);
            return _projectContext.SaveChanges();
        }
        /// <summary>
        /// Onay Bekleyen Kullanıcıları Veren Metod
        /// </summary>
        /// <returns></returns>
        public List<Appuser> OnayBekleyenKullaniciListesi()
        {
            return _table.Where(x => x.Statu == Statu.Confirmation).ToList(); 
        }
    }
}
