using Cookbook.BussinessLayer.Core;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.BussinessLayer.Blls
{
    public class BSEntityHistoryBll : BSBussinessEntityBll<BSEntityHistory>, IBSEntityHistoryBll
    {
        public BSEntityHistoryBll()
        {
        }

        public BSEntityHistoryBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}