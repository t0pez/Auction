using AuctionDAL.Interfaces;

namespace AuctionDAL.Models
{
    public class User : BaseModel, IHaveWallet
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
