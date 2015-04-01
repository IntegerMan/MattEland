using System.Web;
using System.Web.Mvc;
using AniWebApp.Helpers;
using AniWebApp.Models.Accounts;
using Microsoft.AspNet.Identity.Owin;

namespace AniWebApp.Controllers
{
    public abstract class CustomController : Controller
    {
        private ApplicationRoleManager _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomController"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        protected CustomController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        /// <summary>
        /// Gets the role manager.
        /// </summary>
        /// <value>The role manager.</value>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        /// <summary>
        /// Gets the database context for working with entity framework.
        /// </summary>
        /// <value>The entity framework database context.</value>
        protected AniEntities Entities { get; } = new AniEntities();

        protected int GetUserZipCode()
        {
            var user = GetUserEntity();
            return user?.U_ZipCode ?? 43035;
        }

        protected User GetUserEntity()
        {
            return UserHelper.GetCurrentUserEntity(Entities, this.User);
        }

        protected string GetUserAspNetId()
        {
            return UserHelper.GetUserAspNetId(this.User);
        }

        protected int GetUserId()
        {
            var user = GetUserEntity();
            return user?.U_ID ?? 0;
        }
    }
}
