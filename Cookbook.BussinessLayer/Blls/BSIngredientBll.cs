using Cookbook.BussinessLayer.Core;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.BussinessLayer.Blls
{
    public class BSIngredientBll : BSBussinessEntityBll<BSIngredient>, IBSIngredientBll
    {
        public BSIngredientBll()
        {
        }

        public BSIngredientBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}