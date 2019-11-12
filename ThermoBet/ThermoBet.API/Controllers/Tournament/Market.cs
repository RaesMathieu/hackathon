using System.Collections.Generic;

public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public virtual ICollection<Selection> Selections { get; set; }

        public virtual ICollection<TournamentReponse> Tournaments { get; set; }
    }