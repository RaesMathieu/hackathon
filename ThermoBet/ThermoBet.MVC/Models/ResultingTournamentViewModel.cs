using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class ResultingTournamentViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nom du tournament", Prompt = "Nom du tournament")]

        public string Name { get; set; }


        public virtual List<ResultingMarketViewModel> Markets { get; set; }
    }
}
