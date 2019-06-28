using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public virtual ICollection<Selection> Selections { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }

        public virtual ICollection<Bet> Bet { get; set; }
    }


}