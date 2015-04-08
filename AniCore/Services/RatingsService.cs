using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ani.Core.Helpers;
using Ani.Core.Models.Metrics;
using Ani.Core.Models.Users;

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
        /// Gets an entity framework entity for the specified rating.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <returns>The rating or null if no rating was found.</returns>
        public Rating GetRatingEntity(int ratingId)
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
        /// <param name="rating">The rating model.</param>
        /// <param name="user">The user model.</param>
        /// <returns>
        /// A new model with good default values. 
        /// This model should be customized before adding it to the database.
        /// </returns>
        public static AddEditUserRatingModel BuildNewRatingEntryModel(RatingModel rating, UserModel user)
        {
            var model = new AddEditUserRatingModel
            {
                Rating = rating,
                RatingValue = rating.MinValue,
                EntryDate = DateTime.Today,
                CreatedTimeUTC = DateTime.Now.ToUniversalTime(),
                ModifiedTimeUTC = DateTime.Now.ToUniversalTime(),
                User = user
            };

            return model;
        }

        /// <summary>
        /// Adds the user rating to the database
        /// </summary>
        /// <param name="rating">The rating.</param>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The UserRatingHistoryEntry created.</returns>
        public UserRatingHistoryEntry AddUserRating(RatingModel rating, AddEditUserRatingModel model, int userId)
        {
            var entryDateUtc = DateHelper.ToUtcDate(model.EntryDate);
            var id = Entities.InsertUpdateUserRating(userId, rating.Id, model.Comments, model.RatingValue, entryDateUtc);

            return id > 0 ? GetUserRatingHistoryEntryModel(rating, model.User, entryDateUtc) : null;
        }

		/// <summary>
		/// Updates the user rating and returns true if the operation succeeded.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="user">The user.</param>
		/// <returns><c>true</c> if the operation succeeded, <c>false</c> otherwise.</returns>
		/// <exception cref="System.ArgumentNullException">
		/// model
		/// or
		/// user
		/// </exception>
		public bool UpdateUserRating(UserRatingHistoryEntry model, UserModel user)
		{
			if (model == null) throw new ArgumentNullException("model");
			if (user == null) throw new ArgumentNullException("user");
			if (model.Rating == null) throw new ArgumentException("model.Rating cannot be null", "model");

			model.EntryDate = model.EntryDate.Date;

			var entry = this.Entities.RatingEntries.FirstOrDefault(
				e => e.UserId == user.Id &&
					 e.RatingId == model.Rating.Id &&
					 DbFunctions.TruncateTime(e.EntryDateUTC) == model.EntryDate);

			if (entry == null)
			{
				return false;
			}

			// Update Properties on the EF Model
			entry.Comments = model.Comments;
			entry.ModifiedTimeUTC = DateTime.UtcNow;
			entry.Rating = model.RatingValue;
			entry.EntryDateUTC = DateHelper.ToUtcDate(model.EntryDate);

			// Save the entity
			return (Entities.SaveChanges() > 0);
		}

		/// <summary>
		/// Gets the rating user history model for a user and rating.
		/// </summary>
		/// <param name="rating">The rating.</param>
		/// <param name="userEntity">The user entity.</param>
		/// <returns>The rating user history model for a user and rating.</returns>
		/// <exception cref="System.ArgumentNullException">
		/// rating
		/// or
		/// userEntity
		/// </exception>
		public UserRatingHistoryModel GetRatingUserHistory(RatingModel rating, UserModel userEntity)
        {
            if (rating == null) throw new ArgumentNullException("rating");
            if (userEntity == null) throw new ArgumentNullException("userEntity");

            var ratingHistoryModel = new UserRatingHistoryModel {User = userEntity, Rating = rating};

            // Drill into the entries for this combination
            var ratingsHistory = this.Entities.RatingEntries.Where(r => r.UserId == userEntity.Id && r.RatingId == rating.Id)
                .OrderBy(r => r.EntryDateUTC);

            // Populate the history with models for the entities we've encountered
            ratingHistoryModel.HistoryEntries = new List<UserRatingHistoryEntry>();            
            foreach (var historyEntry in ratingsHistory)
            {
                var entryModel = GetUserRatingHistoryEntryFromRatingEntryEntity(historyEntry, rating);
                ratingHistoryModel.HistoryEntries.Add(entryModel);
            }

            return ratingHistoryModel;
        }

	    /// <summary>
	    /// Gets the user rating history entry from a RatingEntry entity.
	    /// This will return null if entry is null, for convenience.
	    /// </summary>
	    /// <param name="entry">The entry. Can be null.</param>
	    /// <param name="rating"></param>
	    /// <returns>A UserRatingHistoryEntry representing the RatingEntry or null if entry is null.</returns>
	    private static UserRatingHistoryEntry GetUserRatingHistoryEntryFromRatingEntryEntity(RatingEntry entry, RatingModel rating)
        {
            if (entry == null)
            {
                return null;
            }

            var historyEntry = new UserRatingHistoryEntry
            {
                Comments = entry.Comments,
                CreatedTimeUTC = entry.CreatedTimeUTC,
                EntryDate = entry.EntryDateUTC,
                Id = entry.Id,
                ModifiedTimeUTC = entry.ModifiedTimeUTC,
                RatingValue = entry.Rating,
                UserId = entry.UserId,
				Rating = rating
            };

            return historyEntry;
        }

        /// <summary>
        /// Gets a rating model for the specified rating Id or null for no rating found.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <returns>A rating model for the specified rating Id or null for no rating found.</returns>
        public RatingModel GetRatingModel(int ratingId)
        {
            var rating = GetRatingEntity(ratingId);

            if (rating == null)
            {
                return null;
            }

            var ratingModel = GetRatingModelFromRatingEntity(rating);

            return ratingModel;
        }

        /// <summary>
        /// Gets a rating model from a rating entity.
        /// </summary>
        /// <param name="rating">The rating entity.</param>
        /// <returns>A RatingModel representing the rating.</returns>
        private static RatingModel GetRatingModelFromRatingEntity(Rating rating)
        {
            var ratingModel = new RatingModel
            {
                Id = rating.Id,
                Name = rating.Name,
                IconClass = rating.IconClass,
                CreatedDateUTC = rating.CreatedDateUTC,
                Description = rating.Description,
                IsActive = rating.IsActive,
                IsGlobal = rating.IsGlobal,
                MaxLabel = rating.MaxLabel,
                MinLabel = rating.MinLabel,
                MaxValue = rating.MaxValue,
                MinValue = rating.MinValue,
                RequireComments = rating.RequireComments
            };

            return ratingModel;
        }

        /// <summary>
        /// Gets the user rating history entry model for a specified user / rating / date combination or null
        /// if no entry exists.
        /// </summary>
        /// <param name="rating">The rating.</param>
        /// <param name="user">The user.</param>
        /// <param name="date">The date.</param>
        /// <returns>A UserRatingHistoryEntry or null.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// rating
        /// or
        /// user
        /// </exception>
        public UserRatingHistoryEntry GetUserRatingHistoryEntryModel(RatingModel rating, UserModel user, DateTime date)
        {
	        var entry = GetUserRatingHistoryEntryEntity(rating, user, date);

	        return GetUserRatingHistoryEntryFromRatingEntryEntity(entry, rating);
        }

		/// <summary>
		/// Gets the user rating history entry entity.
		/// </summary>
		/// <param name="rating">The rating.</param>
		/// <param name="user">The user.</param>
		/// <param name="date">The date.</param>
		/// <returns>An Ani.Core.RatingEntry representing the history entry.</returns>
		private RatingEntry GetUserRatingHistoryEntryEntity(RatingModel rating, UserModel user, DateTime date)
		{
			if (rating == null) throw new ArgumentNullException("rating");
			if (user == null) throw new ArgumentNullException("user");

			// Chop off the time component
			date = date.Date;

			var entry = Entities.RatingEntries.FirstOrDefault(
				e => e.UserId == user.Id &&
					 e.RatingId == rating.Id &&
					 DbFunctions.TruncateTime(e.EntryDateUTC) == date);

			return entry;
		}

        /// <summary>
        /// Deletes a user rating belonging to the executing user
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the entity was deleted, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// entry
        /// or
        /// user
        /// </exception>
        public bool DeleteUserRating(UserRatingHistoryEntry entry, UserModel user)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (user == null) throw new ArgumentNullException("user");

            var entity = Entities.RatingEntries.FirstOrDefault(e => e.Id == entry.Id);

            if (entity != null && entity.UserId == user.Id)
            {
                Entities.RatingEntries.Remove(entity);
                Entities.SaveChanges();

                return true;
            }

            return false;
        }
    }
}