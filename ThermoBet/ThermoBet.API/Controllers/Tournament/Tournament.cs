using System;
using System.Collections.Generic;

public class TournamentReponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Market> Markets { get; set; }
    public virtual ICollection<TournamentWinnable> Winnables { get; set; }
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
}