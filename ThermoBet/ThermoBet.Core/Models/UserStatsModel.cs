using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class UserStatsModel
    {
        public int UserId { get; set; }
        public int MonthlySwipesCount { get; set; }
        public int AllSwipesCount { get; set; }
        public int SucceedSwipesCount { get; set; }

    }
}
