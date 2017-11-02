using System.Data.Entity;

namespace Cookbook.Data.Interfaces
{
    public interface IBSContextFactory
    {
        T CreateContext<T>() where T : IBSDataContext;

        IBSUnitOfWork CreateUnitOfWork(IBSDataContext context);
    }
}