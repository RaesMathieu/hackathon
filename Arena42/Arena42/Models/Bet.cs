using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arena42.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TournamentId { get; set; }
        public int MarketId { get; set; }
        public int SelectionId { get; set; }
        public bool Result { get; set; }
        public int UserId { get; set; }
    }
}