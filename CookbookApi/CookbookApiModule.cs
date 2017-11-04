using AutoMapper;
using CookbookApi.Helpers;
using CookbookApi.Interfaces;
using Ninject.Modules;

namespace CookbookApi
{
    public class CookbookApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBSLogger>().To<BSLogger>();
            Bind<IMapper>().ToMethod(c => new Mapper(Mapper.Configuration));
        }
    }
}