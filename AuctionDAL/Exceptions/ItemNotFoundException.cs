using System;

namespace AuctionDAL.Exceptions
{
    public class ItemNotFoundException : DataLayerException
    {
        public ItemNotFoundException(string argumentName) : base(argumentName)
        {
        }
    }
}