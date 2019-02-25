using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Results;

namespace MarkDonile.Blog.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private DbContext _dbContext;
        
        public EFRepository ( DbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public Result Add( T item )
        {
            try
            {
                _dbContext.Set<T>().Add( item );
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch( Exception e )
            {
                return Result.Failure( e.Message );
            }
        }

        public Result Add( IEnumerable<T> items )
        {
            try
            {
                _dbContext.Set<T>().AddRange( items );
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch( Exception e )
            {
                return Result.Failure( e.Message );
            }
        }

        public Result<T> Get( params object[] keyValues )
        {
            try
            {
                T item = _dbContext.Set<T>().Find( keyValues );
                return Result.Ok<T>( item );
            }
            catch( Exception e )
            {
                return Result.Failure<T>( e.Message );
            }
        }

        public Result<IEnumerable<T>> GetAll()
        {
            try
            {
                IEnumerable<T> items = _dbContext.Set<T>().ToList();
                return Result<IEnumerable<T>>.Ok( items );
            }
            catch( Exception e )
            {
                return Result.Failure<IEnumerable<T>>( e.Message );
            }
        }

        public Result Remove(params object[] keyValues)
        {
            try
            {
                _dbContext.Remove( keyValues );
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch( Exception e )
            {
                return Result.Failure( e.Message );
            }
        }

        public Result Update(T item)
        {
            try
            {
                _dbContext.Update( item );
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch( Exception e )
            {
                return Result.Failure( e.Message );
            }
        }
    }
}