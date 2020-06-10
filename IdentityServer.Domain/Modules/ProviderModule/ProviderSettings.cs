namespace IdentityServer.Domain.Modules.ProviderModule
{
    public abstract class BaseProviderSetting
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }

    public class GoogleProviderSetting : BaseProviderSetting { }
    public class GitHubProviderSetting : BaseProviderSetting { }
    public class AzureProviderSetting : BaseProviderSetting
    {
        public string TenantId { get; set; } = null!;
        public string Authority { get; set; } = null!;

        public string ResponseType { get; set; } = null!;

        public string AuthorityFull => this.Authority + this.TenantId;
    }
}
