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
        public Market Market { get; set; }

        public Selection ChosenSelection { get; set; }

        public Selection WinningSelection { get; set; }

        public bool IsWinning => ChosenSelection != null && WinningSelection != null && ChosenSelection.Id == WinningSelection.Id;
    }


}