using System.Web;
using System.Web.Mvc;
using Ani.Core;
using Ani.Core.Models.Users;
using Ani.Core.Services;
using AniWebApp.Helpers;
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
            Entities = new Entities();
            UserService = new UserService(Entities);
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
        protected Entities Entities { get; }

        protected int GetUserZipCode()
        {
            var user = GetUserEntity();
            return user?.U_ZipCode ?? 43035;
        }

        protected User GetUserEntity()
        {
            return UserHelper.GetCurrentUserEntity(Entities, this.User);
        }

        /// <summary>
        /// Gets a user model representing the current user or null if no user.
        /// </summary>
        /// <returns>A user model representing the current user or null if no user.</returns>
        protected UserModel GetUserModel()
        {
            var userAspNetId = GetUserAspNetId();
            return UserService.GetUserModelFromAspNetId(userAspNetId);
        }

        /// <summary>
        /// Gets the current user's ASP .NET Id.
        /// </summary>
        /// <returns></returns>
        private string GetUserAspNetId()
        {
            return UserHelper.GetUserAspNetId(this.User);
        }

        protected int GetUserId()
        {
            var user = GetUserEntity();
            return user?.U_ID ?? 0;
        }

        /// <summary>
        /// Gets the user service.
        /// </summary>
        /// <value>The user service.</value>
        protected UserService UserService { get; }
    }
}
