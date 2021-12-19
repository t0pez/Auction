using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Logs;

namespace AuctionDAL.Repositories
{
    public interface ILogsRepository<TLog> where TLog : Log 
    {
        // TODO: hierarchy and implementation
        Task<IEnumerable<TLog>> GetAllLotsAsync();
        Task<TLog> GetAllLotByIdAsync(Guid id);
        Task CreateLogAsync(TLog log);
    }
}