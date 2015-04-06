using System;
using System.Web.Mvc;
using Ani.Core.Models.Metrics;
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
        /// Serves up a view for editing the current user's entry for a specific rating on a specific day.
        /// </summary>
        /// <param name="ratingId">The rating identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>A view for editing the rating or a redirect to an item not found.</returns>
        [HttpGet]
		[Route(@"Ratings/{ratingId}/{year}/{month}/{day}")]
		[Authorize]
		public ActionResult EditEntry(int ratingId, int year, int month, int day)
		{
			var rating = _ratingsService.GetRatingModel(ratingId);
			if (rating == null)
			{
				return GetNotFoundAction();
			}

            // Interpret the date, bearing in mind that the user could have entered bogus dates (e.g. March 42nd)
            DateTime date;
	        try
	        {
	            date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
	        }
	        catch (ArgumentOutOfRangeException)
	        {
                // If the user entered an invalid date, it's kinda their loss.
	            return GetNotFoundAction();
	        }

            // Okay, now we know who we're talking about and what rating we're talking about, get the entry.
	        var user = this.GetUserModel();
	        var model = _ratingsService.GetUserRatingHistoryEntryModel(rating, user, date);

            // TODO: It'd be nice to redirect to add new if model is null
            return model == null ? GetNotFoundAction() : View(model);
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
				var userId = this.GetUserId();

			    var rating = _ratingsService.GetRatingModel(ratingId);
				if (rating == null || model == null || userId <= 0)
				{
				    return GetNotFoundAction();
				}

			    _ratingsService.AddUserRating(rating, model, userId);

			    return RedirectToAction("Index");
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
	}
}