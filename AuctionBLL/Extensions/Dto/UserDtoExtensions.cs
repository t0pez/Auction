using System;
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

        public static MoneyDto GetMoneyById(this UserDto user, Guid moneyId)
        {
            var money = user.Wallet.Money.FirstOrDefault(money => money.Id == moneyId);

            if (money is null)
                throw new InvalidOperationException("Money of id not found");

            return money;
        }
    }
}