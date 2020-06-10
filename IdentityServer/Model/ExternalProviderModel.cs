namespace IdentityServer.Model
{
    public class ExternalProviderModel
    {
        public string Provider { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
