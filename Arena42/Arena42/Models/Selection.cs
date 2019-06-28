﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class Selection
    {
        public int Id { get; set; }

        public decimal Odds { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }
        public int MarketId { get; set; }
        public bool? Result { get; set; }

        public virtual ICollection<Bet> Bet { get; set; }

    }
}