using System;
using System.Data.Entity;
using Cookbook.Data.Interfaces;
using Cookbook.Data.UnitOfWork;

namespace Cookbook.Data
{
    public class BSContextFactory : IBSContextFactory
    {
        public T CreateContext<T>() where T : IBSDataContext
        {
            return Activator.CreateInstance<T>();
        }

        public IBSUnitOfWork CreateUnitOfWork(IBSDataContext context)
        {
            return new BSUnitOfWork((DbContext)context);
        }
    }
}