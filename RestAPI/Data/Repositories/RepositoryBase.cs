using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected WebContext _context;
        protected DbSet<T> _dbSet;

        internal RepositoryBase(WebContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            //_context = LibraryDBContext.GetInstance;
            //_context = new LibraryDBContext();
        }

        public virtual IQueryable<T> Set()
        {
            return _context.Set<T>();
        }

        public virtual T Find(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual void Add(T obj)
        {
            _context.Set<T>().Add(obj);
        }

        public virtual void Remove(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public virtual void Remove(int id)
        {
            _context.Set<T>().Remove(Find(id));
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
