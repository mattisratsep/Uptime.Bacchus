using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uptime.Bacchus.Models;
using Uptime.Bacchus.Services;

namespace Uptime.Bacchus.Controllers
{
    [Route("api/[controller]")]
    public class AuctionController : Controller
    {
        private IAuctionService AuctionService { get; set; }

        public AuctionController(IAuctionService auctionService)
        {
            AuctionService = auctionService;
        }

        /// <summary>
        /// Get all ongoing auctions
        /// </summary>
        /// <returns>List of auction items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAuctionItems()
        { 
            return Ok(await AuctionService.GetAuctionData());
        }

        /// <summary>
        /// Get all bets made
        /// </summary>
        /// <returns>List of auction bets</returns>
        [HttpGet("getAllBets")]
        public async Task<IActionResult> GetAllBets()
        {
            return Ok(await AuctionService.GetAllBets());
        }

        /// <summary>
        /// Saves auction bet to the database
        /// </summary>
        /// <param name="auctionBet"></param>
        [HttpPost("makeBet")]
        public IActionResult SaveAuctionBet([FromBody] AuctionBet auctionBet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            return Ok(AuctionService.InsertAuctionBet(auctionBet));
        }

    }
}
