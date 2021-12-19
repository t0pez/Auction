using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface IIndividualUsersService
    {
        Task<IEnumerable<IndividualUserDto>> GetAllUsersAsync();
        Task<IndividualUserDto> GetUserByIdAsync(Guid id);
        Task<IndividualUserDto> CreateUserAsync(IndividualUserDto newUser);
        Task<IndividualUserDto> UpdateUserAsync(IndividualUserDto updated);
    }
}
