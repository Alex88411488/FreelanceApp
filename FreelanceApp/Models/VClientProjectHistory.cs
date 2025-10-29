using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class VClientProjectHistory
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public int ProjectId { get; set; }

    public string ProjectTitle { get; set; } = null!;

    public string? ProjectStatus { get; set; }

    public decimal? Budget { get; set; }

    public byte? Rating { get; set; }

    public string? ReviewComment { get; set; }

    public string? HiredFreelancer { get; set; }
}
