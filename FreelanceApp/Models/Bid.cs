using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class Bid
{
    public int BidId { get; set; }

    public int ProjectId { get; set; }

    public int FreelancerId { get; set; }

    public string? Proposal { get; set; }

    public decimal? BidAmount { get; set; }

    public string? Status { get; set; }

    public virtual Freelancer Freelancer { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
