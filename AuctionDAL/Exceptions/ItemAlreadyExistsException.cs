namespace AuctionDAL.Exceptions
{
    public class ItemAlreadyExistsException : DataLayerException
    {
        public ItemAlreadyExistsException(string message) : base(message)
        {
        }
    }
}