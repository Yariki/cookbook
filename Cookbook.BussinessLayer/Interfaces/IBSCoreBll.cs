using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cookbook.Data.Context;
using Cookbook.Data.Interfaces;

namespace Cookbook.BussinessLayer.Interfaces
{
    public interface IBSCoreBll<TEntity>
        where TEntity: class, IBSCoreEntity
    {
        bool UserTransaction { get; set; }
        TEntity GetById(int Id);
        Task<TEntity> GetByIdAsync(int Id);
        IEnumerable<TEntity> GetAll(params string[] includes);

        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter,
            params string[] includes);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}