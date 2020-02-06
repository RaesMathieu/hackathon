using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class TournamentWinnableModel
    {
        public int Id { get; set; }
        public int NbGoodAnswer { get; set; }
        public int AmountOfWinnings { get; set; }
        public virtual TournamentModel Tournament { get; set; }
    }
}
