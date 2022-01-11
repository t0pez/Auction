namespace AuctionDAL.Exceptions
{
    public class ItemAlreadyExistsException : DataLayerException
    {
        public ItemAlreadyExistsException(string paramName) : base(paramName)
        {
        }
    }
}