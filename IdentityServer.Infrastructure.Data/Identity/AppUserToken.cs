using System;

using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure.Data.Identity
{
    public class AppUserToken : IdentityUserToken<Guid>
    {
        public AppUserToken() : base() { }
    }
}
