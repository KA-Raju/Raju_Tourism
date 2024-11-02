using System;
using System.Collections.Generic;

namespace BackendApi_RajuTourism.Models;

public partial class RegisterDetail
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long MobileNo { get; set; }

    public string Password { get; set; } = null!;
}
