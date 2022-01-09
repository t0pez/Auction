using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(Guid id);
        Task CreateNewsAsync(News lot);
        Task UpdateNewsAsync(News updated);
        Task DeleteNewsAsync(Guid id);
    }
}