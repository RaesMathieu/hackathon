using System;
using System.Collections.Generic;

namespace ThermoBet.API.Controllers
{
    public class Stats
    {
        public UserStats UserStats { get; set; }
        public IEnumerable<StatsPosition> Positions { get; set; }
    }
}
