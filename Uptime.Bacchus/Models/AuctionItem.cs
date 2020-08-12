using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uptime.Bacchus.Models
{
    public class AuctionItem
    {
        
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public DateTime BiddingEndDate { get; set; }
    }
    public class AuctionBet
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Bet { get; set; }
        [Required]
        public double BetTimestampMs { get; set; }
        [Key]
        [Required]
        public string ProductId { get; set; }
    }
}
