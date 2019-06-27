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

        public IEnumerable<Selection> Selections { get; set; }
    }

    public class MarketResult : Market
    {
        public int ChoosenSelectionId { get; }

        public int WinSelectionId { get; }


        public bool IsWinning { get
            {
                return ChoosenSelectionId == WinSelectionId;
            }
        }
    }


}