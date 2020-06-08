using IdentityServer.Domain.Modules.ProviderModule;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasData(new Provider
            {
                AuthenticationScheme = nameof(Provider.Google),
                DisplayName = Provider.Google,
                Icon = "google-color",
                Enabled = false
            },
            new Provider
            {
                AuthenticationScheme = nameof(Provider.Azure),
                DisplayName = Provider.Azure,
                Icon = "azure-color",
                Enabled = false
            },
            new Provider
            {
                AuthenticationScheme = nameof(Provider.GitHub),
                DisplayName = Provider.GitHub,
                Icon = "github",
                Enabled = false
            });
        }
    }
}
