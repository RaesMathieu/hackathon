using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class AllResultsViewModel
    {
        public List<ResultViewModel> Results { get; set; }
    }
    public class ResultViewModel
    {
        public DateTime BetDate { get; set; }
        public int UserId { get; set; }
        public int? ChosenSelectionId { get; set; }
        public int? WinningSelectionId { get; set; }
        public string Pseudo { get; set; }
    }
}
