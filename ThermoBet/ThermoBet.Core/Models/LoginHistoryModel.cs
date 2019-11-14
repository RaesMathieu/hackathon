using System;

namespace ThermoBet.Core.Models
{
    public class LoginHistoryModel
    {
        public int Id { get; set; }

        public virtual UserModel User { get; set; }

        public DateTime LoginDateUtc { get; set; }
    }
}
