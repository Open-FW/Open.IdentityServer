using System.Linq;
using System.Threading.Tasks;

using IdentityModel;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;

using Open.IdentityServer.Domain.Identity;

namespace Open.IdentityServer.Models
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identity = context.Subject.Identity.Name;
            var user = await this.userManager.FindByNameAsync(identity);

            if (user != null)
            {
                var roles = await this.userManager.GetRolesAsync(user);
                context.IssuedClaims.AddRange(roles.Select(s => new System.Security.Claims.Claim(JwtClaimTypes.Role, s)));
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await this.userManager.FindByNameAsync(context.Subject.Identity.Name);
            context.IsActive = user != null;
        }
    }
}
