using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() : base() { }
        public AppUser(string userName) : base(userName) { }
    }
}
