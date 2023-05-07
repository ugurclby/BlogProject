using Blog.Dal.Context;
using Blog.Dal.Repositories.Interfaces.Abstract;
using Blog.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Blog.Model.Entities.Enums;

namespace Blog.Dal.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ProjectContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository(ProjectContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Create(T entity)
        {

           _table.Add(entity);
            _context.SaveChanges(); 
        }

        public void Delete(T entity)
        {
            entity.Statu = Statu.Passive;
            _context.SaveChanges();
        }
        public void DeleteRangeNotPassive(List<T> entities)
        {
            _table.RemoveRange(entities);
            _context.SaveChanges();
        }
        public TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;

            if(include!=null)  query=include(query);
            return query.Where(expression).Select(selector).FirstOrDefault();
        }

        public List<TResult> GetByDefaults<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _table;

            if(include!=null) query=include(query);
            if (orderBy != null) return orderBy(query).Where(expression).Select(selector).ToList();
            return query.Where(expression).Select(selector).ToList();
        }

        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();

        }

        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public void Update(T entity)
        {
            if (entity.Statu != Statu.Confirmation && entity.Statu != Statu.Rejection) // Admin onayına giden bir obje , güncellendiği zaman statusu değişip onay sürecini atlamasın diye yazıldı.
            {
                entity.Statu = Statu.Modified;
            }else if (entity.Statu == Statu.Rejection)
            {
                entity.Statu = Statu.Confirmation; // Admin onayına düşüp, adminin reddettiği bir makale yazar tarafından güncellenirse tekrar admin onayına düşmesi sağlanır.
            } 
            _table.Update(entity); 
            _context.SaveChanges();
        }
        public void UpdateApproval(T entity)
        { 
            _table.Update(entity);
            _context.SaveChanges();
        }
    }

}
