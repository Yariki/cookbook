using System.Collections.Generic;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.BussinessLayer.Interfaces
{
    public interface IBSEntityHistoryBll : IBSCoreBll<BSEntityHistory>
    {
        IEnumerable<T> GetHistoryForEntity<T>(int id) where T: class, IBSCoreEntity;
        void AddHistory<T>(T obj) where T : class, IBSCoreEntity;
    }
}