using Results;
using System.Collections.Generic;

namespace MarkDonile.Blog.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Result Add( T item );
        Result Add( IEnumerable<T> items );
        Result<T> Get( params object[] keyValues );
        Result<IEnumerable<T>> GetAll();
        Result Update( T item );
        Result Remove( params object[] keyValues );
    }
}