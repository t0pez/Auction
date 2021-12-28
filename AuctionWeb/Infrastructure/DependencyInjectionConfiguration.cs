using AuctionBLL.Infrastructure;
using AuctionBLL.Services;
using AutoMapper;
using Ninject.Modules;

namespace AuctionWeb.Infrastructure
{
    public class DependencyInjectionConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersService>().To<UsersService>();
            Bind<ILotsService>().To<LotsService>();


            var mapper = new MapperConfiguration(expression =>
            {
                expression.AddProfile<BusinessLayerAutoMapperProfile>();
                expression.AddProfile<PresentationLayerAutoMapperProfile>();
            }).CreateMapper();

            Bind<IMapper>().ToConstant(mapper);

        }
    }
}
