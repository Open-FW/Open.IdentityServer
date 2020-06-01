using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public AppUserClaim() : base() { }
    }
}
