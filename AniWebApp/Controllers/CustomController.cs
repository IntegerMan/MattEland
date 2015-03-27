using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using AniWebApp.Helpers;

namespace AniWebApp.Controllers
{
    public abstract class CustomController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomController"/> class.
        /// </summary>
        protected CustomController()
        {

        }

        /// <summary>
        /// Gets the database context for working with entity framework.
        /// </summary>
        /// <value>The entity framework database context.</value>
        protected AniEntities Entities { get; } = new AniEntities();

        protected int GetUserZipCode()
        {
            User user = GetUserEntity();
            if (user != null)
            {
                return user.U_ZipCode;
            }

            return 43035;
        }

        protected User GetUserEntity()
        {
            return UserHelper.GetCurrentUserEntity(Entities, this.User);
        }

        protected int GetUserId()
        {
            var user = GetUserEntity();
            return user?.U_ID ?? 0;
        }
    }
}
