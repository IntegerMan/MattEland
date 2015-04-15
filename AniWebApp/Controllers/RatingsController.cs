using System;
using System.Web.Mvc;
using Ani.Core.Models.Metrics;
using Ani.Core.Models.Users;
using Ani.Core.Services;

namespace AniWebApp.Controllers
{
	/// <summary>
	/// An MVC Controller governing ratings-related activities
	/// </summary>
	[Authorize]
	public class RatingsController : CustomController
	{
        private readonly RatingsService _ratingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingsController"/> class.
        /// </summary>
        public RatingsController() : this(null)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomController" /> class.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        public RatingsController(ApplicationRoleManager roleManager) : base(roleManager)
		{
		    _ratingsService = new RatingsService(this.Entities);
		}

		/// <summary>
		/// Goes to the main index page for the ratings application.
		/// </summary>
		/// <returns>The view for the main ratings application.</returns>
		[HttpGet]
		[Route("Ratings")]
		[Authorize]
		public ActionResult Index()
		{
			var model = new RatingsSummaryModel
			{
				Ratings = _ratingsService.GetLatestRatingInfoForUser(this.GetUserId())
			};

			return View(model);
		}

        /// <summary>
        /// Serves up a view for viewing the details of a past entry.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>A view for viewing the rating or a redirect to an item not found.</returns>
        [HttpGet]
		[Route(@"Ratings/{ratingId}/{year}/{month}/{day}")]
		[Authorize]
		public ActionResult ViewEntry(int ratingId, int year, int month, int day)
        {
            return GetRatingEntryView(ratingId, year, month, day);
        }

        /// <summary>
        /// Serves up a view for confirming deletion of an entry.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>A view for deleting the rating or a redirect to an item not found.</returns>
        [HttpGet]
		[Route(@"Ratings/{ratingId}/{year}/{month}/{day}/Delete")]
		[Authorize]
		public ActionResult DeleteEntry(int ratingId, int year, int month, int day)
        {
            return GetRatingEntryView(ratingId, year, month, day);
        }

        /// <summary>
        /// Handles the delete rating action
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>A view for deleting the rating or a redirect to an item not found.</returns>
        [HttpPost]
		[Route(@"Ratings/{ratingId}/{year}/{month}/{day}/Delete")]
		[Authorize]
        [ValidateAntiForgeryToken]
		public ActionResult DeleteEntryPost(int ratingId, int year, int month, int day)
        {
            var user = GetUserModel();
            var entry = GetUserRatingEntry(ratingId, year, month, day, user);

            // No entry for whatever reason. Handle it.
            if (entry == null)
            {
                return GetNotFoundAction();
            }

            // Attempt the delete and redirect as needed on success / failure
            if (_ratingsService.DeleteUserRating(entry, user))
            {
                ShowSuccess("Rating Deleted");

                // Take them back to the list
                return RedirectToAction("History", new {ratingId});
            }
            else
            {
                ShowError("Could not delete rating");

                // FAIL! Redirect to the view
                return GetRedirectToViewEntry(ratingId, entry);
            }

        }

	    /// <summary>
        /// Serves up a view for editing the current user's entry for a specific rating on a specific day.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>A view for editing the rating or a redirect to an item not found.</returns>
        [HttpGet]
		[Route(@"Ratings/{ratingId}/{year}/{month}/{day}/Edit")]
		[Authorize]
		public ActionResult EditEntry(int ratingId, int year, int month, int day)
		{
            return GetRatingEntryView(ratingId, year, month, day);
        }
		
	    [HttpPost]
		[Route(@"Ratings/{ratingId}/Edit")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult EditEntryPost(int ratingId, UserRatingHistoryEntry model)
		{
			if (ModelState.IsValid)
			{
				// Ensure we have a rating object - this won't come back in the post.
				var rating = _ratingsService.GetRatingModel(ratingId);
				if (rating == null)
				{
					return GetNotFoundAction();
				}
				model.Rating = rating;

				// Get our user
				var user = this.GetUserModel();

				if (user.Id != model.UserId)
				{
					return GetNotFoundAction();
				}
			    _ratingsService.UpdateUserRating(model, user);

                ShowSuccess("Rating Edited");

                // Go back to our main view page
			    return GetRedirectToViewEntry(ratingId, model);
			}

			return View(model);
		}

	    private ActionResult GetRedirectToViewEntry(int ratingId, UserRatingHistoryEntry model)
	    {
	        return RedirectToAction("ViewEntry", "Ratings", new {
	                                                                ratingId = ratingId,
	                                                                year = model.EntryDate.Year,
	                                                                month = model.EntryDate.Month,
	                                                                day = model.EntryDate.Day
	                                                            });
	    }

	    [HttpGet]
		[Route(@"Ratings/{ratingId}/Add")]
		[Authorize]
		public ActionResult AddEntry(int ratingId)
		{
			var rating = _ratingsService.GetRatingModel(ratingId);
			if (rating == null)
			{
				return GetNotFoundAction();
			}

	        var user = this.GetUserModel();
	        var model = RatingsService.BuildNewRatingEntryModel(rating, user);

			return View(model);
		}

	    [HttpPost]
		[Route(@"Ratings/{ratingId}/Add")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult AddEntryPost(int ratingId, AddEditUserRatingModel model)
		{
			if (ModelState.IsValid)
			{
			    var rating = _ratingsService.GetRatingModel(ratingId);
				if (rating == null || model == null)
				{
				    return GetNotFoundAction();
				}

			    model.User = GetUserModel();
			    var userRatingHistoryEntry = _ratingsService.AddUserRating(rating, model);

			    if (userRatingHistoryEntry != null)
			    {
			        ShowSuccess("Rating Added");
			        return GetRedirectToViewEntry(ratingId, userRatingHistoryEntry);
			    }

			    ShowError("Could not add rating");
			}

			return View(model);
		}


	    [HttpGet]
		[Route(@"Ratings/{ratingId}")]
		[Authorize]
		public ActionResult History(int ratingId)
		{
            // Grab the rating
	        var ratingModel = _ratingsService.GetRatingModel(ratingId);
	        if (ratingModel == null)
	        {
	            return GetNotFoundAction();
	        }

            // Grab the current user (we'll have one since we require authorized)
            var userModel = this.GetUserModel();

            // Get data for the user and return it to the view
	        var model = _ratingsService.GetRatingUserHistory(ratingModel, userModel);

			return View(model);
		}

        /// <summary>
        /// Gets the rating details or edit view.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>An ActionResult representing the desired action.</returns>
        private ActionResult GetRatingEntryView(int ratingId, int year, int month, int day)
	    {

	        var user = this.GetUserModel();

            var model = GetUserRatingEntry(ratingId, year, month, day, user);

	        return model == null ? GetNotFoundAction() : View(model);
	    }

	    private UserRatingHistoryEntry GetUserRatingEntry(int ratingId,
	        int year,
	        int month,
	        int day,
	        UserModel user)
	    {
            // Grab the rating. We fail if this doesn't exist.
	        var rating = _ratingsService.GetRatingModel(ratingId);
	        if (rating == null)
	        {
                return null;
	        }

	        // Interpret the date, bearing in mind that the user could have entered bogus dates (e.g. March 42nd)
	        DateTime date;
	        try
	        {
	            date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
	        }
	        catch (ArgumentOutOfRangeException)
	        {
                // Bad date. We fail since no no date is possible.
	            return null;
	        }

	        return _ratingsService.GetUserRatingHistoryEntryModel(rating, user, date);
	    }
	}
}