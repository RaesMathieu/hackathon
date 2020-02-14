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

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Code du tournament", Prompt = "Code du tournament")]
        public string Code { get; set; }

        [MaxLength(2500)]
        [Display(Name = "Description du tournament", Prompt = "Description du tournament")]
        public string Description { get; set; }

        public virtual List<MarketViewModel> Markets { get; set; }

        public virtual List<TournamentWinnableViewModel> Winnables { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0: dd/MM/yy hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de demarage du tournament", Prompt = "Date de demarage du tournament")]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0: dd/MM/yy hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de fin du tournament", Prompt = "Date de fin du tournament")]

        public DateTime EndTimeUtc { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0: dd/MM/yy hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date d'affichage des resultat du tournament", Prompt = "Date d'affichage des resultat du tournament")]
        public DateTime ResultTimeUtc { get; set; }
    }
}
