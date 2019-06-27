using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models.DTO
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public IEnumerable<Selection> Selections { get; set; }
    }

    public class MarketResult
    {
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public IEnumerable<Selection> Selections { get; set; }

        public int ChosenSelectionId { get; set; }

        public int? WinningSelectionId { get; set; }


        public bool IsWinning { get
            {
                return ChosenSelectionId == WinningSelectionId;
            }
        }
    }


}