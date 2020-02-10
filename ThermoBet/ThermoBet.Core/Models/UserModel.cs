using System.Collections.Generic;

namespace ThermoBet.Core.Models
{

    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Pseudo { get; set; }

        public string Avatar { get; set; }

        public string HashPassword { get; set; }

        public bool IsAdmin { get; set; }

        public int GlobalPoints { get; set; }

        public int CurrentPoints { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string BetclicUserName { get; set; }


        public virtual ICollection<BetModel> Bets { get; set; }

        public virtual ICollection<TournamentUserOptinModel> OptinTournament { get; set; }

        public virtual ICollection<LoginHistoryModel> LoginDate { get; set; }
    }
}
