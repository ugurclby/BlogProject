using Blog.Model.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Dal.Repositories.Interfaces.Concrete
{
    public interface IAppUserRepository
    {

        Task<List<string>> Create(Appuser appuser); 
        void Delete(Appuser appuser); 
        int Update(Appuser appuser);
        int UpdateApproval(Appuser appuser);  
        // Onay bekleyen kullanıcı listesini verir
        List<Appuser> OnayBekleyenKullaniciListesi(); 
        Appuser GeAppUserById(string appUserId);
    }
}
