using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser(string userName) : base(userName) { }
    }
}
