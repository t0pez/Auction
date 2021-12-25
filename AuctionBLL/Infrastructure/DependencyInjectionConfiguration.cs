using AuctionDAL.Repositories;
using Ninject.Modules;

namespace AuctionBLL.Infrastructure
{
    public class DependencyInjectionConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<ILotsRepository>().To<LotsRepository>();
        }
    }
}