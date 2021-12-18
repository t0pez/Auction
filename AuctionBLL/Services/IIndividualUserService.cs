using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.ViewModels;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface IIndividualUserService
    {
        Task<IEnumerable<IndividualUserViewModel>> GetAllUsersAsync();
        Task<IndividualUserViewModel> GetUserByIdAsync(Guid id);
        Task<IndividualUserViewModel> CreateUserAsync(IndividualUserViewModel newUser);
        Task<IndividualUserViewModel> UpdateUserAsync(IndividualUserViewModel updated);
    }
}
