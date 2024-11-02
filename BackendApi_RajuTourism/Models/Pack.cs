using System;
using System.Collections.Generic;

namespace BackendApi_RajuTourism.Models;

public partial class Pack
{
    public Guid PackId { get; set; }

    public string PackName { get; set; } = null!;

    public int Duration { get; set; }

    public int Price { get; set; }
}
