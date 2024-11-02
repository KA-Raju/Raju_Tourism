using BackendApi_RajuTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BackendApi_RajuTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly RajuTourismContext _rajuTourismContext;
        public RegisterController(RajuTourismContext rajuTourismContext)
        {
            this._rajuTourismContext = rajuTourismContext;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDetail request)
        {
            try
            {
                await _rajuTourismContext.RegisterDetails.AddAsync(request);
                await _rajuTourismContext.SaveChangesAsync();

                SendEmailController se = new SendEmailController();
               // se.RegisterEmail(request);
                se.RegisterEmailMethod(request);

                return Ok(request);
            }
            catch (Exception ex) 
            {
                return BadRequest("Email already exist... Please register with different email Id or login with this email Id");
            }
        }


        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                var res = await _rajuTourismContext.RegisterDetails.ToListAsync();
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong...Please retry.");
            }
        }
    }
}
