using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class Market
    {
        public string BetclicMarketId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public ICollection<Selection> Selections { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }

    //public class MarketResult : Market
    //{
    //    public int ChosenSelectionId { get; set; }

    //    public int WinningSelectionId { get; set; }


    //    public bool IsWinning { get
    //        {
    //            return ChosenSelectionId == WinningSelectionId;
    //        }
    //    }
    //}


}