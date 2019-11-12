namespace ThermoBet.MVC.Models
{
    public class SelectionViewModel
    {
        public int Id { get; set; }

        public decimal Odds { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }
        public int MarketId { get; set; }
        public bool? Result { get; set; }
        public int? Position { get; set; }

    }
}