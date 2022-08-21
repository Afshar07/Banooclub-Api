using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BanooClub.Extentions
{
    public static class IdentityExtensions
    {
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }

        public static string FindFirstValue(this IIdentity identity, string claimType)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(claimType);
        }

        public static long GetUserId(this IIdentity identity)
        {
            return Convert.ToInt32(identity?.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static long GetUserId(this ClaimsPrincipal principal) =>
            long.Parse(principal?.Claims?.FirstOrDefault(x => x.Type is ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
