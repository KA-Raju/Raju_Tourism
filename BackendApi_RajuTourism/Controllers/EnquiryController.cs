using BackendApi_RajuTourism.Models;
using Microsoft.AspNetCore.Mvc;
using BackendApi_RajuTourism.Common;

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

                CommonClass c = new();
                // se.EnquiryEmail(enquiry);
                // c.EnquiryEmailMethod(enquiry);
                c.EnquiryEmailByGRaphAPI(enquiry);

                return Ok(enquiry);
            }
            catch
            {
                return BadRequest("Something went wrong...Please look into it.");
            }
        }
    
    }
}
