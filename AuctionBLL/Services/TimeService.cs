using System;
using System.Collections.Generic;

namespace AuctionBLL.Services
{
    public class TimeService<TKey> : ITimeService<TKey>
    {
        public TimeService()
        {
            _pairs = new Dictionary<TKey, DateTime>();
        }
     
        public event Action<TKey> TimeToInvoke;
        
        private readonly Dictionary<TKey, DateTime> _pairs;

        public void Add(TKey id, DateTime date)
        {
            if (_pairs.ContainsKey(id))
                throw new InvalidOperationException();
            
            _pairs.Add(id, date);
        }

        public void AddRange(IEnumerable<(TKey, DateTime)> source)
        {
            foreach (var tuple in source)
                Add(tuple.Item1, tuple.Item2);
        }

        public void Remove(TKey id)
        {
            if (_pairs.ContainsKey(id) == false)
                throw new InvalidOperationException();

            _pairs.Remove(id);
        }

        public void Prolong(TKey id, DateTime newDate)
        {
            if (_pairs.ContainsKey(id) == false)
                throw new InvalidOperationException();
            if (_pairs[id] > newDate)
                throw new InvalidOperationException();

            _pairs[id] = newDate;
        }

        public void Prolong(TKey id, TimeSpan span)
        {
            if (_pairs.ContainsKey(id) == false)
               throw new InvalidOperationException();

            _pairs[id] += span;
        }

        private void CheckDates()
        {
            // TODO
        }
        
        protected virtual void OnTimeToInvoke(TKey id)
        {
            TimeToInvoke?.Invoke(id);
        }
    }
}
