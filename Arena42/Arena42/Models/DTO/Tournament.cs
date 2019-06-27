using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models.DTO
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public IEnumerable<Market> Market { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
    }
}