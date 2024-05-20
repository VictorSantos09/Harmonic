using Harmonic.API.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<HarmonicIdentityUser> _signInManager;

    public AuthController(SignInManager<HarmonicIdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult IsLogged()
    {
        if (User is null) return Unauthorized();

        return User.Identity.IsAuthenticated ? Ok() : Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(object empty)
    {
        if (empty is not null)
        {
            await _signInManager.ForgetTwoFactorClientAsync();
            await _signInManager.SignOutAsync();
            return Ok();
        }
        return Unauthorized();
    }
}
