using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Uptime.Bacchus.Models;

namespace Uptime.Bacchus.Services
{
    public class AuctionService : IAuctionService
    {
        private IConfiguration Configuration { get; set; }
        public AuctionService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<List<AuctionItem>> GetAuctionData()
        {
            try
            {
                List<AuctionItem> items = new List<AuctionItem>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Configuration["UpdtimeAuctionEndpoint"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("api/Auction");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        items.AddRange(JsonConvert.DeserializeObject<List<AuctionItem>>(jsonResponse));

                    }
                }

                return DateTimeToLocal(items);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public async Task InsertAuctionBet(AuctionBet auctionBet)
        {
            try
            {
                using (AuctionDbContext db = new AuctionDbContext())
                {
                    await db.AuctionBet.AddAsync(auctionBet);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {

                throw exception;
            }
            

        }

        public async Task<List<AuctionBet>> GetAllBets()
        {
            using (AuctionDbContext db = new AuctionDbContext())
            {
                return await db.AuctionBet.OrderByDescending(o=>o.ProductId).ThenByDescending(t=>t.Bet).ToListAsync();
            }            
        }

        private List<AuctionItem> DateTimeToLocal(List<AuctionItem> auctionItems)
        {
            foreach (var auctionItem in auctionItems)
            {
                auctionItem.BiddingEndDate = auctionItem.BiddingEndDate.ToLocalTime();
            }
            return auctionItems;
        }
    }       
}
