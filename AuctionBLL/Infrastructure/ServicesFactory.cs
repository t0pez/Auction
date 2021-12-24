using AuctionBLL.Services;
using AuctionDAL.Context;
using AuctionDAL.Repositories;
using AutoMapper;

namespace AuctionBLL.Infrastructure
{
    public class ServicesFactory : IServicesFactory
    {
        public IUsersService CreateUsersService()
        {
            return new UsersService(new UsersRepository(new AuctionContext()));
        }
    }
}