using System.Linq;
using System.Security.Principal;
using Ani.Core;
using Microsoft.AspNet.Identity;

namespace AniWebApp.Helpers
{
    public static class UserHelper
    {

        public static string GetUserAspNetId(IPrincipal user)
        {
            return user?.Identity?.GetUserId();
        }

        public static User GetCurrentUserEntity(Entities entities, IPrincipal principal)
        {
            var aspNetId = GetUserAspNetId(principal);

            if (!string.IsNullOrWhiteSpace(aspNetId))
            {
                return entities.Users.FirstOrDefault(u => u.U_ASPNET_ID == aspNetId);
            }

            return null;

        }
    }
}