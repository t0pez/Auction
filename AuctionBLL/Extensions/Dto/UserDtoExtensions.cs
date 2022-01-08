using System.Linq;
using AuctionBLL.Dto;

namespace AuctionBLL.Extensions.Dto
{
    public static class UserDtoExtensions
    {
        public static bool HasMoneyOfCurrency(this UserDto user, Currency currency)
        {
            return user.Wallet.Money.Any(money => money.Currency == currency);
        }
    }
}