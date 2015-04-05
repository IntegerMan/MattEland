using System;
using System.Linq;

namespace Ani.Core.Services
{
    /// <summary>
    /// Base logic shared by all service classes.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        protected ServiceBase(Entities entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Gets the database context for working with entity framework.
        /// </summary>
        /// <value>The database context for working with entity framework.</value>
        protected Entities Entities { get; }

        /// <summary>
        /// Determines whether the specified user has the specified role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns><c>true</c> if the user has the role, <c>false</c> otherwise.</returns>
        protected static bool UserHasRole(User user, string roleName)
        {
            // Sanity check against null values
            if (user == null)
            {
                return false;
            }

            // Must have ASP NET user with role equal to the one we mentioned.
            var aspNetUser = user.AspNetUser;
            return aspNetUser != null && 
                aspNetUser.AspNetRoles.Any(r => string.Equals(r.Name,
                                                              roleName, 
                                                              StringComparison.InvariantCultureIgnoreCase));

        }
    }
}