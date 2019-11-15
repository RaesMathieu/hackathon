using System.ComponentModel.DataAnnotations;

namespace ThermoBet.MVC.Models
{
    public class ResultingSelectionViewModel
    {
        public int Id { get; set; }

        public bool IsYes { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nom de la selection", Prompt = "Nom de la selection")]
        public string Name { get; set; }

    }
}