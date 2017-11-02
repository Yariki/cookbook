using System;
using System.Data;
using System.Threading.Tasks;
using Cookbook.Data.Core;

namespace Cookbook.Data.Interfaces
{
    public interface IBSUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
        IBSCoreRepository<TEntity> Repository<TEntity>() where TEntity : class,IBSCoreEntity;
    }
}