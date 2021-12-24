using AuctionBLL.Services;

namespace AuctionBLL.Infrastructure
{
    public interface IServicesFactory
    {
        IUsersService CreateUsersService();
    }
}