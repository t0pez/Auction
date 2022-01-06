using AuctionBLL.Services;
using Ninject.Modules;
using System.Web.Mvc;

namespace AuctionWeb.Infrastructure
{
    public class DependencyInjectionConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersService>().To<UsersService>();
            Bind<ILotsService>().To<LotsService>();
            Unbind<ModelValidatorProvider>();
        }
    }
}
