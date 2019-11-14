using System;
using System.Collections.Generic;

namespace ThermoBet.MVC.Models
{
    public class DashboardViewModel
    {
        
        public Dictionary<DateTime, int> NbUniqueLoginByDay { get; set; }

        public Dictionary<DateTime, int> NbLoginByDay { get; set; }
    }
}
