namespace AuctionDAL.Models
{
    public class Lot : BaseModel
    {
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}