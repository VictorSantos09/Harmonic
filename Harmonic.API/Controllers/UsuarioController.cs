using Harmonic.API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UserManager<HarmonicIdentityUser> _userManager;

    public UsuarioController(UserManager<HarmonicIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = await _userManager.GetUserAsync(User);

        if(user is null) return Unauthorized();

        var returnData = new
        {
            user.PhoneNumber,
            user.UserName,
            user.Email,
            user.PhoneNumberConfirmed,
            user.EmailConfirmed,
        };

        return Ok(returnData);
    }
}
