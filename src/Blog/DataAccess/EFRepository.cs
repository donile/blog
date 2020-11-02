using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MarkDonile.Blog.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext;
        
        public EFRepository (DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            _dbContext.SaveChanges();
        }

        public void Add(IEnumerable<T> items)
        {
            _dbContext.Set<T>().AddRange(items);
            _dbContext.SaveChanges();
        }

        public T Get(params object[] keyValues)
        {
            return _dbContext.Set<T>().Find(keyValues);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Remove(params object[] keyValues)
        {
            T item = Get(keyValues);

            if (item is null)
            {
                return;
            }

            _dbContext.Remove(item );
            _dbContext.SaveChanges();
        }
        public void Update(T item)
        {
            _dbContext.Update( item );
            _dbContext.SaveChanges();
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}