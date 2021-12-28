using System;
using System.Collections.Generic;

namespace AuctionBLL.Services
{
    public interface ITimeService<TKey>
    {
        event Action<TKey> TimeToInvoke;

        void Add(TKey key, DateTime date);
        void AddRange(IEnumerable<(TKey, DateTime)> source);
        void Remove(TKey key);
        void Prolong(TKey key, DateTime newDate);
    }
}
