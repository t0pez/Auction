using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionBLL.Services
{
    public class TimeEventService<TKey> : ITimeEventService<TKey>
    {
        public event Func<TKey, Task> TimeToInvoke;

        private readonly object _syncRoot = new();
        private readonly ConcurrentDictionary<TKey, DateTime> _pairs;
        private readonly List<TKey> _itemsToInvoke;

        public TimeEventService()
        {
            _pairs = new ConcurrentDictionary<TKey, DateTime>();
            _itemsToInvoke = new List<TKey>();

            Task.Run(CheckForInvoke);
        }

        public void Add(TKey id, DateTime date)
        {
            if (_pairs.ContainsKey(id))
                throw new InvalidOperationException();

            _pairs.TryAdd(id, date);
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

            _pairs.TryRemove(id, out var d);
        }

        public void SetNewDate(TKey id, DateTime newDate)
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

        private Task SingleCheckForInvoke => new(async () =>
        {
            foreach (var pair in _pairs.Where(pair => pair.Value <= DateTime.Now))
                await OnTimeToInvoke(pair.Key);

        });

        private async void CheckForInvoke()
        {
            while (true)
            {
                var checkForInvokeTask = SingleCheckForInvoke;

                checkForInvokeTask.Start();
                checkForInvokeTask.Wait();
                await Task.Delay(1_000);
            }
        }

        protected async Task OnTimeToInvoke(TKey id)
        {
            var handler = TimeToInvoke;

            if (handler is null)
            {
                return;
            }

            _pairs.TryRemove(id, out var d);

            var invocationList = handler.GetInvocationList();
            var handlerTasks = new Task[invocationList.Length];

            for (var i = 0; i < invocationList.Length; i++)
            {
                handlerTasks[i] = ((Func<TKey, Task>) invocationList[i])(id);
            }

            await Task.WhenAll(handlerTasks);
        }
    }
}
