using System;
using System.Collections.Generic;

namespace ThermoBet.Core.Models
{
    public class SelectionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }
        
        public bool IsYes { get; set; }

        public virtual MarketModel Market { get; set; }

        public virtual ICollection<BetModel> Bets { get; set; }
    }
}
