using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class TournamentViewModel
    {

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public virtual List<MarketViewModel> Markets { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTimeUtc { get; set; }
    }
}
