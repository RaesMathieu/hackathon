using System.Collections.Generic;

namespace ThermoBet.MVC.Models
{
    public class MarketViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public virtual List<SelectionViewModel> Selections { get; set; }
    }
}
