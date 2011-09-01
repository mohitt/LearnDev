using System;
using System.Linq;
using System.Linq.Expressions;
using Ads.Model.Entities;

namespace Ads.Model.Repositories
{
    public interface IRepository<T, TId, TUserId>
        where T : BaseEntity<TId, TUserId>
        where TId : struct
        where TUserId : struct 
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(TId id);
        void InsertOrUpdate(T entity);
        void Delete(TId id);
        void Save();
    }
}