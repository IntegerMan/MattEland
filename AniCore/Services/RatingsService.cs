using System;
using System.Collections.Generic;
using System.Linq;
using Ani.Core.Helpers;
using Ani.Core.Models.Metrics;

namespace Ani.Core.Services
{
    /// <summary>
    /// A service for working with ratings
    /// </summary>
    public class RatingsService : ServiceBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public RatingsService(Entities entities) : base(entities)
        {
        }

        /// <summary>
        /// Gets the specified rating.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <returns>The rating or null if no rating was found.</returns>
        public Rating GetRating(int ratingId)
        {
            var rating = Entities.Ratings.FirstOrDefault(r => r.Id == ratingId);
            return rating;
        }

        public List<RatingsWithLatestInfoForUserSelect_Result> GetLatestRatingInfoForUser(int userId)
        {
            return Entities.RatingsWithLatestInfoForUserSelect(userId).ToList();
        }

        /// <summary>
        /// Builds the new rating entry model for use when creating a new user rating.
        /// </summary>
        /// <param name="rating">The rating.</param>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>
        /// A new model with good default values. 
        /// This model should be customized before adding it to the database.
        /// </returns>
        public static AddEditRatingModel BuildNewRatingEntryModel(Rating rating, User userEntity)
        {
            var model = new AddEditRatingModel
            {
                Rating = rating,
                RatingValue = rating.MinValue,
                EntryDate = DateTime.Today,
                CreatedTimeUTC = DateTime.Now.ToUniversalTime(),
                ModifiedTimeUTC = DateTime.Now.ToUniversalTime(),
                User = userEntity
            };
            return model;
        }

        /// <summary>
        /// Adds the user rating to the database
        /// </summary>
        /// <param name="rating">The rating.</param>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddUserRating(Rating rating, AddEditRatingModel model, int userId)
        {
            var entryDateUtc = DateHelper.ToUtcDate(model.EntryDate);
            Entities.InsertUpdateUserRating(userId, rating.Id, model.Comments, model.RatingValue, entryDateUtc);
        }
    }
}