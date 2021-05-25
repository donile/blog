using System.Collections.Generic;

namespace MarkDonile.Blog.DataAccess
{
    public interface IRepository<T>
    {
        void Add( T item );
        void Add( IEnumerable<T> items );
        T Get( params object[] keyValues );
        IEnumerable<T> GetAll();
        void Update( T item );
        void Remove( params object[] keyValues );
        bool SaveChanges();
    }
}