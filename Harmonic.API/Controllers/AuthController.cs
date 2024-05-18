using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult IsLogged()
    {
        if (User is null) return Unauthorized();

        return User.Identity.IsAuthenticated ? Ok() : Unauthorized();
    }
}
