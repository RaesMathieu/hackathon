using System;

namespace ThermoBet.Core.Models
{
    public class BetModel
    {
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public int MarketId { get; set; }

        public virtual TournamentModel Tournament { get; set; }
        public virtual UserModel User { get; set; }
        public virtual MarketModel Market { get; set; }
        public virtual SelectionModel Selection { get; set; }

        public DateTime DateUtc { get; set; }
    }
}
