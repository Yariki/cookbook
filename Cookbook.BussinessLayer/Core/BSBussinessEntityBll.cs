using Cookbook.Data.Context;
using Cookbook.Data.Interfaces;

namespace Cookbook.BussinessLayer.Core
{
    public abstract class BSBussinessEntityBll<TEntity> : BSCoreBll<CookbookDbContext,TEntity> where TEntity: class , IBSCoreEntity
    {
        protected BSBussinessEntityBll()
        {
        }

        protected BSBussinessEntityBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}