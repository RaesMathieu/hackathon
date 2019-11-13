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
        [Display(Name = "Nom du tournament", Prompt = "Nom du tournament")]

        public string Name { get; set; }

        [MaxLength(2500)]
        [Display(Name = "Description du tournament", Prompt = "Description du tournament")]
        public string Description { get; set; }

        public virtual List<MarketViewModel> Markets { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date de demarage du tournament", Prompt = "Date de demarage du tournament")]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date de fin du tournament", Prompt = "Date de fin du tournament")]

        public DateTime EndTimeUtc { get; set; }
    }
}
