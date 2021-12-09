using System;
using Ardalis.SmartEnum;

namespace AuctionDAL
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public readonly Currency Currency;

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money operator +(Money left, Money right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));
            if (right is null)
                throw new ArgumentNullException(nameof(right));
            
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Currency types are different");

            left.Amount -= right.Amount;

            return new Money(left.Amount, left.Currency);
        }
        
        public static Money operator -(Money left, Money right)
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

            return new Money(left.Amount, left.Currency);
        }

        void Foo()
        {
            var eur = Currency.Eur;
            var a = new Money(100, Currency.Eur);
            var b = new Money(200, eur);
            var c = new Money(200, Currency.Uah);

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
    }
}