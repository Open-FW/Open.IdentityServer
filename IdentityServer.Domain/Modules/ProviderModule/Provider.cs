using System.Collections.Generic;

namespace IdentityServer.Domain.Modules.ProviderModule
{
    public class Provider
    {
        public static readonly string Google = "Google";
        public static readonly string GitHub = "GitHub";
        public static readonly string Azure = "Azure";
        public static readonly string LDAP = "LDAP";

        public string AuthenticationScheme { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public static IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>()
            {
                new Provider
                {
                    AuthenticationScheme = nameof(Google),
                    DisplayName = Google,
                    Icon = "google-color"
                },
                new Provider
                {
                    AuthenticationScheme = nameof(GitHub),
                    DisplayName = GitHub,
                    Icon ="github"
                },
                new Provider
                {
                    AuthenticationScheme = nameof(Azure),
                    DisplayName = Azure,
                    Icon = "azure-color"
                }
            };
        }
    }
}
