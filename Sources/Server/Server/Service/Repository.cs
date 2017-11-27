using Microsoft.EntityFrameworkCore;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Service
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        public zModel Context { get; set; }

        public Repository(zModel db)
        {
            this.Context = db;
        }

        public bool Delete(int id)
        {
            try
            {
                T item = Context.Set<T>().Find(id);
                if (item == null) return false;
                Context.Set<T>().Remove(item);
                Context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public T Detail(int id)
        {
            try { return Context.Set<T>().Find(id); }
            catch { return null; }
        }

        public IEnumerable<T> GetAll()
        {
            try { return Context.Set<T>().AsEnumerable<T>(); }
            catch { return new List<T>(); }
        }

        public T Insert(T item)
        {
            try
            {
                Context.Set<T>().Add(item);
                Context.SaveChanges();
                Context.Entry(item).Reload();
                return item;
            }
            catch { return null; }
        }

        public T Update(T item)
        {
            try
            {
                return item;
            }
            catch { return null; }
        }
    }

    public class RepositoryCollection : IRepositoryCollection
    {
        private zModel db;

        public RepositoryCollection(zModel db)
        {
            this.db = db;
        }

        public Repository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(db);
        }
    }
}
