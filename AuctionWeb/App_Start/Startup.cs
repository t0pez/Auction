using System.Web.UI.WebControls;
using AuctionBLL.Services;
using AuctionWeb;
using AuctionWeb.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace AuctionWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = CreateKernel();

            app.UseNinject(() => kernel);

            app.CreatePerOwinContext(() => kernel.Get<IUsersService>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Users/LogIn"),
                LogoutPath = new PathString("/Users/LogOut")
            });
        }

        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                var businessLayerConfiguration = new AuctionBLL.Infrastructure.DependencyInjectionConfiguration();
                var presentationLayerConfiguration = new AuctionWeb.Infrastructure.DependencyInjectionConfiguration();
                var automapperConfiguration = new AutomapperNinjectModule();
                kernel.Load(businessLayerConfiguration, presentationLayerConfiguration, automapperConfiguration);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

    }
}
