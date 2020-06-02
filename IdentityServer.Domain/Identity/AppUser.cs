using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() { }
        public AppUser(string userName) : base(userName) { }
    }
}
