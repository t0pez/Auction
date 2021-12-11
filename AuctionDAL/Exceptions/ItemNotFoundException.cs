using System;

namespace AuctionDAL.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string argumentName) : base(argumentName)
        {
        }
    }
}