using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class LeaderBoard
    {
        public int TournamentId { get; set; }
        public List<LeaderBoardRanking> Ranking { get; set; }
    }
}