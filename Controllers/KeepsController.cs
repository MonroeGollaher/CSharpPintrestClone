using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newKeep.CreatorId = userInfo.Id;
                Keep created = _ks.CreateKeep(newKeep);
                created.Creator = userInfo;
                return Ok(created);
                
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Keep>> GetKeeps()
        {
            try
            {
                return Ok(_ks.GetKeeps());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}