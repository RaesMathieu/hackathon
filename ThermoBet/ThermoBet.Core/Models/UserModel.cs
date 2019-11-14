using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public bool IsAdmin { get; set; }
        public int GlobalPoints { get; set; }
        public int CurrentPoints { get; set; }
        public virtual ICollection<BetModel> Bets { get; set; }
    }
}
