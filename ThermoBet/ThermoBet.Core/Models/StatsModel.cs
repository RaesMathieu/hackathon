using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class StatsModel
    {
        public UserStatsModel UserStats { get; set; }
        public IEnumerable<StatsPositionModel> Positions { get; set; }
    }
}
