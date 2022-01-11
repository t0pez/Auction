using System;

namespace AuctionDAL.Exceptions
{
    public class DataLayerException : Exception
    {
        public DataLayerException(string message) : base(message)
        {
        }
    }
}