using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class MarketViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTimeUtc { get; set; }
        public virtual List<SelectionViewModel> Selections { get; set; }
    }
}
