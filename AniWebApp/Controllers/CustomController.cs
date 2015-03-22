using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace AniWebApp.Controllers
{
    public abstract class CustomController : Controller
    {

        private readonly AniEntities _entities = new AniEntities();

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
        protected AniEntities Entities {  get { return _entities;  } }
        
        protected int GetUserZipCode()
        {
            User user = GetUserEntity();
            if (user != null)
            {
                return user.U_ZipCode;
            }

            return 43035;
        }

        protected string GetUserAspNetId()
        {
            if (this.User != null && this.User.Identity != null)
            {
                return this.User.Identity.GetUserId();
            }

            return null;
        }

        protected User GetUserEntity()
        {
            var aspNetId = this.GetUserAspNetId();

            if (!string.IsNullOrWhiteSpace(aspNetId))
            {
                User user = _entities.Users.FirstOrDefault(u => u.U_ASPNET_ID == aspNetId);
                return user;
            }

            return null;
        }

        protected int GetUserId()
        {
            User user = GetUserEntity();
            if (user != null)
            {
                return user.U_ID;
            }

            return 0;
        }
    }
}
