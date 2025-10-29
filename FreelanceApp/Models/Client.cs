using System;
using System.Collections.Generic;

namespace FreelanceApp.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
