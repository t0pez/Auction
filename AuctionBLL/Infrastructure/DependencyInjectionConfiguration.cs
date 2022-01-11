using AuctionDAL;
using Ninject.Modules;

namespace AuctionBLL.Infrastructure
{
    public class DependencyInjectionConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}