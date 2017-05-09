using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Interfaces
{
    public interface IRepository<T> : IPredicate<T>
        where T : class, IEntity
    {
        T Create(T item);
        T Find(int id);
        void Remove(int id);
        void Remove(T item);
        void Update(T item);
    }

    public interface IPredicate<T> where T : class, IEntity
    {
        IEnumerable<T> Take(int count);
        IEnumerable<T> Where(Func<T, bool> predicate);
        T SinleOrDefault(Func<T, bool> predicate);
        T FirstOrDefault(Func<T, bool> predicate);
        List<T> ToList();
    }
}
