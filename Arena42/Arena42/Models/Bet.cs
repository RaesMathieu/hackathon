using System;

namespace Arena42.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Tournament Tournament { get; set; }
        public Market Market { get; set; }
        public Selection Selection { get; set; }
        public User User { get; set; }
        public bool? Result { get; set; }
    }
}