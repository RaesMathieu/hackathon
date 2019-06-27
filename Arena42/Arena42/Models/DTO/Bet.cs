using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models.DTO
{
    public class Bet
    {
        public int TournamentId { get; set; }
        public int MarketId { get; set; }
        public int SelectionId { get; set; }
    }
}