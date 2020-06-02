using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() { }
        public AppRole(string roleName) : base(roleName) { }
    }
}
