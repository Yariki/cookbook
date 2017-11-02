using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Threading.Tasks;
using Cookbook.Data.Context;
using Cookbook.Data.Core;
using Cookbook.Data.Interfaces;

namespace Cookbook.Data.UnitOfWork
{
    public class BSUnitOfWork : IBSUnitOfWork
    {
        private DbContext context;
        private Hashtable repositories;

        private DbTransaction transaction;
        private ObjectContext objectContext;


        private bool isDisposed;

        public BSUnitOfWork(DbContext context)
        {
            this.context = context;
            context.Database.Connection.Open();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IBSCoreRepository<TEntity> Repository<TEntity>() where TEntity : class, IBSCoreEntity
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }
            var name = typeof(TEntity).Name;
            if (repositories.ContainsKey(name))
            {
                return repositories[name] as IBSCoreRepository<TEntity>;
            }
            var type = typeof(BSCoreRepository<>);
            repositories.Add(name, Activator.CreateInstance(type.MakeGenericType(typeof(TEntity)),context));
            return (IBSCoreRepository<TEntity>) repositories[name];
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (isDisposing)
            {
                try
                {
                    if (objectContext != null && objectContext.Connection.State == ConnectionState.Open)
                    {
                        objectContext.Connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing
                }
                
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                if (repositories != null)
                {
                    repositories.Clear();
                    repositories = null;
                }
            }
            isDisposed = true;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            objectContext = ((IObjectContextAdapter)context).ObjectContext;
            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
            }

            transaction = objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            transaction.Rollback();
        }


    }
}