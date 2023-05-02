using Blog.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.Dal.Repositories.Interfaces.Abstract
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);

        void Delete(T entity);
        void DeleteRangeNotPassive(List<T> entities);
        void Update(T entity);

        void UpdateApproval(T entity); 
        T GetDefault(Expression<Func<T, bool>> expression);             // tek t tipini döner

        List<T> GetDefaults(Expression<Func<T, bool>> expression);     // expressinio sağlayan t tiplerini döner

        // selector(seçim) , expression (filtreleme) , include (dahil etme)  işlemleri yapılarak veritabanında tutulmayan o an kullanılacak olan farklı tek bir tresultı döner.
        TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector,
                                      Expression<Func<T, bool>> expression,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        List<TResult> GetByDefaults<TResult>(Expression<Func<T, TResult>> selector,
                                             Expression<Func<T, bool>> expression,
                                             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                             Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null );





    }
}
