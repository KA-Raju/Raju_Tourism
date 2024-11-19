using System;
using System.Collections.Generic;

namespace BackendApi_RajuTourism.Models;

public partial class Enquiry
{
    public string? Name { get; set; } 

    public long MobileNumber { get; set; }

    public string? Email { get; set; }

    public DateTime TravelDate { get; set; }

    public int Duration { get; set; }

    public int NoOfAdults { get; set; }

    public string? SpecialNote { get; set; }

    public DateOnly? EnquiryDate { get; set; }
}
