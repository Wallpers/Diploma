﻿using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        MarketContext db;
        DbSet<T> entities;

        public T Create(T entity)
        {
            entity = entities.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public T Find(int id)
        {
            return entities.Find(id);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
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

        public IEnumerable<T> Take(int count)
        {
            return entities.Take(count).ToList();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return entities.Where(predicate).ToList();
        }

        public T SinleOrDefault(Func<T, bool> predicate)
        {
            return entities.SingleOrDefault(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return entities.FirstOrDefault(predicate);
        }

        public List<T> ToList()
        {
            return entities.ToList();
        }
    }
}