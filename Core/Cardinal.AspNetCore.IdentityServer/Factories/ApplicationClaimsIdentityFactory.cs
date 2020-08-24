using Cardinal.Extensions;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cardinal.Factories
{
    public class ClaimsIdentityFactory<TUserIDentity> : UserClaimsPrincipalFactory<TUserIDentity> where TUserIDentity: IdentityUser
    {
        public ClaimsIdentityFactory(UserManager<TUserIDentity> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUserIDentity user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var claims = new List<Claim>();

            claims.AddIfNotExists(new Claim(JwtClaimTypes.Name, user.UserName));
            claims.AddIfNotExists(new Claim(JwtClaimTypes.GivenName, user.UserName));
            var roles = await this.UserManager.GetRolesAsync(user);

            if (identity.Claims.All(c => c.Type != JwtClaimTypes.Role))
            {
                claims.AddRange(roles.Select(s => new Claim(JwtClaimTypes.Role, s)));
            }

            identity.AddClaims(claims);
            return identity;
        }
    }
}
