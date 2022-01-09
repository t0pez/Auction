using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task<NewsDto> GetNewsByIdAsync(Guid id);
        Task<NewsDto> CreateNewsAsync(NewsDto lot);
        Task<NewsDto> UpdateNewsAsync(NewsDto updated);
        Task DeleteNewsAsync(Guid id);
    }
}