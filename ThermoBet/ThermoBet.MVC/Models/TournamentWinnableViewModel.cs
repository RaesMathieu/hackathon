using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class TournamentWinnableViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de bonne reponse", Prompt = "Nombre de bonne reponse")]
        public int NbGoodAnswer { get; set; }

        [Required]
        [Display(Name = "Gain", Prompt = "Gain")]
        public int AmountOfWinnings { get; set; }
    }
}
