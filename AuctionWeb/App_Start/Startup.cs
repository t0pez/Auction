using AuctionBLL.Infrastructure;
using AuctionBLL.Services;
using AuctionWeb;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace AuctionWeb
{
    public class Startup
    {
        private readonly IServicesFactory _servicesFactory = new ServicesFactory();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUsersService>(_servicesFactory.CreateUsersService);
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Users/LogIn"),
                LogoutPath = new PathString("/Users/LogOut")
            });
        }
    }
}
