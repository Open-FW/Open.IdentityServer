using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName) { }
    }
}
