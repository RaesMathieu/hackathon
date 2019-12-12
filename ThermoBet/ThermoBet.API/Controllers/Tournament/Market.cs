using System.Collections.Generic;
using System;

public class Market
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateTime StartTimeUtc { get; set; }
    public int? WinningSelectionId { get; set; }
    public int? ChosenSelectionId { get; set; }

    public virtual ICollection<Selection> Selections { get; set; }

    public DateTime? BetTime { get; set; }
}