using System;

using Microsoft.AspNetCore.Identity;

namespace Open.IdentityServer.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() { }
        public AppUser(string userName) : base(userName) { }
    }
}
