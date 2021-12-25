using AuctionBLL.Services;
using Ninject.Modules;

namespace AuctionWeb.Infrastructure
{
    public class DependencyInjectionConfiguration : AuctionBLL.Infrastructure.DependencyInjectionConfiguration
    {
        public override void Load()
        {
            base.Load();
            Bind<IUsersService>().To<UsersService>();
            Bind<ILotsService>().To<LotsService>();
        }
    }
}