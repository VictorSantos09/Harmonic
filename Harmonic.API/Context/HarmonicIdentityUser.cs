using Microsoft.AspNetCore.Identity;

namespace Harmonic.API.Context;

public class HarmonicIdentityUser : IdentityUser
{
    public bool IsAdmin { get; set; }
}
