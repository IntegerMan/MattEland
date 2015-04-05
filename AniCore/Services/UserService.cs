using System;
using System.Linq;
using Ani.Core.Models.Users;

namespace Ani.Core.Services
{
    /// <summary>
    /// A service for interacting with user objects.
    /// </summary>
    public class UserService : Services.ServiceBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public UserService(Entities entities) : base(entities)
        {
        }

        /// <summary>
        /// Gets a user model from a user entity.
        /// </summary>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>A user model for the entity.</returns>
        public static UserModel GetModelFromEntity(User userEntity)
        {
            if (userEntity == null)
            {
                return null;
            }

            var user = new UserModel
            {
                EmailAddress = userEntity.AspNetUser.Email,
                FirstName = userEntity.U_FirstName,
                LastName = userEntity.U_LastName,
                AspNetId = userEntity.U_ASPNET_ID,
                Id = userEntity.U_ID,
                ZipCode = userEntity.U_ZipCode
            };

            return user;
        }

        /// <summary>
        /// Gets the user model from an ASP .NET identifier.
        /// </summary>
        /// <param name="userAspNetId">The user ASP .NET identifier.</param>
        /// <returns>The user model or null if no user.</returns>
        public UserModel GetUserModelFromAspNetId(string userAspNetId)
        {
            AspNetUser aspNetUser = Entities.AspNetUsers.FirstOrDefault(u => u.Id == userAspNetId);

            if (aspNetUser?.Users.FirstOrDefault() == null)
                return null;

            return GetModelFromEntity(aspNetUser.Users.FirstOrDefault());
        }
    }
}