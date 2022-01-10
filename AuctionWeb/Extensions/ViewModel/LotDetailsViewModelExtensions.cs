using AuctionWeb.ViewModels.Lots;

namespace AuctionWeb.Extensions.ViewModel
{
    public static class LotDetailsViewModelExtensions
    {
        public static bool UserCanMakeStep(this LotDetailsViewModel lot, string userId)
        {
            if (lot.Owner.Id == userId)
                return false;

            if (lot.Acquirer is null)
                return true;
            return lot.Acquirer.Id != userId;
        }
    }
}