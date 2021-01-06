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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;
        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                Vault created = _vs.CreateVault(newVault);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Vault>> GetVaults()
        {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_vs.GetVaults(userInfo.Id));
        }
        catch (System.Exception error)
        {
            return BadRequest(error.Message);
        }
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Vault>> GetVaultById(int id)
        // {
        //     try
        //     {
        //         Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        //         return Ok(_vs.GetVaultById(id, userInfo));
        //     }
        //     catch (System.Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet("{id}")]
        public ActionResult<Vault> GetVaultById(int id)
        {
            try
            {
                return Ok(_vs.GetVaultById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<IEnumerable<Vault>> GetKeepsByVaultId(int id)
        {
            try
            {
                return Ok(_vks.GetKeepsByVaultId(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVault(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.DeleteVault(id, userInfo));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault editedVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editedVault.Id = id;
                return Ok(_vs.EditVault(userInfo, editedVault));
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}