using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class SelectionViewModel
    {
        public int Id { get; set; }

        [Required]
        public bool IsYes { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom de la selection", Prompt = "Nom de la selection")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image url", Prompt = "Image url")]
        public string ImgUrl { get; set; }

        public int MarketId { get; set; }
    }
}