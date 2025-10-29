using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class VFreelancersWithRating
{
    public int FreelancerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? Email { get; set; }

    public int AverageRating { get; set; }

    public int? ReviewsCount { get; set; }
}
