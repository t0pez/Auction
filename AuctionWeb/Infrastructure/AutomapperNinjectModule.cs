using AuctionBLL.Infrastructure;
using AutoMapper;
using Ninject.Modules;

namespace AuctionWeb.Infrastructure
{
    public class AutomapperNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var mapper = new MapperConfiguration(expression =>
            {
                expression.AddProfile<BusinessLayerAutoMapperProfile>();
                expression.AddProfile<PresentationLayerAutoMapperProfile>();
            }).CreateMapper();

            Bind<IMapper>().ToConstant(mapper);
        }
    }
}