using System;

using Microsoft.AspNetCore.Identity;

namespace Open.IdentityServer.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() { }
        public AppRole(string roleName) : base(roleName) { }
    }
}
