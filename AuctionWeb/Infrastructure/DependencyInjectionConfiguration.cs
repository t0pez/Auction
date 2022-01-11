using AuctionBLL.Services;
using Ninject.Modules;
using System.Web.Mvc;
using AuctionBLL.Interfaces;

namespace AuctionWeb.Infrastructure
{
    public class DependencyInjectionConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersService>().To<UsersService>();
            Bind<INewsService>().To<NewsService>();
            Bind<ILotsService>().To<LotsService>().InSingletonScope();
        }
    }
}
