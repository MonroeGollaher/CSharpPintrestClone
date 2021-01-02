using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Services;
using CodeWorks.Auth0Provider;
using keepr.Models;

namespace keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProfileController : ControllerBase
  {
    private readonly ProfileService _ps;
    private readonly KeepsService _ks;

    public ProfileController(ProfileService ps, KeepsService ks)
    {
      _ps = ps;
      _ks = ks;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<Profile>> GetKeepsByProfile(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ks.GetKeepsByProfile(id, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}