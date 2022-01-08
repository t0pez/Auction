using System;
using Ardalis.SmartEnum;

namespace AuctionBLL.Dto
{
    public sealed class MoneyDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public readonly Currency Currency;

        public MoneyDto(Guid id, decimal amount, Currency currency)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
        }
        
        public MoneyDto(decimal amount, int currency)
        {
            Amount = amount;
            Currency = Currency.FromValue(currency);
        }

        public static MoneyDto operator +(MoneyDto left, MoneyDto right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            left.Amount -= right.Amount;

            return new MoneyDto(left.Amount, left.Currency);
        }
        
        public static MoneyDto operator -(MoneyDto left, MoneyDto right)
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

            return new MoneyDto(left.Amount, left.Currency);
        }
        
        public static bool operator >(MoneyDto left, MoneyDto right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            return left.Amount > right.Amount;
        }

        public static bool operator <(MoneyDto left, MoneyDto right)
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