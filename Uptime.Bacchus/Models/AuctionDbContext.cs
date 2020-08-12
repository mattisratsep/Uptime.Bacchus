using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uptime.Bacchus.Models
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<AuctionBet> AuctionBet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=auctionBets.db");
    }
}
