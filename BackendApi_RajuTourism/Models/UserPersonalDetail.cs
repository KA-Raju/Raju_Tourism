using System;
using System.Collections.Generic;

namespace BackendApi_RajuTourism.Models;

public partial class UserPersonalDetail
{
    public string Uname { get; set; } = null!;

    public string Uemail { get; set; } = null!;

    public long UmobileNo { get; set; }
}
