using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Domain.Modules.ProviderModule
{
    public class Provider
    {
        public const string Google = "Google";
        public const string GitHub = "GitHub";
        public const string Azure = "Azure";
        public const string LDAP = "LDAP";

        [Key]
        public string AuthenticationScheme { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
