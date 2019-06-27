using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class TournamentResult : Tournament
    {
        public List<MarketResult> MarketResults { get; set; }
    }
}