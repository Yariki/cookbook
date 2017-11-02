using Cookbook.BussinessLayer.Blls;
using Cookbook.BussinessLayer.Interfaces;
using Ninject.Modules;

namespace Cookbook.BussinessLayer
{
    public class CookbookBussinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBSRecipeBll>().To<BSRecipeBll>();
            Bind<IBSEntityHistoryBll>().To<BSEntityHistoryBll>();
        }
    }
}