using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;
using AutoMapper;

namespace AuctionBLL.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            var unmappedItems = await _unitOfWork.NewsRepository.GetAllNewsAsync();

            var mappedItems = _mapper.Map<IEnumerable<NewsDto>>(unmappedItems);

            return mappedItems;
        }

        public async Task<NewsDto> GetNewsByIdAsync(Guid id)
        {
            try
            {
                var unmappedItem = await _unitOfWork.NewsRepository.GetNewsByIdAsync(id);

                var mappedItem = _mapper.Map<NewsDto>(unmappedItem);

                return mappedItem;
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task<NewsDto> CreateNewsAsync(NewsDto news)
        {
            if (news is null)
                throw new ArgumentNullException(nameof(news));

            news.Id = Guid.NewGuid();
            news.DateOfCreation = DateTime.Now;

            var mapped = _mapper.Map<News>(news);

            try
            {
                await _unitOfWork.NewsRepository.CreateNewsAsync(mapped);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (ItemAlreadyExistsException) // TODO: ? Remove this (id sets as new)
            {
                throw;
            }

            return news;
        }

        public async Task<NewsDto> UpdateNewsAsync(NewsDto news)
        {
            if (news is null)
                throw new ArgumentNullException(nameof(news));

            var mapped = _mapper.Map<News>(news);

            try
            {
                await _unitOfWork.NewsRepository.UpdateNewsAsync(mapped);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (ItemNotFoundException)
            {
                throw;
            }

            return news;
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            try
            {
                await _unitOfWork.NewsRepository.DeleteNewsAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }
    }
}