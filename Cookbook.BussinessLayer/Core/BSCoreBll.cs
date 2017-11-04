using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Interfaces;
using Cookbook.Data.UnitOfWork;

namespace Cookbook.BussinessLayer.Core
{
    public abstract class BSCoreBll<TContext, TEntity> : IBSCoreBll<TEntity>
        where TContext: DbContext,IBSDataContext
        where TEntity : class,IBSCoreEntity
    {
        private IBSContextFactory contextFactory;

        protected BSCoreBll()
        {   
        }
        
        protected BSCoreBll(IBSContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public bool UserTransaction { get; set; }

        protected Action<TEntity> InsertAction { get; set; }

        protected Action<TEntity> UpdateAction { get; set; }

        protected Action<TEntity> DeleteAction { get; set; }


        public TEntity GetById(int Id)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var rep = unitOfWork.Repository<TEntity>();
                return rep.GetById(Id);
            }
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var rep = unitOfWork.Repository<TEntity>();
                return  await rep.GetByIdAsync(Id);
            }
        }

        public IEnumerable<TEntity> GetAll(params string[] includes)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var rep = unitOfWork.Repository<TEntity>();
                return rep.GetAll(includes);
            }
        }

        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter,
            params string[] includes)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.Repository<TEntity>().GetFiltered(filter, includes);
            }
        }

        public void Insert(TEntity entity)
        {
            if (InsertAction != null)
            {
                InsertAction(entity);
            }

            RepositoryAction(rep => rep.Insert(entity));
        }


        public void Update(TEntity entity)
        {
            if (UpdateAction != null)
            {
                UpdateAction(entity);
            }
            RepositoryAction(r => r. Update(entity));
        }


        public void Delete(TEntity entity)
        {
            if (DeleteAction != null)
            {
                DeleteAction(entity);
            }
            RepositoryAction(r => r.Delete(entity));
        }
        
        protected void RepositoryAction(Action<IBSCoreRepository<TEntity>> action)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                try
                {
                    if (UserTransaction)
                    {
                        unitOfWork.BeginTransaction();
                    }
                    var repository = unitOfWork.Repository<TEntity>();

                    action?.Invoke(repository);
                    unitOfWork.SaveChanges();

                    if (UserTransaction)
                    {
                        unitOfWork.Commit();
                    }
                }
                catch (Exception)
                {
                    if (UserTransaction) 
                    {
                        unitOfWork.Rollback();
                    }
                    throw;
                }
            }
        }
        
        protected TContext CreateContext()
        {
            return contextFactory == null
                ? Activator.CreateInstance<TContext>()
                : contextFactory.CreateContext<TContext>();
        }

        protected IBSUnitOfWork CreateUnitOfWork()
        {
            return contextFactory == null
                ? new BSUnitOfWork(CreateContext())
                : contextFactory.CreateUnitOfWork(CreateContext());
        }

    }
}