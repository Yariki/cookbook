using Cookbook.BussinessLayer.Core;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.BussinessLayer.Blls
{
    public class BSRecipeBll : BSBussinessEntityBll<BSRecipe> , IBSRecipeBll
    {
        public BSRecipeBll()
        {
        }

        public BSRecipeBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}