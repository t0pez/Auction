using System;
using System.Linq;
using AuctionDAL.Models;

namespace AuctionDAL.Extensions.Models
{
    public static class UserExtensions
    {
        public static bool HasMoneyOfCurrency(this User user, int currency)
        {
            return user.Wallet.Money.Any(money => money.Currency == currency);
        }

        public static Money GetMoneyOfCurrency(this User user, int currency)
        {
            var money = user.Wallet.Money.FirstOrDefault(money => money.Currency == currency);

            if (money is null)
                throw new InvalidOperationException($"User does not have money of this currency {currency}");

            return money;
        }
        
        public static Money GetMoneyById(this User user, Guid id)
        {
            var money = user.Wallet.Money.FirstOrDefault(money => money.Id == id);

            if (money is null)
                throw new InvalidOperationException();
            
            return money;
        }
        
        public static bool HasEnoughMoney(this User user, Guid id, decimal amount)
        {
            var money = user.GetMoneyById(id);

            return money.Amount <= amount;
        }
        
        public static bool HasEnoughMoneyOfCurrency(this User user, int currency, decimal amount)
        {
            var money = user.GetMoneyOfCurrency(currency);

            return money.Amount >= amount;
        }

        public static void WriteOffMoney(this User user, int currency, decimal amount)
        {
            var money = user.GetMoneyOfCurrency(currency);

            money.Amount -= amount;
        }
        
        public static void WriteOffMoney(this User user, Money money, decimal amount)
        {
            if (user.Wallet.Money.Contains(money) == false)
                throw new InvalidOperationException();

            money.Amount -= amount;
        }
    }
}