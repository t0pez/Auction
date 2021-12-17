using System;
using Ardalis.SmartEnum;

namespace AuctionBLL.ViewModels
{
    public sealed class MoneyViewModel
    {
        public decimal Amount { get; private set; }
        public readonly Currency Currency;

        public MoneyViewModel(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        
        public MoneyViewModel(decimal amount, int currency)
        {
            Amount = amount;
            Currency = Currency.FromValue(currency);
        }

        public static MoneyViewModel operator +(MoneyViewModel left, MoneyViewModel right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            left.Amount -= right.Amount;

            return new MoneyViewModel(left.Amount, left.Currency);
        }
        
        public static MoneyViewModel operator -(MoneyViewModel left, MoneyViewModel right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");
            if (left.Amount - right.Amount < 0)
                throw new InvalidOperationException("Not enough money amount");

            left.Amount -= right.Amount;

            return new MoneyViewModel(left.Amount, left.Currency);
        }
        
        public static bool operator >(MoneyViewModel left, MoneyViewModel right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            return left.Amount > right.Amount;
        }

        public static bool operator <(MoneyViewModel left, MoneyViewModel right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            return left.Amount < right.Amount;
        }
    }

    public sealed class Currency : SmartEnum<Currency>
    {
        public static readonly Currency Uah = new(1, "UAH", "Ukrainian hryvnas", "₴");
        public static readonly Currency Usd = new(2, "USD", "US dollars", "$");
        public static readonly Currency Eur = new(3, "EUR", "Euro", "€");
        public static readonly Currency Rub = new(4, "RUB", "Russian ruble", "₽");
        
        public string IsoName { get; }
        public string FullName { get; }
        public string Symbol { get; }

        private Currency(int number, string isoName, string fullName, string symbol) : base(isoName, number)
        {
            IsoName = isoName;
            FullName = fullName;
            Symbol = symbol;
        }

        public static bool operator ==(Currency left, Currency right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            
            return left.Value == right.Value;
        }
        
        public static bool operator !=(Currency left, Currency right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            
            return left.Value != right.Value;
        }
    }
}