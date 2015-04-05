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