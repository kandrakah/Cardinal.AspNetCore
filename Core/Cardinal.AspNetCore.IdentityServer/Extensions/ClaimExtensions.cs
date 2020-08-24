using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Cardinal.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddIfNotExists(this List<Claim> claims, Claim newClaim)
        {
            if (claims.Any(c => c.Type == newClaim.Type))
            {
                return;
            }

            claims.Add(newClaim);
        }

        public static void Merge(this List<Claim> claims, IEnumerable<Claim> newClaim)
        {
            foreach (var claim in newClaim)
            {
                if (claims.Any(c => c.Type == claim.Type))
                    continue;

                claims.Add(claim);
            }
        }

        public static string ValueOf(this IEnumerable<Claim> claims, params string[] claimType)
        {
            foreach (var claim in claimType)
            {
                if (claims.Any(s => s.Type == claim))
                    return claims.FirstOrDefault(f => f.Type == claim).Value;
            }

            return null;
        }

        public static Claim Of(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.FirstOrDefault(f => f.Type == claimType);
        }
        public static bool Contains(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Any(f => f.Type == claimType);
        }
    }
}
