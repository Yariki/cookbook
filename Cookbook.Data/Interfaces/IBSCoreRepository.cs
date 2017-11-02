using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cookbook.Data.Interfaces
{
    public interface IBSCoreRepository<T> where T: class, IBSCoreEntity
    {
        DbSet<T> Set { get; }
        Task<IEnumerable<T>> GetAllAsync(params string[] includes);
        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, params string[] includes);
        Task<T> GetByIdAsync(int id);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
    }
}