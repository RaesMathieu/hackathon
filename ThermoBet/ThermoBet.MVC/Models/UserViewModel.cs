using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Display(Name = "Identifier du user", Prompt = "Identifier du user")]
        public string Login { get; set; }

        [MaxLength(50)]
        [Display(Name = "Pseudo afficher sur l'application du user", Prompt = "Pseudo afficher sur l'application du user")]
        public string Pseudo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Avatar afficher sur l'application du user", Prompt = "Avatar afficher sur l'application du user")]
        public string Avatar { get; set; }

        [MaxLength(50)]
        [Display(Name = "Password encrypté du user", Prompt = "Password encrypté du user")]
        public string HashPassword { get; set; }

        [Display(Name = "Meilleur serie de point du user", Prompt = "Meilleur serie de point du user")]
        public int GlobalPoints { get; set; }

        [Display(Name = "Serie de point actuelle du user", Prompt = "Serie de point actuelle du user")]
        public int CurrentPoints { get; set; }

    }
}
