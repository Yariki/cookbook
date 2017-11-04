using System;
using Cookbook.Data.Context;
using Cookbook.Data.Interfaces;

namespace Cookbook.BussinessLayer.Core
{
    public abstract class BSBussinessEntityBll<TEntity> : BSCoreBll<CookbookDbContext, TEntity>
        where TEntity : class, IBSCoreEntity
    {
        protected BSBussinessEntityBll()
        {
            Initialize();
        }

        protected BSBussinessEntityBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
            Initialize();
        }

        private void Initialize()
        {
            InsertAction = entity =>
            {
                entity.Created = DateTime.Now;
            };
        }
    


    }
}