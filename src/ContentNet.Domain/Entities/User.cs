using Microsoft.AspNetCore.Identity;

namespace ContentNet.Domain.Entities;

public class User : IdentityUser<int>
{
    public DateTimeOffset? LastLoginAt { get; set; }
}
