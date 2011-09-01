using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ads.Model.Entities;
using Ads.Model.Repositories;

namespace Ads.Model.EFRepositories
{
    public class BaseEFRepository<T, TId, TUserId> : IRepository<T, TId, TUserId>
        where T : BaseEntity<TId, TUserId>, new()
        where TId : struct
        where TUserId : struct 
    {
        protected AdsWebContext Context = new AdsWebContext();

        public IQueryable<T> All {
            get { return Context.Set<T>(); }
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = Context.Set<T>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public T Find(TId id) {
            return Context.Set<T>().Find(id);
        }

        public void InsertOrUpdate(T item) {
            if (item.Id.ToString() == default(TId).ToString()) {
                // New entity
                Context.Set<T>().Add(item);
            }
            else {
                // Existing entity
                Context.Entry(item).State = EntityState.Modified;
            }
        }

        public void Delete(TId id) {
            var person = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(person);
        }

        public void Save() {
            Context.SaveChanges();
        }
    }
}