using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string roleName) : base(roleName) { }
    }
}
