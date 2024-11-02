using System;
using System.Collections.Generic;

namespace BackendApi_RajuTourism.Models;

public partial class Enquiry
{
    public string Name { get; set; } = null!;

    public long MobileNumber { get; set; }

    public string Email { get; set; } = null!;

    public DateTime TravelDate { get; set; }

    public int Duration { get; set; }

    public int NoOfAdults { get; set; }

    public string SpecialNote { get; set; } = null!;

    public DateOnly? EnquiryDate { get; set; }
}
