using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuctionWeb.Infrastructure;
using Ninject;
using Ninject.Web.Mvc;

namespace AuctionWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var bllConfig = new AuctionBLL.Infrastructure.DependencyInjectionConfiguration();
            var plConfig = new DependencyInjectionConfiguration();
            var kernel = new StandardKernel(bllConfig, plConfig);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}