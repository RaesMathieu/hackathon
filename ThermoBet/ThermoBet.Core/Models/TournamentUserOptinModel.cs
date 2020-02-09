using System;

namespace ThermoBet.Core.Models
{
    public class TournamentUserOptinModel
    {
        public int TournamentId { get; set; }
        public int UserId { get; set; }

        public virtual TournamentModel Tournament { get; set; }
        public virtual UserModel User { get; set; }

        public DateTime DateUtc { get; set; }
    }
}
