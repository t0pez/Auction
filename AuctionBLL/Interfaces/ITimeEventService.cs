using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionBLL.Interfaces
{
    /// <summary>
    /// Service to provide event with a special key when the time has come
    /// </summary>
    /// <remarks>
    /// It is a dictionary with key and date
    /// Service checks every second if this date is over. Invokes event with it's key and removes pair from dictionary
    /// </remarks>
    /// <typeparam name="TKey">Key</typeparam>
    public interface ITimeEventService<TKey>
    {
        event Func<TKey, Task> TimeToInvoke;

        /// <summary>
        /// Adds pair
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="date">Invocation date</param>
        void Add(TKey key, DateTime date);

        /// <summary>
        /// Adds collection of pairs
        /// </summary>
        /// <param name="source"></param>
        void AddRange(IEnumerable<(TKey, DateTime)> source);

        /// <summary>
        /// Removes pair by key
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);

        /// <summary>
        /// Sets new invocation date for the key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="newDate">New invocation date</param>
        void SetNewDate(TKey key, DateTime newDate);

        /// <summary>
        /// Prolongs invocation date by adding TimeSpan to it
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="span">Prolongation time</param>
        void Prolong(TKey key, TimeSpan span);
    }
}
