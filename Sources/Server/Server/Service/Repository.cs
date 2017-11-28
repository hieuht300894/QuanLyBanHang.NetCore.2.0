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

        public async Task<IEnumerable<T>> GetAll()
        {
            try {
                Context = new zModel();
                Task<List<T>> lstResult = Context.Set<T>().ToListAsync();
            }
            catch { return new List<T>(); }
        }

        public Task<T> Detail(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddEntry(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddEntries(T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntry(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntries(T[] items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntry(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntry(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntries(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntries(T[] items)
        {
            throw new NotImplementedException();
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
