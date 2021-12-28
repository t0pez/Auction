using AuctionBLL.Dto;

namespace AuctionBLL.Services
{
    public interface IStepLogger
    {
        LotStepLogDto Log(MoneyDto step, UserDto user, bool lotWasProlonged);
    }
}