using System;
using System.Collections.Generic;

using IdentityServer4.Models;

namespace Open.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resourceapi", "Resource API")
                {
                    Scopes = {"api.read"}
                },
                new ApiResource("resourceis", "Resource IS")
                {
                    Scopes = {"is.admin"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "open_client_angular",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "email", "offline_access" },
                    RedirectUris = {"http://localhost:7200/auth/signin-callback"},
                    PostLogoutRedirectUris = new List<string> {"http://localhost:7200/auth/signout-callback"},
                    AllowedCorsOrigins = {"http://localhost:7200" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "is_spa",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "email", "offline_access", "role", "is.admin"},
                    RedirectUris = {"http://localhost:5000/auth/signin-callback"},
                    PostLogoutRedirectUris = new List<string> {"http://localhost:5000/auth/signout-callback"},
                    AllowedCorsOrigins = {"http://localhost:5000"},
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                }
            };
        }
    }
}
