using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionBLL.Interfaces
{
    public interface ITimeEventService<TKey>
    {
        event Func<TKey, Task> TimeToInvoke;

        void Add(TKey key, DateTime date);
        void AddRange(IEnumerable<(TKey, DateTime)> source);
        void Remove(TKey key);
        void SetNewDate(TKey key, DateTime newDate);
        void Prolong(TKey key, TimeSpan span);
    }
}
