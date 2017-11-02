using System;
using System.Threading.Tasks;

namespace Cookbook.Data.Interfaces
{
    public interface IBSDataContext : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}