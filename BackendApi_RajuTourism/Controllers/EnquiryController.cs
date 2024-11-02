using BackendApi_RajuTourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi_RajuTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : Controller
    {
        private readonly RajuTourismContext _rajuTourismContext;
        public EnquiryController(RajuTourismContext rajuTourismContext)
        {
            this._rajuTourismContext = rajuTourismContext;
        }

        [HttpPost]
        [Route("packageenquiry")]
        public async Task<IActionResult> AddEnquiry([FromBody] Enquiry enquiry)
        {
            try
            {
                await _rajuTourismContext.Enquirys.AddAsync(enquiry);
                await _rajuTourismContext.SaveChangesAsync();
                SendEmailController se = new();
               // se.EnquiryEmail(enquiry);
                se.EnquiryEmailMethod(enquiry);

                return Ok(enquiry);
            }
            catch
            {
                return BadRequest("Something went wrong...Please look into it.");
            }
        }
    
    }
}
