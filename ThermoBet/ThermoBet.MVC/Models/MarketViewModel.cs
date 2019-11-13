using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class MarketViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom du market", Prompt = "Nom du market")]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTimeUtc { get; set; }

        public virtual List<SelectionViewModel> Selections { get; set; }

        public virtual int? WinningSelectionId { get; set; }
    }
}
