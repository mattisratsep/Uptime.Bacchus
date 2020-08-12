using System.Collections.Generic;
using System.Threading.Tasks;
using Uptime.Bacchus.Models;

namespace Uptime.Bacchus.Services
{
    public interface IAuctionService
    {
        Task<List<AuctionItem>> GetAuctionData();
        Task InsertAuctionBet(AuctionBet auctionBet);
        Task<List<AuctionBet>> GetAllBets();
    }
}
