using System.Linq;

namespace RestAPI.Data.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T obj);
        T Find(object id);
        void Remove(int id);
        void Remove(T obj);
        void Save();
        IQueryable<T> Set();
    }
}