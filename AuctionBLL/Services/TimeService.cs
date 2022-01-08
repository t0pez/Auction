using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionBLL.Services
{
    public class TimeService<TKey> : ITimeService<TKey>
    {
        public event Action<TKey> TimeToInvoke;
        
        private readonly Dictionary<TKey, DateTime> _pairs;
        
        public TimeService()
        {
            _pairs = new Dictionary<TKey, DateTime>();
            
            CheckForInvoke();
        }

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

        public void SetNewDate(TKey id, DateTime newDate)
        {
            if (_pairs.ContainsKey(id) == false)
                throw new InvalidOperationException();
            if (_pairs[id] > newDate)
                throw new InvalidOperationException(); // TODO : Custom Exception

            _pairs[id] = newDate;
        }

        public void Prolong(TKey id, TimeSpan span)
        {
            if (_pairs.ContainsKey(id) == false)
               throw new InvalidOperationException();

            _pairs[id] += span;
        }

        private void CheckForInvoke()
        {
            Task.Factory.StartNew(async () =>
            {
                do
                {
                    foreach (var pair in _pairs)
                        if (pair.Value <= DateTime.Now)
                            OnTimeToInvoke(pair.Key);

                    await Task.Delay(60_000);
                } while (true);
            });
        }
        
        protected virtual void OnTimeToInvoke(TKey id)
        {
            TimeToInvoke?.Invoke(id);
        }
    }
}
