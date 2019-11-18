using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class MarketModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public virtual ICollection<SelectionModel> Selections { get; set; }
        public virtual ICollection<BetModel> Bets { get; set; }
        public virtual TournamentModel Tournament { get; set; }
        public virtual int? WinningSelectionId { get; set; }
    }
}
