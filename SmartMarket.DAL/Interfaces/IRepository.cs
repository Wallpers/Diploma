using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Interfaces
{
    public interface IRepository<T> : IPredicate<T>
        where T : class, IEntity
    {
        T Create(T item);
        T Find(int id, params Expression<Func<T, object>>[] includes);
        void Remove(int id);
        void Remove(T item);
        void Update(T item);
    }

    public interface IPredicate<T> where T : class, IEntity
    {
        IEnumerable<T> Take(int count, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Where(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        T SinleOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        T FirstOrDefault(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        List<T> ToList();
    }
}
