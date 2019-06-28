using System;

namespace Arena42.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual Market Market { get; set; }
        public virtual Selection Selection { get; set; }
        public virtual User User { get; set; }
        public bool? Result { get; set; }
    }
}