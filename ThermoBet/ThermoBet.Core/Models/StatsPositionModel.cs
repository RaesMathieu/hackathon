using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class StatsPositionModel
    {
        public int UserId { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public string Pseudo { get; set; }
        public string Avatar { get; set; }
    }
}
