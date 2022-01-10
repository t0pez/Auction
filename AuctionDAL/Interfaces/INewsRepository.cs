using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Interfaces
{
    /// <summary>
    /// Repository for entity news
    /// </summary>
    public interface INewsRepository
    {
        /// <summary>
        /// Gets all news
        /// </summary>
        /// <returns>News collection</returns>
        Task<IEnumerable<News>> GetAllNewsAsync();

        /// <summary>
        /// Gets news by id
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns>News</returns>
        /// <exception cref="ItemNotFoundException">When news not found</exception>
        Task<News> GetNewsByIdAsync(Guid id);

        /// <summary>
        /// Creates news
        /// </summary>
        /// <param name="news">News</param>
        /// <returns></returns>
        Task CreateNewsAsync(News news);

        /// <summary>
        /// Updates news
        /// </summary>
        /// <param name="updated">Updated news</param>
        /// <returns></returns>
        /// <exception cref="ItemNotFoundException">When news not found</exception>
        Task UpdateNewsAsync(News updated);

        /// <summary>
        /// Deletes news
        /// </summary>
        /// <param name="id">News to delete id</param>
        /// <returns></returns>
        Task DeleteNewsAsync(Guid id);
    }
}