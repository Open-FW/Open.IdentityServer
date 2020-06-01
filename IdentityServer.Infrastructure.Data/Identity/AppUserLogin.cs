using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppUserLogin : IdentityUserLogin<Guid>
    {
        public AppUserLogin() : base() { }
    }
}
