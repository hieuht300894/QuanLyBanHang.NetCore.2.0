using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Service
{
    public interface IRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        T Detail(int id);
        T Insert(T item);
        T Update(T item);
        bool Delete(int id);
    }
    public interface IRepositoryCollection
    {
        Repository<T> GetRepository<T>() where T : class, new();
    }
}
