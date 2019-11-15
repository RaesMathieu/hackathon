using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class AdministrationViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Simlation DtateTime.UtcNow", Prompt = "Simlation DtateTime.UtcNow")]
        public DateTime DateTimeUtcNow { get; set; }
    }
}
