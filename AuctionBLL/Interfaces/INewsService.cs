using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Interfaces
{
    /// <summary>
    /// Service for work with application news
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Gets all news
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        
        /// <summary>
        /// Gets news by id
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When news not found</exception>
        Task<NewsDto> GetNewsByIdAsync(Guid id);

        /// <summary>
        /// Creates news
        /// </summary>
        /// <param name="news">News create model</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When already exists</exception>
        Task<NewsDto> CreateNewsAsync(NewsDto news);

        /// <summary>
        /// Updates news 
        /// </summary>
        /// <param name="updated">Updated news</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When news not found</exception>
        Task<NewsDto> UpdateNewsAsync(NewsDto updated);

        /// <summary>
        /// Deletes news
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When news not found</exception>
        Task DeleteNewsAsync(Guid id);
    }
}