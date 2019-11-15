using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class ResultingMarketViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom du market", Prompt = "Nom du market")]
        public string Name { get; set; }
        
        public virtual List<ResultingSelectionViewModel> Selections { get; set; }

        public virtual int? WinningSelectionId { get; set; }
    }
}
