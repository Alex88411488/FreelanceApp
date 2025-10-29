using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class VActiveProject
{
    public int ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string CategoryName { get; set; } = null!;

    public decimal? Budget { get; set; }

    public DateOnly? Deadline { get; set; }

    public string? Status { get; set; }

    public string ClientName { get; set; } = null!;
}
