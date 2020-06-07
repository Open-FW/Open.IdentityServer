namespace IdentityServer.Model
{
    public class ExternalProvider
    {
        public string Provider { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
    }
}
