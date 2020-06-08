using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Domain.Modules.ProviderModule
{
    public class Provider
    {
        public static readonly string Google = "Google";
        public static readonly string GitHub = "GitHub";
        public static readonly string Azure = "Azure";
        public static readonly string LDAP = "LDAP";

        [Key]
        public string AuthenticationScheme { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
