﻿using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }
        public virtual ICollection<TournamentUserOptinModel> OptinUsers { get; set; }
        public virtual ICollection<MarketModel> Markets { get; set; }
        public virtual ICollection<BetModel> Bets {get; set; }
        public virtual ICollection<TournamentWinnableModel> Winnables { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public DateTime ResultTimeUtc { get; set; }

    }
}
