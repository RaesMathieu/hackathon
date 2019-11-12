using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public virtual ICollection<MarketModel> Markets { get; set; }

        public virtual ICollection<BetModel> Bets {get; set;}


        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
    }
}
