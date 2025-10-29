using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public decimal? Budget { get; set; }

    public DateOnly? Deadline { get; set; }

    public string? Status { get; set; }

    public int ClientId { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual Category Category { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Review? Review { get; set; }
}
