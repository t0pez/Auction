using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.ViewModels;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface ILotsService
    {
        Task<IEnumerable<LotViewModel>> GetAllLotsAsync();
        Task<IEnumerable<LotViewModel>> GetAllOpenedLotsAsync();
        Task<IEnumerable<LotViewModel>> GetAllClosedLotsAsync();
        Task<LotViewModel> GetLotByIdAsync(Guid id);
        Task<LotViewModel> CreateLotAsync(LotViewModel lot);
        Task<LotViewModel> AddParticipantAsync(LotViewModel lot, User user);
        Task<LotViewModel> SetLotActualPriceAsync(LotViewModel lot, User user, MoneyViewModel newPrice);
        Task<LotViewModel> OpenLotAsync(LotViewModel lot);
        Task<LotViewModel> CloseLotAsync(LotViewModel viewModel);
    }
}
