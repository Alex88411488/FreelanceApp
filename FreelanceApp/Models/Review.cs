using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int ProjectId { get; set; }

    public int ClientId { get; set; }

    public int FreelancerId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Freelancer Freelancer { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
