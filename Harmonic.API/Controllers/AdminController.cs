using Harmonic.API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly UserManager<HarmonicIdentityUser> _userManager;

    public AdminController(UserManager<HarmonicIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("is-admin")]
    public async Task<IActionResult> IsAdmin()
    {
        var user = await _userManager.GetUserAsync(User);

        if(user is null)
        {
            return BadRequest("Wasn't possible to find the user");
        }

        var r = await _userManager.IsInRoleAsync(user, "ADMIN");

        return Ok(r);
    }

    //[HttpGet("create-admin")]
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    if(user is null)
    //    {
    //        return BadRequest("Wasn't possible to find the user");
    //    }

    //    return Ok(await _userManager.AddToRoleAsync(user, "ADMIN"));
    //}
}
