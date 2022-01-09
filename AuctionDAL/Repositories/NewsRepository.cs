using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AuctionContext _context;

        public NewsRepository(AuctionContext context)
        {
            _context = context;
        }
        private DbSet<News> News => _context.News;

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await News.ToListAsync();
        }

        public async Task<News> GetNewsByIdAsync(Guid id)
        {
            var item = await News.FirstOrDefaultAsync(lot => lot.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateNewsAsync(News news)
        {
            if (await News.AnyAsync(l => l.Id == news.Id))
                throw new ItemAlreadyExistsException(nameof(news));

            News.Add(news);
        }

        public async Task UpdateNewsAsync(News news)
        {
            var item = await GetNewsByIdAsync(news.Id);

            _context.Entry(item).CurrentValues.SetValues(news);
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            var news = await GetNewsByIdAsync(id);

            _context.News.Remove(news);
        }
    }
}