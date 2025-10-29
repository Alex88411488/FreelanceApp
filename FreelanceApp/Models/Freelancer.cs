using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class Freelancer
{
    public int FreelancerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
