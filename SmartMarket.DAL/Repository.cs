using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class, IEntity
    {
        private MarketContext db;
        private IDbSet<T> entities;

        public Repository()
        {
            db = new MarketContext();
            entities = db.Set<T>();
        }

        public T Create(T entity)
        {
            entity = entities.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public T Find(int id, params Expression<Func<T, object>>[] includes)
        { 
            var query = Include(includes);
            return query.FirstOrDefault(x => x.ID == id);
        }

        public void Update(T entity)
        {
            db.Set<T>().AddOrUpdate(entity);
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            var entity = Activator.CreateInstance(typeof(T), id) as T;
            Remove(entity);
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
            db.SaveChanges();
        }

        public IEnumerable<T> Take(int count, params Expression<Func<T, object>>[] includes)
        {
            var query = Include(includes);
            return query.Take(count).ToList();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Include(includes);
            return query.Where(predicate).ToList();
        }

        public T SinleOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Include(includes);
            return query.SingleOrDefault(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Include(includes);
            return query.FirstOrDefault(predicate);
        }

        public List<T> ToList()
        {
            return entities.ToList();
        }

        IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var query = entities.AsQueryable();
            if (includes != null)
            { 
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    entities = null;
                }

                db.Dispose();
                disposed = true;
            }
        }
    }
}
