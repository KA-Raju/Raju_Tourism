using BackendApi_RajuTourism.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BackendApi_RajuTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackController : Controller
    {
        private readonly RajuTourismContext _rajuTourismContext;
        public PackController(RajuTourismContext rajuTourismContext)
        {
            this._rajuTourismContext = rajuTourismContext;
        }
        [HttpGet]
        [Route("getallpacks")]
        public async Task<IActionResult> GetAllPacks()
        {
            try
            {
                var res = await _rajuTourismContext.Packs.ToListAsync();
                return Ok(res);

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong...Please retry.");
            }
        }

        [HttpPost]
        [Route("addpack")]
        public async Task<IActionResult> AddPack([FromBody] Pack packreq)
        {
            try
            {
                packreq.PackId = Guid.NewGuid();
                await _rajuTourismContext.Packs.AddAsync(packreq);
                await _rajuTourismContext.SaveChangesAsync();

                return Ok(packreq);
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong...Please retry.");
            }
        }

        [HttpGet]
        [Route("getpack")]
        public async Task<IActionResult> GetPack([FromRoute] Guid packid)
        {
            try
            {
                var pack = await _rajuTourismContext.Packs.FirstOrDefaultAsync(x => x.PackId == packid);
                if(pack == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(pack);
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong... Please retry.");
            }
        }

        [HttpPut]
        [Route("{packId:Guid}")]
        public async Task<IActionResult> UpdatePack([FromRoute] Guid packId, Pack updatePackRequest)
        {
            try
            {
                var pack = await _rajuTourismContext.Packs.FindAsync(packId);

                if (pack == null)
                {
                    return NotFound();
                }
                else
                {
                    List<string> errorMsg = new();

                    pack.PackId = updatePackRequest.PackId;
                    pack.PackName = updatePackRequest.PackName;
                    pack.Duration = updatePackRequest.Duration;
                    pack.Price = updatePackRequest.Price;

                    Regex regex = new("[a-zA-Z ]$");

                    if (pack.PackName == "" || pack.PackName.Length < 4 || pack.PackName.Length > 30 || !regex.IsMatch(pack.PackName))
                    {
                        errorMsg.Add("Enter a valid name");
                    }
                    if (pack.Duration <= 0 || pack.Duration >= 20 || pack.Duration == null)
                    {
                        errorMsg.Add("Enter a valid duration");
                    }
                    if (pack.Price <= 0 || pack.Price >= 20000 || pack.Price == null)
                    {
                        errorMsg.Add("Enter a valid price");
                    }
                    if (errorMsg.Count > 0)
                    {
                        return BadRequest(errorMsg);
                    }
                    else
                    {
                        await _rajuTourismContext.SaveChangesAsync();
                        return Ok(pack);
                    }
                } 
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong...Please retry");
            }
         }

        [HttpDelete]
        [Route("{packId:guid}")]
        public async Task<IActionResult> DeletePack([FromRoute] Guid PackId)
        {
            try
            {
                var pack = await _rajuTourismContext.Packs.FindAsync(PackId);

                if(pack == null)
                {
                    return NotFound();
                }
                else
                {
                    _rajuTourismContext.Packs.Remove(pack);
                    await _rajuTourismContext.SaveChangesAsync();
                    return Ok(pack);
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong...Please retry.");
            }
        }
     }
}
