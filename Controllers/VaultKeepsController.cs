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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeepsController>> CreateVaultKeep([FromBody] VaultKeep newVk)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVk.CreatorId = userInfo.Id;
                VaultKeep created = _vks.CreateVaultKeep(newVk);
                // newVk.Creator = userInfo;
                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<VaultKeep>> GetAllVaultKeeps()
        {
            try
            {
                return Ok(_vks.GetAllVaultKeeps());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VaultKeep>> GetById(int id)
        {
            try
            {
                return Ok(_vks.GetKeepById(id));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vks.DeleteVaultKeep(id, userInfo));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> EditVaultKeep(int id, [FromBody] VaultKeep editedVaultKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editedVaultKeep.Id = id;
                return Ok(_vks.EditVaultKeep(editedVaultKeep, userInfo));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}